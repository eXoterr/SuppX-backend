using System;
using SuppX.Domain;

namespace SuppX.Storage.Repository;

internal class UserRepository(ApplicationContext context) : IUserRepository
{
    public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
