using MediatR;
using TheSharks.Contracts.Models.Activities.BaseModels;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.AddActivity.Event;

public class AddEventActivityQuery : AddActivityBaseModel, IRequest<BaseIdModel>
{
    public DateTime? Departure { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
