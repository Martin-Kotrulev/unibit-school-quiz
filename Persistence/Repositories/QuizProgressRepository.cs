using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class QuizProgressRepository : Repository<QuizProgress>, IQuizProgressRepository
  {
    public QuizProgressRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}