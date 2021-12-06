using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models.Authentication.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
  
        [EmailAddress]  
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
  
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}