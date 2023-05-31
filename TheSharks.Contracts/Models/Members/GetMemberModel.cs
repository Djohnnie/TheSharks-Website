namespace TheSharks.Contracts.Models.Members;

public class GetMemberModel : MemberNameModel
{
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Bio { get; set; }
    public string Email { get; set; }
    public byte[] ProfilePicture { get; set; }
}