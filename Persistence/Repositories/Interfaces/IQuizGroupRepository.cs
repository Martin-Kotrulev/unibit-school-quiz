using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IQuizGroupRepository : IRepository<QuizGroup>
  {
    Task<IEnumerable<QuizGroup>> SearchQuizGroupByTagsAsync(ICollection<string> tags);
    Task<IEnumerable<QuizGroup>> GetUserGroupsPagedAsync(string userId, int page = 1, int pageSize = 10);
  }
}