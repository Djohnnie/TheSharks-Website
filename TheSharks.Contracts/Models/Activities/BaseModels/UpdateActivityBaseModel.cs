using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Activities.BaseModels;

public class UpdateActivityBaseModel : BaseIdModel
{
    public string Title { get; set; }
    public string Location { get; set; }
    public string? LocationLink { get; set; }
    public string? Info { get; set; }
    public string? MemberInfo { get; set; }
    public DateTimeOffset Date { get; set; }
}