using Microsoft.AspNetCore.Identity;

namespace TheSharks.Domain.Entities;

public class Member : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Bio { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public IList<MemberRole> MemberRoles { get; set; }
    public IList<Enrollment> Enrollments { get; set; }
    public IList<NewsItem> NewsItems { get; set; }
}