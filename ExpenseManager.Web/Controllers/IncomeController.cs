using ExpenseManager.Web.Authentication;
using ExpenseManager.Web.DTOs.Requests;
using ExpenseManager.Web.DTOs.Responses;
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.Services;
using ExpenseManager.Models.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseManager.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private IIncomeService _incomeService;
        private ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IncomeController(IIncomeService incomeService, ICategoryService categoryService, UserManager<ApplicationUser> userManager)
        {
            _incomeService = incomeService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int companyId, int pageIndex = 1, int pageSize = 10)
        {
            var incomeList = await _incomeService.GetPaginatedIncomeAsync(companyId, pageIndex, pageSize);
            var response = new List<IncomeResponse>();
            foreach (var income in incomeList)
            {
                response.Add(new IncomeResponse()
                {
                    Id = income.Id,
                    Amount = income.Amount,
                    Author = income.Author,
                    Time = GeneralUtilityMethods.GetJSDate(income.Time),
                    Comment = income.Comment
                });
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateIncomeRequest request)
        {
            var loggedInUser = await GetLoggedInUserAsync();

            var income = new Income()
            {
                Amount = request.Amount,
                Time =  GeneralUtilityMethods.GetJsConvertedDateTime(request.Time), //request.GetDate(),
                Comment = request.Comment,
                Author = loggedInUser.FullName,
                CompanyId = request.CompanyId
            };

            var createdIncome = await _incomeService.CreateAsync(income);

            return CreatedAtAction(nameof(Create), new { id = createdIncome.Id }, createdIncome);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateIncomeRequest request)
        {
            var income = await _incomeService.GetAsync(id);
            if (income == null) {
                return NotFound();
            }
            income.Amount = request.Amount;
            income.Time = GeneralUtilityMethods.GetJsConvertedDateTime(request.Time);
            income.Comment = request.Comment;

            await _incomeService.EditAsync(income);

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var income = await _incomeService.GetAsync(id);
                await _incomeService.DeleteAsync(income);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("balance")]
        public async Task<double> GetBalance(int companyId)
        {
            var totalIncome = await _incomeService.GetTotalIncomeAsync(companyId);
            var totalExpense = await _categoryService.GetTotalExpenseAsync(companyId);
            var balance = totalIncome - totalExpense;
            return balance;
        }

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.PrimarySid);
            var loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            return loggedInUser;
        }
    }
}
