using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<IEnumerable<Quiz>> GetGroupQuizzesPagedAsync(int quizId, int page = 1, int pageSize = 10)
    {
      return await AppDbContext.Quizzes
        .Where(q => q.QuizGroupId != null && q.QuizGroupId == quizId)
        .OrderBy(q => q.CreatedOn)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }

    public void MarkQuizAsTaken(int quizId, string userId)
    {
      AppDbContext.QuizzesUsers.Add(new QuizzesUsers() 
      {
        QuizId = quizId,
        UserId = userId
      });
    }

    public async Task<IEnumerable<Quiz>> SearchQuizzesByTagsAsync(ICollection<string> tags)
    {
      return await AppDbContext.Quizzes
        .Where(q => q.Tags
          .Select(t => t.Name)
          .Any(t => tags.Contains(t))
        )
        .ToListAsync();
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
  }
}