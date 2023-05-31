using AutoMapper;
using MediatR;
using TheSharks.Contracts.Models.Identity.Roles;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, GetAllRolesOverviewModel>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IRoleService<Role> _roleService;
    private readonly IMapper _mapper;

    public GetAllRolesQueryHandler(IMemberService<Member> memberService, IRoleService<Role> roleService, IMapper mapper)
    {
        _memberService = memberService;
        _roleService = roleService;
        _mapper = mapper;
    }

    public async Task<GetAllRolesOverviewModel> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var members = await _memberService.GetAllOrderBy(x => x.FirstName, false);
        var allMembers = members.ToList();

        var result = await _roleService.GetAllRoles();

        var toReturn = new GetAllRolesOverviewModel
        {
            DiveCertificateRoles = _mapper.Map<IEnumerable<RoleWithMembersCountModel>>(result.Where(x => x.ConcernsDivingCertificate)),
            NonDiveCertificateRoles = _mapper.Map<IEnumerable<RoleWithMembersCountModel>>(result.Where(x => !x.ConcernsDivingCertificate))
        };

        foreach (var role in toReturn.DiveCertificateRoles)
        {
            role.MemberCount = allMembers.Count(x => x.MemberRoles.Select(s => s.Role.Name).Contains(role.Name));
        }

        foreach (var role in toReturn.NonDiveCertificateRoles)
        {
            role.MemberCount = allMembers.Count(x => x.MemberRoles.Select(s => s.Role.Name).Contains(role.Name));
        }

        return toReturn;
    }
}