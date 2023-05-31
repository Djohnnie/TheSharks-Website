using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Identity.Roles;

public class GetRoleModel : BaseIdModel
{
    public IEnumerable<RoleClaimModel> Claims { get; set; }
    public string Name { get; set; }
}