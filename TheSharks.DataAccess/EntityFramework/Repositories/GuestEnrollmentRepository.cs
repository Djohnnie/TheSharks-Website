using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class GuestEnrollmentRepository : Repository<GuestEnrollment>
{
    public GuestEnrollmentRepository(TheSharksContext context) : base(context, context.GuestEnrollments) { }
}