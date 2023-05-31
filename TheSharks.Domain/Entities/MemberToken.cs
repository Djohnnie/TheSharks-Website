using Microsoft.AspNetCore.Identity;

namespace TheSharks.Domain.Entities;

public class MemberToken : IdentityUserToken<Guid> { }