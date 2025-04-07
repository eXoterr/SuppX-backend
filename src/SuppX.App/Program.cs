using SuppX.App.Extensions;
using SuppX.Service;
using SuppX.Utils;
using SuppX.Storage;

namespace SuppX.App;

internal class Program
{
    private static void Main(string[] args)
    {
        DotEnv.Read(".env");

        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Services.AddConfiguredCORS();
        builder.Services.AddRepositories();
        builder.Services.AddPostgresStorage();
        builder.Services.AddServices();
        builder.Services.AddControllers();
        builder.Services.AddConfiguredSwagger();
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