using MediatR;
using TheSharks.Contracts.Models.Pages;

namespace TheSharks.Application.Handlers.Pages.Queries.GetDefaultMembersPage;

public class GetDefaultMembersPageQuery : IRequest<GetDefaultPageModel>
{
}