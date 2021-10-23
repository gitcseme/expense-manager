using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Web.DTOs.Requests
{
    public class UpdateIncomeRequest
    {
        public double Amount { get; set; }

        public string Time { get; set; }

        public string Comment { get; set; }

        public DateTime FormatDateTime()
        {
            var times = Time.Split("-");
            return new DateTime(int.Parse(times[0]), int.Parse(times[1]), int.Parse(times[2]));
        }
    }
}
