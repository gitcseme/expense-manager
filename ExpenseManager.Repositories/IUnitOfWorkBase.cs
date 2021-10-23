using System;
using System.Threading.Tasks;

namespace ExpenseManager.Repositories
{
    public interface IUnitOfWorkBase : IDisposable
    {
        Task SaveAsync();
        void Save();
    }
}
