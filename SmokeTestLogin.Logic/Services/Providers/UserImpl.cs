using Microsoft.EntityFrameworkCore;
using SmokeTestLogin.Data;
using SmokeTestLogin.Data.Entities;
using SmokeTestLogin.Data.Utils;
using SmokeTestLogin.Logic.Models;
using SmokeTestLogin.Logic.Services.Interfaces;

namespace SmokeTestLogin.Logic.Services.Providers;

public class UserImpl : IUserService
{
    private readonly MainContext _context;

    public UserImpl(MainContext context)
    {
        _context = context;
    }

    public async Task DeleteAsync(long id)
    {
        var user = await FindUserByIdAsync(id);
        if (user is null) return;
        _context.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserInfo?> FindUserByIdAsync(long id) =>
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);


    public async Task<IEnumerable<UserInfo>> FindUserByNameAsync(string name) =>
        await _context.Users.AsNoTracking().Where(x => x.Name.Contains(name))
            .Select(x => (UserInfo)x)
            .ToListAsync();

    public async Task<IEnumerable<UserInfo>> GetUsersAsync(int index, int amount)
    {
        if (index < 0 || amount < -1 || amount == 0)
            return await Task.FromResult(new List<UserInfo>());
        var users = _context.Users.AsNoTracking().OrderBy(x => x.Id).Skip(index * amount);
        if (amount > -1)
            users = users.Take(amount);
        return await users.Select(x => (UserInfo)x).ToListAsync();
    }

    public async Task<string> UpdateAsync(UserInfo user)
    {
        User entity = user;
        try
        {
            if (user.Id > 0)
            {
                var oldUser = await FindUserByIdAsync(user.Id);
                if (user.RawPassword != user.Password)
                {
                    entity.Password = await SecretHasher.HashAsync(user.RawPassword);
                }
                else entity.Password = user.Password;

                if (oldUser!.UserName != user.UserName && (await FindUserByUserNameAsync(entity.UserName)).Any())
                    return "UserName has already taken";
                _context.Update(entity);
            }
            else
            {
                entity.Password = await SecretHasher.HashAsync(user.RawPassword);
                entity.Name = !string.IsNullOrEmpty(entity.Name) ? entity.Name : "";
                await _context.AddAsync(user);
            }

            await _context.SaveChangesAsync();
            return "OK";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public async Task<IEnumerable<UserInfo>> FindUserByUserNameAsync(string userName) =>
        await _context.Users.AsNoTracking().Where(x => x.UserName == userName)
            .Select(x => (UserInfo)x)
            .ToListAsync();
}