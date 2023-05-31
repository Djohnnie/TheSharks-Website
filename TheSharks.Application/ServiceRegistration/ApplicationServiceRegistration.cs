using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TheSharks.Application.Helpers;
using TheSharks.Application.Services;
using TheSharks.Application.ValidationPipeline;
using TheSharks.Contracts.Helpers;
using TheSharks.Contracts.Services.Logging;
using TheSharks.Contracts.Services.Statistics;

namespace TheSharks.Application.ServiceRegistration;

public static class ApplicationServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(ValidationPipeline.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<ILogService, LogService>();

        services.AddScoped<IEncryptionHelper, EncryptionHelper>();
        services.AddScoped<IPictureHelper, PictureHelper>();
    }
}