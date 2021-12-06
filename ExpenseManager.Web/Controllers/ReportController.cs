using ExpenseManager.Models;
using ExpenseManager.Models.Services;
using ExpenseManager.Models.Utilities;
using ExpenseManager.Models.Authentication;
using ExpenseManager.Models.DTOs.Requests;
using ExpenseManager.Models.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ICategoryService _categoryService { get; }
        private IWebHostEnvironment _hostEnvironment { get;  }
        private readonly IExpenseService _expenseService;

        public ReportController(UserManager<ApplicationUser> userManager,
            IWebHostEnvironment hostEnvironment, 
            IExpenseService expenseService)
        {
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
            _expenseService = expenseService;
        }

        [HttpPost("expense-report")]
        public async Task<IActionResult> GetExpensesReport(ReportFilterRequest reportFilter)
        {
            var loggedInUser = await GetLoggedInUserAsync();
            var expenseList = await _expenseService.GetExpenseReportAsync(loggedInUser.CompanyId, reportFilter.Categories, reportFilter.StartDate, reportFilter.EndDate);
            var response = new List<ExpenseResponse>();

            foreach (var expense in expenseList)
            {
                response.Add(new ExpenseResponse()
                {
                    Id = expense.Id,
                    UniqueCode = expense.UniqueCode,
                    Amount = expense.Amount,
                    Author = expense.Author,
                    CategoryId = expense.CategoryId,
                    CategoryLabel = expense.Category.Title,
                    Description = expense.Description,
                    PaymentMode = expense.PaymentMode,
                    PaymentReference = expense.PaymentReference,
                    ExpenseReferenceId = expense.ExpenseReferenceId,
                    Time = GeneralUtilityMethods.GetJSDate(expense.Time),
                    Title = expense.Title
                });
            }

            return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
        }

        [HttpPost("download-report")]
        public async Task<IActionResult> DownloadReport(ReportFilterRequest reportFilter)
        {
            var loggedInUser = await GetLoggedInUserAsync();
            var expenseList = await _expenseService.GetExpenseReportAsync(loggedInUser.CompanyId, reportFilter.Categories, reportFilter.StartDate, reportFilter.EndDate);
            var data = new List<ExpenseResponse>();
            foreach (var expense in expenseList)
            {
                data.Add(new ExpenseResponse()
                {
                    Id = expense.Id,
                    UniqueCode = expense.UniqueCode,
                    Amount = expense.Amount,
                    Author = expense.Author,
                    CategoryId = expense.CategoryId,
                    CategoryLabel = expense.Category.Title,
                    Description = expense.Description,
                    PaymentMode = expense.PaymentMode,
                    PaymentReference = expense.PaymentReference,
                    ExpenseReferenceId = expense.ExpenseReferenceId,
                    Time = GeneralUtilityMethods.GetJSDate(expense.Time),
                    Title = expense.Title
                });
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var excelPackage = new ExcelPackage())
                {
                    var workSheet = excelPackage.Workbook.Worksheets.Add("Expense Report");
                    var rowRange = workSheet.Cells["A1"].LoadFromCollection(data, true);
                    rowRange.AutoFitColumns();

                    string path = Path.Combine(_hostEnvironment.WebRootPath, "reports", "expense-report.xlsx");
                    var file = new FileInfo(path);
                    if (file.Exists)
                        file.Delete();

                    excelPackage.SaveAs(file);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-file")]
        public async Task<IActionResult> GetFile()
        {
            try
            {
                string path = Path.Combine(_hostEnvironment.WebRootPath, "reports", "expense-report.xlsx");
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);
                return File(bytes, "application/octet-stream", "expense-report.xlsx");
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
