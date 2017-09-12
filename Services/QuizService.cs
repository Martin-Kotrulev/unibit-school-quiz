using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers;
using App.Config;
using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace App.Services
{
  public class QuizService : IQuizService
  {
    private readonly IUnitOfWork _unitOfWork;

    private readonly GradesSettings _gradesSettings;

    public QuizService(IUnitOfWork unitOfWork, 
      IOptions<GradesSettings> optionsAccessor)
    {
      _unitOfWork = unitOfWork;
      _gradesSettings = optionsAccessor.Value;
    }

    public void CreateGroup(QuizGroup quizGroup)
    {
      _unitOfWork.QuizGroups.Add(quizGroup);
      _unitOfWork.Complete();
    }

    public void CreateQuiz(Quiz quiz)
    {
      _unitOfWork.Quizzes.Add(quiz);
      _unitOfWork.Complete();
    }

    public void CreateQuestion(Question question)
    {
      _unitOfWork.Questions.Add(question);
      _unitOfWork.Complete();
    }

    public async Task CreateAnswerAsync(Answer answer)
    {
      await _unitOfWork.Answers.AddAnswerAsync(answer);
      _unitOfWork.Complete();
    }

    public async Task<bool> DeleteAnswerAsync(int answerId)
    {
      var deleted = await _unitOfWork.Answers.DeleteAnswerAsync(answerId);
      _unitOfWork.Complete();
      return deleted;
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
        ApplicationUser user, int quizId, ICollection<int> answersIds)
    {
      var totalScore = await _unitOfWork.Quizzes
        .GetQuizTotalScoreAsync(quizId);

      var userScore = await _unitOfWork.QuizProgresses
        .GetProgressAnswersWeightSumAsync(user.Id, quizId);

      var score = new Score() 
      { 
        Value = GetScore(totalScore, userScore),
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

    public async Task<IEnumerable<Quiz>> GetGroupQuizzesAsync(
      int quizGroupId, int page = 1, int pageSize = 10)
    {
      return await _unitOfWork.Quizzes
        .GetGroupQuizzesPagedAsync(quizGroupId, page, pageSize);
    }

    /// <summary>
    /// Used to get a quiz with all questions and answers
    /// </summary>
    /// <param name="quizId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Question>> GetQuestionsAsync(int quizId, string userId)
    {
      bool userIsCreator = _unitOfWork.Quizzes.UserIsQuizCreator(quizId, userId);

      if (userIsCreator)
      {
        return await _unitOfWork.Questions.GetUserQuizQuestionsAsync(quizId);
      }
      else
      {
        var progresAnswersIds = await _unitOfWork.QuizProgresses
          .FindUserQuizProgressAnswersIds(quizId, userId);

        if (progresAnswersIds.Count() > 0)
          return await _unitOfWork.Questions
            .GetQuestionsWithProgressAsync(quizId, progresAnswersIds);

        return await _unitOfWork.Questions
          .GetQuestionsForQuizAsync(quizId);
      }
    }

    public async Task<IEnumerable<QuizGroup>> GetGroupsAsync(
      int page = 1, int pageSize = 10, string search = "")
    {
      return await _unitOfWork.QuizGroups
        .GetGroupsPagedBySearchAsync(page, pageSize, search);
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

    public IEnumerable<Quiz> GetUserOwnQuizzes(
      ApplicationUser user, int page = 1, int pageSize = 10)
    {
      return _unitOfWork.Quizzes
        .Paged(page, pageSize, q => q.CreatorId == user.Id);
    }

    public async Task<IEnumerable<QuizGroup>> GetUserOwnGroupsAsync(
      string userId, int page = 1, int pageSize = 10)
    {
      return await _unitOfWork.QuizGroups
        .GetUserGroupsPagedAsync(userId, page, pageSize);
    }

    public async Task<IEnumerable<Quiz>> GetUserTakenQuizzesAsync(
      ApplicationUser user, int page = 1, int pageSize = 10)
    {
      return await _unitOfWork.Users
        .GetUserTakenQuizzesPaged(user.Id, page, pageSize);
    }

    public async Task<IEnumerable<QuizGroup>> SearchQuizGroupsByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10)
    {
      return await _unitOfWork.QuizGroups
        .SearchQuizGroupByTagsAsync(tags, page, pageSize);
    }

    public async Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10)
    {
      return await _unitOfWork.Quizzes
        .SearchQuizzesByTagsAsync(tags, page, pageSize);
    }


    public async Task<IEnumerable<int>> GetRandomQuestionsOrderAsync(int quizId)
    {
      return await _unitOfWork.Answers
        .GetRandomOrderForAnswerIdsAsync(quizId);
    }

    /// <summary>
    /// Tries to add an user to particular quiz.
    /// </summary>
    /// <param name="quizId"></param>
    /// <param name="user"></param>
    /// <returns>True if success, false otherwise.</returns>
    public async Task<QuizEnum> EnterQuizAsync(int quizId, string userId)
    {
      var quiz = await _unitOfWork.Quizzes.GetQuizWithParticipantsAsync(quizId);
      var inQuiz = quiz.Participants.Any(p => p.User.Id == userId);

      if (quiz == null)
        return QuizEnum.NotExistent;
      else if (DateTime.UtcNow < quiz.Starts)
        return QuizEnum.NotStarted;
      else if (DateTime.UtcNow > quiz.Ends)
        return QuizEnum.Ended;
      else if (quiz.Once && inQuiz)
        return QuizEnum.StillTaking;
      else if (inQuiz)
        return QuizEnum.Enter;

      quiz.Participants.Add(new QuizzesUsers() { Quiz = quiz, UserId = userId });
      _unitOfWork.Complete();
      return QuizEnum.Enter;
    }

    public async Task<bool> GroupExistsAsync(QuizGroup quizGroup)
    {
      return await _unitOfWork.QuizGroups
        .FirstOrDefaultAsync(g => g.Name == quizGroup.Name) != null;
    }

    public async Task<bool> QuizExistsAsync(Quiz quiz)
    {
      return await _unitOfWork.Quizzes
        .FirstOrDefaultAsync(q => q.Title == quiz.Title) != null;
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

    public bool DeleteQuizGroup(int id, string userId)
    {
      var quizGroup = _unitOfWork.QuizGroups.Get(id);

      if (quizGroup == null || quizGroup.OwnerId != userId)
        return false;

      _unitOfWork.QuizGroups.Remove(quizGroup);
      _unitOfWork.Complete();
      return true;
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

    public void Subscribe(QuizSubscription subscription)
    {
      throw new System.NotImplementedException();
    }

    public void Subscribe(GroupSubscription subscription)
    {
      throw new System.NotImplementedException();
    }

    private double Difference(double upperBound, double actualScore)
    {
      // Makes sure the difference is always 1.0 point max
      var diff = actualScore - upperBound;
      var diffByTen = diff * 10;

      return Math.Round((((double)diff / (double)diffByTen) * 10), 2);
    }

    private double GetScore(double totalScore, double userScore)
    {
      double finalScore = 2.0;
      double scoreInPercentage = Math.Round((((double)userScore / (double)totalScore)) * 100, 2);

      if (scoreInPercentage > _gradesSettings.VeryGood)
      {
        finalScore = 6.0 - Difference(_gradesSettings.VeryGood, scoreInPercentage);
      }
      else if (scoreInPercentage > _gradesSettings.Good)
      {
        finalScore = 5.0 - Difference(_gradesSettings.VeryGood, scoreInPercentage);
      }
      else if (scoreInPercentage > _gradesSettings.Average)
      {
        finalScore = 4.0 - Difference(_gradesSettings.VeryGood, scoreInPercentage);
      }
      else if (scoreInPercentage > _gradesSettings.Weak)
      {
        finalScore = 3.0 - Difference(_gradesSettings.VeryGood, scoreInPercentage);
      }

      return finalScore;
    }
  }
}