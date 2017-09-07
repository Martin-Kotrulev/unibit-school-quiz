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

    void CreateProgress(QuizProgress progress);

    void ScoreUserAsync(ApplicationUser user, int quizId, ICollection<int> answersIds);

    void MarkQuizAsTaken(int quizId, string userId);

    void Subscribe(QuizSubscription subscription);

    void Subscribe(GroupSubscription subscription);

    Task<IEnumerable<Question>> GetQuestionsAsync(int quizId);

    IEnumerable<Answer> GetAnswersForIds(ICollection<int> ids);

    IEnumerable<QuizGroup> GetQuizGroups(int page = 1, int pageSize = 10);

    Task<IEnumerable<QuizGroup>> SearchQuizGroupsByTagsAsync(ICollection<string> tags);

    IEnumerable<Quiz> GetQuizzes(int page = 1, int pageSize = 10);

    Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(ICollection<string> tags);

    Task<IEnumerable<Quiz>> GetGroupQuizzesAsync(int quizGroupId, int page = 1, int pageSize = 10);

    IEnumerable<Quiz> GetUserOwnQuizzes(ApplicationUser user);

    IEnumerable<Quiz> GetUserTakenQuizzes(ApplicationUser user);

    Task<Question> GetQuestionWithAnswersAsync(int questionId, int quizId, string userId);

    Task<IEnumerable<int>> GetRandomQuestionsOrderAsync(int quizId);

    Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password);
  }
}