using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Identity.Roles;

public class RoleBaseModel : BaseIdModel
{
    public string Name { get; set; }
}

public class MemberRoleOverviewModel : RoleBaseModel
{
    public bool ConcernsDivingCertificate { get; set; }
}

public class RoleWithMembersCountModel : RoleBaseModel
{
    public int MemberCount { get; set; }
}