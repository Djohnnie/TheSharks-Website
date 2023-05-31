using MediatR;
using TheSharks.Contracts.Models.Activities.BaseModels;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.Event;

public class UpdateEventActivityQuery : UpdateActivityBaseModel, IRequest<BaseIdModel>
{
    public DateTimeOffset? Departure { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}