using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokeTestLogin.Logic.Models;
using SmokeTestLogin.Logic.Services.Interfaces;
using SmokeTestLogin.Web.Customs;
using SmokeTestLogin.Web.Models;

namespace SmokeTestLogin.Controllers;

[MustLogin]
public class HomeController(ILogger<HomeController> logger, IUserService userService) : Controller
{
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
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }

    public async Task<IActionResult> Edit(long id)
    {
        var user = await userService.FindUserByIdAsync(id);
        if (user is not null)
            return PartialView("_Edit", user);
        logger.LogError("User with Id {} does not exist", id);
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserInfo user)
    {
        var res = await userService.UpdateAsync(user);
        if (res == "OK")
        {
            return Json(new { res });
        }

        logger.LogError("Error: {}", res);
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
        var res = await userService.UpdateAsync(user);
        if (res == "OK")
        {
            return Json(new { res });
        }

        logger.LogError("Error: {}", res);
        return BadRequest();
    }

    public async Task<IActionResult> Delete(long id)
    {
        await userService.DeleteAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> GetTable(int index, int size, string name = "")
    {
        var result = await userService.GetUsersAsync(index, size, name);
        var count = await userService.CountUsers();
        return PartialView("_UserList", new UserList(result.ToList(), index, count));
    }
}
