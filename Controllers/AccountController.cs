using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _api;

        public AccountController(ApiService api)
        {
            _api = api;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var dto = new LoginViewModel
            {
                UserName = vm.UserName,
                Password = vm.Password
            };

            var result = await _api.LoginAsync(dto);

            if (result == null)
            {
                //vm.ErrorMessage = "Invalid username or password";
                return View(vm);
            }

            // Save user info in session
            HttpContext.Session.SetString("UserId", result.UserId.ToString());
            HttpContext.Session.SetString("UserName", result.UserName);

            // Redirect after successful login
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
