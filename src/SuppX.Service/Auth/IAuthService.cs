using SuppX.Domain;

namespace SuppX.Service;

public interface IAuthService
{
    public Task<TokenPair?> LoginUserAsync(string login, string password, CancellationToken cancellationToken = default);
    public Task<TokenPair?> RefreshUserAsync(string refreshToken, CancellationToken cancellationToken = default);
}
