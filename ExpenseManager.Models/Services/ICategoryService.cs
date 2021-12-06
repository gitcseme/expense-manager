using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpenseManager.Repositories;
using ExpenseManager.Models.Entities;

namespace ExpenseManager.Models.Services
{
    public interface ICategoryService : IService<Category, int>
    {
        Task DeleteAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> filter);
    }
}