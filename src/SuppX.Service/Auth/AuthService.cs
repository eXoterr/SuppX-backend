using System.Collections.Concurrent;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SuppX.Domain;
using SuppX.Storage;
using SuppX.Storage.Repository;

namespace SuppX.Service;

public class AuthService(IUserRepository repository, ITokenService tokenService, ILogger<AuthService> logger) : IAuthService
{
    public async Task<TokenPair?> LoginUserAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        User? user = await repository.GetByLoginAsync(login, cancellationToken);
        if (user is null)
        {
            logger.LogWarning($"user \"{login}\" not found");
            return null;
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        if (!isPasswordValid)
        {
            logger.LogWarning($"incorrect passowrd for user \"{login}\"");
            return null;
        }

        var tokenPair = tokenService.CreateTokenPair(user.Id, user.RoleId, cancellationToken);

        await tokenService.StoreRefreshAsync(tokenPair.RefreshToken, cancellationToken);
        
        return tokenPair;
    }

    public async Task<TokenPair?> RefreshUserAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var validatedToken = tokenService.ValidateToken(refreshToken, cancellationToken);
        if (validatedToken is null)
        {
            return null;
        }

        if (!await tokenService.IsRefreshExistsAsync(refreshToken, cancellationToken))
        {
            return null;
        }

        // TODO: Maybe ERR!
        int.TryParse(validatedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value, out int userId);
        int.TryParse(validatedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value, out int roleId);

        bool isDeleted = await tokenService.TryDeleteRefreshAsync(refreshToken, cancellationToken);

        if(!isDeleted)
        {
            return null;
        }

        var tokenPair = tokenService.CreateTokenPair(userId, roleId, cancellationToken);

        await tokenService.StoreRefreshAsync(tokenPair.RefreshToken, cancellationToken);

        return tokenPair;
    }
}
