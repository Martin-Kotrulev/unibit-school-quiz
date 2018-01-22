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

  public class QuizService : IQuizService
  {
    private readonly GradesSettings _gradesSettings;

    private readonly UniquizbitDbContext _dbContext;

    public QuizService(UniquizbitDbContext dbContext,
      IOptions<GradesSettings> optionsAccessor)
    {
      _dbContext = dbContext;
      _gradesSettings = optionsAccessor.Value;
    }

    public async Task CreateProgressAsync(
        QuizProgress progress, IEnumerable<int> answersIds)
    {
      await _unitOfWork.QuizProgresses
        .AddProgressAsync(progress, answersIds);
      _unitOfWork.Complete();
    }

    public void MarkQuizAsTaken(int quizId, string userId)
    {
      _unitOfWork.Quizzes.MarkQuizAsTaken(quizId, userId);
      _unitOfWork.Complete();
    }

    public async void ScoreUserAsync(
        User user, int quizId, ICollection<int> answersIds)
    {
      var maxScore = await _unitOfWork.Quizzes
        .GetQuizTotalScoreAsync(quizId);

      var userScore = await _unitOfWork.QuizProgresses
        .GetProgressAnswersWeightSumAsync(user.Id, quizId);

      var score = new Score()
      {
        Value = GetScore(maxScore, userScore),
        ScoredAt = DateTime.Now,
        QuizId = quizId,
        UserId = user.Id
      };

      _unitOfWork.Scores.Add(score);
      _unitOfWork.Complete();
    }

    public IEnumerable<Answer> GetAnswersForIds(ICollection<int> ids)
    {
      return _unitOfWork.Answers.Find(a => ids.Contains(a.Id));
    }

    public async Task<IEnumerable<Quiz>> GetQuizzesAsync(
      int page = 1, int pageSize = 10, string search = "")
    {
      return await _unitOfWork.Quizzes
        .GetQuizzesPagedBySearchAsync(page, pageSize, search);
    }

    public async Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password)
    {
      return await _unitOfWork.Quizzes
        .GetQuizWithPasswordAsync(quizId, password);
    }

    public async Task<IEnumerable<Quiz>> GetUserOwnQuizzesAsync(
      string userId, int page = 1, int pageSize = 10)
    {
      return await _unitOfWork.Quizzes
        .PagedAsync(page, pageSize, q => q.CreatorId == userId);
    }

    public async Task<IEnumerable<Quiz>> GetUserTakenQuizzesAsync(
      User user, int page = 1, int pageSize = 10)
    {
      return await _unitOfWork.Users
        .GetUserTakenQuizzesPaged(user.Id, page, pageSize);
    }

    public async Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10)
    {
      return await _unitOfWork.Quizzes
        .SearchQuizzesByTagsAsync(tags, page, pageSize);
    }

    public async Task<QuizEnum> EnterQuizAsync(int quizId, string userId)
    {
      var quiz = await _unitOfWork.Quizzes.GetQuizWithParticipantsAsync(quizId);

      if (quiz == null)
        return QuizEnum.NotExistent;

      var inQuiz = quiz.Participants.Any(p => p.UserId == userId);

      if (quiz.Starts != null && DateTime.UtcNow < quiz.Starts)
        return QuizEnum.NotStarted;
      else if (quiz.Ends != null && DateTime.UtcNow > quiz.Ends)
        return QuizEnum.Ended;
      else if (quiz.Once && inQuiz)
        return QuizEnum.StillTaking;
      else if (inQuiz)
        return QuizEnum.Enter;

      quiz.Participants.Add(new QuizzesUsers() { Quiz = quiz, UserId = userId });
      _unitOfWork.Complete();
      return QuizEnum.Enter;
    }

    public async Task<bool> QuizExistsAsync(Quiz quiz)
    {
      return await _unitOfWork.Quizzes
        .FirstOrDefaultAsync(q => q.Name == quiz.Name) != null;
    }

    public IEnumerable<Tag> CheckForExistingTags(ICollection<string> tags)
    {
      return _unitOfWork.Tags
        .Find(t => tags.Contains(t.Name))
        .ToList();
    }

    public bool DeleteQuiz(int id, string userId)
    {
      var quiz = _unitOfWork.Quizzes.Get(id);

      if (quiz.CreatorId != userId)
        return false;

      if (quiz != null)
      {
        _unitOfWork.Quizzes.Remove(quiz);
        _unitOfWork.Complete();
        return true;
      }

      return false;
    }

    public bool DeleteQuestion(int id)
    {
      var question = _unitOfWork.Questions.Get(id);

      if (question == null)
        return false;

      _unitOfWork.Questions.Remove(question);
      _unitOfWork.Complete();
      return true;
    }

    public bool UserCanAddQuestion(int quizId, string userId)
    {
      return _unitOfWork.Quizzes.UserIsQuizCreator(quizId, userId);
    }

    public Task<bool> UserOwnQuestionAsync(int questionId, string userId)
    {
      return _unitOfWork.Questions.UserOwnQuestionAsync(questionId, userId);
    }

    public bool UserCanAddQuizzes(int id, string userId)
    {
      var group = _unitOfWork.QuizGroups.Get(id);
      return group != null && group.CreatorId == userId;
    }

    public void Subscribe(QuizSubscription subscription)
    {
      throw new System.NotImplementedException();
    }

    public async Task<Quiz> AddQuizAsync(Quiz quiz)
    {
      await _dbContext.Quizzes.AddAsync(quiz);
      await _dbContext.SaveChangesAsync();

      return quiz;
    }

    public async Task<Quiz> FindQuizByIdAsync(int quizId)
      => await _dbContext.Quizzes
          .FirstOrDefaultAsync(q => q.Id == quizId);
          
    public Task CreateQuizProgressAsync(QuizProgress progress, IEnumerable<int> answersIds)
    {
      throw new NotImplementedException();
    }
  }