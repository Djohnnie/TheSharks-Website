using AutoMapper;
using MediatR;
using TheSharks.Contracts.Models.Identity.Roles;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Services.Members;
using TheSharks.Contracts.Services.Statistics;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.GetAllMembersWithRoles;

public class GetAllMembersWithRolesQueryHandler : IRequestHandler<GetAllMembersWithRolesQuery, MemberRolesOverviewListModel>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IStatisticsService _statisticsService;
    private readonly IMapper _mapper;

    public GetAllMembersWithRolesQueryHandler(
        IMemberService<Member> memberService,
        IStatisticsService statisticsService, IMapper mapper)
    {
        _memberService = memberService;
        _statisticsService = statisticsService;
        _mapper = mapper;
    }

    public async Task<MemberRolesOverviewListModel> Handle(GetAllMembersWithRolesQuery request, CancellationToken cancellationToken)
    {
        var members = (await _memberService.GetAllOrderBy(x => x.FirstName, false)).ToList();
        var toReturn = new List<MemberRolesOverviewModel>();

        foreach (var member in members)
        {
            var roles = member.MemberRoles.Select(x => x.Role);

            toReturn.Add(new MemberRolesOverviewModel
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Roles = _mapper.Map<IEnumerable<MemberRoleOverviewModel>>(roles.OrderBy(x => x.ConcernsDivingCertificate))
            });
        }

        await _statisticsService.RecordStatistics("members");

        return new MemberRolesOverviewListModel { Members = toReturn };
    }
}