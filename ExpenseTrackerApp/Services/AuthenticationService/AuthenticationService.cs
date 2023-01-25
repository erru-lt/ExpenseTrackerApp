using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerApp.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Register(RegisterViewModel registerViewModel)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(registerViewModel.EmailAdress);

            if (user != null)
            {
                return false;
            }

            var newUser = new IdentityUser()
            {
                UserName = registerViewModel.FullName,
                Email = registerViewModel.EmailAdress,
            };

            IdentityResult userResult = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (userResult.Succeeded)
            {
                await AddRole(user);
                return true;
            }

            return false;
        }

        private async Task AddRole(IdentityUser? user)
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }
    }
}
