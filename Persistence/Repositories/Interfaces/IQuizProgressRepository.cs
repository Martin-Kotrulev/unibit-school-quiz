using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
	public interface IQuizProgressRepository : IRepository<QuizProgress> 
	{
		Task<QuizProgress> FindQuizProgressAsync(int quizId, int questionId, string userId);

    Task<int> GetProgressAnswersWeightSumAsync(string userId, int quizId);
    
    Task AddProgressAsync(QuizProgress progress, IEnumerable<int> answersIds);
  }
}