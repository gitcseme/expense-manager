using ExpenseManager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Entities
{
    public class Category : EntityBase<int>
    {
        public Category()
        {
            Children = new List<Category>();
            Expenses = new List<Expense>();
        }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public int? ParentId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Category Parent { get; set; }

        public virtual List<Category> Children { get; set; }
        public virtual List<Expense> Expenses { get; set; }
    }
}
