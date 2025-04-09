using Microsoft.Extensions.DependencyInjection;
using SuppX.Service;
using SuppX.Storage;

namespace SuppX.AdminCLI;

public class Core
{
    public IServiceProvider Services { get; init; }

    public Core()
    {
        var services = new ServiceCollection();

        services.AddPostgresStorage();
        services.AddRepositories();
        services.AddServices();
        services.AddUserManager();

        Services = services.BuildServiceProvider();
    }
}
