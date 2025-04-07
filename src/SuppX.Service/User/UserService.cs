using System;
using SuppX.Domain;
using SuppX.Storage;

namespace SuppX.Service;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task CreateAsync(string login, string password, int roleId, CancellationToken cancellationToken = default)
    {
        bool isUserExists = await ExistsAsync(login, cancellationToken);
        if (isUserExists)
        {
            throw new BadRequestException("This login is already registered");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User
        {
            Login = login,
            Password = hashedPassword,
            RoleId = roleId
        };

        await repository.CreateAsync(user, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string login, CancellationToken cancellationToken = default)
    {
        var user = await repository.GetByLoginAsync(login, cancellationToken);
        return user is not null;
    }
}
