namespace ExpenseManager.Models.Configurations
{
    public interface IEmailConfiguration
    {
        string ApiKey { get; }
        string SenderEmail { get; set; }
        string SenderName { get; set; }
    }
}