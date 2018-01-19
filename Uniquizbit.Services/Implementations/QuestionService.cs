using System.Collections.Generic;
using System.Threading.Tasks;
using Uniquizbit.Data.Models;

namespace Uniquizbit.Services.Implementations
{
  public class QuestionService : IQuestionService
  {
    public Task CreateAnswerAsync(Answer answer)
    {
      throw new System.NotImplementedException();
    }

    public void CreateQuestion(Question question)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> DeleteAnswerAsync(int answerId)
    {
      throw new System.NotImplementedException();
    }

    public bool DeleteQuestion(int questionId)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Answer> GetAnswersForIds(ICollection<int> ids)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Question>> GetQuestionsAsync(int quizId, string userId)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<int>> GetRandomQuestionsOrderAsync(int quizId)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> UserOwnQuestionAsync(int questionId, string userId)
    {
      throw new System.NotImplementedException();
    }
  }
}