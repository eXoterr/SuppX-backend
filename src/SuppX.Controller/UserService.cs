using System;
using SuppX.Controller;
using SuppX.Domain;
using SuppX.Storage;

namespace SuppX.Service;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task CreateAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        var user = new User{
            Login = login,
            Password = password,
            RoleId = 1,
        };

        await repository.CreateAsync(user, cancellationToken);
    }
}
