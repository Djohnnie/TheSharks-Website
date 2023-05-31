using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class BoardMeetingActivityRepository : Repository<BoardMeetingActivity>
{
    public BoardMeetingActivityRepository(TheSharksContext context) : base(context, context.BoardMeetingActivities) { }
}