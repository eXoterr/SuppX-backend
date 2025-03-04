using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuppX.Storage.Repository;

namespace SuppX.Storage;

public static class Extensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITokenBlacklistRepository, TokenBlacklistRepository>();
        
        Config config = new();

        services.AddDbContext<ApplicationContext>(x => {
            x.UseNpgsql(config.GetConnectionString());
        });

        return services;
    }
}
