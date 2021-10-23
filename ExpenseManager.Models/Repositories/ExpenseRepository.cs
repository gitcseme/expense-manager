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
    public class ExpenseRepository : RepositoryBase<Expense, int, DataContext>, IExpenseRepository
    {
        public ExpenseRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<Expense>> GetExpenseReportAsync(int companyId, int[] categories, DateTime start, DateTime end)
        {
            IQueryable<Expense> query = _DbSet
                .Include(e => e.Category)
                .Where(e => e.CompanyId == companyId);

            if (start != end)
                query = query.Where(e => e.Time >= start && e.Time <= end);

            if (categories.Length > 0)
                query = query.Where(e => categories.Any(cid => cid == e.CategoryId));

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<Expense>> GetPaginatedExpensesAsync(int companyId, int categoryId, int pageIndex, int pageSize)
        {
            IQueryable<Expense> query = _DbSet
                .Include(e => e.Category)
                .Where(e => e.CompanyId == companyId);
            if (categoryId != 0)
                query = query.Where(e => e.CategoryId == categoryId);
            
            query = query
                    .OrderByDescending(e => e.Time)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<double> GetTotalExpenseAsync(int companyId)
        {
            IQueryable<Expense> query = _DbSet.Where(i => i.CompanyId == companyId);
            var totalExpense = await query.SumAsync(e => e.Amount);
            return totalExpense;
        }
    }
}
