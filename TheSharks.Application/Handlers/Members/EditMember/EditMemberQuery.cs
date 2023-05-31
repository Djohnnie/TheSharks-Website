using MediatR;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.EditMember;

public class EditMemberQuery : IRequest<Member>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}