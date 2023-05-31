using System.Linq.Expressions;
using TheSharks.Contracts.Models.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Contracts.Services.Members;

public interface IMemberService<TModel> where TModel : class
{
    Task<Member?> GetCurrent();
    Task<TModel> Find(Guid id);
    Task<TModel> Update(EditProfileModel model);
    Task<TModel> Update(EditMemberModel model);
    Task<TModel> ResetPassword(ResetPasswordModel model);
    Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>> filter);
    Task<IEnumerable<TModel>> GetAllOrderBy<TProperty>(Expression<Func<TModel, TProperty>> orderProperty, bool descending);
    Task<IEnumerable<TModel>> GetAllInRole(string roleName);
    Task<Member> Deactive(Guid id);
}