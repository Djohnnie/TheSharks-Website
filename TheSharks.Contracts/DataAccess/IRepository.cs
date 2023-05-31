using System.Linq.Expressions;

namespace TheSharks.Contracts.DataAccess;

public interface IRepository<TModel> where TModel : class
{
    Task<TModel> Detach(TModel model);
    Task<int> TableCount();
    Task<int> TableCount<TKey>(Expression<Func<TModel, TKey>> distinctBy);
    Task<int> TableCount(Expression<Func<TModel, bool>> condition);
    Task<int> TableCount<TKey>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TKey>> distinctBy);
    Task<int> TableCount(params Expression<Func<TModel, bool>>?[] filters);
    Task<List<TKey>> GetGroupedKeys<TKey>(Expression<Func<TModel, TKey>> keySelector);
    Task<List<TKey>> GetGroupedKeys<TKey>(Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, TKey>> keySelector);
    Task<TModel?> FindIfExists(Expression<Func<TModel, bool>> condition);
    Task<TModel> Find(Expression<Func<TModel, bool>> condition);
    Task<TModel> Find<TProperty>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TProperty>> property);
    Task<IList<TModel>> GetAll();
    Task<IList<TModel>> GetAll(int page, int recordsPerPage);
    Task<IList<TModel>> GetAll<TProperty>(int page, int recordsPerPage, Expression<Func<TModel, TProperty>> property, params Expression<Func<TModel, bool>>?[] filters);
    Task<IList<TModel>> GetAllOrderBy<TPropertyOrder, TProperty>(int page, int recordsPerPage, Expression<Func<TModel, TPropertyOrder>> propertyOrder, Expression<Func<TModel, TProperty>> property, params Expression<Func<TModel, bool>>?[] filters);
    Task<IList<TModel>> GetAll(Expression<Func<TModel, bool>> condition);
    Task<IList<TModel>> GetAll(Expression<Func<TModel, bool>> condition, int page, int recordsPerPage);
    Task<IList<TModel>> GetAll<TProperty>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TProperty>> property, int page, int recordsPerPage);
    Task<IList<TModel>> GetAllOrderBy<TPropertyOrder1, TPropertyOrder2>(Expression<Func<TModel, TPropertyOrder1>> propertyOrder1, Expression<Func<TModel, TPropertyOrder2>> propertyOrder2);
    Task<IList<TModel>> GetAll<TProperty>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TProperty>> include);
    Task<IList<TModel>> GetAll<TProperty1, TProperty2>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TProperty1>> include1, Expression<Func<TModel, TProperty2>> include2);
    Task<IList<TModel>> GetAll<TProperty>(Expression<Func<TModel, TProperty>> property, int page, int recordsPerPage);
    Task<IList<TModel>> GetAllOrderBy<TPropertyInclude, TPropertyOrder>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TPropertyInclude>> propertyInclude, Expression<Func<TModel, TPropertyOrder>> propertyOrder, int page, int recordsPerPage);
    Task<IList<TModel>> GetAllOrderBy<TPropertyInclude, TPropertyOrder>(Expression<Func<TModel, TPropertyInclude>> propertyInclude, Expression<Func<TModel, TPropertyOrder>> propertyOrder, int page, int recordsPerPage);
    Task<IList<TModel>> GetAllOrderBy<TPropertyOrder>(Expression<Func<TModel, TPropertyOrder>> propertyOrder, int page, int recordsPerPage);
    Task<IList<TModel>> GetAllOrderByDescending<TPropertyOrder>(Expression<Func<TModel, TPropertyOrder>> propertyOrder, int page, int recordsPerPage);
    Task<TModel> Add(TModel entity);
    Task<IList<TModel>> Add(IList<TModel> entities);
    Task<TModel> Update(TModel entity);
    Task<TModel> Update(TModel entity, params Expression<Func<TModel, object>>[] updatedProperties);
    Task<int> UpdateAll(IList<TModel> entities, params Expression<Func<TModel, object>>[] updatedProperties);
    Task<TModel> Delete(Guid id);
    Task<IList<TModel>> Delete(Expression<Func<TModel, bool>> condition);
}