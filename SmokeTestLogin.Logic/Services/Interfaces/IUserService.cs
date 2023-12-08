using SmokeTestLogin.Logic.Models;

namespace SmokeTestLogin.Logic.Services.Interfaces;

public interface IUserService
{
    Task DeleteAsync(long id);

    /// <summary>
    /// Fetch number of user start at <paramref name="index"/>
    /// </summary>
    /// <param name="index">where to start</param>
    /// <param name="size"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<IEnumerable<UserInfo>> GetUsersAsync(int index, int size, string name = "");

    Task<UserInfo?> FindUserByIdAsync(long id);
    Task<IEnumerable<UserInfo>> FindUserByNameAsync(string name);
    Task<string> UpdateAsync(UserInfo user);

    Task<IEnumerable<UserInfo>> FindUserByUserNameAsync(string userName);

    Task<int> CountUsers();
}
