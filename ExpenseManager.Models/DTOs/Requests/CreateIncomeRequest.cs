using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Models.DTOs.Requests
{
    public class CreateIncomeRequest
    {
        public double Amount { get; set; }

        public string Time { get; set; }

        public string Author { get; set; }

        public string Comment { get; set; }

        public int CompanyId { get; set; }
    }
}
