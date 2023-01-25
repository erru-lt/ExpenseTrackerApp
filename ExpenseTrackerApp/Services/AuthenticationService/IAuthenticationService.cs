using ExpenseTrackerApp.Models;

namespace ExpenseTrackerApp.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> Register(RegisterViewModel registerViewModel);
    }
}