using System.Linq.Expressions;
using System.Security.Claims;
using TheSharks.Domain.Entities;

namespace TheSharks.Contracts.Services.Identity;

public interface IRoleService<TModel> where TModel : class
{
    Task<TModel> CreateRole(string roleName, bool concernsDivingCertificate, Dictionary<string, string> privileges);
    Task RemoveRole(Role role);
    Task<TModel> GetRole(Guid id);
    Task<IEnumerable<Claim>> GetClaimsOfRole(Role role);
    Task<IList<TModel>> GetAllRoles();
    Task<IList<TModel>> GetAllRoles(Expression<Func<TModel, bool>> filter);
    Task<IList<TModel>> GetRolesAssignedToMember(Guid memberId);
    Task<IList<TModel>> GetRolesAssignedToMember(Guid memberId, Expression<Func<TModel, bool>> filter);
    Task<Guid> UpdateRolesOfMember(Guid memberId, IEnumerable<string> roleNames);
    Task<Guid> UpdateClaimsOfRole(Guid id, Dictionary<string, string> privileges);
}