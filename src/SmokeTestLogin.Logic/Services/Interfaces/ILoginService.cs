namespace SmokeTestLogin.Logic.Services.Interfaces;

public interface ILoginService
{
    Task<bool> LoginAsync(UserModel model);
    Task LogoutAsync();
}