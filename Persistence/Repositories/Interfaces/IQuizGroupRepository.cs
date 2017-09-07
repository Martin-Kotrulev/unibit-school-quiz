using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IQuizGroupRepository : IRepository<QuizGroup>
  {
    Task<IEnumerable<QuizGroup>> SearchQuizGroupByTagsAsync(ICollection<string> tags);
  }
}