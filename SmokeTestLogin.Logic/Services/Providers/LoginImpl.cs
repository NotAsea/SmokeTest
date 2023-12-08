using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmokeTestLogin.Data;
using SmokeTestLogin.Data.Entities;
using SmokeTestLogin.Data.Utils;
using SmokeTestLogin.Logic.Models;
using SmokeTestLogin.Logic.Services.Interfaces;

namespace SmokeTestLogin.Logic.Services.Providers;

public class LoginImpl(IHttpContextAccessor http, ILogger<LoginImpl> logger, MainContext context)
    : ILoginService
{
    public async Task<bool> LoginAsync(UserModel model)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
            if (user is null)
                return false;
            if (!await SecretHasher.VerifyAsync(model.Password, user.Password))
                return false;
            await http.HttpContext!.AuthenticateAsync();
            await http.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                CreateUserClaims(user)
            );
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError("Some thing wrong {}", ex.Message);
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        await http.HttpContext!.SignOutAsync();
    }

    private static ClaimsPrincipal CreateUserClaims(User user)
    {
        var claimUserName = new Claim(ClaimTypes.NameIdentifier, user.UserName);
        var claimFullName = new Claim(ClaimTypes.Name, user.Name);
        var claimId = new Claim(ClaimTypes.Sid, user.Id.ToString());
        var claimRole = new Claim(ClaimTypes.Role, "admin");
        var identity = new ClaimsIdentity(
            new[] { claimUserName, claimFullName, claimId, claimRole },
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        return new ClaimsPrincipal(identity);
    }
}
