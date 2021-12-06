using ExpenseManager.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Services
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage emailMessage);
    }
}
