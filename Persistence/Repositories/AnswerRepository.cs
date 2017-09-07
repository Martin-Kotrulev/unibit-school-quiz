using System;
using System.Collections.Generic;
using System.Linq;
using App.Models;
using App.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
  internal class AnswerRepository : Repository<Answer>, IAnswerRepository
  {
    public AppDbContext AppDbContext { get { return Context as AppDbContext; } }
    public AnswerRepository(AppDbContext context) 
      : base(context)
    {
    }

    public IEnumerable<int> GetRandomOrderForAnswerIds(int quizId)
    {
      var rnd = new Random();

      return AppDbContext.Answers
        .Where(a => a.QuestionId == quizId)
        .Select(a => a.Id)
        .OrderBy(a => rnd.Next())
        .ToList();
    }
  }
}