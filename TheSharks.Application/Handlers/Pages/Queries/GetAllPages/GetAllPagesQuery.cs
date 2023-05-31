using MediatR;
using TheSharks.Contracts.Models.Pages;

namespace TheSharks.Application.Handlers.Pages.Queries.GetAllPages;

public class GetAllPagesQuery : IRequest<PageOverviewListModel>
{
}