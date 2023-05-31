using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class OpenWaterTestRepository : Repository<OpenWaterTest>
{
    public OpenWaterTestRepository(TheSharksContext context) : base(context, context.OpenWaterTests) { }
}