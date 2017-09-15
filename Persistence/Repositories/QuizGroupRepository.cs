using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class QuizGroupRepository : Repository<QuizGroup>, IQuizGroupRepository
  {
    public AppDbContext AppDbContext { get { return Context as AppDbContext; } }

    public QuizGroupRepository(AppDbContext context)
      : base(context)
    {

    }

    public async Task<IEnumerable<QuizGroup>> SearchQuizGroupByTagsAsync(ICollection<string> tags, int page = 1, int pageSize = 10)
    {
      return await ApplyPaging(qg =>
        qg.Tags.Select(t => t.Tag.Name).Any(t => tags.Contains(t)),
        page, pageSize);
    }

    public async Task<IEnumerable<QuizGroup>> GetUserGroupsPagedAsync(string userId, int page = 1, int pageSize = 10)
    {
      return await ApplyPaging(qg =>
        qg.CreatorId == userId,
        page, pageSize);
    }

    public async Task<IEnumerable<QuizGroup>> GetGroupsPagedBySearchAsync(int page = 1, int pageSize = 10, string search = "")
    {
      return await ApplyPaging(qg =>
        qg.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()),
        page, pageSize);
    }

    private async Task<IEnumerable<QuizGroup>> ApplyPaging(Expression<Func<QuizGroup, bool>> predicate, int page, int pageSize)
    {
      return await AppDbContext.QuizGroups
        .Include(qg => qg.Tags)
          .ThenInclude(t => t.Tag)
        .Where(predicate)
        .OrderByDescending(qg => qg.CreatedOn)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }
  }
}