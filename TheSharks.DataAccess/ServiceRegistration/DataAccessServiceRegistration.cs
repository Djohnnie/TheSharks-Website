using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheSharks.Contracts.DataAccess;
using TheSharks.DataAccess.EntityFramework;
using TheSharks.DataAccess.EntityFramework.Repositories;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.ServiceRegistration;

public static class DataAccessServiceRegistration
{
    public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddDatabase(services, configuration);

        services.AddScoped<IRepository<Activity>, ActivityRepository>();
        services.AddScoped<IRepository<EventActivity>, EventActivityRepository>();
        services.AddScoped<IRepository<DiveActivity>, DiveActivityRepository>();
        services.AddScoped<IRepository<BoardMeetingActivity>, BoardMeetingActivityRepository>();
        services.AddScoped<IRepository<MonitorBoardActivity>, MonitorBoardActivityRepository>();
        services.AddScoped<IRepository<MemberEnrollment>, MemberEnrollmentRepository>();
        services.AddScoped<IRepository<GuestEnrollment>, GuestEnrollmentRepository>();
        services.AddScoped<IMemberEnrollmentRepository<MemberEnrollment>, MemberEnrollmentRepository>();
        services.AddScoped<IRepository<NewsItem>, NewsItemRepository>();
        services.AddScoped<IRepository<Document>, DocumentRepository>();
        services.AddScoped<IRepository<Component>, ComponentRepository>();
        services.AddScoped<IRepository<Page>, PageRepository>();
        services.AddScoped<IRepository<Gallery>, GalleryRepository>();
        services.AddScoped<IRepository<Picture>, PictureRepository>();
        services.AddScoped<IRepository<Enrollment>, EnrollmentRepository>();
        services.AddScoped<IRepository<Statistics>, StatisticsRepository>();
        services.AddScoped<IRepository<LogEntry>, LogRepository>();
        services.AddScoped<IRepository<OpenWaterTest>, OpenWaterTestRepository>();
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("Database")["ConnectionString"];
        if (connectionString == null) throw new ApplicationException("Connection string not defined");

        services.AddDbContext<TheSharksContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}