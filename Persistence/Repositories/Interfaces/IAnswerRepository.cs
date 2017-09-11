using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IAnswerRepository : IRepository<Answer>
  {
    Task<IEnumerable<int>> GetRandomOrderForAnswerIdsAsync(int quizId);
    Task AddAnswerAsync(Answer answer);
    Task<bool> DeleteAnswerAsync(int answerId);
  }
}