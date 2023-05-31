using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Activities;

public class ActivityOverviewListModel
{
    public int TotalRecords { get; set; }
    public IEnumerable<ActivityOverviewModel> Activities { get; set; }
}

public class ActivityOverviewModel : BaseIdModel
{
    public string ActivityType { get; set; }
    public string Name { get; set; }
    public DateTimeOffset Date { get; set; }
    public string Location { get; set; }
    public string LocationLink { get; set; }
    public string ResponsibleFirstName { get; set; }
    public string ResponsibleLastName { get; set; }
    public DateTimeOffset? Departure { get; set; }
    public DateTimeOffset? BriefingTime { get; set; }
    public string? Tide { get; set; }
    public DateTimeOffset? AtWater { get; set; }
    public int? MemberCount { get; set; }
    public bool UserEnrolled { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}