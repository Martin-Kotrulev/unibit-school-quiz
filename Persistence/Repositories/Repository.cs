using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
      this.Context = context;
    }

    public async void AddAsync(TEntity entity)
    {
      await this.Context.Set<TEntity>().AddAsync(entity);
    }

    public void Add(TEntity entity)
    {
      this.Context.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
      this.Context.Set<TEntity>().AddRange();
    }

    public async void AddRangeAsync(IEnumerable<TEntity> entities)
    {
      await this.Context.Set<TEntity>().AddRangeAsync();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
      return this.Context.Set<TEntity>().Where(predicate);
    }

    public async Task<IEnumerable<TEntity>> PagedAsync(int page = 1, int pageSize = 10,
      Expression<Func<TEntity, bool>> predicate = null)
    {
      var query = this.Context.Set<TEntity>().AsQueryable();

      if (predicate != null)
      {
        query = query.Where(predicate);
      }
      
      query
        .Skip((page - 1) * pageSize)
        .Take(pageSize);

      return await query.ToListAsync();
    }

    public IEnumerable<TEntity> Paged(int page = 1, int pageSize = 10,
      Expression<Func<TEntity, bool>> predicate = null)
    {
      var query = this.Context.Set<TEntity>().AsQueryable();

      if (predicate != null)
      {
        query = query.Where(predicate);
      }
      
      query
        .Skip((page - 1) * pageSize)
        .Take(pageSize);

      return query.ToList();
    }

    public async Task<TEntity> GetAsync(int id)
    {
      return await this.Context.Set<TEntity>().FindAsync(id);
    }

    public TEntity Get(int id)
    {
      return this.Context.Set<TEntity>().Find(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
      return await this.Context.Set<TEntity>().ToListAsync();
    }

    public IEnumerable<TEntity> GetAll()
    {
      return this.Context.Set<TEntity>().ToList();
    }

    public void Remove(TEntity entity)
    {
      this.Context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
      this.Context.Set<TEntity>().RemoveRange(entities);
    }
  }
}