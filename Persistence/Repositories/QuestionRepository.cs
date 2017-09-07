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

    public async Task<Question> GetQuestionWithProgressAsync(int questionId, QuizProgress progress)
    {
      var question = await AppDbContext.Questions
        .Include(q => q.Answers)
        .FirstOrDefaultAsync(q => q.Id == questionId);

      question.Answers = question.Answers
        .Select(a => {
          if (progress.GivenAnswers.Contains(a))
            a.IsSelected = true; 
          return a;
        })
        .ToList();

      return question;
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

    public async Task<Question> GetQuestionWithAnswersAsync(int questionId)
    {
      var rnd = new Random();
      var question = await AppDbContext.Questions
        .Include(q => q.Answers)
        .FirstOrDefaultAsync(q => q.Id == questionId);

      question.Answers = question.Answers
        .OrderBy(x => rnd.Next())
        .ToList();

      return question;
    }
  }
}