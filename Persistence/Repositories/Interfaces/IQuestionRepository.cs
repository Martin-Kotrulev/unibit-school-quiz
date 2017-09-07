using System.Collections.Generic;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IQuestionRepository : IRepository<Question>
  {
    Question GetQuestionWithProgress(int questionId, QuizProgress progress);
    IEnumerable<Question> GetQuestionsForQuiz(int quizId);
    Question GetQuestionWithAnswers(int questionId);
  }
}