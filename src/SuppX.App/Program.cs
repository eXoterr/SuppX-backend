using SuppX.Service;
using SuppX.Utils;
using SuppX.Storage;

DotEnv.Read(".env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStorage();
builder.Services.AddServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// app.MapGet("/", () => $"Hello {Environment.GetEnvironmentVariable("DB_HOST")}");

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
