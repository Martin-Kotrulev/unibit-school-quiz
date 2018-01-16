using System.Collections.Generic;
using System.Threading.Tasks;
using Uniquizbit.Models;

namespace Uniquizbit.Persistence.Repositories.Interfaces
{
  public interface IQuestionRepository : IRepository<Question>
  {
    Task<IEnumerable<Question>> GetQuestionsWithProgressAsync(int quizId,
      IEnumerable<int> progressAnswersIds);

    Task<IEnumerable<Question>> GetQuestionsForQuizAsync(int quizId);

    Task<IEnumerable<Question>> GetUserQuizQuestionsAsync(int quizId);

    Task<bool> UserOwnQuestionAsync(int questionId, string userId);
  }
}