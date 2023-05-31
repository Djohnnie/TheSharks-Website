using MediatR;
using TheSharks.Contracts.Models.Activities.BaseModels;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.BoardMeeting;

public class UpdateBoardMeetingActivityQuery : UpdateActivityBaseModel, IRequest<BaseIdModel>
{
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}