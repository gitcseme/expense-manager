using System;

namespace ExpenseManager.Web.DTOs.Responses
{
    public class ExpenseResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string uniqueCode { get; set; }
        public double amount { get; set; }
        public string author { get; set; }
        public string time { get; set; }
        public string description { get; set; }
        public string paymentMode { get; set; }
        public string paymentReference { get; set; }
        public string expenseReferenceId { get; set; }
        public string ricitImageName { get; set; }
        public int categoryId { get; set; }
        public string categoryLabel { get; set; }
    }
}