using Uniquizbit.Models;
using Uniquizbit.Persistence.Repositories.Interfaces;

namespace Uniquizbit.Persistence.Repositories
{
  internal class TagRepository : Repository<Tag>, ITagRepository
  {
    public TagRepository(UniquizbitDbContext context)
      :base(context)
    {
        
    }
  }
}