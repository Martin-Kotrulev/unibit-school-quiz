namespace Uniquizbit.Services
{
	using Data.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;

  public interface IAnswerService : IService
  {
    Task<bool> DeleteAnswerAsync(int answerId);

    Task<IEnumerable<Answer>> FindAnswersByIds(ICollection<int> answersIds);

    Task<Answer> CreateAnswerAsync(Answer answer);

		Task<IEnumerable<int>> GetRandomOrderForAnswersIdsAsync(int quizId);
  }
}