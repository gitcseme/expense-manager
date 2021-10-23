using ExpenseManager.Repositories;
using ExpenseManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Repositories
{
	public interface IExpenseRepository : IRepositoryBase<Expense, int>
	{
		Task<List<Expense>> GetPaginatedExpensesAsync(int companyId, int categoryId, int pageIndex, int pageSize);
        Task<double> GetTotalExpenseAsync(int companyId);
        Task<List<Expense>> GetExpenseReportAsync(int companyId, int[] categories, DateTime start, DateTime end);
    }
}
