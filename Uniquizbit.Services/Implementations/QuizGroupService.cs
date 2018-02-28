namespace Uniquizbit.Services.Implementations
{
  using Data;
  using Data.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using System.Linq;
  using Microsoft.EntityFrameworkCore;
  using System;
  using System.Linq.Expressions;

  public class QuizGroupService : IQuizGroupService
  {
    private readonly UniquizbitDbContext _dbContext;

    public QuizGroupService(UniquizbitDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<QuizGroup> AddQuizGroupAsync(QuizGroup quizGroup)
    {
      await _dbContext.QuizGroups.AddAsync(quizGroup);
      await _dbContext.SaveChangesAsync();

      return quizGroup;
    }

    public async Task<bool> DeleteQuizGroupAsync(int groupId, string userId)
    {
      var quizGroup = await _dbContext.QuizGroups
        .FindAsync(groupId);

      if (quizGroup == null || quizGroup.CreatorId != userId)
        return false;

      _dbContext.QuizGroups.Remove(quizGroup);
      await _dbContext.SaveChangesAsync();

      return true;
    }

    public async Task<QuizGroup> FindGroupByIdAsync(int groupId)
      => await _dbContext.QuizGroups.FindAsync(groupId);

    public async Task<IEnumerable<Quiz>> GetGroupQuizzesAsync(int quizGroupId, int page = 1, int pageSize = 10)
      => await _dbContext.Quizzes
          .Where(q => q.GroupId == quizGroupId)
          .OrderByDescending(q => q.CreatedOn)
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();

    public async Task<IEnumerable<QuizGroup>> GetQuizGroupsAsync(
      int page = 1, int pageSize = 10, string search = "")
      => await ApplyPaging(qg =>
        qg.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()),
        page,
        pageSize);

    public async Task<IEnumerable<QuizGroup>> GetUserOwnGroupsAsync(
      string userId, int page = 1, int pageSize = 10)
      => await ApplyPaging(qg => qg.CreatorId == userId,
        page,
        pageSize);

    public async Task<bool> QuizGroupExistsAsync(string groupName)
      => await _dbContext.QuizGroups
          .FirstOrDefaultAsync(qg => qg.Name == groupName) != null;

    public async Task<bool> QuizGroupExistsAsync(int groupId)
      => await _dbContext.QuizGroups.FindAsync(groupId) != null;

    public async Task<IEnumerable<QuizGroup>> SearchQuizGroupsByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10)
      => await ApplyPaging(qg =>
          qg.Tags
            .Select(t => t.Tag.Name)
            .Any(t => tags.Contains(t)),
          page,
          pageSize);

    public void Subscribe(GroupSubscription subscription)
    {
      throw new System.NotImplementedException();
    }

    public async Task<bool> UserCanAddQuizzesToGroupAsync(int groupId, string userId)
    {
      var group = await _dbContext.QuizGroups.FindAsync(groupId);
      return group != null && group.CreatorId == userId;
    }

    private async Task<IEnumerable<QuizGroup>> ApplyPaging(
      Expression<Func<QuizGroup, bool>> predicate, int page, int pageSize)
      => await _dbContext.QuizGroups
        .Include(qg => qg.Tags)
          .ThenInclude(t => t.Tag)
        .Where(predicate)
        .OrderByDescending(qg => qg.CreatedOn)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
  }
}