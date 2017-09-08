using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IQuizRepository : IRepository<Quiz> 
  {
    Task<IEnumerable<Quiz>> GetGroupQuizzesPagedAsync(int quizId, int page, int pageSize);
    void MarkQuizAsTaken(int quizId, string userId);
    Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(ICollection<string> tags);
    Task<int> GetQuizTotalScoreAsync(int quizId);
    Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password);
  }
}