using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SuppX.Storage;

public static class Extensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        Config config = new();

        services.AddDbContext<AppContext>(x => {
            x.UseNpgsql(config.GetConnectionString());
        });

        return services;
    }
}
