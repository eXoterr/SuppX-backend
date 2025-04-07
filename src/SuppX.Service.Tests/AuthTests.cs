using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuppX.Domain;
using SuppX.Storage;
using SuppX.Utils;

namespace SuppX.Service.Tests;

public class AuthTests
{
    private readonly ApplicationContext context;
    private readonly IServiceProvider services;
    private readonly IUserService userService;
    private readonly IAuthService authService;
    private readonly ITokenService tokenService;
    public AuthTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>()
        .UseInMemoryDatabase("InMemoryDb");

        var contextOptions = builder.Options;

        context = new ApplicationContext(contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging();
        serviceCollection.AddRepositories();
        serviceCollection.AddSingleton(context);
        serviceCollection.AddServices();

        services = serviceCollection.BuildServiceProvider();

        var authSvc = services.GetService<IAuthService>();
        var userSvc = services.GetService<IUserService>();
        var tokenSvc = services.GetService<ITokenService>();

        userService = userSvc ?? throw new Exception("user service not initialized"); ;
        authService = authSvc ?? throw new Exception("auth service not initialized"); ;
        tokenService = tokenSvc ?? throw new Exception("token service not initialized"); ;
    }


    [Theory]
    [InlineData("admin", "12345")]
    [InlineData("user", "54321")]
    [InlineData("100500", "admin")]
    public async Task TestRegister(string login, string password)
    {
        await userService.CreateAsync(login, password, Globals.ROLE_USER_ID);
    }

    [Theory]
    [InlineData("admin", "12345", Globals.ROLE_USER_ID)]
    [InlineData("admin", "12345", Globals.ROLE_ADMIN_ID)]
    public async Task TestDoubleRegister(string login, string password, int roleId)
    {
        await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await userService.CreateAsync(login, password, roleId);
            await userService.CreateAsync(login, password, roleId);
        });
    }


    [Theory]
    [InlineData("admin", "12345")]
    [InlineData("100500", "admin")]
    public async Task TestLogin(string login, string password)
    {
        await userService.CreateAsync(login, password, Globals.ROLE_USER_ID);

        TokenPair? tokenPair = await authService.LoginUserAsync(login, password);

        Assert.NotNull(tokenPair);

        JwtSecurityToken? securityToken = tokenService.ValidateToken(tokenPair.AccessToken);

        Assert.NotNull(securityToken);
    }

    [Theory]
    [InlineData("admin", "12345")]
    [InlineData("100500", "admin")]
    public async Task TestLoginWrongLogin(string login, string password)
    {
        await userService.CreateAsync(login, password, Globals.ROLE_USER_ID);

        TokenPair? tokenPair = await authService.LoginUserAsync(login + "123", password);

        Assert.Null(tokenPair);
    }

    [Theory]
    [InlineData("admin", "12345")]
    [InlineData("100500", "admin")]
    public async Task TestLoginWrongPassword(string login, string password)
    {
        await userService.CreateAsync(login, password, Globals.ROLE_USER_ID);

        TokenPair? tokenPair = await authService.LoginUserAsync(login, password + "123");

        Assert.Null(tokenPair);
    }

    [Theory]
    [InlineData("admin", "12345")]
    [InlineData("100500", "admin")]
    public async Task TestRefresh(string login, string password)
    {
        await userService.CreateAsync(login, password, Globals.ROLE_USER_ID);

        TokenPair? tokenPair = await authService.LoginUserAsync(login, password);

        TokenPair? newTokenPair = await authService.RefreshUserAsync(tokenPair.RefreshToken);

        Assert.NotNull(newTokenPair);
    }

    [Theory]
    [InlineData("admin", "12345")]
    [InlineData("100500", "admin")]
    public async Task TestDoubleRefresh(string login, string password)
    {
        await userService.CreateAsync(login, password, Globals.ROLE_USER_ID);

        TokenPair? tokenPair = await authService.LoginUserAsync(login, password);

        TokenPair? newTokenPair = await authService.RefreshUserAsync(tokenPair.RefreshToken);
        
        TokenPair? newTokenPair2 = await authService.RefreshUserAsync(tokenPair.RefreshToken);

        Assert.NotNull(newTokenPair);

        Assert.Null(newTokenPair2);
    }
}
