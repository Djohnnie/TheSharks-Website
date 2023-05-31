using MediatR;
using TheSharks.Contracts.Models.Activities.BaseModels;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.MonitorBoard;

public class UpdateMonitorBoardActivityQuery : UpdateActivityBaseModel, IRequest<BaseIdModel>
{
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}