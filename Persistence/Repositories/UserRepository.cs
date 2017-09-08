using System.Collections.Generic;
using System.Linq;
using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class UserRepository : Repository<ApplicationUser>, IUserRepository
  {
    public AppDbContext AppDbContext { get { return Context as AppDbContext; } }
    public UserRepository(AppDbContext context)
      : base(context)
    {
        
    }

    public IEnumerable<Quiz> GetUserTakenQuizzesPaged(string userId, int page = 1, int pageSize = 10)
    {
      var user = AppDbContext.Users
        .FirstOrDefault(u => u.Id == userId);
      
      var userEntry = AppDbContext.Entry(user);

      var takenQuizes = userEntry.Collection(ue => ue.TakenQuizzes)
        .Query()
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(tq => tq.Quiz);
      
      takenQuizes.Load();

      return takenQuizes;
    }
  }
}