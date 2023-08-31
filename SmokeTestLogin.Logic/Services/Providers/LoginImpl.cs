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
using System.Security.Claims;

namespace SmokeTestLogin.Logic.Services.Providers;

public class LoginImpl : ILoginService
{
    private readonly IHttpContextAccessor _http;
    private readonly ILogger<LoginImpl> _logger;
    private readonly MainContext _context;

    public LoginImpl(IHttpContextAccessor http, ILogger<LoginImpl> logger, MainContext context)
    {
        _http = http;
        _logger = logger;
        _context = context;
    }

    public async Task<bool> LoginAsync(UserModel model)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
            if (user is null) return false;
            if (!await SecretHasher.VerifyAsync(model.Password, user.Password)) return false;
            await _http.HttpContext!.AuthenticateAsync();
            await _http.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                createUserClaims(user));
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Some thing wrong {}", ex.Message);
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        await _http.HttpContext!.SignOutAsync();
    }

    private static ClaimsPrincipal createUserClaims(User user)
    {
        var claimUserName = new Claim(ClaimTypes.NameIdentifier, user.UserName);
        var claimFullName = new Claim(ClaimTypes.Name, user.Name);
        var claimId = new Claim(ClaimTypes.Sid, user.Id.ToString());
        var identity = new ClaimsIdentity(new[] { claimUserName, claimFullName, claimId },
            CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }
}