using System;
using Microsoft.AspNetCore.Mvc;
using SuppX.Controller;

namespace SuppX.App.Controllers;

[ApiController]
[Route("user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(string login, string password)
    {
        await userService.CreateAsync(login, password);
        return Ok();
    }
}
