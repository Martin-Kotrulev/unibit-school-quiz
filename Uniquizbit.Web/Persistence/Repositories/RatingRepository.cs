using Uniquizbit.Models;
using Uniquizbit.Persistence.Repositories.Interfaces;

namespace Uniquizbit.Persistence.Repositories
{
  internal class RatingRepository : Repository<Rating>, IRatingRepository
  {
    public RatingRepository(UniquizbitDbContext context)
      : base(context)
    {
    }
  }
}