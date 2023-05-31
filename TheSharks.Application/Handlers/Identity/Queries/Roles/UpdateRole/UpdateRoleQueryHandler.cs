using MediatR;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.UpdateRole;

public class UpdateRoleQueryHandler : IRequestHandler<UpdateRoleQuery, BaseIdModel>
{
    private readonly IRoleService<Role> _roleService;

    public UpdateRoleQueryHandler(IRoleService<Role> roleService)
    {
        _roleService = roleService;
    }

    public async Task<BaseIdModel> Handle(UpdateRoleQuery request, CancellationToken cancellationToken)
    {
        if (!request.Claims.Any(x => x.IsChecked)) throw new RoleException("Je moet rechten toewijzen!");

        var claimsToBeAssigned = new Dictionary<string, string>();
        foreach (var claim in request.Claims)
        {
            if (claim.IsChecked) claimsToBeAssigned.Add(claim.ClaimName, "Can" + claim.ClaimName);
        }

        var updateResult = await _roleService.UpdateClaimsOfRole(request.Id, claimsToBeAssigned);

        return new BaseIdModel { Id = updateResult };
    }
}