using System.IdentityModel.Tokens.Jwt;
using SuppX.Domain;

namespace SuppX.Service;

public interface ITokenService
{
    public TokenPair CreateTokenPair(int userId, int roleId);
    public JwtSecurityToken? ValidateToken(string token);
    public Task StoreRefreshAsync(string token, CancellationToken cancellationToken = default);
    public Task DeleteRefreshAsync(string token, CancellationToken cancellationToken = default);
    public Task<bool> IsRefreshExistsAsync(string token, CancellationToken cancellationToken = default);
}
