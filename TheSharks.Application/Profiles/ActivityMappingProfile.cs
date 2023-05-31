using AutoMapper;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Profiles;

public class ActivityMappingProfile : Profile
{
    public ActivityMappingProfile()
    {
        CreateMap<Activity, ActivityOverviewModel>();
        CreateMap<DiveActivity, ActivityOverviewModel>();
        CreateMap<EventActivity, ActivityOverviewModel>();
        CreateMap<MonitorBoardActivity, ActivityOverviewModel>();
        CreateMap<BoardMeetingActivity, ActivityOverviewModel>();
        CreateMap<BoardMeetingActivity, BoardMeetingActivityModel>();
        CreateMap<DiveActivity, DiveActivityModel>();
        CreateMap<EventActivity, EventActivityModel>();
        CreateMap<MonitorBoardActivity, MonitorBoardActivityModel>();
    }
}