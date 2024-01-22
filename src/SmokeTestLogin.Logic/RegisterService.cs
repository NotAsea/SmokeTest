using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmokeTestLogin.Data;
using SmokeTestLogin.Logic.Services.Providers;

namespace SmokeTestLogin.Logic;

public static class RegisterService
{
    public static void AddLogicService(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        serviceCollection.AddScoped<ILoginService, LoginImpl>().AddScoped<IUserService, UserImpl>();
        serviceCollection.AddDbService(configuration);
    }
}

public static class DbStartupAction
{
    public static async Task InitDatabase(this IServiceProvider provider)
    {
        await using var scope = provider.CreateAsyncScope();
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
