using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
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

    Task<IEnumerable<Question>> GetQuestionsAsync(int quizId, string userId);

    IEnumerable<Answer> GetAnswersForIds(ICollection<int> ids);

    Task<IEnumerable<QuizGroup>> GetGroupsAsync(int page = 1, int pageSize = 10, string search = "");

    Task<IEnumerable<QuizGroup>> SearchQuizGroupsByTagsAsync(ICollection<string> tags, int page = 1, int pageSize = 10);

    Task<IEnumerable<Quiz>> GetQuizzesAsync(int page = 1, int pageSize = 10, string search = "");

    Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(ICollection<string> tags, int page = 1, int pageSize = 10);

    Task<IEnumerable<Quiz>> GetGroupQuizzesAsync(int quizGroupId, int page = 1, int pageSize = 10);

    IEnumerable<Quiz> GetUserOwnQuizzes(ApplicationUser user, int page = 1, int pageSize = 10);

    Task<IEnumerable<QuizGroup>> GetUserOwnGroupsAsync(string userId, int page = 1, int pageSize = 10);

    Task<bool> GroupExistsAsync(QuizGroup quizGroup);

    Task<IEnumerable<Quiz>> GetUserTakenQuizzesAsync(ApplicationUser user, int page = 1, int pageSize = 10);

    Task<Question> GetQuestionWithAnswersAsync(int questionId, int quizId, string userId);

    IEnumerable<Tag> CheckForExistingTags(ICollection<string> tags);

    Task<IEnumerable<int>> GetRandomQuestionsOrderAsync(int quizId);

    Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password);

    Task<QuizEnum> EnterQuizAsync(int quizId, string userId);

    Task<bool> QuizExistsAsync(Quiz quiz);

    bool DeleteQuiz(int id, string userId);
    
    bool DeleteQuizGroup(int id, string userId);
    bool UserCanAddQuestion(int quizId, string userId);
  }
}