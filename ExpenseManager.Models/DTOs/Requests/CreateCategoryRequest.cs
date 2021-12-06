using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Models.DTOs.Requests
{
    public class CreateCategoryRequest 
    {
        public string Title { get; set; }

        public int CompanyId { get; set; }

        public int? ParentId { get; set; }
    }
}