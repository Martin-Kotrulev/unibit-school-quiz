namespace Uniquizbit.Services
{
  using Data.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using System.Collections;

  public interface IQuestionService : IService
  {
    Task<Question> AddQuestionAsync(Question question);

    Task<ICollection<Question>> UpdateQuestionsForQuiz(int quizId, string userId, ICollection<Question> questions);

    Task<IEnumerable<Question>> GetQuestionsForQuizAsync(int quizId, string userId);

    Task<bool> DeleteQuestionAsync(int questionId, string userId);

    Task<Question> FindQuestionByIdAsync(int questionId);

    Task<bool> QuestionHasAnswerWithId(int questionId, int answerId);
  }
}