using TheSharks.Contracts.Models.Activities.BaseModels;

namespace TheSharks.Contracts.Models.Activities;

public class MonitorBoardActivityModel : GetActivityBaseModel
{
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}