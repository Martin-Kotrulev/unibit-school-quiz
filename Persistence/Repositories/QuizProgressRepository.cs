using System.Linq;
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

    public QuizProgress FindQuizProgress(int quizId, int questionId, string userId)
    {
      return AppDbContext.QuizProgresses
        .Include(qp => qp.GivenAnswers)
        .FirstOrDefault(qp => qp.QuizId == quizId 
          && qp.QuestionId == questionId
          && qp.UserId == userId);
    }
  }
}