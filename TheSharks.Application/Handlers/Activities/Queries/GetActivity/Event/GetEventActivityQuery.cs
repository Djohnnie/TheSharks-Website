using MediatR;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.GetActivity.Event;

public class GetEventActivityQuery : BaseIdModel, IRequest<EventActivityModel>
{
}