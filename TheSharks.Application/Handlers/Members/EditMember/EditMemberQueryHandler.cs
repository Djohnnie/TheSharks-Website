using MediatR;
using TheSharks.Contracts.Models.Identity.Authentication;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.EditMember;

public class EditMemberQueryHandler : IRequestHandler<EditMemberQuery, Member>
{
    private readonly IMemberService<Member> _memberService;

    public EditMemberQueryHandler(IMemberService<Member> memberService)
    {
        _memberService = memberService;
    }

    public async Task<Member> Handle(EditMemberQuery request, CancellationToken cancellationToken)
    {
        var result = await _memberService.Update(new EditMemberModel
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,            
            Email = request.Email,
            UserName = request.UserName
        });

        return result;
    }
}