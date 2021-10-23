using ExpenseManager.Repositories;
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Services
{
    public class IncomeService : IIncomeService
    {
        private IIncomeUnitOfWork _incomeUnitOfWork { get; }

        public IncomeService(IIncomeUnitOfWork incomeUnitOfWork)
        {
            _incomeUnitOfWork = incomeUnitOfWork;
        }

        public async Task<Income> CreateAsync(Income entity)
        {
            await _incomeUnitOfWork.IncomeRepository.AddAsync(entity);
            await _incomeUnitOfWork.SaveAsync();
            return entity;
        }

        public async Task DeleteAsync(Income entity)
        {
            await _incomeUnitOfWork.IncomeRepository.RemoveAsync(entity);
            await _incomeUnitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _incomeUnitOfWork.Dispose();
        }

        public async Task EditAsync(Income entity)
        {
            await _incomeUnitOfWork.IncomeRepository.EditAsync(entity);
            await _incomeUnitOfWork.SaveAsync();
        }

        public Income Get(int id)
        {
            return _incomeUnitOfWork.IncomeRepository.Get(id);
        }

        public async Task<Income> GetAsync(int id)
        {
            return await _incomeUnitOfWork.IncomeRepository.GetAsync(id);
        }

        public async Task<List<Income>> GetPaginatedIncomeAsync(int companyId, int pageIndex, int pageSize)
        {
            return await _incomeUnitOfWork.IncomeRepository.GetPaginatedIncome(companyId, pageIndex, pageSize);
        }

        public async Task<double> GetTotalIncomeAsync(int companyId)
        {
            return await _incomeUnitOfWork.IncomeRepository.GetTotalIncomeAsync(companyId);
        }
    }
}
