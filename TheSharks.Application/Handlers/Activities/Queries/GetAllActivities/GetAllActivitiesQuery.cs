using MediatR;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Contracts.Models.Pagination;

namespace TheSharks.Application.Handlers.Activities.Queries.GetAllActivities;

public class GetAllActivitiesQuery : PaginationBaseModel, IRequest<ActivityOverviewListModel>
{
    public DateTimeOffset? DateFilter { get; set; }
    public string? ActivityTypeFilter { get; set; }
}