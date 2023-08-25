using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var user = await _userService.GetUsersAsync();
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
    }
}