using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IUserRepository : IRepository<ApplicationUser>
  {
    Task<IEnumerable<Quiz>> GetUserTakenQuizzesPaged(string userId, int page, int pageSize);
  }
}