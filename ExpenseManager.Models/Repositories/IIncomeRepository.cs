using ExpenseManager.Repositories;
using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Models.Models;
using ExpenseManager.Models.DTOs.Responses;

namespace ExpenseManager.Models.Repositories
{
    public interface IIncomeRepository : IRepositoryBase<Income, int>
    {
        Task<PaginatedData<IncomeResponse>> GetPaginatedIncome(int companyId, int pageIndex, int pageSize);
        Task<double> GetTotalIncomeAsync(int companyId);
    }
}
