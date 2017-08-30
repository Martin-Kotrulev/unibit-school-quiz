using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class NotificationRepository : Repository<Notification>, INotficationRepository
  {
    public NotificationRepository(AppDbContext context) 
      : base(context)
    {
    }
  }
}