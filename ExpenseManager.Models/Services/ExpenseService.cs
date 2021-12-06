using ExpenseManager.Models.DTOs.Requests;
using ExpenseManager.Models.DTOs.Responses;
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.Models;
using ExpenseManager.Models.UnitOfWorks;
using ExpenseManager.Models.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Services
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseUnitOfWork _expenseUnitOfWork;
        private IHttpContextAccessor _HttpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyService _companyService;

        public ExpenseService(IExpenseUnitOfWork expenseUnitOfWork,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager, 
            ICompanyService companyService)
        {
            _expenseUnitOfWork = expenseUnitOfWork;
            _HttpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _companyService = companyService;
        }

        public async Task<Expense> CreateAsync(Expense entity)
        {
            await _expenseUnitOfWork.ExpenseRepository.AddAsync(entity);
            await _expenseUnitOfWork.SaveAsync();
            return entity;
        }

        public async Task DeleteAsync(Expense entity)
        {
            await _expenseUnitOfWork.ExpenseRepository.RemoveAsync(entity);
            await _expenseUnitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _expenseUnitOfWork.Dispose();
        }

        public async Task EditAsync(Expense entity)
        {
            await _expenseUnitOfWork.ExpenseRepository.EditAsync(entity);
            await _expenseUnitOfWork.SaveAsync();
        }

        public Expense Get(int Id)
        {
            return _expenseUnitOfWork.ExpenseRepository.Get(Id);
        }

        public async Task<Expense> GetAsync(int Id)
        {
            return await _expenseUnitOfWork.ExpenseRepository.GetAsync(Id);
        }

        public async Task BulkInsertAsync(List<ExpenseCreateRequest> expenses)
        {
            var loggedInUser = await GetLoggedInUserAsync();
            var expenseList = new List<Expense>();

            foreach (var expenseDto in expenses)
                expenseList.Add(expenseDto.SetExpense(loggedInUser));

            await _expenseUnitOfWork.ExpenseRepository.BulkInsertAsync(expenseList);
            await _expenseUnitOfWork.SaveAsync();
        }

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            var loggedInUserId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid);
            var loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            return loggedInUser;
        }

        public async Task<PaginatedData<ExpenseResponse>> GetPaginatedAsync(int companyId, int categoryId, string catIds, int pageIndex, int pageSize)
        {
            var paginatedData = await _expenseUnitOfWork
                .ExpenseRepository
                .GetPaginatedExpensesAsync(companyId, categoryId, catIds, pageIndex, pageSize);
            return paginatedData;
        }

        public async Task DeleteAsync(int id)
        {
            await _expenseUnitOfWork.ExpenseRepository.RemoveAsync(id);
            await _expenseUnitOfWork.SaveAsync();
        }

        public async Task Update(int id, UpdateExpenseRequest request)
        {
            var expenseToUpdate = await _expenseUnitOfWork.ExpenseRepository.GetAsync(id);
            expenseToUpdate = request.SetExpense(expenseToUpdate);
            await _expenseUnitOfWork.ExpenseRepository.EditAsync(expenseToUpdate);
            await _expenseUnitOfWork.SaveAsync();
        }

        public async Task<double> GetTotalExpenseAsync(int companyId)
        {
            return await _expenseUnitOfWork.ExpenseRepository.GetTotalExpenseAsync(companyId);
        }

        public async Task<List<Expense>> GetExpenseReportAsync(int companyId, int[] categories, System.DateTime start, System.DateTime end)
        {
            return await _expenseUnitOfWork.ExpenseRepository.GetExpenseReportAsync(companyId, categories, start, end);
        }
    }
}
