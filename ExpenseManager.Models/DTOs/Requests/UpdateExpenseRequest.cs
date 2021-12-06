using ExpenseManager.Models.Entities;
using ExpenseManager.Models.Utilities;
using System;

namespace ExpenseManager.Models.DTOs.Requests
{
    public class UpdateExpenseRequest
    {
        public string Title { get; set; }
        public double Amount { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentReference { get; set; }
        public string ExpenseReferenceId { get; set; }
        public string RicitImageName { get; set; }
        public int CategoryId { get; set; }

        public Expense SetExpense(Expense expenseToUpdate)
        {
            expenseToUpdate.Title = this.Title;
            expenseToUpdate.Amount = this.Amount;
            expenseToUpdate.CategoryId = this.CategoryId;
            expenseToUpdate.Description = this.Description;
            expenseToUpdate.Time = GeneralUtilityMethods.GetJsConvertedDateTime(this.Time);
            expenseToUpdate.PaymentMode = this.PaymentMode;
            expenseToUpdate.PaymentReference = this.PaymentReference;
            expenseToUpdate.ExpenseReferenceId = this.ExpenseReferenceId;
            return expenseToUpdate;
        }
    }
}