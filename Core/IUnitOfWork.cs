using System;

namespace App.Core
{
    public interface IUnitOfWork : IDisposable
    {
        // Declare repository interfaces here
        // IRepository SomeRepository { get; }
        void SaveChanges();
    }
}