using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class GroupSubscribtionRepository : Repository<GroupSubscribtion>, IGroupSubscribtionRepository
  {
    public GroupSubscribtionRepository(AppDbContext context) 
      : base(context)
    {
    }
  }
}