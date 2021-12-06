using ExpenseManager.Repositories;
using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Models.Models;
using ExpenseManager.Models.Utilities;
using ExpenseManager.Models.DTOs.Responses;

namespace ExpenseManager.Models.Repositories
{
    public class IncomeRepository : RepositoryBase<Income, int, DataContext>, IIncomeRepository
    {
        public IncomeRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<PaginatedData<IncomeResponse>> GetPaginatedIncome(int companyId, int pageIndex, int pageSize)
        {
            IQueryable<Income> query = _DbSet;
            query = query
                    .Where(i => i.CompanyId == companyId);

            int totalItems = await query.CountAsync();

            var result = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(i => new IncomeResponse() 
                    {
                        Id = i.Id,
                        Amount = i.Amount,
                        Author = i.Author,
                        Time = GeneralUtilityMethods.GetJSDate(i.Time),
                        Comment = i.Comment
                    })
                    .AsNoTracking()
                    .ToListAsync();

            var pagedData = new PaginatedData<IncomeResponse>(result, pageIndex, pageSize, totalItems);

            return pagedData;
        }

        public async Task<double> GetTotalIncomeAsync(int companyId)
        {
            IQueryable<Income> query = _DbSet.Where(i => i.CompanyId == companyId);
            var sum = await query.SumAsync(i => i.Amount);
            return sum;
        }
    }
}
