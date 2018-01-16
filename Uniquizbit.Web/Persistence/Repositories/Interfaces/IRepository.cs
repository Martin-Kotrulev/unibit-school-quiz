using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Uniquizbit.Persistence.Repositories.Interfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
    TEntity Get(int id);

    Task<TEntity> GetAsync(int id);

    IEnumerable<TEntity> GetAll();

    Task<IEnumerable<TEntity>> GetAllAsync();

    TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    IEnumerable<TEntity> Paged(int page, int pageSize,
      Expression<Func<TEntity, bool>> predicate = null,
      params string[] includes);

    Task<IEnumerable<TEntity>> PagedAsync(int page, int pageSize,
      Expression<Func<TEntity, bool>> predicate = null,
      params string[] includes);

    void Add(TEntity entity);

    void AddAsync(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);
  }
}