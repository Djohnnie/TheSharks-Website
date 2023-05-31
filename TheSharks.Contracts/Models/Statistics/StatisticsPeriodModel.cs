namespace TheSharks.Contracts.Models.Statistics;

public class StatisticsPeriodModel
{
    public string Description { get; set; }
    public int TotalVisits { get; set; }
    public int TotalAuthenticatedVisits { get; set; }
    public int TotalAppVisits { get; set; }
    public int TotalUniqueVisits { get; set; }
    public int TotalUniqueAuthenticatedVisits { get; set; }

    public List<StatisticsPeriodModel>? SubPeriods { get; set; }
}