using MediatR;
using TheSharks.Contracts.Models.Identity.Roles;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.GetRoleStateOfMember;

public class GetRoleStateOfMemberQueryHandler : IRequestHandler<GetRoleStateOfMemberQuery, GetMemberRolesOverviewModel>
{
    private readonly IRoleService<Role> _roleService;

    public GetRoleStateOfMemberQueryHandler(IRoleService<Role> roleService)
    {
        _roleService = roleService;
    }

    public async Task<GetMemberRolesOverviewModel> Handle(GetRoleStateOfMemberQuery request, CancellationToken cancellationToken)
    {
        var regularRoles = new List<GetMemberRoleBaseModel>();
        var diveCertificateRoles = new List<GetMemberRoleBaseModel>();
        var roleNames = new List<string>();

        var userRoles = await _roleService.GetRolesAssignedToMember(request.Id);
        foreach (var role in userRoles)
        {
            var entity = new GetMemberRoleBaseModel { Name = role.Name, IsAssignedToMember = true, Id = role.Id };

            if (role.ConcernsDivingCertificate) diveCertificateRoles.Add(entity);
            else regularRoles.Add(entity);
            roleNames.Add(role.Name);
        }

        var remainingRoles = await _roleService.GetAllRoles(x => !roleNames.Contains(x.Name));
        foreach (var role in remainingRoles)
        {
            var entity = new GetMemberRoleBaseModel { Name = role.Name, IsAssignedToMember = false, Id = role.Id };

            if (role.ConcernsDivingCertificate) diveCertificateRoles.Add(entity);
            else regularRoles.Add(entity);
        }

        return new GetMemberRolesOverviewModel { DiveCertificateRoles = diveCertificateRoles.OrderBy(x => x.Name), NonDiveCertificateRoles = regularRoles.OrderBy(x => x.Name) };
    }
}