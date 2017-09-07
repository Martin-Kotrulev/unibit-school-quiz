using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
    TEntity Get(int id);

    Task<TEntity> GetAsync(int id);

    IEnumerable<TEntity> GetAll();

    Task<IEnumerable<TEntity>> GetAllAsync();

    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    IEnumerable<TEntity> Paged(int page = 1, int pageSize = 10,
      Expression<Func<TEntity, bool>> predicate = null);

    Task<IEnumerable<TEntity>> PagedAsync(int page = 1, int pageSize = 10,
      Expression<Func<TEntity, bool>> predicate = null);

    void Add(TEntity entity);

    void AddAsync(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);
  }
}