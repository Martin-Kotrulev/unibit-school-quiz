using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class GroupSubscriptionRepository : Repository<GroupSubscription>, IGroupSubscriptionRepository
  {
    public GroupSubscriptionRepository(AppDbContext context) 
      : base(context)
    {
    }
  }
}