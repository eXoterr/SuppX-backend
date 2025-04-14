using System;
using Microsoft.AspNetCore.Mvc;
using SuppX.App.Models;
using SuppX.Domain;
using SuppX.Domain.Globals;
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
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <response code="201">User created</response>
    /// <response code="409">Login is already taken</response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await userService.CreateAsync(request.Login, request.Password, Roles.ROLE_USER_ID, cancellationToken);
            return Created();
        }
        catch (BadRequestException error)
        {
            return BadRequest(new JSONError(error.Message));
        }
    }

    /// <summary>
    /// Creates User session
    /// </summary>
    /// <param name="request">Login data Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>JWT Token Pair</returns>.
    /// <response code="200">Returns the new JWT token pair</response>
    /// <response code="400">Got invalid login or password</response>
    [Route("session")]
    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        TokenPair? tokenPair = await authService.LoginUserAsync(request.Login, request.Password, cancellationToken);
        if (tokenPair is null)
        {
            return BadRequest(new JSONError("incorrect login or password"));
        }
        return Ok(tokenPair);
    }

    /// <summary>
    /// Refreshes user session using 'refreshToken'
    /// </summary>
    /// <param name="request">Request containing refresh token</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>New Token Pair</returns>
    /// <response code="200">Returns the new JWT token pair</response>
    /// <response code="400">Got invalid token</response>
    [Route("session")]
    [HttpPatch]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshRequest request, CancellationToken cancellationToken = default)
    {
        // string? token = await authService.LoginUserAsync(request.Login, request.Password);
        TokenPair? tokenPair = await authService.RefreshUserAsync(request.RefreshToken, cancellationToken);
        if (tokenPair is null)
        {
            return BadRequest(new JSONError("incorrect login or password"));
        }
        return Ok(tokenPair);
    }
}
