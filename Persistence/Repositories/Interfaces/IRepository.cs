using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
    TEntity Get(int id);

    IEnumerable<TEntity> GetAll();

    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    IEnumerable<TEntity> Paged(int page = 1, int pageSize = 10,
      Expression<Func<TEntity, bool>> predicate = null);

    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);
  }
}