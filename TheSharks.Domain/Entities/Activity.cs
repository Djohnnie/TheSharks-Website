namespace TheSharks.Domain.Entities;

public abstract class Activity
{
    public Guid Id { get; set; }
    public string ActivityType { get; set; }
    public string Name { get; set; }
    public DateTimeOffset Date { get; set; }
    public string Location { get; set; }
    public string? LocationLink { get; set; }
    public string? Info { get; set; }
    public string? MemberInfo { get; set; }
    public bool NecessarySubscription { get; set; } = false;
    public Member Responsible { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
}