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

    /// <summary>
    /// Registers new User account
    /// </summary>
    /// <param name="request">Register request, containing login and password</param>
    /// <response code="201">User created</response>
    /// <response code="409">Login is already taken</response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(RegisterRequest request)
    {
        var isUserExits = await userService.ExistsAsync(request.Login);
        if (!isUserExits)
        {
            await userService.CreateAsync(request.Login, request.Password, Globals.ROLE_USER_ID);
            return Created();
        }
        else
        {
            return Conflict("login is already taken");
        }
    }

    /// <summary>
    /// Creates User session
    /// </summary>
    /// <param name="request">Login data Request</param>
    /// <returns>JWT Token Pair</returns>.
    /// <response code="200">Returns the new JWT token pair</response>
    /// <response code="400">Got invalid login or password</response>
    [Route("session")]
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

    /// <summary>
    /// Refreshes user session using 'refreshToken'
    /// </summary>
    /// <param name="request">Request containing refresh token</param>
    /// <returns>New Token Pair</returns>
    /// <response code="200">Returns the new JWT token pair</response>
    /// <response code="400">Got invalid token</response>
    [Route("session")]
    [HttpPatch]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshRequest request)
    {
        // string? token = await authService.LoginUserAsync(request.Login, request.Password);
        TokenPair? tokenPair = await authService.RefreshUser(request.RefreshToken);
        if (tokenPair is null)
        {
            return BadRequest("incorrect refresh token");
        }
        return Ok(tokenPair);
    }
}
