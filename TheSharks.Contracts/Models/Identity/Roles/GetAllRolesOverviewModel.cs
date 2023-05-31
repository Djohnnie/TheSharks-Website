namespace TheSharks.Contracts.Models.Identity.Roles;

public class GetAllRolesOverviewModel
{
    public IEnumerable<RoleWithMembersCountModel> NonDiveCertificateRoles { get; set; }
    public IEnumerable<RoleWithMembersCountModel> DiveCertificateRoles { get; set; }
}