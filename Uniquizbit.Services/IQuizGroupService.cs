namespace Uniquizbit.Services
{
  using Data.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  public interface IQuizGroupService : IService
  {
    Task<QuizGroup> CreateQuizGroupAsync(QuizGroup quizGroup);

		Task<IEnumerable<QuizGroup>> GetQuizGroupsAsync(
      int page = 1, int pageSize = 10, string search = "");

		Task<IEnumerable<QuizGroup>> SearchQuizGroupsByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10);

    void Subscribe(GroupSubscription subscription);

		Task<IEnumerable<QuizGroup>> GetUserOwnGroupsAsync(
      string userId, int page = 1, int pageSize = 10);

		Task<bool> GroupExistsAsync(QuizGroup quizGroup);

		Task<bool> DeleteQuizGroupAsync(int id, string userId);

		Task<QuizGroup> FindGroupByIdAsync(int groupId);

		Task<IEnumerable<Quiz>> GetGroupQuizzesAsync(
      int quizGroupId, int page = 1, int pageSize = 10);
  }
}