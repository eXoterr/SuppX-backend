using System;
using SuppX.Service;
using SuppX.Utils;

namespace SuppX.AdminCLI;

public class UserManager(IUserService userService)
{
    public async Task CreateAdmin(string login, string password)
    {
        await userService.CreateAsync(login, password, Globals.ROLE_ADMIN_ID);
    }
}
