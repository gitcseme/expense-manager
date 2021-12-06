namespace ExpenseManager.Models.DTOs.Responses
{
    public class ExpenseResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UniqueCode { get; set; }
        public double Amount { get; set; }
        public string Author { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentReference { get; set; }
        public string ExpenseReferenceId { get; set; }
        public string RicitImageName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryLabel { get; set; }
    }
}