using ExpenseManager.Repositories;
using ExpenseManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Services
{
    public interface IIncomeService : IService<Income, int>
    {
        Task<List<Income>> GetPaginatedIncomeAsync(int companyId, int pageIndex, int pageSize);
        Task<double> GetTotalIncomeAsync(int companyId);
  }
}
