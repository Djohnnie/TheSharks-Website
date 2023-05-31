using MediatR;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Identity.Roles;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.AddRole;

public class AddRoleQueryHandler : IRequestHandler<AddRoleQuery, RoleBaseModel>
{
    private readonly IRoleService<Role> _roleService;

    public AddRoleQueryHandler(IRoleService<Role> roleService)
    {
        _roleService = roleService;
    }

    public async Task<RoleBaseModel> Handle(AddRoleQuery request, CancellationToken cancellationToken)
    {
        if (!request.Claims.Any()) throw new RoleException("Je moet rechten koppelen aan je rol");

        var claimsToBeAssigned = new Dictionary<string, string>();
        foreach (var claim in request.Claims.Where(x => x.IsChecked))
        {
            claimsToBeAssigned.Add(claim.ClaimName, "Can" + claim.ClaimName);
        }

        var role = await _roleService.CreateRole(request.Name, request.ConcernsDivingCertificate, claimsToBeAssigned);

        return new RoleBaseModel { Id = role.Id, Name = role.Name };
    }
}