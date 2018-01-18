using Uniquizbit.Models;
using Uniquizbit.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Uniquizbit.Persistence.Repositories
{
  internal class ScoreRepository : Repository<Score>, IScoreRepository
  {
    public ScoreRepository(UniquizbitDbContext context) 
      : base(context)
    {
    }
  }
}