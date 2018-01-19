namespace Uniquizbit.Services
{
	using Data.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;

  public interface IQuestionService : IService
  {
		void CreateQuestion(Question question);

		Task<IEnumerable<Question>> GetQuestionsAsync(int quizId, string userId);

		Task<IEnumerable<int>> GetRandomQuestionsOrderAsync(int quizId);

		Task<bool> UserOwnQuestionAsync(int questionId, string userId);

    bool DeleteQuestion(int questionId);
  }
}