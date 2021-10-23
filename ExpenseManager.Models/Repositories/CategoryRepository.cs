using ExpenseManager.Repositories;
using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Repositories
{
    public class CategoryRepository : RepositoryBase<Category, int, DataContext>, ICategoryRepository
    {
        public CategoryRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> filter)
        {
            return await Task.Run(() => {
                IQueryable<Category> data = _DbSet.Include(c => c.Expenses);

                if (filter != null)
                    data = data.Where(filter);
                
                return data;
            });
        }
    }
}
