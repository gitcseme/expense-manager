
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.Utilities;
using System;

namespace ExpenseManager.Models.DTOs.Requests
{
    public class ExpenseCreateRequest
    {
        public string Title { get; set; }
        public double Amount { get; set; }
        public string Author { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentReference { get; set; }
        public string ExpenseReferenceId { get; set; }
        public string RicitImageName { get; set; }
        public int? CategoryId { get; set; }
        public int? CompanyId { get; set; }

        public Expense SetExpense(ApplicationUser loggedInUser)
        {
            return new Expense()
            {
                Title = this.Title,
                Amount = this.Amount,
                UniqueCode = GeneralUtilityMethods.GenerateExpenseCode(),
                Time = GeneralUtilityMethods.GetJsConvertedDateTime(this.Time),
                Author = loggedInUser.FullName,
                PaymentMode = this.PaymentMode,
                PaymentReference = this.PaymentReference,
                ExpenseReferenceId = this.ExpenseReferenceId,
                CategoryId = this.CategoryId.Value,
                Description = this.Description,
                RicitImageName = "dummy-image-url",
                CompanyId = loggedInUser.CompanyId
            };
        }
    }
}