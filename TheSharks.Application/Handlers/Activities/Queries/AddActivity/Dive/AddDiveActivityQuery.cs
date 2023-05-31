using MediatR;
using TheSharks.Contracts.Models.Activities.BaseModels;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.AddActivity.Dive;

public class AddDiveActivityQuery : AddActivityBaseModel, IRequest<BaseIdModel>
{
    public DateTime? Departure { get; set; }
    public DateTime? BriefingTime { get; set; }
    public string? Tide { get; set; }
    public DateTime? AtWater { get; set; }
}