using MediatR;
using TheSharks.Contracts.Models.Identity.Roles;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.GetRole;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, GetRoleModel>
{
    private readonly IRoleService<Role> _roleService;

    public GetRoleQueryHandler(IRoleService<Role> roleService)
    {
        _roleService = roleService;
    }

    public async Task<GetRoleModel> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRole(request.Id);
        var claims = await _roleService.GetClaimsOfRole(role);

        var claimDto = new List<RoleClaimModel>();
        foreach (var claim in claims)
        {
            claimDto.Add(new RoleClaimModel { ClaimName = claim.Type, IsChecked = true });
        }

        return new GetRoleModel { Id = role.Id, Claims = claimDto, Name = role.Name };
    }
}