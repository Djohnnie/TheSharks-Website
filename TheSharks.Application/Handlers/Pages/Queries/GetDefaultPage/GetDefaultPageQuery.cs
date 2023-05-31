using MediatR;
using TheSharks.Contracts.Models.Pages;

namespace TheSharks.Application.Handlers.Pages.Queries.GetDefaultPage;

public class GetDefaultPageQuery : IRequest<GetDefaultPageModel>
{
}