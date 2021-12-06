using ExpenseManager.Models.DTOs.Requests;
using ExpenseManager.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IExpenseService _expenseService;

        public ExpenseController(UserManager<ApplicationUser> userManager, 
            IExpenseService expenseService)
        {
            _userManager = userManager;
            _expenseService = expenseService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ExpenseCreateRequest request)
        {
            try
            {
                var loggedInUser = await GetLoggedInUserAsync();
                await _expenseService.CreateAsync(request.SetExpense(loggedInUser));
                return StatusCode(201, "Expense created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getall/{categoryId}")]
        public async Task<IActionResult> GetAll(int companyId, int categoryId = 0, string catIds="", int pageIndex = 1, int pageSize = 10)
        {
            var pagedExpenses = await _expenseService.GetPaginatedAsync(companyId, categoryId, catIds, pageIndex, pageSize);

            return Ok(JsonConvert.SerializeObject(pagedExpenses, Formatting.Indented));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExpenseRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _expenseService.Update(id, request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _expenseService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("bulk-insert")]
        public async Task<IActionResult> BulkInsert(List<ExpenseCreateRequest> expenses)
        {
            try
            {
                await _expenseService.BulkInsertAsync(expenses);
                return StatusCode(200, "Expenses are created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.PrimarySid);
            var loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            return loggedInUser;
        }
    }
}