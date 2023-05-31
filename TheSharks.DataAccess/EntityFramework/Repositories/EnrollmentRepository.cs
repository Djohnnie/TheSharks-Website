using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class EnrollmentRepository : Repository<Enrollment>
{
    public EnrollmentRepository(TheSharksContext context) : base(context, context.Enrollments) { }
}