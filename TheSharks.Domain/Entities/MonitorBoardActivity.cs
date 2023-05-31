namespace TheSharks.Domain.Entities;

public class MonitorBoardActivity : Activity
{
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}