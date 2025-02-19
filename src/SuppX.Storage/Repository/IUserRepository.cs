using System;
using SuppX.Domain;

namespace SuppX.Storage;

public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken cancellationToken = default);
}
