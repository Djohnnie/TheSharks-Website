using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Identity.Roles;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.GetRole;

public class GetRoleQuery : BaseIdModel, IRequest<GetRoleModel>
{

}