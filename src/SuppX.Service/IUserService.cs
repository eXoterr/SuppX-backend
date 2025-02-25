using SuppX.Domain;

namespace SuppX.Service;

public interface IUserService
{
    Task CreateAsync(string login, string password, int roleId, CancellationToken cancellationToken = default);
}
