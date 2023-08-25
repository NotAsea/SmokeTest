using Microsoft.EntityFrameworkCore;
using SmokeTestLogin.Data;
using SmokeTestLogin.Data.Entities;
using SmokeTestLogin.Web.Services.Interfaces;

namespace SmokeTestLogin.Web.Services.Providers
{
    public class UserImpl : IUserService
    {
        private readonly MainContext _context;

        public UserImpl(MainContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
