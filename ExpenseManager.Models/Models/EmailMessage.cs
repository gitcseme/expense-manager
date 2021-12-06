using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManager.Models.Models
{
	public class EmailMessage
	{
		public EmailMessage()
		{
			ToAddresses = new List<string>();
		}

		public List<string> ToAddresses { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
	}
}
