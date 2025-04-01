using Npgsql.Replication;

namespace SuppX.App.Extensions;

public static class Cors
{
    public static IServiceCollection AddConfiguredCORS(this IServiceCollection services)
    {
        services.AddCors(
            options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            }
        );

        return services;
    }
}
