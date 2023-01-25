using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerApp.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Login(LoginViewModel loginViewModel)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(loginViewModel.EmailAdress);

            if(user == null)
            {
                return false;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

            if(passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                if(result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
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
                await AddRole(newUser);
                return true;
            }

            return false;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task AddRole(IdentityUser? user)
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }
    }
}
