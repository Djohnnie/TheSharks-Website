using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheSharks.Contracts.Services.Email;
using TheSharks.Mail.Options;
using TheSharks.Mail.Service;

namespace TheSharks.Mail.ServiceRegistration;

public static class MailServiceRegistration
{
    public static void AddMailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMailService, MailService>();
        services.Configure<MailOptions>(configuration.GetSection(MailOptions.Mail));
    }
}