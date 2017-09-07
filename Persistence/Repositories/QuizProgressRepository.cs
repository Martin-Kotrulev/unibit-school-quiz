using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class QuizProgressRepository : Repository<QuizProgress>, IQuizProgressRepository
  {
    public AppDbContext AppDbContext { get {return Context as AppDbContext;} }

    public QuizProgressRepository(AppDbContext context)
      : base(context)
    {
        
    }

    public async Task<QuizProgress> FindQuizProgressAsync(int quizId, int questionId, string userId)
    {
      return await AppDbContext.QuizProgresses
        .Include(qp => qp.GivenAnswers)
        .FirstOrDefaultAsync(qp => qp.QuizId == quizId 
          && qp.QuestionId == questionId
          && qp.UserId == userId);
    }

    public async Task<int> GetProgressAnswersWeightSumAsync(string userId, int quizId)
    {
      return await AppDbContext.QuizProgresses
        .Where(qp => qp.UserId == userId && qp.QuizId == quizId)
        .Include(qp => qp.GivenAnswers)
        .SelectMany(qp => qp.GivenAnswers)
        .Where(ga => ga.IsRight)
        .SumAsync(ga => ga.Weight);
    }
  }
}