using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class AnswerRepository : Repository<Answer>, IAnswerRepository
  {
    public AppDbContext AppDbContext { get { return Context as AppDbContext; } }
    public AnswerRepository(AppDbContext context) 
      : base(context)
    {
    }

    public async Task<IEnumerable<int>> GetRandomOrderForAnswerIdsAsync(int quizId)
    {
      var rnd = new Random();

      return await AppDbContext.Answers
        .Where(a => a.QuestionId == quizId)
        .Select(a => a.Id)
        .OrderBy(a => rnd.Next())
        .ToListAsync();
    }

    public void AddAnswer(Answer answer)
    {
      var question = AppDbContext.Questions
        .FirstOrDefault(q => q.Id == answer.QuestionId);

      var entry = AppDbContext.Entry(question);
      entry.Collection(e => e.Answers)
        .Query()
        .OrderBy(a => a.Letter)
        .Load();

      char letter = 'a';
      question.Answers = question.Answers
        .Select(a => {a.Letter = letter++; return a;})
        .ToList();

      answer.QuizId = question.QuizId;
      answer.Letter = letter;

      question.Answers.Add(answer);
    }

    public void DeleteAnswer(int answerId)
    {
      throw new NotImplementedException();
    }
  }
}