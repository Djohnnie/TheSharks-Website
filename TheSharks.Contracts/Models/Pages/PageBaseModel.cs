using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Pages;

public class PageBaseModel : BaseIdModel
{
    public string Title { get; set; }
    public string? Link { get; set; }
    public bool IsOnlyAvailableForMembers { get; set; }
    public bool IsDefaultPage { get; set; }
    public bool IsDefaultPageForMembers { get; set; }
    public int NavBarPosition { get; set; }
    public int NavBarSubPosition { get; set; }
}