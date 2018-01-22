namespace Uniquizbit.Services
{
	using Data.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;

  public interface IAnswerService : IService
  {
    Task<Answer> AddAnswerAsync(Answer answer);

    Task<bool> DeleteAnswerAsync(int answerId, string userId);

    Task<IEnumerable<Answer>> FindAnswersByIds(ICollection<int> answersIds);
  }
}