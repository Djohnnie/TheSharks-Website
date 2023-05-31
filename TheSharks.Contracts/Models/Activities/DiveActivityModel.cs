using TheSharks.Contracts.Models.Activities.BaseModels;

namespace TheSharks.Contracts.Models.Activities;

public class DiveActivityModel : GetActivityBaseModel
{
    public DateTimeOffset? Departure { get; set; }
    public DateTimeOffset? BriefingTime { get; set; }
    public string? Tide { get; set; }
    public DateTimeOffset? AtWater { get; set; }
}