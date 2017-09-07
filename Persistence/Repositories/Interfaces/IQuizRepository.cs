using System.Collections.Generic;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IQuizRepository : IRepository<Quiz> 
  {
    IEnumerable<Quiz> GetGroupQuizzesPaged(int quizId, int page = 1, int pageSize = 10);
    void MarkQuizAsTaken(int quizId, string userId);
  }
}