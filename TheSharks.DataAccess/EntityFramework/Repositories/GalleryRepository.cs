using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class GalleryRepository : Repository<Gallery>
{
    public GalleryRepository(TheSharksContext context) : base(context, context.Galleries) { }
}