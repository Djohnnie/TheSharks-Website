using MediatR;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.GetActivity.MonitorBoard;

public class GetMonitorBoardActivityQuery : BaseIdModel, IRequest<MonitorBoardActivityModel>
{
}