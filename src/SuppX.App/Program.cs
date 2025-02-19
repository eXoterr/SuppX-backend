using SuppX.Core;
using SuppX.Storage;

DotEnv.Read(".env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStorage();

var app = builder.Build();


// app.MapGet("/", () => $"Hello {Environment.GetEnvironmentVariable("DB_HOST")}");

app.Run();
