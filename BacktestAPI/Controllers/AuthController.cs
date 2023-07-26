using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using BacktestAPI.Data;

using BacktestAPI.Services.AuthService;
using BacktestAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BacktestAPI
{
    [Route("api/auth")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly IAuthService _auth;



        public Auth( IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]

        public async Task<ActionResult<User>> Register(UserDto request)
        {

            if (!IsEmailValid(request.Email))
            {
                return BadRequest("Email Invalid!");
            }

            var result = await _auth.Register(request);

            if(result is null)
            {
                BadRequest("User with this credentilas alreadt exist!");
            }

           
            return Ok("You are succesfully registered, login!");
        }

        [HttpPost("login")]

        public async Task<ActionResult<User>> Login(LoginUserDto request)
        {
            var userDB = await _auth.Login(request);

            if(userDB is null)
            {
                return BadRequest("Credentials are wrong!");
            }

            string token = _auth.CreateToken(userDB);

            var user = new
            {
                id = userDB.Id,
                email = userDB.Email,
                username = userDB.Username,
                trades = userDB.Trades
            };
            

            return Ok(new {token, user });
           
        }



        private static bool IsEmailValid(string email)
        {
            
                bool valid = true;

                try
                {
                    var emailAddress = new MailAddress(email);
                }
                catch
                {
                    valid = false;
                }

                return valid;
            
        }

    }
}

