namespace TheSharks.Domain.Entities;

public class BoardMeetingActivity : Activity
{
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}