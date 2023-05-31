using Microsoft.AspNetCore.Http;

namespace TheSharks.Contracts.Models.Members;

public class EditProfileModel : MemberNameModel
{
    public string Bio { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}