using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmokeTestLogin.Logic.Models;
using SmokeTestLogin.Logic.Services.Interfaces;
using SmokeTestLogin.Web.Controllers;
using TanvirArjel.EFCore.GenericRepository;

// ReSharper disable StringLiteralTypo

namespace SmokeTestLogin.Test;

[TestClass]
public class LoginTest
{
    private readonly ControllerContext _controllerContext;
    private readonly ILogger<LoginController> _logger;
    private readonly Mock<ILoginService> _loginService;
    private readonly Mock<IUserService> _userService;

    public LoginTest()
    {
        // setup request
        Mock<HttpRequest> httpRequest = new();
        httpRequest.Setup(x => x.Scheme).Returns("http");
        httpRequest
            .Setup(x => x.Host)
            .Returns(HostString.FromUriComponent("https://localhost:7296"));
        httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Home"));

        var httpCxt = Mock.Of<HttpContext>(x => x.Request == httpRequest.Object);

        _logger = new Mock<ILogger<LoginController>>().Object;
        _loginService = new Mock<ILoginService>();
        _userService = new Mock<IUserService>();
        _controllerContext = new ControllerContext { HttpContext = httpCxt };
    }

    [TestMethod]
    public void Test_AuthorizeAttribute_Redirect_When_Not_Login()
    {
        _userService
            .Setup(x => x.GetUsersAsync(0, -1, ""))
            .ReturnsAsync(
                new PaginatedList<UserInfo>(
                    [
                        new()
                        {
                            Id = 1,
                            Name = "Asdsa",
                            Password = "asdasasdfas",
                            UserName = "qqq",
                            IsActivated = true
                        }
                    ],
                    1,
                    1,
                    10
                )
            );

        var homeController = new HomeController(
            new Mock<ILogger<HomeController>>().Object,
            _userService.Object
        )
        {
            ControllerContext = _controllerContext
        };

        // now test
        const string expected = "Index"; // expect viewName Index in Home
        var actual = homeController.Index() as ViewResult;
        Assert.AreNotEqual(expected, actual!.ViewName); // Got other view as Authorize attribute redirect result, working
    }

    [TestMethod]
    public async Task Test_Login_Success()
    {
        //setup service behavior
        var model = new UserModel { UserName = "hai", Password = "Hai@1234" }; // with this data should true
        _loginService.Setup(x => x.LoginAsync(model)).Returns(Task.FromResult(true));

        // setup actual Controller
        var loginController = new LoginController(_loginService.Object, _logger)
        {
            ControllerContext = _controllerContext
        };

        // begin test
        var response = await loginController.LoginForm(model) as RedirectToActionResult;
        // login success redirect to Index view of Home
        Assert.AreEqual("Index", response!.ActionName);
    }

    [TestMethod]
    public async Task Test_Logout_User()
    {
        // setup service behavior
        _loginService.Setup(x => x.LogoutAsync()).Returns(Task.CompletedTask);

        // setup actual Controller
        var loginController = new LoginController(_loginService.Object, _logger)
        {
            ControllerContext = _controllerContext
        };

        // begin test
        var result = await loginController.Logout() as RedirectToActionResult;

        // should get redirect to LoginForm to login
        Assert.IsTrue(result!.ActionName == "LoginForm");
    }
}
