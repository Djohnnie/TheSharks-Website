using MediatR;
using TheSharks.Contracts.Models.Identity.Roles;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.AddRole;

public class AddRoleQuery : AddRoleModel, IRequest<RoleBaseModel>
{
}