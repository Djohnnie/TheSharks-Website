using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class ComponentRepository : Repository<Component>
{
    public ComponentRepository(TheSharksContext context) : base(context, context.Components)
    {
    }
}