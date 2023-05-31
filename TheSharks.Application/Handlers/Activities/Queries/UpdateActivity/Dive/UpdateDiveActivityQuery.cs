using MediatR;
using TheSharks.Contracts.Models.Activities.BaseModels;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.Dive;

public class UpdateDiveActivityQuery : UpdateActivityBaseModel, IRequest<BaseIdModel>
{
    public DateTimeOffset? Departure { get; set; }
    public DateTimeOffset? BriefingTime { get; set; }
    public string? Tide { get; set; }
    public DateTimeOffset? AtWater { get; set; }
}