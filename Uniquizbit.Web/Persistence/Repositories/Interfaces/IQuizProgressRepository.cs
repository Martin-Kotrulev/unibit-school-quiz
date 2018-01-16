using System.Collections.Generic;
using System.Threading.Tasks;
using Uniquizbit.Models;

namespace Uniquizbit.Persistence.Repositories.Interfaces
{
	public interface IQuizProgressRepository : IRepository<QuizProgress> 
	{
		Task<IEnumerable<int>> FindUserQuizProgressAnswersIds(int quizId, string userId);

    Task<int> GetProgressAnswersWeightSumAsync(string userId, int quizId);
    
    Task AddProgressAsync(QuizProgress progress, IEnumerable<int> answersIds);
  }
}