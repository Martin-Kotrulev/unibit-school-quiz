using System.Collections.Generic;
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

    public async Task<IEnumerable<int>> FindUserQuizProgressAnswersIds(
      int quizId, string userId)
    {
      return await AppDbContext.QuizProgresses
        .Include(qp => qp.GivenAnswers)
          .ThenInclude(ga => ga.Answer)
        .Where(qp => 
          qp.QuizId == quizId && qp.UserId == userId)
        .SelectMany(qp => qp.GivenAnswers)
        .Select(ga => ga.Answer.Id)
        .ToListAsync();
    }

    public async Task<int> GetProgressAnswersWeightSumAsync(string userId, int quizId)
    {
      return await AppDbContext.QuizProgresses
        .Where(qp => qp.UserId == userId && qp.QuizId == quizId)
        .Include(qp => qp.GivenAnswers)
          .ThenInclude(ga => ga.Answer)
        .SelectMany(qp => qp.GivenAnswers
          .Where(ga => ga.Answer.IsRight))
        .SumAsync(ga => ga.Answer.Weight);
    }

    public async Task AddProgressAsync(QuizProgress progress, IEnumerable<int> answersIds)
    {
      var existingProgress = await AppDbContext.QuizProgresses
        .Include(qp => qp.GivenAnswers)
        .FirstOrDefaultAsync(qp => qp.QuizId == progress.QuizId
          && qp.QuestionId == progress.QuestionId
          && qp.UserId == progress.UserId);

      var answers = await AppDbContext.Answers
          .Where(a => answersIds.Contains(a.Id))
          .ToListAsync();

      if (existingProgress != null)
      {
        foreach (var a in answers)
        {
          existingProgress.GivenAnswers.Add(
            new ProgressesAnswers()
            {
              Answer = a,
              Progress = existingProgress
            }
          );
        }
      }
      else
      {
        foreach (var a in answers)
        {
          progress.GivenAnswers.Add(
            new ProgressesAnswers()
            {
              Answer = a,
              Progress = existingProgress
            }
          );
        }
      }
    }
  }
}