using SmokeTestLogin.Logic.Models;

namespace SmokeTestLogin.Logic.Interfaces
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(UserModel model);
        Task LogoutAsync();
    }
}