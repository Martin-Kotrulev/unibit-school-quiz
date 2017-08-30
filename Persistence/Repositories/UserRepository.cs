using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class UserRepository : Repository<ApplicationUser>, IUserRepository
  {
    public UserRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}