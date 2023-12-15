using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmokeTestLogin.Logic.Models;
using SmokeTestLogin.Logic.Services.Interfaces;
using SmokeTestLogin.Web.Controllers;

namespace SmokeTestLogin.Test;

[TestClass]
public class UserFunctionTest
{
    private readonly ControllerContext _controllerContext;
    private readonly HttpContext _httpCtx;
    private readonly ILogger<HomeController> _logger;
    private readonly Mock<IUserService> _mockUsers;

    public UserFunctionTest()
    {
        Mock<HttpRequest> httpRequest = new();
        httpRequest.Setup(x => x.Scheme).Returns("http");
        httpRequest
            .Setup(x => x.Host)
            .Returns(HostString.FromUriComponent("https://localhost:7296"));
        httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Home"));

        _httpCtx = Mock.Of<HttpContext>(x => x.Request == httpRequest.Object);
        _logger = new Mock<ILogger<HomeController>>().Object;
        _mockUsers = new Mock<IUserService>();
        _controllerContext = new ControllerContext { HttpContext = _httpCtx };
    }

    [TestMethod]
    public async Task Test_Edit_User()
    {
        var model = new UserInfo
        {
            Id = 1,
            Name = "Asdsa",
            Password = "asdasasdfas",
            UserName = "qqq",
            IsActivated = true
        };
        _mockUsers.Setup(x => x.FindUserByIdAsync(100)).ReturnsAsync(model);
        _mockUsers.Setup(x => x.UpdateAsync(model)).ReturnsAsync("OK");

        // setup actual Controller
        var homeController = new HomeController(_logger, _mockUsers.Object)
        {
            ControllerContext = _controllerContext
        };

        // begin test
        var view = await homeController.Edit(100) as PartialViewResult;

        Assert.IsTrue(view!.ViewName == "_Edit");

        var result = await homeController.Edit(model) as JsonResult;
        const string expect = "{ res = OK }";
        Assert.IsTrue(result!.Value != null && result.Value.ToString() == expect);
    }

    [TestMethod]
    public async Task Test_Add_User()
    {
        var model = new UserInfo
        {
            Id = 1,
            Name = "Asdsa",
            Password = "asdasasdfas",
            UserName = "qqq",
            IsActivated = true
        };
        _mockUsers.Setup(x => x.UpdateAsync(model)).ReturnsAsync("OK");

        // setup actual Controller
        var homeController = new HomeController(_logger, _mockUsers.Object)
        {
            ControllerContext = _controllerContext
        };

        // begin test
        var view = homeController.Add() as PartialViewResult;
        Assert.IsTrue(view!.ViewName == "_Add");

        var result = await homeController.Add(model) as JsonResult;
        const string expected = "{ res = OK }";
        Assert.AreEqual(expected, result!.Value!.ToString());
    }

    [TestMethod]
    public async Task Test_Search_User()
    {
        var model = new UserInfo
        {
            Id = 1,
            Name = "Asdsa",
            Password = "asdasasdfas",
            UserName = "qqq",
            IsActivated = true
        };
        const string searchStr = "blabalab";
        _mockUsers.Setup(x => x.FindUserByNameAsync(searchStr)).ReturnsAsync(new[] { model });

        // setup actual Controller
        var homeController = new HomeController(_logger, _mockUsers.Object)
        {
            ControllerContext = _controllerContext
        };

        // begin test
        var result = await homeController.GetTable(0, 15, searchStr) as PartialViewResult;
        Assert.IsTrue(result!.ViewName == "_UserList");
    }

    [TestMethod]
    public async Task Test_Delete_User()
    {
        _mockUsers.Setup(x => x.DeleteAsync(121)).Returns(Task.CompletedTask);

        // setup actual Controller
        var homeController = new HomeController(_logger, _mockUsers.Object)
        {
            ControllerContext = _controllerContext
        };

        // begin test
        var result = await homeController.Delete(121) as RedirectToActionResult;
        Assert.IsTrue(result!.ActionName == "Index");
    }
}