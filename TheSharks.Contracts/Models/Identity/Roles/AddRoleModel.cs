namespace TheSharks.Contracts.Models.Identity.Roles;

public class AddRoleModel
{
    public string Name { get; set; }
    public bool ConcernsDivingCertificate { get; set; }
    public IEnumerable<RoleClaimModel> Claims { get; set; }
}