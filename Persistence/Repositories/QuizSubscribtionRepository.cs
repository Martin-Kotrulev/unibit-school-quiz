using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class QuizSubscribtionRepository : Repository<QuizSubscribtion>, IQuizSubscribtionRepository
  {
    public QuizSubscribtionRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}