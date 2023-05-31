using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class DiveActivityRepository : Repository<DiveActivity>
{
    public DiveActivityRepository(TheSharksContext context) : base(context, context.DiveActivities) { }
}