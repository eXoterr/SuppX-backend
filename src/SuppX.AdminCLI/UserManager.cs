using System;
using SuppX.Service;
using SuppX.Domain.Globals;

namespace SuppX.AdminCLI;

public class UserManager(IUserService userService)
{
    public async Task CreateAdmin(string login, string password)
    {
        await userService.CreateAsync(login, password, Roles.ROLE_ADMIN_ID);
    }

    public async Task CreateUser(string login, string password)
    {
        await userService.CreateAsync(login, password, Roles.ROLE_USER_ID);
    }
}
