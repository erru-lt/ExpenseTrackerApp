using ExpenseTrackerApp.Models;
using ExpenseTrackerApp.Services.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var loginViewModel = new Login();
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login loginViewModel)
        {
            if(ModelState.IsValid == false)
            {
                return View(loginViewModel);
            }

            var result = await _authenticationService.Login(loginViewModel);

            if(result)
            {
                return RedirectToAction("Index", nameof(Transaction));
            }
            else
            {
                TempData["Error"] = "Unable to log in";
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var registerViewModel = new Register();
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register registerViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var result = await _authenticationService.Register(registerViewModel);

            if(result)
            {
                return View("RegisterCompleted");
            }
            else
            {
                TempData["Error"] = "Unable to create user. Try another email adress or come back later";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
