using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email adress is required")]
        [Display(Name = "Email Adress")]
        public string EmailAdress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
