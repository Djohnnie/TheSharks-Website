using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Exceptions;

namespace TheSharks.DataAccess.EntityFramework.Repositories;

public class Repository<TModel> : IRepository<TModel> where TModel : class
{
    private readonly DbContext _dbContext;
    private DbSet<TModel> _dbSet;

    public Repository(DbContext context, DbSet<TModel> dbSet)
    {
        _dbContext = context;
        _dbSet = dbSet;
    }

    public async Task<int> TableCount()
    {
        return _dbSet.Count();
    }

    public async Task<int> TableCount<TKey>(Expression<Func<TModel, TKey>> distinctBy)
    {
        return _dbSet.GroupBy(distinctBy).Count();
    }

    public async Task<int> TableCount(Expression<Func<TModel, bool>> condition)
    {
        return _dbSet.Where(condition).Count();
    }

    public async Task<int> TableCount<TKey>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TKey>> distinctBy)
    {
        return _dbSet.Where(condition).GroupBy(distinctBy).Count();
    }

    public async Task<int> TableCount(params Expression<Func<TModel, bool>>?[] filters)
    {
        var result = _dbSet.AsQueryable();

        foreach (var filter in filters.Where(x => x != null))
        {
            result = result.Where(filter);
        }

        return result.Count();
    }

    public async Task<List<TKey>> GetGroupedKeys<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        return await _dbSet.GroupBy(keySelector).OrderByDescending(x => x.Count()).Select(x => x.Key).ToListAsync();
    }

    public async Task<List<TKey>> GetGroupedKeys<TKey>(Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, TKey>> keySelector)
    {
        return await _dbSet.Where(predicate).GroupBy(keySelector).OrderByDescending(x => x.Count()).Select(x => x.Key).ToListAsync();
    }

    public async Task<TModel> Add(TModel entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IList<TModel>> Add(IList<TModel> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
        return entities;
    }

    public async Task<TModel> Delete(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) throw new AppException("Systeem error");
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IList<TModel>> Delete(Expression<Func<TModel, bool>> condition)
    {
        var entities = await _dbSet.Where(condition).ToListAsync();
        if (entities == null) throw new AppException("Systeem error");
        if (!entities.Any()) return entities;
        _dbSet.RemoveRange(entities);
        await _dbContext.SaveChangesAsync();
        return entities;
    }

    public async Task<TModel> Update(TModel entity, params Expression<Func<TModel, object>>[] updatedProperties)
    {
        _dbSet.Attach(entity);
        foreach (var property in updatedProperties)
        {
            _dbContext.Entry(entity).Property(property).IsModified = true;
        }
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<int> UpdateAll(IList<TModel> entities, params Expression<Func<TModel, object>>[] updatedProperties)
    {
        foreach (var entity in entities)
        {
            foreach (var property in updatedProperties)
            {
                _dbContext.Entry(entity).Property(property).IsModified = true;
            }
        }
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<TModel?> FindIfExists(Expression<Func<TModel, bool>> condition)
    {
        var result = await _dbSet.SingleOrDefaultAsync(condition);
        return result;
    }

    public async Task<TModel> Find(Expression<Func<TModel, bool>> condition)
    {
        var result = await _dbSet.SingleOrDefaultAsync(condition);
        if (result == null) throw new AppException("Systeem error");
        return result;
    }

    public async Task<TModel> Find<TProperty>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TProperty>> property)
    {
        var result = await _dbSet.Include(property).SingleOrDefaultAsync(condition);
        if (result == null) throw new AppException("Systeem error");
        return result;
    }

    public async Task<IList<TModel>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IList<TModel>> GetAll(int page, int recordsPerPage)
    {
        return await _dbSet.Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }

    public async Task<IList<TModel>> GetAll<TProperty>(Expression<Func<TModel, TProperty>> property, int page, int recordsPerPage)
    {
        return await _dbSet.Include(property).Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }

    public async Task<IList<TModel>> GetAll<TProperty>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TProperty>> include)
    {
        return await _dbSet.Include(include).Where(condition).ToListAsync();
    }

    public async Task<IList<TModel>> GetAll(Expression<Func<TModel, bool>> condition)
    {
        return await _dbSet.Where(condition).ToListAsync();
    }

    public async Task<IList<TModel>> GetAll<TProperty1, TProperty2>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TProperty1>> include1, Expression<Func<TModel, TProperty2>> include2)
    {
        return await _dbSet.Include(include1).Include(include2).Where(condition).ToListAsync();
    }

    public async Task<IList<TModel>> GetAll(Expression<Func<TModel, bool>> condition, int page, int recordsPerPage)
    {
        return await _dbSet.Where(condition).Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }

    public async Task<IList<TModel>> GetAll<TProperty>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TProperty>> property, int page, int recordsPerPage)
    {
        return await _dbSet.Include(property).Where(condition).Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }

    public async Task<IList<TModel>> GetAllOrderBy<TPropertyInclude, TPropertyOrder>(Expression<Func<TModel, bool>> condition, Expression<Func<TModel, TPropertyInclude>> propertyInclude, Expression<Func<TModel, TPropertyOrder>> propertyOrder, int page, int recordsPerPage)
    {
        return await _dbSet.Include(propertyInclude).Where(condition).OrderByDescending(propertyOrder).Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }

    public async Task<IList<TModel>> GetAllOrderBy<TPropertyInclude, TPropertyOrder>(Expression<Func<TModel, TPropertyInclude>> propertyInclude, Expression<Func<TModel, TPropertyOrder>> propertyOrder, int page, int recordsPerPage)
    {
        return await _dbSet.Include(propertyInclude).OrderByDescending(propertyOrder).Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }

    public async Task<IList<TModel>> GetAllOrderBy<TPropertyOrder1, TPropertyOrder2>(Expression<Func<TModel, TPropertyOrder1>> propertyOrder1, Expression<Func<TModel, TPropertyOrder2>> propertyOrder2)
    {
        return await _dbSet.OrderBy(propertyOrder1).ThenBy(propertyOrder2).ToListAsync();
    }

    public async Task<IList<TModel>> GetAll<TProperty>(int page, int recordsPerPage, Expression<Func<TModel, TProperty>> property, params Expression<Func<TModel, bool>>?[] filters)
    {
        var result = _dbSet.Include(property).AsQueryable();

        foreach (var filter in filters.Where(x => x != null))
        {
            result = result.Where(filter);
        }

        return await result.Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }

    public async Task<IList<TModel>> GetAllOrderBy<TPropertyOrder, TProperty>(
        int page, int recordsPerPage, Expression<Func<TModel, TPropertyOrder>> propertyOrder,
        Expression<Func<TModel, TProperty>> property, params Expression<Func<TModel, bool>>?[] filters)
    {
        var result = _dbSet.Include(property).OrderBy(propertyOrder).AsQueryable();

        foreach (var filter in filters.Where(x => x != null))
        {
            result = result.Where(filter);
        }

        return await result.Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }

    public async Task<TModel> Update(TModel entity)
    {
        _dbSet.Attach(entity);
        var entry = _dbContext.Entry(entity);
        entry.State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IList<TModel>> GetAllOrderBy<TPropertyOrder>(Expression<Func<TModel, TPropertyOrder>> propertyOrder, int page, int recordsPerPage)
    {
        return await _dbSet.OrderByDescending(propertyOrder).Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }

    public async Task<IList<TModel>> GetAllOrderByDescending<TPropertyOrder>(Expression<Func<TModel, TPropertyOrder>> propertyOrder, int page, int recordsPerPage)
    {
        return await _dbSet.OrderByDescending(propertyOrder).Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
    }


    public async Task<TModel> Detach(TModel entity)
    {
        var entry = _dbContext.Entry(entity);
        entry.State = EntityState.Detached;
        return entity;
    }
}