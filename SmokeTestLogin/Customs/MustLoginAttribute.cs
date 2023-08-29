using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmokeTestLogin.Web.Customs
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class MustLoginAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity!.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("LoginForm", "Login",
                    new { returnUrl = context.HttpContext.Request.GetEncodedUrl() });
            }
        }
    }
}