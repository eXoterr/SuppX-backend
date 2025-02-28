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

        builder.Services.AddStorage();
        builder.Services.AddServices();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthorization();
        builder.Services.AddConfiguredAuth();

        var app = builder.Build();

        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.Run();
    }
}