using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmokeTestLogin.Web.Customs;

/// <summary>
///     Attribute to indicate every request to Action or Controller muse be authenticated
/// </summary>
/// <remarks>
///     Initialize this Attribute
/// </remarks>
/// <param name="role">Role to check, (hint: every role must delimit by comma) </param>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class MustLoginAttribute(string? role = null)
    : AuthorizeAttribute,
        IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if ((user.Identity is null || !user.Identity.IsAuthenticated) && !CheckRole(user))
        {
            context.Result = new RedirectToActionResult(
                "LoginForm",
                "Login",
                new { returnUrl = context.HttpContext.Request.GetEncodedUrl() }
            );
        }
    }

    private bool CheckRole(ClaimsPrincipal user) =>
        string.IsNullOrEmpty(role)
        || role.Split(',').Any(s => s == user.FindFirstValue(ClaimTypes.Role));
}
