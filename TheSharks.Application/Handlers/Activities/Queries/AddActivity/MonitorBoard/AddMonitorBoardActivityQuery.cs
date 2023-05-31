using MediatR;
using TheSharks.Contracts.Models.Activities.BaseModels;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.AddActivity.MonitorBoard;

public class AddMonitorBoardActivityQuery : AddActivityBaseModel, IRequest<BaseIdModel>
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}