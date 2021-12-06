using ExpenseManager.Models.Configurations;
using ExpenseManager.Models.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly IEmailConfiguration _EmailConfiguration;

        public SendGridEmailSender(IEmailConfiguration emailConfiguration)
        {
            _EmailConfiguration = emailConfiguration;
        }

        public async Task SendAsync(EmailMessage emailMessage)
        {
            var from = new EmailAddress(_EmailConfiguration.SenderEmail, _EmailConfiguration.SenderName);
            List<EmailAddress> to = emailMessage.ToAddresses.Select(toAddress => new EmailAddress(toAddress)).ToList();
            var message = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, emailMessage.Subject, string.Empty, emailMessage.Content);
            SendGridClient emailClient = new SendGridClient(_EmailConfiguration.ApiKey);
            await emailClient.SendEmailAsync(message);
        }
    }
}
