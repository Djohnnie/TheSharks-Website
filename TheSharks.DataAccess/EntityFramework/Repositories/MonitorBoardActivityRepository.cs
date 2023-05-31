using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class MonitorBoardActivityRepository : Repository<MonitorBoardActivity>
{
    public MonitorBoardActivityRepository(TheSharksContext context) : base(context, context.MonitorBoardActivities) { }
}