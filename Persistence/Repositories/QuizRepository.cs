using System;
using System.Collections.Generic;
using System.Linq;
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

    public IEnumerable<Quiz> GetGroupQuizzesPaged(int quizId, int page = 1, int pageSize = 10)
    {
      return AppDbContext.Quizzes
        .Where(q => q.QuizGroupId != null && q.QuizGroupId == quizId)
        .OrderBy(q => q.CreatedOn)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public void MarkQuizAsTaken(int quizId, string userId)
    {
      AppDbContext.QuizzesUsers.Add(new QuizzesUsers() 
      {
        QuizId = quizId,
        UserId = userId
      });
    }
  }
}