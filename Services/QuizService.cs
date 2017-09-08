using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public QuizService(IUnitOfWork unitOfWork, IOptions<GradesSettings> optionsAccessor)
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

    public void CreateAnswer(Answer answer)
    {
      _unitOfWork.Answers.Add(answer);
      _unitOfWork.Complete();
    }

    public async void CreateProgressAsync(QuizProgress progress, IEnumerable<int> answersIds)
    {
      await _unitOfWork.QuizProgresses.AddProgressAsync(progress, answersIds);
      _unitOfWork.Complete();
    }

    public void MarkQuizAsTaken(int quizId, string userId)
    {
      _unitOfWork.Quizzes.MarkQuizAsTaken(quizId, userId);
      _unitOfWork.Complete();
    }

    public async void ScoreUserAsync(ApplicationUser user, int quizId, ICollection<int> answersIds)
    {
      var totalScore = await _unitOfWork.Quizzes.GetQuizTotalScoreAsync(quizId);

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

    public async Task<IEnumerable<Quiz>> GetGroupQuizzesAsync(int quizGroupId, int page = 1, int pageSize = 10)
    {
      return await _unitOfWork.Quizzes.GetGroupQuizzesPagedAsync(quizGroupId, page, pageSize);
    }

    /// <summary>
    /// Checks if there is a progress made by the current user on this question.
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="quizId"></param>
    /// <param name="userId"></param>
    /// <returns>Question by Id or Question with added progress.</returns>
    public async Task<Question> GetQuestionWithAnswersAsync(int questionId, int quizId, string userId)
    {
      var progress = await _unitOfWork.QuizProgresses
        .FindQuizProgressAsync(quizId, questionId, userId);

      if (progress != null) 
      {
        return await _unitOfWork.Questions
          .GetQuestionWithProgressAsync(questionId, progress);
      }
      else {
        return await _unitOfWork.Questions
          .GetQuestionWithAnswersAsync(questionId);
      }
    }

    /// <summary>
    /// Used to get a quiz with all questions and answers
    /// </summary>
    /// <param name="quizId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Question>> GetQuestionsAsync(int quizId)
    {
      return await _unitOfWork.Questions.GetQuestionsForQuizAsync(quizId);
    }

    public IEnumerable<QuizGroup> GetQuizGroups(int page = 1, int pageSize = 10)
    {
      return _unitOfWork.QuizGroups.Paged(page, pageSize);
    }

    public IEnumerable<Quiz> GetQuizzes(int page = 1, int pageSize = 10)
    {
      return _unitOfWork.Quizzes.Paged(page, pageSize);
    }

    public async Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password)
    {
      return await _unitOfWork.Quizzes.GetQuizWithPasswordAsync(quizId, password);
    }

    public IEnumerable<Quiz> GetUserOwnQuizzes(ApplicationUser user, int page = 1, int pageSize = 10)
    {
      return _unitOfWork.Quizzes.Paged(page, pageSize, q => q.CreatorId == user.Id);
    }

    public IEnumerable<QuizGroup> GetUserOwnGroups(ApplicationUser user, int page = 1, int pageSize = 10)
    {
      return _unitOfWork.QuizGroups.Paged(page, pageSize, qg => qg.OwnerId == user.Id);
    }

    public IEnumerable<Quiz> GetUserTakenQuizzes(ApplicationUser user, int page = 1, int pageSize = 10)
    {
      return _unitOfWork.Users.GetUserTakenQuizzesPaged(user.Id, page, pageSize);
    }

    public async Task<IEnumerable<QuizGroup>> SearchQuizGroupsByTagsAsync(ICollection<string> tags)
    {
      return await _unitOfWork.QuizGroups.SearchQuizGroupByTagsAsync(tags);
    }

    public IEnumerable<QuizGroup> SearchQuizGroupByName(string name)
    {
      return _unitOfWork.QuizGroups
        .Find(qg => qg.Name.ToLowerInvariant()
          .Contains(name.ToLowerInvariant())
        );
    }

    public async Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(ICollection<string> tags)
    {
      return await _unitOfWork.Quizzes.SearchQuizzesByTagsAsync(tags);
    }

    public IEnumerable<Quiz> SearchQuizzesByName(string title)
    {
      return _unitOfWork.Quizzes.Find(q => q.Title.ToLowerInvariant()
        .Contains(title.ToLowerInvariant())
      );
    }

    public async Task<IEnumerable<int>> GetRandomQuestionsOrderAsync(int quizId)
    {
      return await _unitOfWork.Answers.GetRandomOrderForAnswerIdsAsync(quizId);
    }

    /// <summary>
    /// Tries to add an user to particular quiz.
    /// </summary>
    /// <param name="quizId"></param>
    /// <param name="user"></param>
    /// <returns>True if success, false otherwise.</returns>
    public async Task<bool> EnterQuizAsync(int quizId, ApplicationUser user)
    {
      var quiz = await _unitOfWork.Quizzes.GetAsync(quizId);

      if (quiz == null)
        return false;
      else if (DateTime.UtcNow < quiz.StartDateTime && DateTime.UtcNow > quiz.EndDateTime)
        return false;
      else if (quiz.IsOneTime && quiz.Participants.Any(p => p.Id == user.Id))
        // Checks if the user is already in this quiz
        return false;

      quiz.Participants.Add(user);
      _unitOfWork.Complete();
      return true;
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