namespace TheSharks.Contracts.Services.Statistics;

public interface IStatisticsService
{
    Task RecordStatistics(string page);
}