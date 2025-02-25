using System;
using SuppX.Domain;
using SuppX.Storage;

namespace SuppX.Service;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task CreateAsync(string login, string password, int roleId, CancellationToken cancellationToken = default)
    {
        var user = new User{
            Login = login,
            Password = password,
            RoleId = roleId,
        };

        await repository.CreateAsync(user, cancellationToken);
    }
}
