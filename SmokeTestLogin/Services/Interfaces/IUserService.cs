using SmokeTestLogin.Data.Entities;

namespace SmokeTestLogin.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
