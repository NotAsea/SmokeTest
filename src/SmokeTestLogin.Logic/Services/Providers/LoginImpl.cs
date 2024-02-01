using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace SmokeTestLogin.Logic.Services.Providers;

internal sealed class LoginImpl(
    IHttpContextAccessor http,
    ILogger<LoginImpl> logger,
    IQueryRepository repository
) : ILoginService
{
    public async Task<bool> LoginAsync(UserModel model)
    {
        try
        {
            var user = await repository.GetAsync<User>(x => x.UserName == model.UserName, true);

            if (user is null)
            {
                return false;
            }

            if (!await SecretHasher.VerifyAsync(model.Password, user.Password))
            {
                return false;
            }

            await http.HttpContext.AuthenticateAsync();
            await http.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                CreateUserClaims(user),
                new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24),
                    IsPersistent = true,
                    IssuedUtc = DateTimeOffset.UtcNow
                }
            );
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError("Some thing wrong {}", ex.Message);
            return false;
        }
    }

    public Task LogoutAsync() => http.HttpContext.SignOutAsync();

    private static ClaimsPrincipal CreateUserClaims(User user)
    {
        var claimUserName = new Claim(ClaimTypes.NameIdentifier, user.UserName);
        var claimFullName = new Claim(ClaimTypes.Name, user.Name);
        var claimId = new Claim(ClaimTypes.Sid, user.Id.ToString());
        var claimRole = new Claim(ClaimTypes.Role, "admin");
        var identity = new ClaimsIdentity(
            [claimUserName, claimFullName, claimId, claimRole],
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        return new ClaimsPrincipal(identity);
    }
}
