using SmokeTestLogin.Data.Entities;
using System.Security.Claims;

namespace SmokeTestLogin.Web.Customs
{
    public static class ClaimPrincipalExtension
    {
        public static User GetUser(this ClaimsPrincipal context)
        {
            if (!context.Identity!.IsAuthenticated)
                throw new InvalidOperationException("User must login");
            return new User
            {
                Id = long.Parse(context.FindFirstValue(ClaimTypes.Sid)),
                Name = context.FindFirstValue(ClaimTypes.Name),
                UserName = context.FindFirstValue(ClaimTypes.NameIdentifier)
            };
        }
    }
}
