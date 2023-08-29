using SmokeTestLogin.Data.Entities;
using SmokeTestLogin.Web.Models;

namespace SmokeTestLogin.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task DeleteAsync(long id);
        /// <summary>
        /// Fetch number of user as given <paramref name="amount"/>, and start at <paramref name="index"/>
        /// </summary>
        /// <param name="index">where to start</param>
        /// <param name="amount">number of record to fetch, pass -1 will make it fetch all</param>
        /// <returns></returns>
        Task<IEnumerable<UserInfo>> GetUsersAsync(int index, int amount);
        Task<UserInfo?> FindUserAsync(long id);
        Task<IEnumerable<UserInfo>> FindUserByNameAsync(string name);
        Task<string> UpdateAsync(UserInfo user);
    }
}
