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

    public async Task AddAnswerAsync(Answer answer)
    {
      var question = await AppDbContext.Questions
        .FirstOrDefaultAsync(q => q.Id == answer.QuestionId);

      var entry = AppDbContext.Entry(question);
      await entry.Collection(e => e.Answers)
        .Query()
        .OrderBy(a => a.Letter)
        .LoadAsync();

      char letter = 'a';
      question.Answers = question.Answers
        .Select(a => {a.Letter = (letter++).ToString(); return a;})
        .ToList();

      answer.QuizId = question.QuizId;
      answer.Letter = letter.ToString();

      question.Answers.Add(answer);
    }

    public async Task<bool> DeleteAnswerAsync(int answerId)
    {
      var answer = await AppDbContext.Answers
        .FirstOrDefaultAsync(a => a.Id == answerId);

      if (answer != null)
      {
        AppDbContext.Answers.Remove(answer);
        AppDbContext.SaveChanges();
        
        var question = await AppDbContext.Questions
          .FirstOrDefaultAsync(q => q.Id == answer.QuestionId);

        var entry = AppDbContext.Entry(question);
        await entry.Collection(e => e.Answers)
          .Query()
          .OrderBy(a => a.Letter)
          .LoadAsync();

        char letter = 'a';
        question.Answers = question.Answers
          .Select(a => {a.Letter = (letter++).ToString(); return a;})
          .ToList();

        return true;
      }
      else
        return false;
    }
  }
}