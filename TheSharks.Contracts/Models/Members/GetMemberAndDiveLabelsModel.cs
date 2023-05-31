using TheSharks.Contracts.Models.Identity.Roles;

namespace TheSharks.Contracts.Models.Members;

public class GetMemberAndDiveLabelsModel
{
    public IEnumerable<MemberNameModel> Members { get; set; }
    public IEnumerable<RoleBaseModel> DiveRoles { get; set; }
}