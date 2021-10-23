using System;

namespace ExpenseManager.Web.DTOs.Requests
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

        // public DateTime FormatDateTime()
        // {
        //     var times = Time.Split("-");
        //     return new DateTime(int.Parse(times[0]), int.Parse(times[1]), int.Parse(times[2]));
        // }
    }
}