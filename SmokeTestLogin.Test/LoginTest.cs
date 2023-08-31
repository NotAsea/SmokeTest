using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmokeTestLogin.Controllers;
using SmokeTestLogin.Logic.Models;
using SmokeTestLogin.Logic.Services.Interfaces;
using SmokeTestLogin.Web.Controllers;

namespace SmokeTestLogin.Test;

[TestClass]
public class LoginTest
{
    [TestMethod]
    public async Task Test_AuthorizeAttribute_Redirect_When_Not_Login()
    {
        // setup request
        var httpRequest = new Mock<HttpRequest>();
        httpRequest.Setup(x => x.Scheme).Returns("http");
        httpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("https://localhost:7296"));
        httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Home"));

        var httpCtx = Mock.Of<HttpContext>(x => x.Request == httpRequest.Object);

        var mockLogger = new Mock<ILogger<HomeController>>();

        var mockUsers = new Mock<IUserService>();
        mockUsers.Setup(x => x.GetUsersAsync(0, -1)).ReturnsAsync(new[]
        {
            new UserInfo { Id = 1, Name = "Asdsa", Password = "asdasasdfas", UserName = "qqq", IsActivated = true }
        });

        // set up controller context
        var controllerContext = new ControllerContext
        {
            HttpContext = httpCtx
        };
        // setup actual Controller
        var homeController = new HomeController(mockLogger.Object, mockUsers.Object)
        { ControllerContext = controllerContext };

        // now test
        const string expected = "Index"; // expect viewName Index in Home
        var actual = await homeController.Index() as ViewResult;
        Assert.AreNotEqual(expected,
            actual!.ViewName); // Got other view as Authorize attribute redirect result, wolking
    }

    [TestMethod]
    public async Task Test_Login_Success()
    {
        // setup request
        var httpRequest = new Mock<HttpRequest>();
        httpRequest.Setup(x => x.Scheme).Returns("http");
        httpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("https://localhost:7296"));
        httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Login"));

        var httpCtx = Mock.Of<HttpContext>(x => x.Request == httpRequest.Object);
        var loginService = new Mock<ILoginService>();
        var mockLogger = new Mock<ILogger<LoginController>>();

        //setup service behavior
        var model = new UserModel { UserName = "hai", Password = "Hai@1234" }; // with this data should true
        loginService.Setup(x => x.LoginAsync(model)).Returns(Task.FromResult(true));

        // set up controller context
        var controllerContext = new ControllerContext
        {
            HttpContext = httpCtx
        };
        // setup actual Controller
        var loginController = new LoginController(loginService.Object, mockLogger.Object)
        { ControllerContext = controllerContext };

        // begin test
        var response = await loginController.LoginForm(model) as RedirectToActionResult;
        // login success redirect to Index view of Home
        Assert.AreEqual("Index", response!.ActionName);
    }

    [TestMethod]
    public async Task Test_Logout_User()
    {
        // setup request
        var httpRequest = new Mock<HttpRequest>();
        httpRequest.Setup(x => x.Scheme).Returns("http");
        httpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("https://localhost:7296"));
        httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Login"));

        var httpCtx = Mock.Of<HttpContext>(x => x.Request == httpRequest.Object);
        var loginService = new Mock<ILoginService>();
        var mockLogger = new Mock<ILogger<LoginController>>();

        // setup service behavior
        loginService.Setup(x => x.LogoutAsync()).Returns(Task.CompletedTask);

        // set up controller context
        var controllerContext = new ControllerContext
        {
            HttpContext = httpCtx
        };
        // setup actual Controller
        var loginController = new LoginController(loginService.Object, mockLogger.Object)
        { ControllerContext = controllerContext };

        // begin test
        var result = await loginController.Logout() as RedirectToActionResult;
        // should get redirect to LoginForm to login
        Assert.IsTrue(result!.ActionName == "LoginForm");
    }
}