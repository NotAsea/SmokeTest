using Microsoft.EntityFrameworkCore;
using SmokeTestLogin.Data.Utils;

namespace SmokeTestLogin.Web.Customs;

public static class InitialDb
{
    public static async Task InitDatabase(this WebApplication application)
    {
        await using var scope = application.Services.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<MainContext>();
        await context.Database.EnsureCreatedAsync();
        if (await context.Users.AsNoTracking().AnyAsync())
            return;
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.BulkInsertOptimizedAsync(await SeedData.Seed());
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e.GetBaseException().ToString());
        }
    }
}
