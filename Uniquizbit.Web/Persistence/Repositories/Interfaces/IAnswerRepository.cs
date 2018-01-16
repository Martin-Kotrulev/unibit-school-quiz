using System.Collections.Generic;
using System.Threading.Tasks;
using Uniquizbit.Models;

namespace Uniquizbit.Persistence.Repositories.Interfaces
{
  public interface IAnswerRepository : IRepository<Answer>
  {
    Task<IEnumerable<int>> GetRandomOrderForAnswerIdsAsync(int quizId);
    Task AddAnswerAsync(Answer answer);
    Task<bool> DeleteAnswerAsync(int answerId);
  }
}