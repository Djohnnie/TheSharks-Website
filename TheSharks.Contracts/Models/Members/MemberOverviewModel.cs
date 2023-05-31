using TheSharks.Contracts.Models.Identity.Roles;

namespace TheSharks.Contracts.Models.Members;

public class MemberOverviewModel : MemberNameModel
{
    public string Role { get; set; }
}

public class MemberRolesOverviewModel : MemberNameModel
{
    public IEnumerable<MemberRoleOverviewModel> Roles { get; set; }
}

public class MemberRolesOverviewListModel
{
    public IEnumerable<MemberRolesOverviewModel> Members { get; set; }
}

public class MemberOverviewListModel
{
    public IEnumerable<MemberOverviewModel> Members { get; set; }
    public IEnumerable<RoleBaseModel> Roles { get; set; }
}