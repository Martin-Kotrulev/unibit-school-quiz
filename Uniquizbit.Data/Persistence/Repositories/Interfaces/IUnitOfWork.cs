namespace Uniquizbit.Persistence.Repositories.Interfaces
{
  using System;
  using System.Threading.Tasks;

  public interface IUnitOfWork : IDisposable
  {
    // Declare repository interfaces here
    // IRepository SomeRepository { get; }

    void Complete();

    Task<int> CompleteAsync();
  }
}