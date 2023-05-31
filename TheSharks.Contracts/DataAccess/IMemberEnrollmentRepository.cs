using TheSharks.Domain.Entities;

namespace TheSharks.Contracts.DataAccess;

public interface IMemberEnrollmentRepository<TModel> : IRepository<TModel> where TModel : class
{
    Task<IEnumerable<TModel>> GetAllWithRegistreeAndRegistrator(Guid id);
    Task<IEnumerable<Member>> GetTopBuddies(Guid id, int recordLimit);
    Task<IEnumerable<TModel>> GetAllOfActivityAndRegistree(Guid activityId, Guid registreeId);
    Task<TModel?> GetUpcomingWithResponsible(Guid id);
}