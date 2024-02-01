using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TanvirArjel.EFCore.GenericRepository;

namespace SmokeTestLogin.Data;

internal static class RegisterService
{
    public static void AddDbService(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        serviceCollection.AddDbContext<MainContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("Main"))
        );
        serviceCollection.AddQueryRepository<MainContext>();
        serviceCollection.AddGenericRepository<MainContext>();
    }
}
