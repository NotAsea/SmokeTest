using Microsoft.AspNetCore.Authentication.Cookies;
using SmokeTestLogin.Logic;

namespace SmokeTestLogin.Web.Customs;

public static class ProjectService
{
    public static void AddProjectService(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        builder
            .Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(24);
                    options.LoginPath = new PathString("/Login/LoginForm");
                }
            );
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddLogicService(builder.Configuration);
    }
}
