using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IQuizRepository : IRepository<Quiz> 
  {
    void MarkQuizAsTaken(int quizId, string userId);

    Task<int> GetQuizTotalScoreAsync(int quizId);

    Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password);

    Task<IEnumerable<Quiz>> GetGroupQuizzesPagedAsync(int quizGroupId, int page = 1, int pageSize = 10);

    Task<IEnumerable<Quiz>> GetQuizzesPagedBySearchAsync(int page = 1, int pageSize = 10, string search = "");

    Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(ICollection<string> tags, int page = 1, int pageSize = 10);

    Task<Quiz> GetQuizWithParticipantsAsync(int quizId);
    bool UserIsQuizCreator(int quizId, string userId);
  }
}