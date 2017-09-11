using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class QuestionRepository : Repository<Question>, IQuestionRepository
  {
    public AppDbContext AppDbContext { get { return Context as AppDbContext; } }
    public QuestionRepository(AppDbContext context) 
      : base(context)
    {
    }

    public async Task<IEnumerable<Question>> GetQuestionsWithProgressAsync(int quizId,
      IEnumerable<int> progressAnswersIds)
    {
      var questions = await AppDbContext.Questions
        .Include(q => q.Answers)
        .Where(q => q.QuizId == quizId)
        .ToListAsync();

      foreach (var q in questions)
      {
        q.Answers = q.Answers
        .Select(a => {
          if (progressAnswersIds.Contains(a.Id))
            a.IsSelected = true; 
          return a;
        })
        .ToList();
      }

      return questions;
    }

    public async Task<IEnumerable<Question>> GetQuestionsForQuizAsync(int quizId)
    {
      var rnd = new Random();

      var questions = await AppDbContext.Questions
        .Where(q => q.QuizId == quizId)
        .ToListAsync();

      // Random answers order
      foreach (var q in questions) 
      {
        q.Answers = q.Answers
          .OrderBy(a => rnd.Next())
          .ToList();
      }

      return questions;
    }

    public async Task<IEnumerable<Question>> GetUserQuizQuestionsAsync(int quizId)
    {
      var questions = await AppDbContext.Questions
        .Include(q => q.Answers)
        .Where(q => q.QuizId == quizId)
        .OrderBy(q => q.Id)
        .ToListAsync();

      foreach (var q in questions) 
      {
        q.Answers = q.Answers
          .OrderBy(a => a.Id)
          .ToList();
      }

      return questions;
    }

    public async Task<bool> UserOwnQuestionAsync(int questionId, string userId)
    {
      return await AppDbContext.Questions
        .Include(q => q.Quiz)
        .FirstOrDefaultAsync(q => q.Id == questionId && q.Quiz.CreatorId == userId) != null;
    }
  }
}