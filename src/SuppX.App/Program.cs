using SuppX.Service;
using SuppX.Utils;
using SuppX.Storage;
using System.Reflection;

namespace SuppX.App;

internal class Program
{
    private static void Main(string[] args)
    {
        DotEnv.Read(".env");

        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Services.AddCors(
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
        builder.Services.AddStorage();
        builder.Services.AddServices();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(
            opts =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            }
        );
        builder.Services.AddAuthorization();
        builder.Services.AddConfiguredAuth();

        var app = builder.Build();

        app.UseCors();
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.Run();
    }
}