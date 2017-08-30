using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class QuizSubscriptionRepository : Repository<QuizSubscription>, IQuizSubsciptionRepository
  {
    public QuizSubscriptionRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}