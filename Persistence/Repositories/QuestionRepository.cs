using System;
using System.Collections.Generic;
using System.Linq;
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

    public Question GetQuestionWithProgress(int questionId, QuizProgress progress)
    {
      var question = AppDbContext.Questions
        .Include(q => q.Answers)
        .FirstOrDefault(q => q.Id == questionId);

      question.Answers = question.Answers
        .Select(a => {
          if (progress.GivenAnswers.Contains(a))
            a.IsSelected = true; 
          return a;
        })
        .ToList();

      return question;
    }

    public IEnumerable<Question> GetQuestionsForQuiz(int quizId)
    {
      var rnd = new Random();

      var questions = AppDbContext.Questions
        .Where(q => q.QuizId == quizId)
        .ToList();

      // Random answers order
      foreach (var q in questions) 
      {
        q.Answers = q.Answers
          .OrderBy(a => rnd.Next())
          .ToList();
      }

      return questions;
    }

    public Question GetQuestionWithAnswers(int questionId)
    {
      var rnd = new Random();
      var question = AppDbContext.Questions
        .Include(q => q.Answers)
        .FirstOrDefault(q => q.Id == questionId);

      question.Answers = question.Answers
        .OrderBy(x => rnd.Next())
        .ToList();

      return question;
    }
  }
}