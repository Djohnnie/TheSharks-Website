using MediatR;
using System.Globalization;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Statistics;
using TheSharks.Domain.Entities;
using StatisticsEntity = TheSharks.Domain.Entities.Statistics;

namespace TheSharks.Application.Handlers.Statistics.Queries.GetAllStatistics;

public class GetAllStatisticsQueryHandler : IRequestHandler<GetAllStatisticsQuery, StatisticsOverviewListModel>
{
    private readonly CultureInfo _currentCulture = CultureInfo.GetCultureInfo("nl-BE");
    private readonly IRepository<StatisticsEntity> _statisticsRepository;
    private readonly IRepository<Page> _pageRepository;

    public DateTime CurrentDate => TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Central European Standard Time").Date;

    public GetAllStatisticsQueryHandler(
        IRepository<StatisticsEntity> statisticsRepository,
        IRepository<Page> pageRepository)
    {
        _statisticsRepository = statisticsRepository;
        _pageRepository = pageRepository;
    }

    public async Task<StatisticsOverviewListModel> Handle(GetAllStatisticsQuery request, CancellationToken cancellationToken)
    {
        var totalVisits = await _statisticsRepository.TableCount();
        var totalAuthenticatedVisits = await _statisticsRepository.TableCount(x => x.IsLoggedIn);
        var totalAppVisits = await _statisticsRepository.TableCount(x => x.IsApp);
        var totalUniqueVisits = await _statisticsRepository.TableCount(x => x.SessionId);
        var totalUniqueAuthenticatedVisits = await _statisticsRepository.TableCount(x => x.IsLoggedIn, x => x.SessionId);
        var topPages = await _statisticsRepository.GetGroupedKeys(x => !x.IsLoggedIn, x => x.Page);
        var topPage = await NormalizePageTitle(topPages.FirstOrDefault());
        var topAuthenticatedPages = await _statisticsRepository.GetGroupedKeys(x => x.IsLoggedIn, x => x.Page);
        var topAuthenticatedPage = await NormalizePageTitle(topAuthenticatedPages.FirstOrDefault());

        var result = new StatisticsOverviewListModel
        {
            TotalVisits = totalVisits,
            TotalAuthenticatedVisits = totalAuthenticatedVisits,
            TotalAppVisits = totalAppVisits,
            TotalUniqueVisits = totalUniqueVisits,
            TotalUniqueAuthenticatedVisits = totalUniqueAuthenticatedVisits,
            TopPage = topPage,
            TopAuthenticatedPage = topAuthenticatedPage,
            Today = await GetStatisticsForToday(),
            ThisWeek = await GetStatisticsForThisWeek(),
            ThisMonth = await GetStatisticsForThisMonth(),
            ThisYear = await GetStatisticsForThisYear(),
            MostRecent = await GetMostRecent(25)
        };

        return result;
    }

    private async Task<string> NormalizePageTitle(string pageLink)
    {
        try
        {
            return pageLink switch
            {
                "news" => "Nieuws",
                "activities" => "Activiteiten",
                "documents" => "Documenten",
                "galleries" => "Gallerijen",
                "members" => "Leden",
                "login" => "Aanmelden",
                "reset" => "Wachtwoord herstellen",
                _ => (await _pageRepository.Find(x => x.Link == pageLink)).Title
            };
        }
        catch
        {
            return "Onbekend";
        }
    }

    private async Task<StatisticsPeriodModel> GetStatisticsForToday()
    {
        var startDate = CurrentDate;
        var endDate = startDate.AddDays(1).AddSeconds(-1);
        var description = startDate.ToString("D", _currentCulture);

        return await GetStatisticsForPeriod(description, startDate, endDate);
    }

    private async Task<StatisticsPeriodModel> GetStatisticsForThisWeek()
    {
        var weekOfYear = _currentCulture.Calendar.GetWeekOfYear(CurrentDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        int dayOfWeekOffset = CurrentDate.DayOfWeek == 0 ? 6 : (int)CurrentDate.DayOfWeek - 1;
        var startDate = CurrentDate.AddDays(-dayOfWeekOffset);
        var endDate = startDate.AddDays(7).AddSeconds(-1);

        var description = $"{startDate.ToString($"week {weekOfYear} yyyy", _currentCulture)} ({startDate.ToString("d", _currentCulture)} - {endDate.ToString("d", _currentCulture)})";
        var result = await GetStatisticsForPeriod(description, startDate, endDate);
        result.SubPeriods = await GetDailyStatisticsForPeriod(startDate, endDate, "dddd");

        return result;
    }

    private async Task<StatisticsPeriodModel> GetStatisticsForThisMonth()
    {
        var startDate = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
        var endDate = startDate.AddMonths(1).AddSeconds(-1);

        var description = $"{startDate.ToString("MMMM yyyy", _currentCulture)} ({startDate.ToString("d", _currentCulture)} - {endDate.ToString("d", _currentCulture)})";
        var result = await GetStatisticsForPeriod(description, startDate, endDate);
        result.SubPeriods = await GetDailyStatisticsForPeriod(startDate, endDate, "dd");

        return result;
    }

    private async Task<StatisticsPeriodModel> GetStatisticsForThisYear()
    {
        var startDate = new DateTime(CurrentDate.Year, 1, 1);
        var endDate = startDate.AddYears(1).AddSeconds(-1);

        var description = $"{startDate.ToString("yyyy", _currentCulture)} ({startDate.ToString("d", _currentCulture)} - {endDate.ToString("d", _currentCulture)})";
        var result = await GetStatisticsForPeriod(description, startDate, endDate);
        result.SubPeriods = await GetMonthlyStatisticsForYear(startDate.Year);

        return result;
    }

    private async Task<List<StatisticsPeriodModel>> GetDailyStatisticsForPeriod(DateTime startDate, DateTime endDate, string format)
    {
        var dailyStatistics = new List<StatisticsPeriodModel>();

        var statistics = await _statisticsRepository.GetAll(x => x.Date >= startDate && x.Date <= endDate);

        var currentDay = startDate;

        while (currentDay <= endDate)
        {
            dailyStatistics.Add(new StatisticsPeriodModel
            {
                Description = currentDay.ToString(format, _currentCulture),
                TotalVisits = statistics.Count(x => x.Date.Date == currentDay.Date),
                TotalAuthenticatedVisits = statistics.Count(x => x.Date.Date == currentDay.Date && x.IsLoggedIn),
                TotalAppVisits = statistics.Count(x => x.Date.Date == currentDay.Date && x.IsApp),
            });

            currentDay = currentDay.AddDays(1);
        }

        return dailyStatistics;
    }

    private async Task<List<StatisticsPeriodModel>> GetMonthlyStatisticsForYear(int year)
    {
        var monthlyStatistics = new List<StatisticsPeriodModel>();

        for (int i = 1; i <= 12; i++)
        {
            var startDate = new DateTime(year, i, 1);
            var endDate = startDate.AddMonths(1).AddSeconds(-1);

            var description = startDate.ToString("MMMM", _currentCulture);
            var statistics = await GetStatisticsForPeriod(description, startDate, endDate);
            monthlyStatistics.Add(statistics);
        }

        return monthlyStatistics;
    }

    private async Task<StatisticsPeriodModel> GetStatisticsForPeriod(string description, DateTime startDate, DateTime endDate)
    {
        var totalVisits = await _statisticsRepository.TableCount(x => x.Date >= startDate && x.Date <= endDate);
        var totalAuthenticatedVisits = await _statisticsRepository.TableCount(x => x.Date >= startDate && x.Date <= endDate && x.IsLoggedIn);
        var totalAppVisits = await _statisticsRepository.TableCount(x => x.Date >= startDate && x.Date <= endDate && x.IsApp);
        var totalUniqueVisits = await _statisticsRepository.TableCount(x => x.Date >= startDate && x.Date <= endDate, x => x.SessionId);
        var totalUniqueAuthenticatedVisits = await _statisticsRepository.TableCount(x => x.Date >= startDate && x.Date <= endDate && x.IsLoggedIn, x => x.SessionId);

        return new StatisticsPeriodModel
        {
            Description = description,
            TotalVisits = totalVisits,
            TotalAuthenticatedVisits = totalAuthenticatedVisits,
            TotalAppVisits = totalAppVisits,
            TotalUniqueVisits = totalUniqueVisits,
            TotalUniqueAuthenticatedVisits = totalUniqueAuthenticatedVisits
        };
    }

    private async Task<List<StatisticsDetailsModel>> GetMostRecent(int numberOfItems)
    {
        var mostRecent = await _statisticsRepository.GetAllOrderBy(x => x.SysId, 1, numberOfItems);
        var toReturn = new List<StatisticsDetailsModel>();

        foreach (var statistic in mostRecent)
        {
            toReturn.Add(new StatisticsDetailsModel
            {
                Date = statistic.Date,
                Page = await NormalizePageTitle(statistic.Page),
                IsLoggedIn = statistic.IsLoggedIn,
                IsApp = statistic.IsApp
            });
        }

        return toReturn;
    }
}