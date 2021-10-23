using ExpenseManager.Repositories;
using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category, int>
    {
        Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> filter);
    }
}
