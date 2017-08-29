using System;
using System.Threading.Tasks;

namespace App.Persistence.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Declare repository interfaces here
        // IRepository SomeRepository { get; }
        void Complete();

        Task<int> CompleteAsync();
    }
}