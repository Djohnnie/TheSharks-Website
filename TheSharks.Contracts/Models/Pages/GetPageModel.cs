using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Pages;

public class GetPageModel : PageBaseModel
{
    public IEnumerable<GetComponentModel> Components { get; set; }
}

public class GetComponentModel : BaseIdModel
{
    public string Name { get; set; }
    public string Content { get; set; }
    public int Position { get; set; }
}