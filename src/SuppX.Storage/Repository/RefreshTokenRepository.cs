using Microsoft.EntityFrameworkCore;
using SuppX.Domain;

namespace SuppX.Storage.Repository;

public class RefreshTokenRepository(ApplicationContext context) : IRefreshTokenRepository
{
    public async Task CreateAsync(string token, CancellationToken cancellationToken = default)
    {
        var refreshToken = new RefreshToken
        {
            Value = token
        };
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string token, CancellationToken cancellationToken = default)
    {
        RefreshToken? refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Value == token);
        return refreshToken is not null;
    }

    public async Task DeleteAsync(string token, CancellationToken cancellationToken = default)
    {
        var toDelete = new RefreshToken
        {
            Value = token
        };
        var refreshToken = await context.RefreshTokens.Where(x => x.Value == token).FirstOrDefaultAsync();
        if (refreshToken != null)
        {
            context.RefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
