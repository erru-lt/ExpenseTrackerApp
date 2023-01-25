using ExpenseTrackerApp.Models;
using ExpenseTrackerApp.Services.AuthenticationService;
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

        public ActionResult Register()
        {
            var registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
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
                return View(nameof(NotFound));
            }
        }
    }
}
