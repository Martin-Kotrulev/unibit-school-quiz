using System.Collections.Generic;
using System.Threading.Tasks;
using Uniquizbit.Models;

namespace Uniquizbit.Persistence.Repositories.Interfaces
{
  public interface IUserRepository : IRepository<ApplicationUser>
  {
    Task<IEnumerable<Quiz>> GetUserTakenQuizzesPaged(string userId, int page, int pageSize);
  }
}