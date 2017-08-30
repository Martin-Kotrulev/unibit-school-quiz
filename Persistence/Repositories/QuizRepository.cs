using App.Models;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class QuizRepository : Repository<Quiz>, IQuizRepository
  {
    public QuizRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}