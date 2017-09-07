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

    public IEnumerable<Quiz> GetUserTakenQuizzes(string userId)
    {
      var user = AppDbContext.Users
        .Include(u => u.TakenQuizzes)
        .FirstOrDefault(u => u.Id == userId);

      return user?.TakenQuizzes
        .Select(tq => tq.Quiz);
    }
  }
}