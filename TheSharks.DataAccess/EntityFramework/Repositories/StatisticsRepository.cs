using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class StatisticsRepository : Repository<Statistics>
{
    public StatisticsRepository(TheSharksContext context) : base(context, context.Statistics) { }
}