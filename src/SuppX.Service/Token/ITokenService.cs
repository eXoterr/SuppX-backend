using System.IdentityModel.Tokens.Jwt;
using SuppX.Domain;

namespace SuppX.Service;

public interface ITokenService
{
    public TokenPair CreateTokenPair(int userId, int roleId);
    public JwtSecurityToken? ValidateToken(string token);
    public Task RevokeTokenAsync(string token, CancellationToken cancellationToken = default);
    public Task<bool> IsBlacklistedAsync(string token, CancellationToken cancellationToken = default);
}
