using System;
using System.Threading.Tasks;

namespace ExpenseManager.Repositories
{
    public interface IService<T, TKey> : IDisposable where T : class
    {
        Task<T> CreateAsync(T entity);
        Task EditAsync(T entity);
        Task DeleteAsync(T entity);

        T Get(TKey Id);
        Task<T> GetAsync(TKey Id);
    }
}
