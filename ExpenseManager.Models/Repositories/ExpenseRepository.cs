using ExpenseManager.Repositories;
using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Models.DTOs.Responses;
using ExpenseManager.Models.Utilities;
using ExpenseManager.Models.Models;

namespace ExpenseManager.Models.Repositories
{
    public class ExpenseRepository : RepositoryBase<Expense, int, DataContext>, IExpenseRepository
    {
        public ExpenseRepository(DataContext dataContext) : base(dataContext) { }

        public async Task BulkInsertAsync(List<Expense> expenseList)
        {
            await _dbContext.Expenses.AddRangeAsync(expenseList);
        }

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

        public async Task<PaginatedData<ExpenseResponse>> GetPaginatedExpensesAsync(int companyId, int categoryId, string catIds, int pageIndex, int pageSize)
        {
            var categoryIdList = catIds.Split(",").Select(id => int.Parse(id)).ToList();

            IQueryable<Expense> query = _DbSet;

            query = query
                .Where(e => e.CompanyId == e.CompanyId)
                .OrderByDescending(e => e.Time);

            int totalItems = await query.AsNoTracking().CountAsync();
            
            if (categoryIdList.Count > 0 && categoryId != 0)
                query = query.Where(e => categoryIdList.Contains(e.CategoryId));


            var result = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(exp => new ExpenseResponse()
                    {
                        Id = exp.Id,
                        UniqueCode = exp.UniqueCode,
                        Amount = exp.Amount,
                        Author = exp.Author,
                        CategoryId = exp.CategoryId,
                        CategoryLabel = exp.Category.Title,
                        Description = exp.Description,
                        PaymentMode = exp.PaymentMode,
                        PaymentReference = exp.PaymentReference,
                        ExpenseReferenceId = exp.ExpenseReferenceId,
                        Time = GeneralUtilityMethods.GetJSDate(exp.Time),
                        Title = exp.Title
                    })
                    .AsNoTracking()
                    .ToListAsync();

            var pagedData = new PaginatedData<ExpenseResponse>(result, pageIndex, pageSize, totalItems);
            return pagedData;
        }

        public async Task<double> GetTotalExpenseAsync(int companyId)
        {
            IQueryable<Expense> query = _DbSet.Where(i => i.CompanyId == companyId);
            var totalExpense = await query.SumAsync(e => e.Amount);
            return totalExpense;
        }
    }
}
