using System.Collections.Generic;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IUserRepository : IRepository<ApplicationUser>
  {
    IEnumerable<Quiz> GetUserTakenQuizzes(string userId);
  }
}