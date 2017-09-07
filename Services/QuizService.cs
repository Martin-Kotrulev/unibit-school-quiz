using System;
using System.Collections.Generic;
using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Services
{
  public class QuizService : IQuizService
  {
    private readonly IUnitOfWork _unitOfWork;

    public QuizService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public void CreateAnswer(Answer answer, int questionId)
    {
      _unitOfWork.Questions
        .Get(questionId).Answers
        .Add(answer);

      _unitOfWork.Complete();
    }

    public void CreateGroup(QuizGroup quizGroup)
    {
      _unitOfWork.QuizGroups
        .Add(quizGroup);

      _unitOfWork.Complete();
    }

    public void CreateProgress(QuizProgress progress)
    {
      _unitOfWork.QuizProgresses
        .Add(progress);

      _unitOfWork.Complete();
    }

    public void CreateQuestion(Question question, int quizId)
    {
      var quiz = _unitOfWork.Quizzes.Get(quizId);
      quiz.Questions.Add(question);

      _unitOfWork.Complete();
    }

    public void CreateQuiz(Quiz quiz, int? quizGroupId = null)
    {
      if (quizGroupId != null) 
      {
        var quizGroup = _unitOfWork.QuizGroups.Get((int) quizGroupId);
        quizGroup.Quizzes.Add(quiz);
      }
      else {
        _unitOfWork.Quizzes.Add(quiz);
      }

      _unitOfWork.Complete();
    }

    public void MarkQuizAsTaken(int quizId, string userId)
    {
      _unitOfWork.Quizzes.MarkQuizAsTaken(quizId, userId);
      _unitOfWork.Complete();
    }

    public IEnumerable<Answer> GetAnswersForIds(ICollection<int> ids)
    {
      return _unitOfWork.Answers.Find(a => ids.Contains(a.Id));
    }

    public IEnumerable<Quiz> GetGroupQuizzes(int quizGroupId, int page = 1, int pageSize = 10)
    {
      return _unitOfWork.Quizzes.GetGroupQuizzesPaged(quizGroupId, page, pageSize);
    }

    /// <summary>
    /// Checks if there is a progress made by the current user.
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="quizId"></param>
    /// <param name="userId"></param>
    /// <returns>Question by Id or Question with added progress.</returns>
    public Question GetQuestionWithAnswers(int questionId, int quizId, string userId)
    {
      var progress = _unitOfWork.QuizProgresses
        .FindQuizProgress(quizId, questionId, userId);

      if (progress != null) 
      {
        return _unitOfWork.Questions
          .GetQuestionWithProgress(questionId, progress);
      }
      else {
        return _unitOfWork.Questions
          .GetQuestionWithAnswers(questionId);
      }
    }

    /// <summary>
    /// Used to get given quiz with all questions and answers
    /// </summary>
    /// <param name="quizId"></param>
    /// <returns></returns>
    public IEnumerable<Question> GetQuestions(int quizId)
    {
      return _unitOfWork.Questions.GetQuestionsForQuiz(quizId);
    }

    public IEnumerable<QuizGroup> GetQuizGroups(int page = 1, int pageSize = 10)
    {
      return _unitOfWork.QuizGroups.Paged(page, pageSize);
    }

    public IEnumerable<Quiz> GetQuizzes(int page = 1, int pageSize = 10)
    {
      return _unitOfWork.Quizzes.Paged(page, pageSize);
    }

    public IEnumerable<Quiz> GetUserOwnQuizzes(ApplicationUser user)
    {
      return _unitOfWork.Quizzes.Find(q => q.CreatorId == user.Id);
    }

    public IEnumerable<Quiz> GetUserTakenQuizzes(ApplicationUser user)
    {
      return _unitOfWork.Users.GetUserTakenQuizzes(user.Id);
    }

    public void ScoreUser(ApplicationUser user, int quizId, ICollection<Answer> answers)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<QuizGroup> SearchQuizGroupsByTags(ICollection<string> tags)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Quiz> SearchQuizzesByTags(ICollection<string> tags)
    {
      throw new System.NotImplementedException();
    }

    public void Subscribe(QuizSubscription subscription)
    {
      throw new System.NotImplementedException();
    }

    public void Subscribe(GroupSubscription subscription)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<int> GetRandomQuestionsOrder(int quizId)
    {
      return _unitOfWork.Answers.GetRandomOrderForAnswerIds(quizId);
    }
  }
}