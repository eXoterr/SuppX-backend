using System.Collections.Concurrent;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SuppX.Domain;
using SuppX.Storage;

namespace SuppX.Service;

public class AuthService(IUserRepository repository, ITokenService tokenService, ILogger<AuthService> logger) : IAuthService
{
    public async Task<TokenPair?> LoginUserAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        User? user = await repository.GetByLoginAsync(login, cancellationToken);
        if (user is null)
        {
            logger.LogError("user not found");
            return null;
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        if (!isPasswordValid)
        {
            logger.LogError("incorrect login or passowrd");
            return null;
        }

        var tokenPair = tokenService.CreateTokenPair(user.Id, user.RoleId);
        return tokenPair;
    }

    public TokenPair? RefreshUser(string refreshToken, CancellationToken cancellationToken = default)
    {
        var validatedToken = tokenService.ValidateToken(refreshToken);
        if (validatedToken is null)
        {
            return null;
        }

        bool parsed = int.TryParse(validatedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value, out int userId);
        if (!parsed)
        {
            logger.LogError("unable to parse userId from refresh token");
            return null;
        }
        parsed = int.TryParse(validatedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value, out int roleId);
        if (!parsed)
        {
            logger.LogError("unable to parse roleId from refresh token");
            return null;
        }

        var tokenPair = tokenService.CreateTokenPair(userId, roleId);

        // TODO: Disable used refresh token 

        return tokenPair;
    }
}
