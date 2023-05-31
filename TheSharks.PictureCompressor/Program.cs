using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheSharks.Application.ServiceRegistration;
using TheSharks.Contracts.Helpers;
using TheSharks.DataAccess.ServiceRegistration;
using TheSharks.Domain.Entities;
using TheSharks.Identity.ServiceRegistration;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

var services = new ServiceCollection();
services.AddLogging(c =>
{
    c.AddConfiguration(configuration.GetSection("Logging"));
    c.AddConsole();
});
services.AddDataAccessServices(configuration);
services.AddIdentityServices(configuration);
services.AddApplicationServices();

var serviceProvider = services.BuildServiceProvider();
var userManager = serviceProvider.GetService<UserManager<Member>>();
var pictureHelper = serviceProvider.GetService<IPictureHelper>();

foreach (var user in userManager.Users.ToList())
{
    if (user.ProfilePicture != null)
    {
        Console.WriteLine($"Updating {user.FirstName} {user.LastName}");
        user.ProfilePicture = await pictureHelper.PreparePicture(user.ProfilePicture, 512);
        await userManager.UpdateAsync(user);
        Console.WriteLine("DONE");
    }
}