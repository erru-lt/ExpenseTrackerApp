using ExpenseTrackerApp.Models;

namespace ExpenseTrackerApp.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> Login(LoginViewModel loginViewModel);
        Task<bool> Register(RegisterViewModel registerViewModel);
        Task Logout();
    }
}