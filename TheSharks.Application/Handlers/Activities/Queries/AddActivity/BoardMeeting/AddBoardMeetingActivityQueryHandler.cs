using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Activities.Queries.AddActivity.BoardMeeting;

public class AddBoardMeetingActivityQueryHandler : IRequestHandler<AddBoardMeetingActivityQuery, BaseIdModel>
{
    private readonly IRepository<BoardMeetingActivity> _activityRepository;
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _memberEnrollmentRepository;
    private readonly IMemberService<Member> _memberService;

    public AddBoardMeetingActivityQueryHandler(IRepository<BoardMeetingActivity> activityRepository, IMemberService<Member> memberService, IMemberEnrollmentRepository<MemberEnrollment> memberEnrollmentRepository)
    {
        _activityRepository = activityRepository;
        _memberService = memberService;
        _memberEnrollmentRepository = memberEnrollmentRepository;
    }

    public async Task<BaseIdModel> Handle(AddBoardMeetingActivityQuery request, CancellationToken cancellationToken)
    {
        var user = await _memberService.Find(request.ResponsibleId);

        var result = await _activityRepository.Add(new BoardMeetingActivity
        {
            Name = string.IsNullOrWhiteSpace(request.Title) ? $"Bestuursvergadering - {request.Location}" : request.Title,
            Date = request.Date.UtcDateTime.Date.AddDays(1),
            Info = request.Info,
            MemberInfo = request.MemberInfo,
            Location = request.Location,
            LocationLink = request.LocationLink,
            NecessarySubscription = request.NecessarySubscription,
            Responsible = user,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
        });

        await _memberEnrollmentRepository.Add(new MemberEnrollment { Activity = result, AsDiver = true, Registrator = user, Registree = user });

        return new BaseIdModel { Id = result.Id };
    }
}