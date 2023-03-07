using ExpenseTrackerApp.Models;

namespace ExpenseTrackerApp.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> Login(Login loginViewModel);
        Task<bool> Register(Register registerViewModel);
        Task Logout();
    }
}