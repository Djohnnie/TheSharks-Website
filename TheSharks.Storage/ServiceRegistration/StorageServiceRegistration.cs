using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheSharks.Contracts.Services.Storage;
using TheSharks.Storage.Options;
using TheSharks.Storage.Service;

namespace TheSharks.Storage.ServiceRegistration;

public static class StorageServiceRegistration
{
    public static void AddStorageServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StorageOptions>(configuration.GetSection(StorageOptions.Storages));

        services.AddScoped<IDocumentStorageService, DocumentStorageService>();
        services.AddScoped<IPictureStorageService, PictureStorageService>();
    }
}