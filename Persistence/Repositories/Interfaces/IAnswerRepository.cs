using System.Collections.Generic;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IAnswerRepository : IRepository<Answer>
  {
    IEnumerable<int> GetRandomOrderForAnswerIds(int quizId);
  }
}