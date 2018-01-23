namespace Uniquizbit.Services.Implementations
{
  using Data.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Data;
  using System.Linq;
  using Microsoft.EntityFrameworkCore;
  using System;
  using System.Text;

  public class QuestionService : IQuestionService
  {
    private readonly UniquizbitDbContext _dbContext;

    public QuestionService(UniquizbitDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Question> AddQuestionAsync(Question question)
    {
      await _dbContext.Questions.AddAsync(question);
      _dbContext.SaveChanges();
      return question;
    }

    public async Task<bool> DeleteQuestionAsync(int questionId, string userId)
    {
      var question = await _dbContext.Questions.FindAsync(questionId);

      if (question == null)
        return false;

      _dbContext.Questions.Remove(question);
      _dbContext.SaveChanges();
      return true;
    }

    public async Task<IEnumerable<Question>> GetQuestionsForQuizAsync(int quizId, string userId)
    {
      var isCreator = await _dbContext.Quizzes
        .FirstOrDefaultAsync(q => q.Id == quizId && q.CreatorId == userId) != null;

      if (isCreator)
      {
        return await GetQuestionsWithAnswersForQuizAsync(quizId);
      }
      else
      {
        if (await UserHasProgressOnQuizAsync(quizId, userId))
        {
          return await GetQuestionsFromProgressAsync(quizId, userId);
        }
        else
        {
          return await GetQuestionsWithNewProgressAsync(quizId, userId);
        }
      }
    }

    private async Task<IEnumerable<Question>> GetQuestionsWithAnswersForQuizAsync(int quizId)
    {
      var questions = await _dbContext.Questions
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

    private async Task<bool> UserHasProgressOnQuizAsync(int quizId, string userId)
    {
      return await _dbContext.QuizProgresses
        .Where(qp => qp.QuizId == quizId && qp.UserId == userId)
        .FirstOrDefaultAsync() != null;
    }

    private async Task<IEnumerable<Question>> GetQuestionsFromProgressAsync(int quizId, string userId)
    {
      var progress = await _dbContext.QuizProgresses
        .Where(qp => qp.QuizId == quizId && qp.UserId == userId)
        .FirstOrDefaultAsync();

      var questionOrder = GetListFromOrderString(progress.QuestionsOrder);
      var answersOrder = GetListFromOrderString(progress.AnswersOrder);

      var progressAnswersIds = await _dbContext.QuizProgresses
        .Include(qp => qp.GivenAnswers)
        .Where(qp => qp.QuizId == quizId && qp.UserId == userId)
        .SelectMany(qp => qp.GivenAnswers)
        .Select(ga => ga.AnswerId)
        .ToListAsync();

      var questions = await _dbContext.Questions
        .Where(q => q.QuizId == quizId)
        .Include(q => q.Answers)
        .ToListAsync();

      questions = questions
        .OrderBy(q => questionOrder.IndexOf(q.Id))
        .ToList();

      foreach (var q in questions)
      {
        q.Answers = q.Answers
          .Select(a =>
          {
            if (progressAnswersIds.Contains(a.Id))
              a.IsSelected = true;
            return a;
          })
          .OrderBy(a => answersOrder.IndexOf(a.Id))
          .ToList();
      }

      return questions;
    }

    private async Task<IEnumerable<Question>> GetQuestionsWithNewProgressAsync(int quizId, string userId)
    {
      var questions = await _dbContext.Questions
        .Where(q => q.QuizId == quizId)
        .Include(q => q.Answers)
        .ToListAsync();

      var rand = new Random();
      var questionsOrder = new List<int>();
      var answersOrder = new List<int>();

      questions = questions
        .OrderBy(q => rand.Next())
        .ToList();

      foreach (var q in questions)
      {
        questionsOrder.Add(q.Id);
        q.Answers = q.Answers
          .OrderBy(a => rand.Next())
          .ToList();

        foreach (var a in q.Answers)
        {
          answersOrder.Add(a.Id);
        }
      }

      var progress = new QuizProgress()
      {
        UserId = userId,
        QuizId = quizId,
        QuestionsOrder = GetStringFromOrderList(questionsOrder),
        AnswersOrder = GetStringFromOrderList(answersOrder)
      };

      _dbContext.QuizProgresses
        .Add(progress);
      _dbContext.SaveChanges();

      return questions;
    }

    private string GetStringFromOrderList(List<int> orderList)
      => string.Join(".", orderList);

    private List<int> GetListFromOrderString(string orderString)
      => orderString.Split('.').Select(Int32.Parse).ToList();
  }
}