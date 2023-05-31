namespace TheSharks.Contracts.Models.Pages;

public class PageOverviewModel : PageBaseModel
{
}

public class PageOverviewListModel
{
    public IEnumerable<PageOverviewModel> Pages { get; set; }
}