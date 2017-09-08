using System.Collections.Generic;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IUserRepository : IRepository<ApplicationUser>
  {
    IEnumerable<Quiz> GetUserTakenQuizzesPaged(string userId, int page, int pageSize);
  }
}