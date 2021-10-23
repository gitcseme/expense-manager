using ExpenseManager.Web.Authentication;
using ExpenseManager.Web.DTOs.Requests;
using ExpenseManager.Web.DTOs.Responses;
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.Services;
using ExpenseManager.Models.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseManager.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private ICategoryService _categoryService { get; }
        private readonly UserManager<ApplicationUser> _userManager;

        public ExpenseController(ICategoryService categoryService, UserManager<ApplicationUser> userManager)
        {
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ExpenseCreateRequest request)
        {
            var loggedInUser = await GetLoggedInUserAsync();

            var expense = new Expense {
                Title = request.Title,
                Amount = request.Amount,
                UniqueCode = GeneralUtilityMethods.GenerateExpenseCode(),
                Time = GeneralUtilityMethods.GetJsConvertedDateTime(request.Time),
                Author = loggedInUser.FullName,
                PaymentMode = request.PaymentMode,
                PaymentReference = request.PaymentReference,
                ExpenseReferenceId = request.ExpenseReferenceId,
                CategoryId = request.CategoryId,
                Description = request.Description,
                RicitImageName = "dummy-image-url",
                CompanyId = request.CompanyId
            };
            
            var createdExpense = await _categoryService.CreateExpenseAsync(expense);
            
            return CreatedAtAction(nameof(Create), new { id = createdExpense.Id }, createdExpense);
        }

        [HttpGet("getall/{categoryId}")]
        public async Task<IActionResult> GetAll(int companyId, int categoryId = 0, int pageIndex = 1, int pageSize = 10)
        {
            var expenseList = await _categoryService.GetPaginatedExpensesAsync(companyId, categoryId, pageIndex, pageSize);
            var response = new List<ExpenseResponse>();
            foreach (var expense in expenseList)
            {
                response.Add(new ExpenseResponse() {
                    id = expense.Id,
                    uniqueCode = expense.UniqueCode,
                    amount = expense.Amount,
                    author = expense.Author,
                    categoryId = expense.CategoryId,
                    categoryLabel = expense.Category.Title,
                    description = expense.Description,
                    paymentMode = expense.PaymentMode,
                    paymentReference = expense.PaymentReference,
                    expenseReferenceId = expense.ExpenseReferenceId,
                    time = GeneralUtilityMethods.GetJSDate(expense.Time),
                    title = expense.Title
                });
            }

            return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExpenseRequest request)
        {
            var expenseToUpdate = await _categoryService.GetExpenseAsync(id);
            if (expenseToUpdate == null) {
                return BadRequest();
            }
            expenseToUpdate.Title = request.Title;
            expenseToUpdate.Amount = request.Amount;
            expenseToUpdate.CategoryId = request.CategoryId;
            expenseToUpdate.Description = request.Description;
            expenseToUpdate.Time = GeneralUtilityMethods.GetJsConvertedDateTime(request.Time);
            expenseToUpdate.PaymentMode = request.PaymentMode;
            expenseToUpdate.PaymentReference = request.PaymentReference;
            expenseToUpdate.ExpenseReferenceId = request.ExpenseReferenceId;

            try {
                await _categoryService.EditExpenseAsync(expenseToUpdate);
            }
            catch (Exception) {
                throw;
            }

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                await _categoryService.DeleteExpenseAsync(id);
            }
            catch (Exception) {
                throw;
            }

            return NoContent();
        }

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.PrimarySid);
            var loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            return loggedInUser;
        }
    }
}