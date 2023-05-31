using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Identity.Roles;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.GetAllMembersAndDiveRoles;

public class GetAllMembersAndDiveRolesQueryHandler : IRequestHandler<GetAllMembersAndDiveRolesQuery, GetMemberAndDiveLabelsModel>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _memberEnrollmentRepository;
    private readonly IRoleService<Role> _roleService;
    private readonly IMapper _mapper;

    public GetAllMembersAndDiveRolesQueryHandler(IMemberService<Member> memberService, IRoleService<Role> roleService, IMapper mapper, IMemberEnrollmentRepository<MemberEnrollment> memberEnrollmentRepository)
    {
        _memberService = memberService;
        _roleService = roleService;
        _mapper = mapper;
        _memberEnrollmentRepository = memberEnrollmentRepository;
    }

    public async Task<GetMemberAndDiveLabelsModel> Handle(GetAllMembersAndDiveRolesQuery request, CancellationToken cancellationToken)
    {
        var bestBuddies = await _memberEnrollmentRepository.GetTopBuddies(request.Id, 10);
        var buddiesIds = bestBuddies.Select(x => x.Id).ToList();

        // Avoid fetching self from db
        buddiesIds.Add(request.Id);

        var remainingMembers = await _memberService.GetAll(member => !buddiesIds.Contains(member.Id));

        var diveRoles = await _roleService.GetAllRoles(x => x.ConcernsDivingCertificate);

        return new GetMemberAndDiveLabelsModel
        {
            Members = _mapper.Map<IEnumerable<MemberNameModel>>(bestBuddies.Concat(remainingMembers.OrderBy(x => x.FirstName))),
            DiveRoles = _mapper.Map<IEnumerable<RoleBaseModel>>(diveRoles)
        };
    }
}