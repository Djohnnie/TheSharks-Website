using MediatR;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.UpdateRolesOfMember;

public class UpdateRolesOfMemberQueryHandler : IRequestHandler<UpdateRolesOfMemberQuery, BaseIdModel>
{
    private readonly IRoleService<Role> _roleService;

    public UpdateRolesOfMemberQueryHandler(IRoleService<Role> roleService)
    {
        _roleService = roleService;
    }


    public async Task<BaseIdModel> Handle(UpdateRolesOfMemberQuery request, CancellationToken cancellationToken)
    {
        if (!request.RegularRoles.Any(x => x.IsChecked) && request.DiveCertificateRole == null) throw new RoleException("Je moet een rol toewijzen!");

        var roles = new List<string>();
        roles.Add(request.DiveCertificateRole.RoleName);
        roles.AddRange(request.RegularRoles.Where(x => x.IsChecked).Select(x => x.RoleName));

        var updateResult = await _roleService.UpdateRolesOfMember(request.MemberId, roles);

        return new BaseIdModel { Id = request.MemberId };
    }
}