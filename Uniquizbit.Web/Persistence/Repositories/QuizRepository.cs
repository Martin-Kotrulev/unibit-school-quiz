using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Uniquizbit.Web.Models;
using Uniquizbit.Models;
using Uniquizbit.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Uniquizbit.Persistence.Repositories
{
  internal class QuizRepository : Repository<Quiz>, IQuizRepository
  {
    public UniquizbitDbContext UniquizbitDbContext { get { return this.Context as UniquizbitDbContext; } }

    public QuizRepository(UniquizbitDbContext context)
      : base(context)
    {
        
    }

    public void MarkQuizAsTaken(int quizId, string userId)
    {
      UniquizbitDbContext.QuizzesUsers.Add(new QuizzesUsers() 
      {
        QuizId = quizId,
        UserId = userId
      });
    }

    public async Task<int> GetQuizTotalScoreAsync(int quizId)
    {
      return await UniquizbitDbContext.Answers
        .Where(a => a.QuizId == quizId && a.IsRight)
        .SumAsync(a => a.Weight);
    }

    public async Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password)
    {
      return await UniquizbitDbContext.Quizzes
        .FirstOrDefaultAsync(q => q.Id == quizId && q.Password == password);
    }

    public async Task<IEnumerable<Quiz>> GetGroupQuizzesPagedAsync(
      int quizGroupId, int page = 1, int pageSize = 10)
    {
      return await ApplyPaging(q => q.GroupId == quizGroupId, page, pageSize);
    }

    public async Task<IEnumerable<Quiz>> GetQuizzesPagedBySearchAsync(
      int page = 1, int pageSize = 10, string search = "")
    {
      return await ApplyPaging(qg =>
          qg.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()),
      page, pageSize);
    }

    public async Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(
      ICollection<string> tags, int page = 1, int pageSize = 10)
    {
      return await ApplyPaging(qg =>
        qg.Tags.Select(t => t.Tag.Name).Any(n => tags.Contains(n)),
      page, pageSize);
    }

    private async Task<IEnumerable<Quiz>> ApplyPaging(
      Expression<Func<Quiz, bool>> predicate, int page, int pageSize)
    {
      return await UniquizbitDbContext.Quizzes
        .Include(qg => qg.Tags)
          .ThenInclude(t => t.Tag)
        .Where(predicate)
        .OrderByDescending(qg => qg.CreatedOn)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }

    public Task<Quiz> GetQuizWithParticipantsAsync(int quizId)
    {
      return UniquizbitDbContext.Quizzes
        .Include(q => q.Participants)
        .FirstOrDefaultAsync(q => q.Id == quizId);
    }

    public bool UserIsQuizCreator(int quizId, string userId)
    {
      return UniquizbitDbContext.Quizzes
        .FirstOrDefault(q => q.Id == quizId && q.CreatorId == userId) != null;
    }
  }
}