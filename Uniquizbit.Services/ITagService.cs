namespace Uniquizbit.Services
{
  using Data.Models;
  using System.Threading.Tasks;
  using System.Collections.Generic;

  public interface ITagService : IService
  {
    Task<IEnumerable<Tag>> UpdateTagsAsync(ICollection<string> tags);
  }
}