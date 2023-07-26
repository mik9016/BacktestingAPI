using System;
using BacktestAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BacktestAPI.Services.AuthService
{
	public interface IAuthService
	{
        Task<ActionResult<User>> Register(UserDto request);
        Task<User> Login(LoginUserDto request);
        string CreateToken(User user);

    }
}

