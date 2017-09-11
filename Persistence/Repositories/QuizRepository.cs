using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Controllers.Resources;
using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class QuizRepository : Repository<Quiz>, IQuizRepository
  {
    public AppDbContext AppDbContext { get { return this.Context as AppDbContext; } }

    public QuizRepository(AppDbContext context)
      : base(context)
    {
        
    }

    public void MarkQuizAsTaken(int quizId, string userId)
    {
      AppDbContext.QuizzesUsers.Add(new QuizzesUsers() 
      {
        QuizId = quizId,
        UserId = userId
      });
    }

    public async Task<int> GetQuizTotalScoreAsync(int quizId)
    {
      return await AppDbContext.Answers
        .Where(a => a.QuizId == quizId && a.IsRight)
        .SumAsync(a => a.Weight);
    }

    public async Task<Quiz> GetQuizWithPasswordAsync(int quizId, string password)
    {
      return await AppDbContext.Quizzes
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
          qg.Title.ToLowerInvariant().Contains(search.ToLowerInvariant()),
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
      return await AppDbContext.Quizzes
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
      return AppDbContext.Quizzes
        .Include(q => q.Participants)
        .FirstOrDefaultAsync(q => q.Id == quizId);
    }

    public bool UserIsQuizCreator(int quizId, string userId)
    {
      return AppDbContext.Quizzes
        .FirstOrDefault(q => q.Id == quizId && q.CreatorId == userId) != null;
    }
  }
}