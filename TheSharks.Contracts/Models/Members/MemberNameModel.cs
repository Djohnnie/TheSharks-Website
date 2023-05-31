using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Members;

public class MemberNameModel : BaseIdModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}