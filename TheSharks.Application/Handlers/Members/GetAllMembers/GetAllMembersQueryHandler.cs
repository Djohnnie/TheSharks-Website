using AutoMapper;
using MediatR;
using TheSharks.Contracts.Models.Identity.Roles;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.GetAllMembers;

public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, MemberOverviewListModel>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IRoleService<Role> _roleService;
    private readonly IMapper _mapper;

    public GetAllMembersQueryHandler(
        IMemberService<Member> memberService,
        IRoleService<Role> roleService,
        IMapper mapper)
    {
        _memberService = memberService;
        _roleService = roleService;
        _mapper = mapper;
    }

    public async Task<MemberOverviewListModel> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        var toReturn = new List<MemberOverviewModel>();
        var idsMemberWithRole = new List<Guid>();
        var roles = await _roleService.GetAllRoles();

        foreach (var role in roles.Where(x => x.ConcernsDivingCertificate))
        {
            var members = await _memberService.GetAllInRole(role.Name);
            toReturn.AddRange(members.Select(x => new MemberOverviewModel { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Role = role.Name }));
            idsMemberWithRole.AddRange(members.Select(x => x.Id));
        }

        var membersWithoutRole = await _memberService.GetAll(x => !idsMemberWithRole.Contains(x.Id));
        toReturn.AddRange(membersWithoutRole.Select(x => new MemberOverviewModel { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Role = "Onbekend" }));

        return new MemberOverviewListModel { Members = toReturn.OrderBy(x => x.FirstName), Roles = _mapper.Map<IEnumerable<RoleBaseModel>>(roles.OrderByDescending(x => x.ConcernsDivingCertificate)) };
    }
}