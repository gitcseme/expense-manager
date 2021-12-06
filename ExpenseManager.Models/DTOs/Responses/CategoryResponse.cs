using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Models.DTOs.Responses
{
    public class CategoryResponse
    {
        public int id { get; set; }
        public int? parentId { get; set; }
        public string label { get; set; }
        public List<CategoryResponse> children { get; set; }
        public List<RelatedExpenseRespoonse> expenses { get; set; }
    }

    public class RelatedExpenseRespoonse {
        public int id { get; set; }
        public double amount { get; set; }
    }
}