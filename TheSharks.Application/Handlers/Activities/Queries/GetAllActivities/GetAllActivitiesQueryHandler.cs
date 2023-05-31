using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Contracts.Services.Members;
using TheSharks.Contracts.Services.Statistics;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Activities.Queries.GetAllActivities;

public class GetAllActiviesQueryHandler : IRequestHandler<GetAllActivitiesQuery, ActivityOverviewListModel>
{
    private readonly IRepository<Activity> _activityRepository;
    private readonly IRepository<Enrollment> _enrollmentRepository;
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _memberEnrollmentRepository;
    private readonly IStatisticsService _statisticsService;
    private readonly IMapper _mapper;
    private readonly IMemberService<Member> _memberService;

    public GetAllActiviesQueryHandler(
        IRepository<Activity> activityRepository,
        IRepository<Enrollment> enrollmentRepository,
        IMemberEnrollmentRepository<MemberEnrollment> memberEnrollmentRepository,
        IStatisticsService statisticsService,
        IMapper mapper, IMemberService<Member> memberService)
    {
        _activityRepository = activityRepository;
        _enrollmentRepository = enrollmentRepository;
        _memberEnrollmentRepository = memberEnrollmentRepository;
        _statisticsService = statisticsService;
        _mapper = mapper;
        _memberService = memberService;
    }

    public async Task<ActivityOverviewListModel> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
    {
        var minimumDate = DateTime.UtcNow.Date.AddDays(-6);

        var currentMemberId = (await _memberService.GetCurrent())?.Id ?? Guid.Empty;

        var totalRecords = await _activityRepository.TableCount(
            request.ActivityTypeFilter != null ? x => x.ActivityType == request.ActivityTypeFilter : null,
            request.DateFilter != null
            ? x => x.Date.Month == request.DateFilter.Value.Month && x.Date.Year == request.DateFilter.Value.Year
            : x => x.Date.Date >= minimumDate);

        var filterResult = await _activityRepository.GetAllOrderBy(
            request.Page, request.RecordsPerPage, x => x.Date, x => x.Responsible,
            request.ActivityTypeFilter != null ? x => x.ActivityType == request.ActivityTypeFilter : null,
            request.DateFilter != null
            ? x => x.Date.Month == request.DateFilter.Value.Month && x.Date.Year == request.DateFilter.Value.Year
            : x => x.Date.Date >= minimumDate);

        var activities = new List<ActivityOverviewModel>();

        foreach (var activity in filterResult)
        {
            var memberCount = await _enrollmentRepository.TableCount(x => x.Activity.Id == activity.Id);
            var userEnrolled = await _memberEnrollmentRepository.TableCount(x => x.Activity.Id == activity.Id && x.Registree.Id == currentMemberId) > 0;

            var activityOverview = _mapper.Map<ActivityOverviewModel>(activity);

            if (activity is DiveActivity diveActivity)
            {
                activityOverview = _mapper.Map<ActivityOverviewModel>(diveActivity);
            }

            if (activity is EventActivity eventActivity)
            {
                activityOverview = _mapper.Map<ActivityOverviewModel>(eventActivity);
            }

            if (activity is MonitorBoardActivity monitorBoardActivity)
            {
                activityOverview = _mapper.Map<ActivityOverviewModel>(monitorBoardActivity);
            }

            if (activity is BoardMeetingActivity boardMeetingActivity)
            {
                activityOverview = _mapper.Map<ActivityOverviewModel>(boardMeetingActivity);
            }

            activityOverview.MemberCount = activity.NecessarySubscription ? memberCount : null;
            activityOverview.UserEnrolled = userEnrolled;

            activities.Add(activityOverview);
        }

        await _statisticsService.RecordStatistics("activities");

        return new ActivityOverviewListModel { Activities = activities, TotalRecords = totalRecords };
    }
}