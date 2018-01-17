using System.Collections.Generic;
using System.Threading.Tasks;
using Uniquizbit.Controllers;
using Uniquizbit.Models;

namespace Uniquizbit.Services
{
  public interface IQuizService
  {
    void CreateGroup(QuizGroup quizGroup);

    void CreateQuiz(Quiz quiz);

    void CreateQuestion(Question question);

    Task CreateAnswerAsync(Answer answer);

    void ScoreUserAsync(ApplicationUser user, int quizId, ICollection<int> answersIds);

    void MarkQuizAsTaken(int quizId, string userId);

    void Subscribe(QuizSubscription subscription);

    void Subscribe(GroupSubscription subscription);

    Task CreateProgressAsync(QuizProgress progress, IEnumerable<int> answersIds);

    Task<IEnumerable<Question>> GetQuestionsAsync(int quizId, string userId);

    IEnumerable<Answer> GetAnswersForIds(ICollection<int> ids);

    Task<IEnumerable<QuizGroup>> GetGroupsAsync(
      int page = 1, int pageSize = 10, string search = "");

    Task<IEnumerable<QuizGroup>> SearchQuizGroupsByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10);

    Task<IEnumerable<Quiz>> GetQuizzesAsync(
      int page = 1, int pageSize = 10, string search = "");

    Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10);

    Task<IEnumerable<Quiz>> GetGroupQuizzesAsync(
      int quizGroupId, int page = 1, int pageSize = 10);

    Task<IEnumerable<Quiz>> GetUserOwnQuizzesAsync(string userId, int page = 1, int pageSize = 10);

    Task<IEnumerable<QuizGroup>> GetUserOwnGroupsAsync(
      string userId, int page = 1, int pageSize = 10);

    Task<bool> GroupExistsAsync(QuizGroup quizGroup);

    Task<IEnumerable<Quiz>> GetUserTakenQuizzesAsync(
      ApplicationUser user, int page = 1, int pageSize = 10);

    IEnumerable<Tag> CheckForExistingTags(ICollection<string> tags);

    Task<IEnumerable<int>> GetRandomQuestionsOrderAsync(int quizId);

    Task<bool> UserOwnQuestionAsync(int questionId, string userId);

    Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password);

    Task<QuizEnum> EnterQuizAsync(int quizId, string userId);

    Task<bool> QuizExistsAsync(Quiz quiz);

    bool DeleteQuiz(int id, string userId);
    
    bool DeleteQuizGroup(int id, string userId);

    bool UserCanAddQuestion(int quizId, string userId);

    Task<bool> DeleteAnswerAsync(int answerId);

    bool DeleteQuestion(int id);

    bool UserCanAddQuizzes(int id, string userId);

    QuizGroup GetGroup(int id);

    Quiz GetQuiz(int id);
  }
}