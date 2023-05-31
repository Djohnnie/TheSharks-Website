using MediatR;
using TheSharks.Contracts.Models.Identity.Roles;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.GetAllRoles;

public class GetAllRolesQuery : IRequest<GetAllRolesOverviewModel>
{

}