using System.Collections.Generic;
using App.Models;

namespace App.Services
{
  public interface IQuizService
  {
    void CreateGroup(QuizGroup quizGroup);

    void CreateQuiz(Quiz quiz, int? quizGroupId = null);

    void CreateQuestion(Question question, int quizId);

    void CreateAnswer(Answer answer, int questionId);

    void CreateProgress(QuizProgress progress);

    void ScoreUser(ApplicationUser user, int quizId, ICollection<Answer> answers);

    void Subscribe(QuizSubscription subscription);

    void Subscribe(GroupSubscription subscription);

    IEnumerable<Question> GetQuestions(int quizId, int page = 1, int size = 1);

    IEnumerable<Answer> GetAnswersForIds(ICollection<int> ids);

    IEnumerable<QuizGroup> GetQuizGroups(int page = 1, int pageSize = 10);

    IEnumerable<QuizGroup> SearchQuizGroupsByTags(ICollection<string> tags);

    IEnumerable<Quiz> GetQuizzes(int page = 1, int pageSize = 10);

    IEnumerable<Quiz> SearchQuizzesByTags(ICollection<string> tags);

    IEnumerable<Quiz> GetGroupQuizzes(int quizGroupId, int page = 1, int pageSize = 10);

    IEnumerable<Quiz> GetUserOwnQuizzes(ApplicationUser user);

    IEnumerable<Quiz> GetUserTakenQuizzes(ApplicationUser user);

    IEnumerable<Answer> GetQuestionAnswers(int questionId);

    IEnumerable<Quiz> GetQuiz(ApplicationUser user, int quizId);
  }
}