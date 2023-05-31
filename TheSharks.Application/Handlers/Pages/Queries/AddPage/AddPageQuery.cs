using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Pages;

namespace TheSharks.Application.Handlers.Pages.Queries.AddPage;

public class AddPageQuery : IRequest<BaseIdModel>
{
    public string Title { get; set; }
    public string? Link { get; set; }
    public IEnumerable<AddComponentModel> Components { get; set; }
}