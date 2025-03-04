using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using SuppX.Domain;

namespace SuppX.Storage.Repository;

public class TokenBlacklistRepository(ApplicationContext context) : ITokenBlacklistRepository
{
    public async Task CreateAsync(string token, CancellationToken cancellationToken = default)
    {
        var toBlacklist = new BlacklistedToken
        {
            Token = token
        };
        await context.BlacklistedTokens.AddAsync(toBlacklist);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string token, CancellationToken cancellationToken = default)
    {
        BlacklistedToken? blacklistedToken = await context.BlacklistedTokens.FirstOrDefaultAsync(x => x.Token == token);
        if (blacklistedToken is null)
        {
            return false;
        }
        return true;
    }

    public async Task DeleteAsync(string token, CancellationToken cancellationToken = default)
    {
        var toDelete = new BlacklistedToken
        {
            Token = token
        };
        await context.BlacklistedTokens.Select(x => x.Token == token).ExecuteDeleteAsync(cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
