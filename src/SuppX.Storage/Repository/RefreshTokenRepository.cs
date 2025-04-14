using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SuppX.Domain;

namespace SuppX.Storage.Repository;

public class RefreshTokenRepository(ApplicationContext context, ILogger<RefreshTokenRepository> logger) : IRefreshTokenRepository
{
    public async Task CreateAsync(string token, CancellationToken cancellationToken = default)
    {
        var refreshToken = new RefreshToken
        {
            Value = token
        };
        await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string token, CancellationToken cancellationToken = default)
    {
        RefreshToken? refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Value == token, cancellationToken);
        return refreshToken is not null;
    }

    public async Task<bool> TryDeleteAsync(string token, CancellationToken cancellationToken = default)
    {
        var refreshToken = await context.RefreshTokens.Where(x => x.Value == token).FirstOrDefaultAsync(cancellationToken);
        if(refreshToken == null)
        {
            return false;
        }

        try
        {
            context.RefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            logger.LogError("attempt to delete already deleted refresh token");
            context.RefreshTokens.Remove(refreshToken);
            return false;
        }
    }
}
