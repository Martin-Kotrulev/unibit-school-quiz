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

    public void Add(TEntity entity)
    {
      this.Context.Set<TEntity>().Add(entity);
    }

    public async void AddAsync(TEntity entity)
    {
      await this.Context.Set<TEntity>().AddAsync(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
      this.Context.Set<TEntity>().AddRange();
    }

    public async void AddRangeAsync(IEnumerable<TEntity> entities)
    {
      await this.Context.Set<TEntity>().AddRangeAsync();
    }

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
      return this.Context.Set<TEntity>()
        .FirstOrDefault(predicate);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
      return await this.Context.Set<TEntity>()
        .FirstOrDefaultAsync(predicate);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
      return this.Context.Set<TEntity>().Where(predicate);
    }

    public IEnumerable<TEntity> Paged(int page = 1, int pageSize = 10,
      Expression<Func<TEntity, bool>> predicate = null,
      params string[] includes)
    {
      var q = this.Context.Set<TEntity>()
        .Where(predicate ?? (p => true));

        foreach (var i in includes)
          q = q.Include(i);

        return q.Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToList();
    }

    public async Task<IEnumerable<TEntity>> PagedAsync(int page = 1, int pageSize = 10,
      Expression<Func<TEntity, bool>> predicate = null,
      params string[] includes)
    {
      var q = this.Context.Set<TEntity>()
        .Where(predicate ?? (p => true));

        foreach (var i in includes)
          q = q.Include(i);

        return await q.Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();
    }

    public TEntity Get(int id)
    {
      return this.Context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity> GetAsync(int id)
    {
      return await this.Context.Set<TEntity>().FindAsync(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
      return this.Context.Set<TEntity>().ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
      return await this.Context.Set<TEntity>().ToListAsync();
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