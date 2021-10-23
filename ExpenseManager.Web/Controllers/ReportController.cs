using ExpenseManager.Models.Services;
using ExpenseManager.Models.Utilities;
using ExpenseManager.Web.Authentication;
using ExpenseManager.Web.DTOs.Requests;
using ExpenseManager.Web.DTOs.Responses;
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

namespace ExpenseManager.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ICategoryService _categoryService { get; }
        private IWebHostEnvironment _hostEnvironment { get;  }

        public ReportController(UserManager<ApplicationUser> userManager, ICategoryService categoryService, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _categoryService = categoryService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost("expense-report")]
        public async Task<IActionResult> GetExpensesReport(ReportFilterRequest reportFilter)
        {
            var loggedInUser = await GetLoggedInUserAsync();
            var expenseList = await _categoryService.GetExpenseReportAsync(loggedInUser.CompanyId, reportFilter.Categories, reportFilter.StartDate, reportFilter.EndDate);
            var response = new List<ExpenseResponse>();

            foreach (var expense in expenseList)
            {
                response.Add(new ExpenseResponse()
                {
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

        [HttpPost("download-report")]
        public async Task<IActionResult> DownloadReport(ReportFilterRequest reportFilter)
        {
            var loggedInUser = await GetLoggedInUserAsync();
            var expenseList = await _categoryService.GetExpenseReportAsync(loggedInUser.CompanyId, reportFilter.Categories, reportFilter.StartDate, reportFilter.EndDate);
            var data = new List<ExpenseResponse>();
            foreach (var expense in expenseList)
            {
                data.Add(new ExpenseResponse()
                {
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
