using Microsoft.EntityFrameworkCore;
using SmokeTestLogin.Data;
using SmokeTestLogin.Data.Utils;
using SmokeTestLogin.Logic.Models;
using SmokeTestLogin.Logic.Services.Interfaces;

namespace SmokeTestLogin.Logic.Services.Providers;

public class UserImpl(MainContext context) : IUserService
{
    public async Task DeleteAsync(long id)
    {
        var user = await FindUserByIdAsync(id);
        if (user is null)
        {
            return;
        }

        context.Remove(user);
        await context.SaveChangesAsync();
    }

    public async Task<UserInfo?> FindUserByIdAsync(long id)
    {
        return await context
            .Users
            .AsNoTracking()
            .Select(x => x.ToDto())
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<UserInfo>> FindUserByNameAsync(string name)
    {
        return await context
            .Users
            .AsNoTracking()
            .Where(x => x.Name.Contains(name))
            .Select(x => x.ToDto())
            .ToListAsync();
    }

    public async Task<IEnumerable<UserInfo>> GetUsersAsync(int index, int size, string name = "")
    {
        if (!string.IsNullOrEmpty(name))
        {
            return await context
                .Users
                .AsNoTracking()
                .Where(x => x.Name.Contains(name) || x.UserName.Contains(name))
                .OrderBy(x => x.Id)
                .Skip((index - 1) * size)
                .Take(size)
                .Select(x => x.ToDto())
                .ToListAsync();
        }

        return await context
            .Users
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((index - 1) * size)
            .Take(size)
            .Select(x => x.ToDto())
            .ToListAsync();
    }

    public async Task<string> UpdateAsync(UserInfo user)
    {
        var entity = user.ToEntity();
        try
        {
            if (user.Id > 0)
            {
                var oldUser = await FindUserByIdAsync(user.Id);
                if (user.RawPassword != user.Password)
                {
                    entity.Password = await SecretHasher.HashAsync(user.RawPassword);
                }
                else
                {
                    entity.Password = user.Password;
                }

                if (
                    oldUser!.UserName != user.UserName
                    && (await FindUserByUserNameAsync(entity.UserName)).Any()
                )
                {
                    return "UserName has already taken";
                }

                context.Update(entity);
            }
            else
            {
                entity.Password = await SecretHasher.HashAsync(user.RawPassword);
                entity.Name = !string.IsNullOrEmpty(entity.Name) ? entity.Name : "";
                await context.AddAsync(user);
            }

            await context.SaveChangesAsync();
            return "OK";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public async Task<IEnumerable<UserInfo>> FindUserByUserNameAsync(string userName)
    {
        return await context
            .Users
            .AsNoTracking()
            .Where(x => x.UserName == userName)
            .Select(x => x.ToDto())
            .ToListAsync();
    }

    public async Task<int> CountUsers()
    {
        return await context.Users.AsNoTracking().CountAsync();
    }
}