using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class ActivityRepository : Repository<Activity>
{
    public ActivityRepository(TheSharksContext context) : base(context, context.Activities) { }
}