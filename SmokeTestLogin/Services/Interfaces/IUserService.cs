using SmokeTestLogin.Data.Entities;

namespace SmokeTestLogin.Web.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Fetch number of user as given <paramref name="amount"/>, and start at <paramref name="index"/>
        /// </summary>
        /// <param name="index">where to start</param>
        /// <param name="amount">number of record to fetch, pass -1 will make it fetch all</param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsersAsync(int index, int amount);
        Task<User?> FindUserAsync(long id);
        Task<string> UpdateAsync(User user);
    }
}
