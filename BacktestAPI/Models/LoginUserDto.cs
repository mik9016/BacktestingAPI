using System;
namespace BacktestAPI.Models
{
	public class LoginUserDto
	{
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}

