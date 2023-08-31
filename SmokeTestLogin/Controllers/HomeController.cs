using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokeTestLogin.Logic.Models;
using SmokeTestLogin.Logic.Services.Interfaces;
using SmokeTestLogin.Web.Customs;
using SmokeTestLogin.Web.Models;
using System.Diagnostics;

namespace SmokeTestLogin.Controllers;

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
        var user = await _userService.FindUserByIdAsync(id);
        if (user is not null) return PartialView("_Edit", user);
        _logger.LogError("User with Id {} does not exist", id);
        return BadRequest();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserInfo user)
    {
        var res = await _userService.UpdateAsync(user);
        if (res == "OK")
        {
            return Json(new { res });
        }

        _logger.LogError("Error: {}", res);
        return BadRequest(new { res });
    }

    public IActionResult Add()
    {
        return PartialView("_Add", new UserInfo());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(UserInfo user)
    {
        var res = await _userService.UpdateAsync(user);
        if (res == "OK")
        {
            return Json(new { res });
        }

        _logger.LogError("Error: {}", res);
        return BadRequest();
    }

    public async Task<IActionResult> Search(string param = "")
    {
        if (string.IsNullOrEmpty(param))
            return RedirectToAction("Index");
        var users = await _userService.FindUserByNameAsync(param);
        var userByUserName = await _userService.FindUserByUserNameAsync(param);
        return View("Index", users.Concat(userByUserName));
    }

    public async Task<IActionResult> Delete(long id)
    {
        await _userService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}