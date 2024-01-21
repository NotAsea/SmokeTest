using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SmokeTestLogin.Logic.Services.Interfaces;
using SmokeTestLogin.Logic.Services.Providers;
using TanvirArjel.EFCore.GenericRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
builder.Services.AddScoped<ILoginService, LoginImpl>().AddScoped<IUserService, UserImpl>();
builder.Services.AddDbContext<MainContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Main"))
);
builder.Services.AddGenericRepository<MainContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
await app.InitDatabase();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
