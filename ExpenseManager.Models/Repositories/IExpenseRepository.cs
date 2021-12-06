using ExpenseManager.Repositories;
using ExpenseManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Models.DTOs.Responses;
using ExpenseManager.Models.Models;

namespace ExpenseManager.Models.Repositories
{
	public interface IExpenseRepository : IRepositoryBase<Expense, int>
	{
		Task<PaginatedData<ExpenseResponse>> GetPaginatedExpensesAsync(int companyId, int categoryId, string catIds, int pageIndex, int pageSize);
        Task<double> GetTotalExpenseAsync(int companyId);
        Task<List<Expense>> GetExpenseReportAsync(int companyId, int[] categories, DateTime start, DateTime end);
        Task BulkInsertAsync(List<Expense> expenseList);
    }
}
