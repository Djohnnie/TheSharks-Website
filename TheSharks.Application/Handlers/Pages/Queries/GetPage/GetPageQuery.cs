using MediatR;
using TheSharks.Contracts.Models.Pages;

namespace TheSharks.Application.Handlers.Pages.Queries.GetPage;

public class GetPageQuery : IRequest<GetPageModel>
{
    public string Link { get; set; }
}