using ExpenseManager.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models.Entities
{
    public class Expense : EntityBase<int>
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(10)]
        public string UniqueCode { get; set; }

        [Required]
        public double Amount { get; set; }

        [StringLength(50)]
        public string Author { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required, StringLength(50)]
        public string PaymentMode { get; set; }

        [StringLength(100)]
        public string PaymentReference { get; set; }

        [Required, StringLength(100)]
        public string ExpenseReferenceId { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [StringLength(100)]
        public string RicitImageName { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}