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
        .Where(qg => qg.Tags
          .Select(t => t.Name)
          .Any(t => tags.Contains(t))
        )
        .ToListAsync();
    }
  }
}