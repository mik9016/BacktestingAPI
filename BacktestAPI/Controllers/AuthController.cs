using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BacktestAPI.Data;
using BacktestAPI.Models;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using BacktestAPI.Services.AuthService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BacktestAPI
{
    [Route("api/auth")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthService _auth;



        public Auth(DataContext context, IAuthService auth)
        {
            _context = context;
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

        public async Task<ActionResult<User>> Login(UserDto request)
        {
            var result = await _auth.Login(request);

            if(result is null)
            {
                return BadRequest("Credentials are wrong!");
            }

            string token = _auth.CreateToken(result);

            return Ok(token);
           
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

