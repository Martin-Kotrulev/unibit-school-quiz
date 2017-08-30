using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class QuestionRepository : Repository<Question>, IQuestionRepository
  {
    public QuestionRepository(AppDbContext context) 
      : base(context)
    {
    }
  }
}