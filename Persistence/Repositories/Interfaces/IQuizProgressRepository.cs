using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
	public interface IQuizProgressRepository : IRepository<QuizProgress> 
	{
		QuizProgress FindQuizProgress(int quizId, int questionId, string userId);
	}
}