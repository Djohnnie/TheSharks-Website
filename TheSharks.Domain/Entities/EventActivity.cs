namespace TheSharks.Domain.Entities;

public class EventActivity : Activity
{
    public DateTimeOffset? Departure { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}