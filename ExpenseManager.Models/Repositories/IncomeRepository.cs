using ExpenseManager.Repositories;
using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Repositories
{
    public class IncomeRepository : RepositoryBase<Income, int, DataContext>, IIncomeRepository
    {
        public IncomeRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<Income>> GetPaginatedIncome(int companyId, int pageIndex, int pageSize)
        {
            IQueryable<Income> query = _DbSet;
            query = query
                    .Where(i => i.CompanyId == companyId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<double> GetTotalIncomeAsync(int companyId)
        {
            IQueryable<Income> query = _DbSet.Where(i => i.CompanyId == companyId);
            var sum = await query.SumAsync(i => i.Amount);
            return sum;
        }
    }
}
