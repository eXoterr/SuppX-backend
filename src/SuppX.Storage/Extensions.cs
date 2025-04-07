using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuppX.Storage.Repository;

namespace SuppX.Storage;

public static class Extensions
{
    public static IServiceCollection AddPostgresStorage(this IServiceCollection services)
    {
        // services.AddScoped<DbConfig>();

        DbConfig config = new();

        var contextOptions = new DbContextOptionsBuilder();
        contextOptions.UseNpgsql(config.GetConnectionString());

        var canConnect = new ApplicationContext(contextOptions.Options).Database.CanConnect();
        if (!canConnect)
        {
            throw new Exception("Unable to connect to db!");
        }

        services.AddScoped<ApplicationContext, ApplicationContext>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        return services;
    }
}
