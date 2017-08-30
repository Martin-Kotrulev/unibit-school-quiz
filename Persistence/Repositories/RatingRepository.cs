using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class RatingRepository : Repository<Rating>, IRatingRepository
  {
    public RatingRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}