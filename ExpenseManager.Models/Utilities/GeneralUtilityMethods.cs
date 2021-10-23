using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Utilities
{
    public static class GeneralUtilityMethods
    {
        public static string GenerateExpenseCode()
        {
            Random random = new Random();
            string fourDigitNumber = (random.Next(10000, 99999)).ToString();
            string twoLetter = "" + (char)('A' + random.Next(0, 25)) + (char)('A' + random.Next(0, 25));
            string code = twoLetter + fourDigitNumber;
            return code;
        }

        public static DateTime GetJsConvertedDateTime(string date)
        {
            var times = date.Split("-");
            return new DateTime(int.Parse(times[0]), int.Parse(times[1]), int.Parse(times[2]));
        }

        public static string GetJSDate(DateTime date)
        {
            string month = date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();
            string day = date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();

            return $"{date.Year}-{month}-{day}";
        }
    }
}
