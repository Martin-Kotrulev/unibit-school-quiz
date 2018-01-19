namespace Uniquizbit.Persistence.Repositories
{
  internal class AnswerRepository : Repository<Answer>, IAnswerRepository
  {
    public UniquizbitDbContext UniquizbitDbContext { get { return Context as UniquizbitDbContext; } }
    public AnswerRepository(UniquizbitDbContext context) 
      : base(context)
    {
    }

    public async Task<IEnumerable<int>> GetRandomOrderForAnswerIdsAsync(int quizId)
    {
      var rnd = new Random();

      return await UniquizbitDbContext.Answers
        .Where(a => a.QuestionId == quizId)
        .Select(a => a.Id)
        .OrderBy(a => rnd.Next())
        .ToListAsync();
    }

    public async Task AddAnswerAsync(Answer answer)
    {
      var question = await UniquizbitDbContext.Questions
        .FirstOrDefaultAsync(q => q.Id == answer.QuestionId);

      var entry = UniquizbitDbContext.Entry(question);
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
      var answer = await UniquizbitDbContext.Answers
        .FirstOrDefaultAsync(a => a.Id == answerId);

      if (answer != null)
      {
        UniquizbitDbContext.Answers.Remove(answer);
        UniquizbitDbContext.SaveChanges();
        
        var question = await UniquizbitDbContext.Questions
          .FirstOrDefaultAsync(q => q.Id == answer.QuestionId);

        var entry = UniquizbitDbContext.Entry(question);
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