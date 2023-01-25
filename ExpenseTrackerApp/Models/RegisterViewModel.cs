namespace ExpenseTrackerApp.Models
{
    public class RegisterViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string EmailAdress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
