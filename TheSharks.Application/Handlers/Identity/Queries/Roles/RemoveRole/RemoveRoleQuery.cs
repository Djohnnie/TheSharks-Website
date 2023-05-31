using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.RemoveRole;

public class RemoveRoleQuery : BaseIdModel, IRequest<BaseIdModel>
{
}