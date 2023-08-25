using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokeTestLogin.Web.Models;
using SmokeTestLogin.Web.Services.Interfaces;

namespace SmokeTestLogin.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        public IActionResult LoginForm(string? returnUrl)
        {
            return View(new UserModel { ReturnUrl = returnUrl ?? "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginForm(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _loginService.LoginAsync(model))
            {
                _logger.LogInformation("Login success");
                return !string.IsNullOrEmpty(model.ReturnUrl) ? Redirect(model.ReturnUrl) : RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("login", "UserName or Password is Invalid");
                return View(model);
            }
        }
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("User logout");
            await _loginService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
