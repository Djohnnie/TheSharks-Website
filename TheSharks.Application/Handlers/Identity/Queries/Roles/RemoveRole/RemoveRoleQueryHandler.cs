using MediatR;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.RemoveRole;

public class RemoveRoleQueryHandler : IRequestHandler<RemoveRoleQuery, BaseIdModel>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IRoleService<Role> _roleService;

    public RemoveRoleQueryHandler(IMemberService<Member> memberService, IRoleService<Role> roleService)
    {
        _memberService = memberService;
        _roleService = roleService;
    }

    public async Task<BaseIdModel> Handle(RemoveRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRole(request.Id);

        var members = await _memberService.GetAllInRole(role.Name);
        if (members != null && members.Count() > 0) throw new RoleException($"Deze rol is nog in gebruik door {members.Count()} leden!");

        await _roleService.RemoveRole(role);

        return new BaseIdModel { Id = role.Id };
    }
}