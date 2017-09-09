using System.Collections.Generic;
using System.Linq;
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

    public async Task<IEnumerable<QuizGroup>> SearchQuizGroupByTagsAsync(ICollection<string> tags)
    {
      return await AppDbContext.QuizGroups
        .Include(qg => qg.Tags)
        .Where(qg => qg.Tags
          .Select(t => t.Tag.Name)
          .Any(t => tags.Contains(t))
        )
        .ToListAsync();
    }

    public async Task<IEnumerable<QuizGroup>> GetUserGroupsPagedAsync(string userId, int page = 1, int pageSize = 10)
    {
      return await AppDbContext.QuizGroups
        .Include(qg => qg.Tags)
          .ThenInclude(t => t.Tag)
        .Where(qg => qg.OwnerId == userId)
        .OrderByDescending(qg => qg.CreatedOn)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }
  }
}