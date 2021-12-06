using ExpenseManager.Models.DTOs.Requests;
using ExpenseManager.Models.DTOs.Responses;
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.Models;
using ExpenseManager.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Services
{
    public interface IExpenseService : IService<Expense, int>
    {
        Task BulkInsertAsync(List<ExpenseCreateRequest> expenses);
        Task<PaginatedData<ExpenseResponse>> GetPaginatedAsync(int companyId, int categoryId, string catIds, int pageIndex, int pageSize);
        Task DeleteAsync(int id);
        Task Update(int id, UpdateExpenseRequest request);
        Task<double> GetTotalExpenseAsync(int companyId);
        Task<List<Expense>> GetExpenseReportAsync(int companyId, int[] categories, DateTime start, DateTime end);
    }
}
