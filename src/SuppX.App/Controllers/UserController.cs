using System;
using Microsoft.AspNetCore.Mvc;
using SuppX.App.Models;
using SuppX.Domain;
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
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        TokenPair? tokenPair = await authService.LoginUserAsync(request.Login, request.Password);
        if (tokenPair is null)
        {
            return BadRequest("incorrect login or password");
        }
        return Ok(tokenPair);
    }

    [Route("refresh")]
    [HttpPost]
    public async Task<IActionResult> RefreshAsync([FromBody] string refreshToken)
    {
        // string? token = await authService.LoginUserAsync(request.Login, request.Password);
        TokenPair? tokenPair = await authService.RefreshUser(refreshToken);
        if (tokenPair is null)
        {
            return BadRequest("incorrect refresh token");
        }
        return Ok(tokenPair);
    }
}
