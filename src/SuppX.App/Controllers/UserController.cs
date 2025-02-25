using System;
using Microsoft.AspNetCore.Mvc;
using SuppX.Service;
using SuppX.Utils;

namespace SuppX.App.Controllers;

[ApiController]
[Route("user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(string login, string password)
    {
        await userService.CreateAsync(login, password, Globals.ROLE_USER_ID);
        return Created();
    }
}
