using ExpenseManager.Web.Authentication;
using ExpenseManager.Web.DTOs.Requests;
using ExpenseManager.Web.DTOs.Responses;
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseManager.Web.Controllers
{
  [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService { get; }
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryController(ICategoryService categoryService, UserManager<ApplicationUser> userManager)
        {
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateCategoryRequest request) 
        {
            var category = new Category() {
                Title = request.Title,
                ParentId = request.ParentId,
                CompanyId = request.CompanyId
            };

            var createdCategory = await _categoryService.CreateAsync(category);

            return CreatedAtAction(nameof(Create), new { id = createdCategory.Id}, createdCategory);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int id) 
        {
            var category = await _categoryService.GetAsync(id);
             
            var categoryResponse = new CategoryResponse() {
                id = category.Id,
                parentId = category.ParentId,
                label = category.Title,
                children = new List<CategoryResponse>()
            };

            return Ok(JsonConvert.SerializeObject(categoryResponse, Formatting.Indented));
        }
        
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int companyId) 
        {
            var categories = await _categoryService.GetAllAsync(c => c.CompanyId == companyId);

            var data = categories.Select(c => {
                return new CategoryResponse {
                    id = c.Id,
                    parentId = c.ParentId,
                    label = c.Title,
                    children = new List<CategoryResponse>(),
                    expenses = c.Expenses.Select(e => new RelatedExpenseRespoonse {
                        id = e.Id,
                        amount = e.Amount
                    }).ToList()
                };
            });

            return Ok(JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, UpdateCategoryRequest request) 
        {
            var category = await _categoryService.GetAsync(id);
            if (category == null)
                return NotFound();

            category.Title = request.Title;
            await _categoryService.EditAsync(category);

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