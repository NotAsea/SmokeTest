using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlTypes;
using System.Security.Claims;

namespace SmokeTestLogin.Web.Customs;

/// <summary>
/// Atribute to indicate every request to Action or Controller muse be authenticated
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class MustLoginAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    /// <summary>
    /// Iniatialize this Atribute
    /// </summary>
    /// <param name="role">Role to check, (hint: every role must delimit by comma) </param>
    public MustLoginAttribute(string? role = null)
    {
        _role = role ?? string.Empty;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity!.IsAuthenticated && !CheckRole(user))
        {
            context.Result = new RedirectToActionResult("LoginForm", "Login",
                new { returnUrl = context.HttpContext.Request.GetEncodedUrl() });
        }
    }

    private bool CheckRole(ClaimsPrincipal user)
    {
        if (string.IsNullOrEmpty(_role)) return true;
        return _role.Split(',').Any(role => role == user.FindFirstValue(ClaimTypes.Role));
    }
}