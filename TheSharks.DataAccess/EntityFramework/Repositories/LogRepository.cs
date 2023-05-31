using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class LogRepository : Repository<LogEntry>
{
    public LogRepository(TheSharksContext context) : base(context, context.Logs) { }
}