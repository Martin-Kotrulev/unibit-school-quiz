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

    public void CreateAnswer(Answer answer, Question question)
    {
      throw new System.NotImplementedException();
    }

    public void CreateGroup(QuizGroup quizGroup, ApplicationUser owner)
    {
      throw new System.NotImplementedException();
    }

    public void CreateProgress(QuizProgress progress)
    {
      throw new System.NotImplementedException();
    }

    public void CreateQuestion(Question question, ApplicationUser owner)
    {
      throw new System.NotImplementedException();
    }

    public void CreateQuiz(Quiz quiz, ApplicationUser owner, QuizGroup quizGroup = null)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Quiz> GetGroupQuizes(QuizGroup quizGroup, int page = 1, int pageSize = 10)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Answer> GetQuestionAnswers(Question question, int page = 1, int pageSize = 1)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Quiz> GetQuizes(int page = 1, int pageSize = 10)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Quiz> GetUsersQuizes(ApplicationUser user)
    {
      throw new System.NotImplementedException();
    }
  }
}