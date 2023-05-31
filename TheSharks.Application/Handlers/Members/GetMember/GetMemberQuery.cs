using MediatR;
using TheSharks.Contracts.Models.Members;

namespace TheSharks.Application.Handlers.Members.GetMember;

public class GetMemberQuery : IRequest<GetMemberModel>
{
    public Guid Id { get; set; }
}