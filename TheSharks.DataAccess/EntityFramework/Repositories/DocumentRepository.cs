using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class DocumentRepository : Repository<Document>
{
    public DocumentRepository(TheSharksContext context) : base(context, context.Documents) { }
}