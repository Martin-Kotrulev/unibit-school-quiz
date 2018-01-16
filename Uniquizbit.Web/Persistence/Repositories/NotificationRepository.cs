using Uniquizbit.Models;
using Uniquizbit.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Uniquizbit.Persistence.Repositories
{
  internal class NotificationRepository : Repository<Notification>, INotificationRepository
  {
    public NotificationRepository(AppDbContext context) 
      : base(context)
    {
    }
  }
}