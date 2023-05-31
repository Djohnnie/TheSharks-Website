using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Activities.Queries.AddActivity.MonitorBoard;

public class AddMonitorBoardActivityQueryHandler : IRequestHandler<AddMonitorBoardActivityQuery, BaseIdModel>
{
    private readonly IRepository<MonitorBoardActivity> _activityRepository;
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _memberEnrollmentRepository;
    private readonly IMemberService<Member> _memberService;

    public AddMonitorBoardActivityQueryHandler(IRepository<MonitorBoardActivity> activityRepository, IMemberService<Member> memberService, IMemberEnrollmentRepository<MemberEnrollment> memberEnrollmentRepository)
    {
        _activityRepository = activityRepository;
        _memberService = memberService;
        _memberEnrollmentRepository = memberEnrollmentRepository;
    }

    public async Task<BaseIdModel> Handle(AddMonitorBoardActivityQuery request, CancellationToken cancellationToken)
    {
        var user = await _memberService.Find(request.ResponsibleId);

        var result = await _activityRepository.Add(new MonitorBoardActivity
        {
            Name = string.IsNullOrWhiteSpace(request.Title) ? $"Monitorenvergadering - {request.Location}" : request.Title,
            Date = request.Date.UtcDateTime.Date.AddDays(1),
            Info = request.Info,
            Location = request.Location,
            LocationLink = request.LocationLink,
            MemberInfo = request.MemberInfo,
            NecessarySubscription = request.NecessarySubscription,
            Responsible = user,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
        });

        await _memberEnrollmentRepository.Add(new MemberEnrollment { Activity = result, AsDiver = true, Registrator = user, Registree = user });

        return new BaseIdModel { Id = result.Id };
    }
}