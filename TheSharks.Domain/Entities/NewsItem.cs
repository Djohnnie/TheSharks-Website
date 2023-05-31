namespace TheSharks.Domain.Entities;

public class NewsItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTimeOffset PublishDate { get; set; } = DateTimeOffset.UtcNow;
    public Member Author { get; set; }
    public bool MembersOnly { get; set; }
}