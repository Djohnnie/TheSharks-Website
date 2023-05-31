using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class EventActivityRepository : Repository<EventActivity>
{
    public EventActivityRepository(TheSharksContext context) : base(context, context.EventActivities) { }
}