namespace TheSharks.Contracts.Models.Pages;

public class MenuNode
{
    public string Title { get; set; }
    public string? Link { get; set; }
    public int NavBarPosition { get; set; }
    public int NavBarSubPosition { get; set; }
    public bool MembersOnly { get; set; }
    public IEnumerable<MenuNode> Children { get; set; }
}

public class MenuTreeModel
{
    public IEnumerable<MenuNode> Tree { get; set; }
}