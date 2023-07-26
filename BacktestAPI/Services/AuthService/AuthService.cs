using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BacktestAPI.Data;
using BacktestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BacktestAPI.Services.AuthService
{
	public class AuthService : IAuthService
	{
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;

        }

        public async Task<ActionResult<User>> Register(UserDto request)
        {
            // check if user is already in db
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return null;
            }

            if (_context.Users.Any(u => u.Username == request.Username))
            {
                return null;
            }

            // Fetch the last user from the database
            var lastUser = _context.Users.OrderByDescending(u => u.Id).FirstOrDefault();

            // Increment the ID for the new user
            int nextId = (lastUser != null) ? lastUser.Id + 1 : 1;

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = nextId,
                Username = request.Username,
                PasswordHash = passwordHash,
                Email = request.Email
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Login(LoginUserDto request)
        {
            var userDB = await _context.Users.Include(u => u.Trades)
                .FirstOrDefaultAsync(u => u.Email == request.Email);
                

            if (userDB is null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, userDB.PasswordHash))
            {
                return null;
            }

            return userDB;
        }


        public string CreateToken(User user)
        {
            /* List Whatever data you want in token
             * then take token on frontend and take data out!
             * 
             */
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("userId", user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(4),
                    signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}

