using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SuppX.App;

public static class AuthExtension
{
    public static void AddConfiguredAuth(this IServiceCollection services)
    {
        byte[] secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? "secretKeySecretKeySecretKey!!!");
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secret)
            });
    }
}
