using Microsoft.AspNetCore.Identity;

namespace TheSharks.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public bool ConcernsDivingCertificate { get; set; }

    public IList<MemberRole> MemberRoles { get; set; }
}