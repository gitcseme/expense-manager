using ExpenseManager.Repositories;
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryUnitOfWork _categoryUnitOfWork { get; }

        public CategoryService(ICategoryUnitOfWork categoryUnitOfWork)
        {
            _categoryUnitOfWork = categoryUnitOfWork;
        }

        public async Task<Category> CreateAsync(Category entity)
        {
            await _categoryUnitOfWork.CategoryRepository.AddAsync(entity);
            await _categoryUnitOfWork.SaveAsync();
            return entity;
        }

        public async Task DeleteAsync(Category entity)
        {
            await _categoryUnitOfWork.CategoryRepository.RemoveAsync(entity);
            await _categoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryUnitOfWork.CategoryRepository.RemoveAsync(id);
            await _categoryUnitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _categoryUnitOfWork.Dispose();
        }

        public async Task EditAsync(Category entity)
        {
            await _categoryUnitOfWork.CategoryRepository.EditAsync(entity);
            await _categoryUnitOfWork.SaveAsync();
        }

        public Category Get(int id)
        {
            return _categoryUnitOfWork.CategoryRepository.Get(id);
        }

        public async Task<Category> GetAsync(int Id)
        {
            return await _categoryUnitOfWork.CategoryRepository.GetAsync(Id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryUnitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> filter)
        {
            return await _categoryUnitOfWork.CategoryRepository.GetAllAsync(filter);
        }

		public async Task<Expense> CreateExpenseAsync(Expense expense)
		{
			await _categoryUnitOfWork.ExpenseRepository.AddAsync(expense);
			await _categoryUnitOfWork.SaveAsync();
            return expense;
        }

        public async Task<List<Expense>> GetPaginatedExpensesAsync(int companyId, int categoryId, int pageIndex, int pageSize)
        {
            return await _categoryUnitOfWork.ExpenseRepository.GetPaginatedExpensesAsync(companyId, categoryId, pageIndex, pageSize);
        }

        public async Task<Expense> GetExpenseAsync(int id)
        {
            return await _categoryUnitOfWork.ExpenseRepository.GetAsync(id);
        }

        public async Task EditExpenseAsync(Expense expenseToUpdate)
        {
            await _categoryUnitOfWork.ExpenseRepository.EditAsync(expenseToUpdate);
            await _categoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteExpenseAsync(int id)
        {
            await _categoryUnitOfWork.ExpenseRepository.RemoveAsync(id);
            await _categoryUnitOfWork.SaveAsync();
        }

        public async Task<double> GetTotalExpenseAsync(int companyId)
        {
            return await _categoryUnitOfWork.ExpenseRepository.GetTotalExpenseAsync(companyId);
        }

        public async Task<List<Expense>> GetExpenseReportAsync(int companyId, int[] categories, DateTime start, DateTime end)
        {
            return await _categoryUnitOfWork.ExpenseRepository.GetExpenseReportAsync(companyId, categories, start, end);
        }
    }
}
