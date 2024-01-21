using System.Security.Claims;

namespace SmokeTestLogin.Web.Customs;

public static class ClaimPrincipalExtension
{
    public static UserInfo GetUser(this ClaimsPrincipal principal)
    {
        if (principal.Identity is null || !principal.Identity.IsAuthenticated)
        {
            throw new InvalidOperationException("User must login");
        }

        return new UserInfo
        {
            Id = long.Parse(principal.FindFirstValue(ClaimTypes.Sid)!),
            Name = principal.FindFirstValue(ClaimTypes.Name)!,
            UserName = principal.FindFirstValue(ClaimTypes.NameIdentifier)!
        };
    }
}
