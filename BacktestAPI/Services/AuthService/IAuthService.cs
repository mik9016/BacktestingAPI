using System;
using Microsoft.AspNetCore.Mvc;

namespace BacktestAPI.Services.AuthService
{
	public interface IAuthService
	{
        Task<ActionResult<User>> Register(UserDto request);
        Task<User> Login(UserDto request);
        string CreateToken(User user);

    }
}

