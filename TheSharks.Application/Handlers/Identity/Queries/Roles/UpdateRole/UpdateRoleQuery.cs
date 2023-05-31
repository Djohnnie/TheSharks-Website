using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.UpdateRole;

public class UpdateRoleQuery : BaseIdModel, IRequest<BaseIdModel>
{
    public IEnumerable<UpdateRoleClaimModel> Claims { get; set; }
}

public class UpdateRoleClaimModel
{
    public string ClaimName { get; set; }
    public bool IsChecked { get; set; }
}