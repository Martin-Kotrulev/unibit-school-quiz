using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class QuizGroupRepository : Repository<QuizGroup>, IQuizGroupRepository
  {
    public QuizGroupRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}