using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class TagRepository : Repository<Tag>, ITagRepository
  {
    public TagRepository(AppDbContext context)
      :base(context)
    {
        
    }
  }
}