namespace TheSharks.Domain.Entities;

public class DiveActivity : Activity
{
    public DateTimeOffset? Departure { get; set; }
    public DateTimeOffset? BriefingTime { get; set; }
    public string? Tide { get; set; }
    public DateTimeOffset? AtWater { get; set; }
}