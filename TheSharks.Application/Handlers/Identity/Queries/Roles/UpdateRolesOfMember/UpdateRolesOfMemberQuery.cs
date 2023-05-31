using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.UpdateRolesOfMember;

public class UpdateRolesOfMemberQuery : IRequest<BaseIdModel>
{
    public Guid MemberId { get; set; }
    public UpdateUserRoleModel DiveCertificateRole { get; set; }
    public IEnumerable<UpdateUserRoleModel> RegularRoles { get; set; } = Enumerable.Empty<UpdateUserRoleModel>();
}

public class UpdateUserRoleModel
{
    public string RoleName { get; set; }
    public bool IsChecked { get; set; }
}