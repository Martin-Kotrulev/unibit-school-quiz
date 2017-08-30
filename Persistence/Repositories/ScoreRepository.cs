using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class ScoreRepository : Repository<Score>, IScoreRepository
  {
    public ScoreRepository(AppDbContext context)
      :base(context)
    {
        
    }
  }
}