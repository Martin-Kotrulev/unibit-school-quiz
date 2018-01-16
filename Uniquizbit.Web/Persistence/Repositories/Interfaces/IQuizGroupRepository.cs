using System.Collections.Generic;
using System.Threading.Tasks;
using Uniquizbit.Models;

namespace Uniquizbit.Persistence.Repositories.Interfaces
{
  public interface IQuizGroupRepository : IRepository<QuizGroup>
  {
    Task<IEnumerable<QuizGroup>> SearchQuizGroupByTagsAsync(ICollection<string> tags, int page = 1, int pageSize = 10);

    Task<IEnumerable<QuizGroup>> GetUserGroupsPagedAsync(string userId, int page = 1, int pageSize = 10);
    
    Task<IEnumerable<QuizGroup>> GetGroupsPagedBySearchAsync(int page, int pageSize, string search);
  }
}