namespace Uniquizbit.Persistence.Repositories
{
  using Data;
  using Interfaces;
  using System.Threading.Tasks;

  public class UnitOfWork : IUnitOfWork
  {
    private readonly UniquizbitDbContext _context;

    public UnitOfWork(UniquizbitDbContext context)
    {
      this._context = context;
    }

    public void Complete()
    {
      _context.SaveChanges();
    }

    public async Task<int> CompleteAsync()
    {
      return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
      _context.Dispose();
    }

  }
}