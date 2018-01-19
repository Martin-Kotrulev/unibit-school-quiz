namespace Uniquizbit.Services.Implementations
{
  using Data.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Data;

  public class QuestionService : IQuestionService
  {
    private readonly UniquizbitDbContext _dbContext;

    public QuestionService(UniquizbitDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void CreateQuestion(Question question)
    {
      throw new System.NotImplementedException();
    }

    public bool DeleteQuestion(int questionId)
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