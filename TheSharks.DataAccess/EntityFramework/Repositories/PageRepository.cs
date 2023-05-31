using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class PageRepository : Repository<Page>
{
    public PageRepository(TheSharksContext context) : base(context, context.Pages)
    {
    }
}