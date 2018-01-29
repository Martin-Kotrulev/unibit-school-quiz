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

      if (question == null || question.CreatorId != userId)
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

    public async Task<ICollection<Question>> UpdateQuestionsForQuiz(int quizId,
      string userId,
      ICollection<Question> questions)
    {
      var quiz = await _dbContext.Quizzes
        .FirstOrDefaultAsync(q => q.Id == quizId);

      if (quiz != null)
      {
        // Track changes or insert new questions
        foreach (var question in questions)
        {
          question.CreatorId = userId;
          question.QuizId = quizId;
          _dbContext.Entry(question).State = question.Id == 0 ?
                                             EntityState.Added :
                                             EntityState.Modified;
        }

        await _dbContext.Entry(quiz)
          .Collection(q => q.Questions)
          .LoadAsync();

        // Clean up questions from database that are not part of the new list
        foreach (var question in quiz.Questions)
        {
          if (!questions.Any(q => q.Id == question.Id))
            _dbContext.Questions.Remove(question);
        }

        var newAnswersList = questions
          .SelectMany(q => q.Answers)
          .ToList();

        foreach (var answer in newAnswersList)
        {
          answer.CreatorId = userId;
          answer.QuizId = quizId;
          _dbContext.Entry(answer).State = answer.Id == 0 ?
                                           EntityState.Added :
                                           EntityState.Modified;
        }

        var existingAnswersList = _dbContext.Answers
          .Where(a => a.QuizId == quizId)
          .ToList();

        // Clean up answers from database that are not part of the new list
        foreach (var existingAnswer in existingAnswersList)
        {
          if (!newAnswersList.Any(a => a.Id == existingAnswer.Id))
            _dbContext.Answers.Remove(existingAnswer);
        }
      }

      await _dbContext.SaveChangesAsync();

      return questions;
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

      var progressAnswers = await _dbContext.QuizProgresses
        .Include(qp => qp.GivenAnswers)
          .ThenInclude(ga => ga.ProgressAnswer)
        .Where(qp => qp.QuizId == quizId && qp.UserId == userId)
        .SelectMany(qp => qp.GivenAnswers)
        .Select(ga => ga.ProgressAnswer)
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
            progressAnswers.Any(pa =>
            {
              var equal = pa.AnswerId == a.Id;

              if (equal)
                a.IsChecked = pa.IsChecked;

              return equal;
            });
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

    public async Task<Question> FindQuestionByIdAsync(int questionId)
      => await _dbContext.Questions.FindAsync(questionId);
  }
}