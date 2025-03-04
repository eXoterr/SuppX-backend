using System;
using Microsoft.Extensions.DependencyInjection;
using SuppX.Service;

namespace SuppX.Service;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}
