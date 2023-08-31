using SmokeTestLogin.Data.Entities;
using System.Security.Claims;

namespace SmokeTestLogin.Web.Customs;

public static class ClaimPrincipalExtension
{
    public static User GetUser(this ClaimsPrincipal principal)
    {
        if (!principal.Identity!.IsAuthenticated)
            throw new InvalidOperationException("User must login");
        return new User
        {
            Id = long.Parse(principal.FindFirstValue(ClaimTypes.Sid)),
            Name = principal.FindFirstValue(ClaimTypes.Name),
            UserName = principal.FindFirstValue(ClaimTypes.NameIdentifier)
        };
    }
}