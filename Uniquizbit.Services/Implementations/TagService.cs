namespace Uniquizbit.Services.Implementations
{
	using Data;
	using Data.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;
  using System.Linq;
  using Microsoft.EntityFrameworkCore;

  public class TagService : ITagService
  {
    private readonly UniquizbitDbContext _dbContext;

    public TagService(UniquizbitDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IEnumerable<Tag>> UpdateTagsAsync(ICollection<string> tags)
    {
      var existingTags = await _dbContext.Tags
				.Where(t => tags.Contains(t.Name))
				.ToListAsync();

			var newTags = tags
					.Where(t => !existingTags.Exists(tag => tag.Name == t))
					.Select(t => new Tag { Name = t });

      await _dbContext.Tags.AddRangeAsync(newTags);
      await _dbContext.SaveChangesAsync();

      existingTags.AddRange(newTags);

      return existingTags;
    }
  }
}