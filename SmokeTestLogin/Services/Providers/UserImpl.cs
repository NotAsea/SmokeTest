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

        public async Task<User?> FindUserAsync(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int index, int amount)
        {
            if (index < 0 || amount < -1 || amount == 0)
                return await Task.FromResult(new List<User>());
            var users = _context.Users.OrderBy(x => x.Id).Skip(index * amount);
            if (amount > -1)
                users = users.Take(amount);
            return await users.ToListAsync();
        }

        public async Task<string> UpdateAsync(User user)
        {
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
