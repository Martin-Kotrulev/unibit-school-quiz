namespace Uniquizbit.Services.Implementations
{
  using Data.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Uniquizbit.Data;
  using Microsoft.EntityFrameworkCore;
  using System.Linq;
  using System;

  public class AnswerService : IAnswerService
  {
    private readonly UniquizbitDbContext _dbContext;

    AnswerService(UniquizbitDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Answer> AddAnswerAsync(Answer answer)
    {
      var question = await _dbContext.Questions
        .FirstOrDefaultAsync(q => q.Id == answer.QuestionId);

      var entry = _dbContext.Entry(question);
      await entry.Collection(e => e.Answers)
        .Query()
        .OrderBy(a => a.Letter)
        .LoadAsync();

      char letter = 'a';
      question.Answers = question.Answers
        .Select(a => { a.Letter = (letter++).ToString(); return a; })
        .ToList();

      answer.QuizId = question.QuizId;
      answer.Letter = letter.ToString();

      question.Answers.Add(answer);

      return answer;
    }

    public async Task<bool> DeleteAnswerAsync(int answerId, string userId)
    {
      var answer = await _dbContext.Answers.FindAsync(answerId);

      if (answer != null && answer.CreatorId == userId)
      {
        _dbContext.Answers.Remove(answer);
        _dbContext.SaveChanges();

        var question = await _dbContext.Questions
          .FirstOrDefaultAsync(q => q.Id == answer.QuestionId);

        var entry = _dbContext.Entry(question);
        await entry.Collection(e => e.Answers)
          .Query()
          .OrderBy(a => a.Letter)
          .LoadAsync();

        char letter = 'a';
        question.Answers = question.Answers
          .Select(a => { a.Letter = (letter++).ToString(); return a; })
          .ToList();

        return true;
      }
      else
        return false;
    }

    public async Task<IEnumerable<Answer>> FindAnswersByIds(ICollection<int> answersIds)
      => await _dbContext.Answers
            .Where(a => answersIds.Contains(a.Id))
            .ToListAsync();
  }
}