using MediatR;

namespace TheSharks.Application.Handlers.Members.RemoveMember;

public class RemoveMemberCommand : IRequest
{
    public Guid Id { get; set; }
}