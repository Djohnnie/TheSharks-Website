namespace TheSharks.Domain.Entities;

public class Page
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Link { get; set; }
    public bool IsOnlyAvailableForMembers { get; set; }
    public bool IsDefaultPage { get; set; }
    public bool IsDefaultPageForMembers { get; set; }
    public int NavBarPosition { get; set; }
    public int NavBarSubPosition { get; set; }
    public IList<Component> Components { get; set; }
}