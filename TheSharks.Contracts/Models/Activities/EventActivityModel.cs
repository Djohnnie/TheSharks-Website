using TheSharks.Contracts.Models.Activities.BaseModels;

namespace TheSharks.Contracts.Models.Activities;

public class EventActivityModel : GetActivityBaseModel
{
    public DateTimeOffset? Departure { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}