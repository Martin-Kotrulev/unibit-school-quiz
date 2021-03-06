namespace Uniquizbit.Services.Implementations
{
  using Data;
  using Data.Models;
  using Common.Config;
  using Common.Enums;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.Extensions.Options;
  using Microsoft.EntityFrameworkCore;
  using System.Linq.Expressions;

  public class QuizService : IQuizService
  {
    private readonly GradesSettings _gradesSettings;

    private readonly UniquizbitDbContext _dbContext;

    private readonly IScoreCalculator _scoreCalculator;

    public QuizService(UniquizbitDbContext dbContext,
      IOptions<GradesSettings> optionsAccessor,
      IScoreCalculator scoreCalculator)
    {
      _scoreCalculator = scoreCalculator;
      _dbContext = dbContext;
      _gradesSettings = optionsAccessor.Value;
    }

    public async Task<Quiz> AddQuizAsync(Quiz quiz)
    {
      await _dbContext.Quizzes.AddAsync(quiz);
      await _dbContext.SaveChangesAsync();

      return quiz;
    }

    public async Task<bool> MarkQuizAsTakenAsync(int quizId, string userId)
    {
      var quizUser = await _dbContext.QuizzesUsers
        .FirstOrDefaultAsync(qu => qu.UserId == userId && qu.QuizId == quizId);

      if (quizUser != null)
      {
        quizUser.Finished = true;
        return quizUser.Finished;
      }
      else
      {
        var newQuizUser = new QuizzesUsers()
        {
          QuizId = quizId,
          UserId = userId,
          Finished = false
        };

        await _dbContext.QuizzesUsers.AddAsync(newQuizUser);
        await _dbContext.SaveChangesAsync();

        return newQuizUser.Finished;
      }
    }

    public async Task<Score> ScoreUserAsync(string userId, int quizId)
    {
      var quizMaxScore = await _dbContext.Answers
        .Where(a => a.QuizId == quizId && a.IsRight)
        .SumAsync(a => a.Weight);

      var userScore = await _dbContext.QuizProgresses
        .Where(qp => qp.UserId == userId && qp.QuizId == quizId)
        .Include(qp => qp.GivenAnswers)
          .ThenInclude(pa => pa.Answer)
        .SelectMany(qp => qp.GivenAnswers
          .Where(ga => ga.Answer.IsRight && ga.IsChecked))
        .SumAsync(ga => ga.Answer.Weight);

      var score = new Score()
      {
        Value = _scoreCalculator.GetScore(quizMaxScore, userScore),
        ScoredAt = DateTime.Now,
        QuizId = quizId,
        UserId = userId
      };

      var existingScore = await _dbContext.Scores
        .FirstOrDefaultAsync(s => s.QuizId == quizId && s.UserId == userId);

      if (existingScore != null) {
        existingScore.Value = score.Value;
        existingScore.ScoredAt = score.ScoredAt;
      } else {
        await _dbContext.Scores.AddAsync(score);
      }

      await _dbContext.SaveChangesAsync();

      return score;
    }

    public async Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password)
    {
      return await _dbContext.Quizzes
        .FirstOrDefaultAsync(q => q.Id == quizId && q.Password == password);
    }

    public async Task<IEnumerable<Quiz>> GetUserOwnQuizzesAsync(
      string userId, int page = 1, int pageSize = 10)
      => await ApplyPaging(q =>
          q.CreatorId == userId,
          page,
          pageSize);

    public async Task<IEnumerable<Quiz>> GetUserTakenQuizzesAsync(
      string userId, int page = 1, int pageSize = 10)
      => await _dbContext.QuizzesUsers
          .Include(qu => qu.Quiz)
          .Where(qu => qu.UserId == userId)
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .Select(qu => qu.Quiz)
          .ToListAsync();

    public async Task<IEnumerable<Quiz>> GetQuizzesAsync(
      int page = 1, int pageSize = 10, string search = "")
      => await ApplyPaging(q =>
          q.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()),
          page,
          pageSize);

    public async Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10)
      => await ApplyPaging(q =>
          q.Tags.Select(t => t.Tag.Name).Any(n => tags.Contains(n)),
          page,
          pageSize);

    public async Task<QuizEnum> EnterQuizAsync(int quizId, string userId)
    {
      var quiz = await _dbContext.Quizzes
        .Include(q => q.Participants)
        .FirstOrDefaultAsync(q => q.Id == quizId);

      if (quiz == null)
        return QuizEnum.NotExistent;

      var participant = quiz.Participants
        .FirstOrDefault(p => p.UserId == userId);
      var inQuiz = participant != null;

      if (quiz.Starts != null && DateTime.UtcNow < quiz.Starts)
        return QuizEnum.NotStarted;
      else if (quiz.Ends != null && DateTime.UtcNow > quiz.Ends)
        return QuizEnum.Ended;
      else if (quiz.Once && participant.Finished)
        return QuizEnum.Taken;
      else if (inQuiz && participant.Finished)
        return QuizEnum.Finished;
      else if (inQuiz)
        return QuizEnum.StillTaking;

      quiz.Participants.Add(new QuizzesUsers()
      {
        Quiz = quiz,
        UserId = userId,
        Finished = false
      });
      
      await _dbContext.SaveChangesAsync();

      return QuizEnum.Enter;
    }

    public async Task<QuizEnum> FinishQuizAsync(int quizId, string userId)
    {
      var quiz = await _dbContext.Quizzes
        .Include(q => q.Participants)
        .FirstOrDefaultAsync(q => q.Id == quizId);

      if (quiz == null)
        return QuizEnum.NotExistent;

      var participant = quiz.Participants
        .FirstOrDefault(p => p.UserId == userId);

      if (participant == null)
        return QuizEnum.NotInQuiz;

      if (!participant.Finished)
      {
        participant.Finished = true;
        await _dbContext.SaveChangesAsync();
      }

      return QuizEnum.Taken;
    }

    public async Task<bool> QuizExistsAsync(Quiz quiz)
    {
      return await _dbContext.Quizzes
        .FirstOrDefaultAsync(q => q.Name == quiz.Name) != null;
    }

    public async Task<bool> DeleteQuizAsync(int quizId, string userId)
    {
      var quiz = await _dbContext.Quizzes.FindAsync(quizId);

      if (quiz == null || quiz.CreatorId != userId)
        return false;

      _dbContext.Quizzes.Remove(quiz);
      await _dbContext.SaveChangesAsync();

      return true;
    }

    public async Task<bool> UserCanAddQuestionToQuizAsync(int quizId, string userId)
    {
      var quiz = await _dbContext.Quizzes.FindAsync(quizId);
      return quiz != null && quiz.CreatorId == userId;
    }

    public async Task<bool> UserCanAddQuizzes(int quizId, string userId)
    {
      var group = await _dbContext.QuizGroups.FindAsync(quizId);
      return group != null && group.CreatorId == userId;
    }

    public async Task<Quiz> FindQuizByIdAsync(int quizId)
      => await _dbContext.Quizzes
          .FirstOrDefaultAsync(q => q.Id == quizId);

    private async Task<IEnumerable<Quiz>> ApplyPaging(
      Expression<Func<Quiz, bool>> predicate, int page, int pageSize)
    {
      return await _dbContext.Quizzes
        .Include(qg => qg.Tags)
          .ThenInclude(t => t.Tag)
        .Where(predicate)
        .OrderByDescending(qg => qg.CreatedOn)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }

    public void Subscribe(QuizSubscription subscription)
    {
      throw new System.NotImplementedException();
    }

    public async Task<QuizProgress> AddProgressToQuizAsync(
      int quizId, string userId, ProgressAnswer answer)
    {
      var progress = await _dbContext.QuizProgresses
        .Include(qp => qp.GivenAnswers)
        .FirstOrDefaultAsync(qp => qp.UserId == userId && qp.QuizId == quizId);

      if (progress != null)
      {
        var progressAnswer = progress.GivenAnswers
          .FirstOrDefault(ga => ga.AnswerId == answer.AnswerId);

        if (progressAnswer != null)
        {
          progressAnswer.IsChecked = answer.IsChecked;
        }
        else
        {
          var question = await _dbContext.Questions
            .FindAsync(answer.QuestionId);

          if (!question.IsMultiselect)
          {
            var questionAnswersIds = await _dbContext.Entry(question)
              .Collection(q => q.Answers)
              .Query()
              .Select(a => a.Id)
              .ToListAsync();

            progress.GivenAnswers = progress.GivenAnswers
              .Where(ga => !questionAnswersIds.Contains(ga.AnswerId))
              .ToList();
          }

          progress.GivenAnswers.Add(answer);
        }

        await _dbContext.SaveChangesAsync();
      }

      return progress;
    }
  }
}