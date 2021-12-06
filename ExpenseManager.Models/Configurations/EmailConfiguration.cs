using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManager.Models.Configurations
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
