using MediatR;
using TheSharks.Application.Handlers.Members.EditMember;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.RemoveMember;

public class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand>
{
    private readonly IMemberService<Member> _memberService;

    public RemoveMemberCommandHandler(IMemberService<Member> memberService)
    {
        _memberService = memberService;
    }

    public async Task<Unit> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
    {
        await _memberService.Deactive(request.Id);

        return Unit.Value;
    }
}