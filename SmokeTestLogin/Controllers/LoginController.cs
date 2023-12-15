using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokeTestLogin.Logic.Models;
using SmokeTestLogin.Logic.Services.Interfaces;

namespace SmokeTestLogin.Web.Controllers;

[AllowAnonymous]
public class LoginController(ILoginService loginService, ILogger<LoginController> logger)
    : Controller
{
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

        if (await loginService.LoginAsync(model))
        {
            logger.LogInformation("Login success");
            return !string.IsNullOrEmpty(model.ReturnUrl)
                ? Redirect(model.ReturnUrl)
                : RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("login", "UserName or Password is Invalid");
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        logger.LogInformation("User logout");
        await loginService.LogoutAsync();
        return RedirectToAction("LoginForm");
    }
}