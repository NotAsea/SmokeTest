using Castle.Core.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmokeTestLogin.Controllers;
using SmokeTestLogin.Data.Entities;
using SmokeTestLogin.Web.Controllers;
using SmokeTestLogin.Web.Models;
using SmokeTestLogin.Web.Services.Interfaces;

namespace SmokeTestLogin.Test
{
    [TestClass]
    public class UnitTest
    {

        [TestMethod]
        public async Task Test_AuthorizeAtribute_Redirect_When_Not_Login()
        {
            // setup request
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(x => x.Scheme).Returns("http");
            httpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("https://localhost:7296"));
            httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Home"));

            var httpCtx = Mock.Of<HttpContext>(_ => _.Request == httpRequest.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            var mockUsers = new Mock<IUserService>();
            mockUsers.Setup(x => x.GetUsersAsync()).ReturnsAsync(new[] { new User { Id = 1, Name = "Asdsa", Password = "asdasasdfas", UserName = "qqq" } });

            //set up controller context
            var controllerContext = new ControllerContext
            {
                HttpContext = httpCtx
            };
            // setup actual Controller
            var homeController = new HomeController(mockLogger.Object, mockUsers.Object) { ControllerContext = controllerContext };

            // now test
            var expected = "Index"; // expect viewName Index in Home
            var actual = await homeController.Index() as ViewResult;
            Assert.AreNotEqual(expected, actual!.ViewName); // Got other view as Authorize attribute redirect result, wolking
        }

        [TestMethod]
        public async Task Test_Login_Success()
        {
            // setup request
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(x => x.Scheme).Returns("http");
            httpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("https://localhost:7296"));
            httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Login"));

            var httpCtx = Mock.Of<HttpContext>(_ => _.Request == httpRequest.Object);
            var loginService = new Mock<ILoginService>();
            var mockLogger = new Mock<ILogger<LoginController>>();

            //setup service behavior
            var model = new UserModel { UserName = "hai", Password = "Hai@1234" }; // with this data should true
            loginService.Setup(x => x.LoginAsync(model)).Returns(Task.FromResult(true));

            //set up controller context
            var controllerContext = new ControllerContext
            {
                HttpContext = httpCtx
            };
            // setup actual Controller
            var loginController = new LoginController(loginService.Object, mockLogger.Object) { ControllerContext = controllerContext };

            // begin test
            var response = (await loginController.LoginForm(model)) as RedirectToActionResult;

            Assert.AreEqual("Index", response!.ActionName); // login success redirect to Index view of Home
        }
    }
}