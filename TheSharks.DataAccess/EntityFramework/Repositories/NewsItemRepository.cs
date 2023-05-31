using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class NewsItemRepository : Repository<NewsItem>
{
    public NewsItemRepository(TheSharksContext context) : base(context, context.NewsItems) { }
}