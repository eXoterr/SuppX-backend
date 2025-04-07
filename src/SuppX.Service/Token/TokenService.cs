using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SuppX.Domain;
using SuppX.Storage.Repository;

namespace SuppX.Service;

public class TokenService(IRefreshTokenRepository refreshTokenRepository, ILogger<TokenService> logger) : ITokenService
{
    const string DEFAULT_JWT = "secretKeySecretKeySecretKey!!!12345secretKey!";

    public TokenPair CreateTokenPair(int userId, int roleId)
    {
        var claims = new List<Claim>
        {
            new("id", userId.ToString()),
            new("roleId", roleId.ToString())
        };

        byte[] secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? DEFAULT_JWT);
        var key = new SymmetricSecurityKey(secret);

        var jwtAccessToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(TimeSpan.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRES") ?? "00:10:00")),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var jwtRefreshToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(TimeSpan.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRES") ?? "00:30:00")),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return new TokenPair
        {
            AccessToken = tokenHandler.WriteToken(jwtAccessToken),
            RefreshToken = tokenHandler.WriteToken(jwtRefreshToken)
        };
    }

    public JwtSecurityToken? ValidateToken(string token)
    {
        byte[] secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? DEFAULT_JWT);
        var key = new SymmetricSecurityKey(secret);
        var validationParams = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateAudience = false,
            IssuerSigningKey = key,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
            var jwt = (JwtSecurityToken)validatedToken;
            return jwt;
        }
        catch (Exception ex)
        {
            logger.LogError($"got invalid jwt token: {ex}");
            return null;
        }
    }

    public async Task StoreRefreshAsync(string token, CancellationToken cancellationToken = default)
    {
        await refreshTokenRepository.CreateAsync(token);
    }

    public async Task<bool> IsRefreshExistsAsync(string token, CancellationToken cancellationToken = default)
    {
        return await refreshTokenRepository.ExistsAsync(token);
    }

    public async Task DeleteRefreshAsync(string token, CancellationToken cancellationToken = default)
    {
        await refreshTokenRepository.DeleteAsync(token, cancellationToken);
    }
}
