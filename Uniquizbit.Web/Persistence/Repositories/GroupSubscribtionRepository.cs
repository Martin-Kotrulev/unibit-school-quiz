using Uniquizbit.Models;
using Uniquizbit.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Uniquizbit.Persistence.Repositories
{
  internal class GroupSubscriptionRepository : Repository<GroupSubscription>, IGroupSubscriptionRepository
  {
    public GroupSubscriptionRepository(AppDbContext context) 
      : base(context)
    {
    }
  }
}