using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokeTestLogin.Data.Entities;
using SmokeTestLogin.Data.Utils;
using SmokeTestLogin.Models;
using SmokeTestLogin.Web.Customs;
using SmokeTestLogin.Web.Services.Interfaces;
using System.Diagnostics;

namespace SmokeTestLogin.Controllers
{
    [MustLogin]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Home = "true";
            _logger.LogInformation("Enter Home");
            var user = await _userService.GetUsersAsync(0, -1);
            return View(user);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Enter Privacy");
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Edit(long id)
        {
            var user = await _userService.FindUserAsync(id);
            if (user is null)
            {
                _logger.LogError("User with Id {} does not exist", id);
                return BadRequest();
            }
            ViewData["user"] = user;
            return PartialView("_Edit", user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            user._passwordUnHash = user.Password;
            user.Password = await SecretHasher.HashAsync(user._passwordUnHash);
            var res = await _userService.UpdateAsync(user);
            if (res == "OK")
            {
                return Json(new { res });
            }
            else
            {
                _logger.LogError("Error: {}", res);
                return BadRequest();
            }
        }
    }
}