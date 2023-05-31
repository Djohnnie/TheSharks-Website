namespace TheSharks.Contracts.Models.Activities.BaseModels;

public class ActivityBaseModel
{
    public DateTimeOffset Date { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }
    public string? LocationLink { get; set; }
    public string? Info { get; set; }
    public string? MemberInfo { get; set; }
    public bool NecessarySubscription { get; set; }
}