using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Pages;

namespace TheSharks.Application.Handlers.Pages.Queries.UpdatePage;

public class UpdatePageQuery : BaseIdModel, IRequest<BaseIdModel>
{
    public string Title { get; set; }
    public string Link { get; set; }
    public bool IsOnlyAvailableForMembers { get; set; }
    public bool IsDefaultPage { get; set; }
    public bool IsDefaultPageForMembers { get; set; }
    public IEnumerable<AddComponentModel> Components { get; set; }
    public int NavBarPosition { get; set; }
    public int NavBarSubPosition { get; set; }
}