namespace Uniquizbit.Services
{
  using Common.Config;
  using Common.Enums;
  using Data.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  public interface IQuizService
  {
    void CreateQuiz(Quiz quiz); 

    void ScoreUserAsync(User user, int quizId, ICollection<int> answersIds);

    void MarkQuizAsTaken(int quizId, string userId);

    void Subscribe(QuizSubscription subscription);

    Task CreateQuizProgressAsync(QuizProgress progress, IEnumerable<int> answersIds);

    Task<IEnumerable<Quiz>> GetQuizzesAsync(
      int page = 1, int pageSize = 10, string search = "");

    Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10);

    Task<IEnumerable<Quiz>> GetUserOwnQuizzesAsync(string userId, int page = 1, int pageSize = 10);

    Task<IEnumerable<Quiz>> GetUserTakenQuizzesAsync(
      User user, int page = 1, int pageSize = 10);

    IEnumerable<Tag> CheckForExistingTags(ICollection<string> tags);

    Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password);

    Task<QuizEnum> EnterQuizAsync(int quizId, string userId);

    Task<bool> QuizExistsAsync(Quiz quiz);

    bool DeleteQuiz(int id, string userId);
    
    bool UserCanAddQuestion(int quizId, string userId);

    bool UserCanAddQuizzes(int id, string userId);

    Quiz FindQuizById(int id);
  }
}