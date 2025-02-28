namespace SuppX.Service;

public interface IAuthService
{
    public Task<string?> LoginUserAsync(string login, string password, CancellationToken cancellationToken = default);
}
