using Microsoft.Extensions.DependencyInjection;
using SuppX.AdminCLI;

Console.WriteLine("SuppX Admin CLI v1.0");

Core core = new();


while (true)
{
    string? command = Console.ReadLine();
    switch (command)
    {
        case "create admin":
            Console.WriteLine("Enter new login:");
            string login = Console.ReadLine() ?? "admin";
            Console.WriteLine("Enter new password:");
            string password = Console.ReadLine() ?? "1234567890";
            var userManager = core.Services.GetService<UserManager>();
            userManager?.CreateAdmin(login, password);
            Console.WriteLine("New admin account created");
            break;
        default:
            Console.WriteLine("Command not recognized");
            break;
    }
}