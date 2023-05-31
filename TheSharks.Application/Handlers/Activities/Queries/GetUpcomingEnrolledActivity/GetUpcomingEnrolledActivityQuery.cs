using MediatR;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.GetUpcomingEnrolledActivity;

public class GetUpcomingEnrolledActivityQuery : BaseIdModel, IRequest<ActivityOverviewModel>
{
}