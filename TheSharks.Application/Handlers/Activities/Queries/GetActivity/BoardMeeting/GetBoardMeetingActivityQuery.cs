using MediatR;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.GetActivity.BoardMeeting;

public class GetBoardMeetingActivityQuery : BaseIdModel, IRequest<BoardMeetingActivityModel>
{
}