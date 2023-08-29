using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmokeTestLogin.Controllers;
using SmokeTestLogin.Web.Models;
using SmokeTestLogin.Web.Services.Interfaces;

namespace SmokeTestLogin.Test
{
    [TestClass]
    public class UserFunctionTest
    {
        [TestMethod]
        public async Task Test_Edit_User()
        {
            // setup request
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(x => x.Scheme).Returns("http");
            httpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("https://localhost:7296"));
            httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Home"));

            var httpCtx = Mock.Of<HttpContext>(x => x.Request == httpRequest.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            var mockUsers = new Mock<IUserService>();
            var model = new UserInfo
                { Id = 1, Name = "Asdsa", Password = "asdasasdfas", UserName = "qqq", IsActivated = true };
            mockUsers.Setup(x => x.FindUserAsync(100)).ReturnsAsync(model);
            mockUsers.Setup(x => x.UpdateAsync(model)).ReturnsAsync("OK");

            // set up controller context
            var controllerContext = new ControllerContext
            {
                HttpContext = httpCtx
            };
            // setup actual Controller
            var homeController = new HomeController(mockLogger.Object, mockUsers.Object)
                { ControllerContext = controllerContext };

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
            // setup request
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(x => x.Scheme).Returns("http");
            httpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("https://localhost:7296"));
            httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Home"));

            var httpCtx = Mock.Of<HttpContext>(x => x.Request == httpRequest.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            var mockUsers = new Mock<IUserService>();
            var model = new UserInfo
                { Id = 1, Name = "Asdsa", Password = "asdasasdfas", UserName = "qqq", IsActivated = true };
            mockUsers.Setup(x => x.UpdateAsync(model)).ReturnsAsync("OK");

            // set up controller context
            var controllerContext = new ControllerContext
            {
                HttpContext = httpCtx
            };

            // setup actual Controller
            var homeController = new HomeController(mockLogger.Object, mockUsers.Object)
                { ControllerContext = controllerContext };

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
            // setup request
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(x => x.Scheme).Returns("http");
            httpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("https://localhost:7296"));
            httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Home"));

            var httpCtx = Mock.Of<HttpContext>(x => x.Request == httpRequest.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            var mockUsers = new Mock<IUserService>();
            var model = new UserInfo
                { Id = 1, Name = "Asdsa", Password = "asdasasdfas", UserName = "qqq", IsActivated = true };
            const string searchStr = "blabalab";
            mockUsers.Setup(x => x.FindUserByNameAsync(searchStr)).ReturnsAsync(new[] { model });

            // set up controller context
            var controllerContext = new ControllerContext
            {
                HttpContext = httpCtx
            };

            // setup actual Controller
            var homeController = new HomeController(mockLogger.Object, mockUsers.Object)
                { ControllerContext = controllerContext };

            // begin test
            var result = await homeController.Search(searchStr) as ViewResult;
            Assert.IsTrue(result!.ViewName == "Index");
        }

        [TestMethod]
        public async Task Test_Delete_User()
        {
            // setup request
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(x => x.Scheme).Returns("http");
            httpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("https://localhost:7296"));
            httpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/Home"));

            var httpCtx = Mock.Of<HttpContext>(x => x.Request == httpRequest.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            var mockUsers = new Mock<IUserService>();
            mockUsers.Setup(x => x.DeleteAsync(121)).Returns(Task.CompletedTask);

            // set up controller context
            var controllerContext = new ControllerContext
            {
                HttpContext = httpCtx
            };

            // setup actual Controller
            var homeController = new HomeController(mockLogger.Object, mockUsers.Object)
                { ControllerContext = controllerContext };

            // begin test
            var result = await homeController.Delete(121) as RedirectToActionResult;
            Assert.IsTrue(result!.ActionName == "Index");
        }
    }
}