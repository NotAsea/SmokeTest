using SmokeTestLogin.Logic;

namespace SmokeTestLogin.Web.Customs;

public static class ProjectStartup
{
    public static async Task Startup(this WebApplication app)
    {
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
        
        await app.Services.InitDatabase();

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
    }
}
