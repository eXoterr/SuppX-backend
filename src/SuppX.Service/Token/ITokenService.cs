using System.IdentityModel.Tokens.Jwt;
using SuppX.Domain;

namespace SuppX.Service;

public interface ITokenService
{
    public TokenPair CreateTokenPair(int userId, int roleId, CancellationToken cancellationToken = default);
    public JwtSecurityToken? ValidateToken(string token, CancellationToken cancellationToken = default);
    public Task StoreRefreshAsync(string token, CancellationToken cancellationToken = default);
    public Task<bool> TryDeleteRefreshAsync(string token, CancellationToken cancellationToken = default);
    public Task<bool> IsRefreshExistsAsync(string token, CancellationToken cancellationToken = default);
}
