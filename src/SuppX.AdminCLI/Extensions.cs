using System;
using Microsoft.Extensions.DependencyInjection;

namespace SuppX.AdminCLI;

public static class Extensions
{
    public static IServiceCollection AddUserManager(this IServiceCollection services)
    {
        services.AddScoped<UserManager>();
        return services;
    }
}
