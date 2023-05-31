using Microsoft.AspNetCore.Http;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Services.Statistics;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IRepository<Statistics> _statisticsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StatisticsService(
        IRepository<Statistics> statisticsRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _statisticsRepository = statisticsRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task RecordStatistics(string page)
    {
        var isLoggedIn = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        // Only log statistics for anonymous users and authenticated users that do not have the ManageStatistics claim.
        if (!isLoggedIn || !_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Value == "CanManageStatistics"))
        {
            var sessionHeader = _httpContextAccessor.HttpContext?.Request?.Headers?.SingleOrDefault(x => x.Key.ToLowerInvariant() == "thesharkssession");
            var sessionId = sessionHeader != null && sessionHeader.Value.Key != null ? Guid.Parse(sessionHeader.Value.Value) : Guid.Empty;

            var isAppHeader = _httpContextAccessor.HttpContext?.Request?.Headers?.SingleOrDefault(x => x.Key.ToLowerInvariant() == "thesharksisapp");
            var isApp = isAppHeader != null && isAppHeader.Value.Key != null ? bool.Parse(isAppHeader.Value.Value) : false;

            var entity = new Statistics
            {
                Id = Guid.NewGuid(),
                Page = page,
                Date = DateTime.UtcNow.Date,
                IsLoggedIn = isLoggedIn,
                IsApp = isApp,
                SessionId = sessionId
            };

            await _statisticsRepository.Add(entity);
        }
    }
}