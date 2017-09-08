using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Services
{
  public interface IQuizService
  {
    void CreateGroup(QuizGroup quizGroup);

    void CreateQuiz(Quiz quiz);

    void CreateQuestion(Question question);

    void CreateAnswer(Answer answer);

    void CreateProgressAsync(QuizProgress progress, IEnumerable<int> answersIds);

    void ScoreUserAsync(ApplicationUser user, int quizId, ICollection<int> answersIds);

    void MarkQuizAsTaken(int quizId, string userId);

    void Subscribe(QuizSubscription subscription);

    void Subscribe(GroupSubscription subscription);

    Task<IEnumerable<Question>> GetQuestionsAsync(int quizId);

    IEnumerable<Answer> GetAnswersForIds(ICollection<int> ids);

    IEnumerable<QuizGroup> GetQuizGroups(int page, int pageSize);

    Task<IEnumerable<QuizGroup>> SearchQuizGroupsByTagsAsync(ICollection<string> tags);

    IEnumerable<Quiz> GetQuizzes(int page, int pageSize);

    Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(ICollection<string> tags);

    Task<IEnumerable<Quiz>> GetGroupQuizzesAsync(int quizGroupId, int page, int pageSize);

    IEnumerable<Quiz> GetUserOwnQuizzes(ApplicationUser user, int page, int pageSize);

    IEnumerable<QuizGroup> GetUserOwnGroups(ApplicationUser user, int page, int pageSize);

    IEnumerable<Quiz> GetUserTakenQuizzes(ApplicationUser user, int page, int pageSize);

    Task<Question> GetQuestionWithAnswersAsync(int questionId, int quizId, string userId);

    Task<IEnumerable<int>> GetRandomQuestionsOrderAsync(int quizId);

    Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password);

    Task<bool> EnterQuizAsync(int quizId, ApplicationUser user);
  }
}