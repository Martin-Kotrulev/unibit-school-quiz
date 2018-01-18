using Uniquizbit.Models;
using Uniquizbit.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Uniquizbit.Persistence.Repositories
{
  internal class GroupSubscriptionRepository : Repository<GroupSubscription>, IGroupSubscriptionRepository
  {
    public GroupSubscriptionRepository(UniquizbitDbContext context) 
      : base(context)
    {
    }
  }
}