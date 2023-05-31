using MediatR;
using Microsoft.AspNetCore.Http;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.EditProfile;

public class EditProfileQuery : IRequest<Member>
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Bio { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}