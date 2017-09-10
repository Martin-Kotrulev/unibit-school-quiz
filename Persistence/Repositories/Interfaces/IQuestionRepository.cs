using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IQuestionRepository : IRepository<Question>
  {
    Task<Question> GetQuestionWithProgressAsync(int questionId, QuizProgress progress);

    Task<IEnumerable<Question>> GetQuestionsForQuizAsync(int quizId);
    
    Task<Question> GetQuestionWithAnswersAsync(int questionId);
  }
}