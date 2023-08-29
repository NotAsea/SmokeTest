using SmokeTestLogin.Web.Models;

namespace SmokeTestLogin.Web.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(UserModel model);
        Task LogoutAsync();
    }
}