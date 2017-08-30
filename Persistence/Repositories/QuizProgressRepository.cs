using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  internal class QuizProgressRepository : IQuizProgressRepository
  {
    public QuizProgressRepository(AppDbContext context)
      : base(context)
    {
        
    }
  }
}