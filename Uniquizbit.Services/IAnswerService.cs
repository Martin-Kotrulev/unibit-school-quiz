namespace Uniquizbit.Services
{
	using Data.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;

  public interface IAnswerService : IService
  {
    Task<Answer> AddAnswerAsync(Answer answer);

    Task<bool> DeleteAnswerAsync(int answerId);

    Task<IEnumerable<Answer>> FindAnswersByIds(ICollection<int> answersIds);

		Task<IEnumerable<int>> GetRandomOrderForAnswersIdsAsync(int quizId);
  }
}