namespace TheSharks.Contracts.Models.Identity.Roles;

public class GetMemberRoleBaseModel : RoleBaseModel
{
    public bool IsAssignedToMember { get; set; }
}

public class GetMemberRolesOverviewModel
{
    public IEnumerable<GetMemberRoleBaseModel> NonDiveCertificateRoles { get; set; }
    public IEnumerable<GetMemberRoleBaseModel> DiveCertificateRoles { get; set; }
}