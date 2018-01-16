using Uniquizbit.Models;
using Uniquizbit.Persistence.Repositories.Interfaces;

namespace Uniquizbit.Persistence.Repositories
{
  internal class QuizSubscriptionRepository : Repository<QuizSubscription>, IQuizSubscriptionRepository
  {
    public QuizSubscriptionRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}