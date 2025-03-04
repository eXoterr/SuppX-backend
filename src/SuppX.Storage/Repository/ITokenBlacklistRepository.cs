namespace SuppX.Storage.Repository;

public interface ITokenBlacklistRepository
{
    public Task CreateAsync(string token, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(string token, CancellationToken cancellationToken = default);
    public Task DeleteAsync(string token, CancellationToken cancellationToken = default);
}
