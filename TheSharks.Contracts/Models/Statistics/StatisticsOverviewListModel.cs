namespace TheSharks.Contracts.Models.Statistics;

public class StatisticsOverviewListModel
{
    public int TotalVisits { get; set; }
    public int TotalAuthenticatedVisits { get; set; }
    public int TotalAppVisits { get; set; }
    public int TotalUniqueVisits { get; set; }
    public int TotalUniqueAuthenticatedVisits { get; set; }
    public string TopPage { get; set; }
    public string TopAuthenticatedPage { get; set; }

    public StatisticsPeriodModel Today { get; set; }
    public StatisticsPeriodModel ThisWeek { get; set; }
    public StatisticsPeriodModel ThisMonth { get; set; }
    public StatisticsPeriodModel ThisYear { get; set; }

    public List<StatisticsDetailsModel> MostRecent { get; set; }
}