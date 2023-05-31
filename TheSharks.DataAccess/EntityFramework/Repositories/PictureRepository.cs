using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class PictureRepository : Repository<Picture>
{
    public PictureRepository(TheSharksContext context) : base(context, context.Pictures) { }
}