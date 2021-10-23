using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Web.Authentication.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]  
        public string Email { get; set; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}