using SuppX.Domain;

namespace SuppX.Controller;

public interface IUserService
{
    Task CreateAsync(string login, string password, CancellationToken cancellationToken = default);
}
