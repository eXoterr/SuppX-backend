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
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<DbConfig>();

        DbConfig config = new();

        var canConnect = new ApplicationContext(config).Database.CanConnect();
        if (!canConnect)
        {
            throw new Exception("Unable to connect to db!");
        }

        services.AddScoped<ApplicationContext, ApplicationContext>();

        return services;
    }
}
