namespace TheSharks.Contracts.Models.Statistics;

public class StatisticsDetailsModel
{
    public string Page { get; set; }
    public DateTime Date { get; set; }
    public bool IsLoggedIn { get; set; }
    public bool IsApp { get; set; }
}