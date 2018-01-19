namespace Uniquizbit.Services.Implementations
{
	using Data.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;

  public class QuizGroupService : IQuizGroupService
  {
    public void CreateQuizGroup(QuizGroup quizGroup)
    {
      throw new System.NotImplementedException();
    }

    public bool DeleteQuizGroup(int id, string userId)
    {
      throw new System.NotImplementedException();
    }

    public QuizGroup FindGroupById(int groupId)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Quiz>> GetGroupQuizzesAsync(int quizGroupId, int page = 1, int pageSize = 10)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<QuizGroup>> GetQuizGroupsAsync(int page = 1, int pageSize = 10, string search = "")
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<QuizGroup>> GetUserOwnGroupsAsync(string userId, int page = 1, int pageSize = 10)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> GroupExistsAsync(QuizGroup quizGroup)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<QuizGroup>> SearchQuizGroupsByTagsAsync(ICollection<string> tags, int page = 1, int pageSize = 10)
    {
      throw new System.NotImplementedException();
    }

    public void Subscribe(GroupSubscription subscription)
    {
      throw new System.NotImplementedException();
    }
  }
}