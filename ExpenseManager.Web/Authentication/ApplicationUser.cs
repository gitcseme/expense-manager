using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ExpenseManager.Web.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}