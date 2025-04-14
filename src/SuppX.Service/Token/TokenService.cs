using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SuppX.Domain;
using SuppX.Storage.Repository;
using SuppX.Utils;
using SuppX.Domain.Globals;

namespace SuppX.Service;

public class TokenService(IRefreshTokenRepository refreshTokenRepository, ILogger<TokenService> logger) : ITokenService
{
    const string JWT_SECRET_ENV = "JWT_SECRET";

    public TokenPair CreateTokenPair(int userId, int roleId, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new("id", userId.ToString()),
            new("roleId", roleId.ToString())
        };

        var sigKey = Environment.GetEnvironmentVariable(JWT_SECRET_ENV) ?? DefaultEnv.JWT_SECRET;
        if(sigKey == DefaultEnv.JWT_SECRET)
        {
            logger.LogWarning("default JWT signature key is used!");
        }

        byte[] secret = Encoding.UTF8.GetBytes(sigKey);
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

    public JwtSecurityToken? ValidateToken(string token, CancellationToken cancellationToken)
    {
        byte[] secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(JWT_SECRET_ENV) ?? DefaultEnv.JWT_SECRET);
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
        await refreshTokenRepository.CreateAsync(token, cancellationToken);
    }

    public async Task<bool> IsRefreshExistsAsync(string token, CancellationToken cancellationToken = default)
    {
        return await refreshTokenRepository.ExistsAsync(token, cancellationToken);
    }

    public async Task<bool> TryDeleteRefreshAsync(string token, CancellationToken cancellationToken = default)
    {
        return await refreshTokenRepository.TryDeleteAsync(token, cancellationToken);
    }
}
