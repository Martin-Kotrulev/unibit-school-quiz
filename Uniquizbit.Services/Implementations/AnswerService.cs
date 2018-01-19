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

    public async Task<Answer> CreateAnswerAsync(Answer answer)
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

    public async Task<bool> DeleteAnswerAsync(int answerId)
    {
      var answer = await _dbContext.Answers
        .FirstOrDefaultAsync(a => a.Id == answerId);

      if (answer != null)
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
			=>  await _dbContext.Answers
						.Where(a => answersIds.Contains(a.Id))
						.ToListAsync();

    public async Task<IEnumerable<int>> GetRandomOrderForAnswersIdsAsync(int quizId)
    {
      var rnd = new Random();

      return await _dbContext.Answers
        .Where(a => a.QuestionId == quizId)
        .Select(a => a.Id)
        .OrderBy(a => rnd.Next())
        .ToListAsync();
    }
  }
}