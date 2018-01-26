namespace Uniquizbit.Services
{
  using Data.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  public interface IQuestionService : IService
  {
    Task<Question> AddQuestionAsync(Question question);

    Task<IEnumerable<Question>> GetQuestionsForQuizAsync(int quizId, string userId);

    Task<bool> DeleteQuestionAsync(int questionId, string userId);

    Task<Question> FindQuestionByIdAsync(int questionId);
  }
}