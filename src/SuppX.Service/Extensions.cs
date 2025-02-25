using System;
using Microsoft.Extensions.DependencyInjection;
using SuppX.Service;

namespace SuppX.Service;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
