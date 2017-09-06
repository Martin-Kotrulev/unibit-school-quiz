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
      if (quizGroupId != null) {
        var quizGroup = _unitOfWork.QuizGroups.Get((int) quizGroupId);
        quizGroup.Quizzes.Add(quiz);
      } else {
        _unitOfWork.Quizzes.Add(quiz);
      }

      _unitOfWork.Complete();
    }

    public IEnumerable<Answer> GetAnswersForIds(ICollection<int> ids)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Quiz> GetGroupQuizzes(int quizGroupId, int page = 1, int pageSize = 10)
    {
      return _unitOfWork.Quizzes.GetGroupQuizzesPaged(quizGroupId, page, pageSize);
    }

    public IEnumerable<Answer> GetQuestionAnswers(int questionId)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Question> GetQuestions(int quizId, int page = 1, int size = 1)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Quiz> GetQuiz(ApplicationUser user, int quizId)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<QuizGroup> GetQuizGroups(int page = 1, int pageSize = 10)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Quiz> GetQuizzes(int page = 1, int pageSize = 10)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Quiz> GetUserOwnQuizzes(ApplicationUser user)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Quiz> GetUserTakenQuizzes(ApplicationUser user)
    {
      throw new System.NotImplementedException();
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
  }
}