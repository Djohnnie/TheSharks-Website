using Microsoft.AspNetCore.Identity;

namespace TheSharks.Domain.Entities;

public class MemberRole : IdentityUserRole<Guid>
{
    public virtual Member Member { get; set; }

    public virtual Role Role { get; set; }
}