using Microsoft.EntityFrameworkCore;
using TheSharks.Contracts.DataAccess;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class MemberEnrollmentRepository : Repository<MemberEnrollment>, IMemberEnrollmentRepository<MemberEnrollment>
{
    private readonly DbSet<MemberEnrollment> _memberEnrollments;
    public MemberEnrollmentRepository(TheSharksContext context) : base(context, context.MemberEnrollments)
    {
        _memberEnrollments = context.MemberEnrollments;
    }

    public async Task<IEnumerable<Member>> GetTopBuddies(Guid id, int recordLimit)
    {
        var buddies = await _memberEnrollments
            .Where(x => x.Registrator.Id.Equals(id) && !x.Registree.Id.Equals(id))
            .Where(x => x.Registree.FirstName != "Ex-lid")
            .GroupBy(x => new { x.Registree.Id, x.Registree.FirstName, x.Registree.LastName })
            .Select(group => new { Id = group.Key.Id, FirstName = group.Key.FirstName, LastName = group.Key.LastName, Count = group.Count() })
            .OrderByDescending(x => x.Count)
            .Take(recordLimit)
            .ToListAsync();

        return buddies.Select(x => new Member { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName });
    }

    public async Task<IEnumerable<MemberEnrollment>> GetAllOfActivityAndRegistree(Guid activityId, Guid registreeId)
    {
        return await _memberEnrollments.Where(x => x.Activity.Id.Equals(activityId) && x.Registree.Id.Equals(registreeId)).ToListAsync();
    }

    public async Task<IEnumerable<MemberEnrollment>> GetAllWithRegistreeAndRegistrator(Guid id)
    {
        return await _memberEnrollments.Where(x => x.Activity.Id.Equals(id))
            .Include(x => x.Registrator)
            .Include(x => x.Registree)
            .Include(x => x.OpenWaterTest)
            .ToListAsync();
    }

    public async Task<MemberEnrollment?> GetUpcomingWithResponsible(Guid id)
    {
        return await _memberEnrollments.Where(x => x.Registree.Id.Equals(id) && x.Activity.Date > DateTimeOffset.Now)
            .Include(x => x.Activity)
            .ThenInclude(x => x.Responsible)
            .OrderBy(x => x.Activity.Date)
            .Take(1)
            .FirstOrDefaultAsync();
    }
}