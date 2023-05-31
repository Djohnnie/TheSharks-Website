using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Activities.Queries.AddActivity.Dive;

public class AddDiveActivityQueryHandler : IRequestHandler<AddDiveActivityQuery, BaseIdModel>
{
    private readonly IRepository<DiveActivity> _activityRepository;
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _memberEnrollmentRepository;
    private readonly IMemberService<Member> _memberService;

    public AddDiveActivityQueryHandler(IRepository<DiveActivity> activityRepository, IMemberService<Member> memberService, IMemberEnrollmentRepository<MemberEnrollment> memberEnrollmentRepository)
    {
        _activityRepository = activityRepository;
        _memberService = memberService;
        _memberEnrollmentRepository = memberEnrollmentRepository;
    }

    public async Task<BaseIdModel> Handle(AddDiveActivityQuery request, CancellationToken cancellationToken)
    {
        var user = await _memberService.Find(request.ResponsibleId);

        var result = await _activityRepository.Add(new DiveActivity
        {
            Name = string.IsNullOrWhiteSpace(request.Title) ? $"Duik - {request.Location}" : request.Title,
            Date = request.Date.UtcDateTime.Date.AddDays(1),
            Departure = request.Departure,
            Info = request.Info,
            Location = request.Location,
            LocationLink = request.LocationLink,
            MemberInfo = request.MemberInfo,
            NecessarySubscription = request.NecessarySubscription,
            Responsible = user,
            AtWater = request.AtWater,
            BriefingTime = request.BriefingTime,
            Tide = request.Tide
        });

        await _memberEnrollmentRepository.Add(new MemberEnrollment { Activity = result, AsDiver = true, Registrator = user, Registree = user });

        return new BaseIdModel { Id = result.Id };
    }
}