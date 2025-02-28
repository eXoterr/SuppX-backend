using System;
using Microsoft.AspNetCore.Mvc;
using SuppX.Service;
using SuppX.Utils;

namespace SuppX.App.Controllers;

[ApiController]
[Route("user")]
public class UserController(IUserService userService, IAuthService authService) : ControllerBase
{
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(string login, string password)
    {
        var isUserExits = await userService.ExistsAsync(login);
        if (!isUserExits)
        {
            await userService.CreateAsync(login, password, Globals.ROLE_USER_ID);
            return Created();
        }
        else
        {
            return Conflict("login is already taken");
        }
    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> LoginAsync(string login, string password)
    {
        string? token = await authService.LoginUserAsync(login, password);
        if (token is null)
        {
            return Forbid("incorrect login or password");
        }
        return Ok(token);
    }
}
