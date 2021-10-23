using ExpenseManager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Entities
{
    public class Income : EntityBase<int>
    {
        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required, StringLength(50)]
        public string Author { get; set; }

        [StringLength(150)]
        public string Comment { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
