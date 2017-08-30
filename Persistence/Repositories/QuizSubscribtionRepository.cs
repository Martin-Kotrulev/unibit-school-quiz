using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class QuizSubscribtionRepository : Repository<QuizSubscribtion>, IQuizSubscribtion
  {
    public QuizSubscribtionRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}