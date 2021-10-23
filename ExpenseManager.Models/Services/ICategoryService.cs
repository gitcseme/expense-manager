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
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<List<Expense>> GetPaginatedExpensesAsync(int companyId, int categoryId, int pageIndex, int pageSize);
        Task<Expense> GetExpenseAsync(int id);
        Task EditExpenseAsync(Expense expenseToUpdate);
        Task DeleteExpenseAsync(int id);
        Task<double> GetTotalExpenseAsync(int companyId);
        Task<List<Expense>> GetExpenseReportAsync(int companyId, int[] categories, DateTime start, DateTime end);
  }
}