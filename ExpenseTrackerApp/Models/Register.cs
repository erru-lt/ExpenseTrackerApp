using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApp.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email adress is required")]
        [Display(Name = "Email Adress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
