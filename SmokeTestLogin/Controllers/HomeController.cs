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

    public IActionResult Index()
    {
        ViewBag.Home = "true";
        return View();
    }

    public IActionResult Privacy()
    {
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
        return NotFound();
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

    public async Task<IActionResult> Delete(long id)
    {
        await _userService.DeleteAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> GetTable(int index, int size, string name = "")
    {
        var result = await _userService.GetUsersAsync(index, size, name);
        var count = await _userService.CountUsers();
        return PartialView("_UserList", new UserList(result.ToList(), index, count));
    }
}