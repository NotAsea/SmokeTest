namespace SmokeTestLogin.Logic.Services.Providers;

internal sealed class UserImpl(IRepository repository) : IUserService
{
    public async Task DeleteAsync(long id)
    {
        var user = await FindUserByIdAsync(id);
        if (user is null)
        {
            return;
        }

        repository.Remove(user);
        await repository.SaveChangesAsync().ConfigureAwait(false);
    }

    public Task<UserInfo?> FindUserByIdAsync(long id) =>
        repository.GetAsync<User, UserInfo?>(x => x.Id == id, x => x.ToDto());

    public async Task<IEnumerable<UserInfo>> FindUserByNameAsync(string name) =>
        await repository
            .GetListAsync<User, UserInfo>(x => x.Name.Contains(name), x => x.ToDto())
            .ConfigureAwait(false);

    public Task<PaginatedList<UserInfo>> GetUsersAsync(int index, int size, string name = "")
    {
        var spec = new PaginationSpecification<User>
        {
            OrderBy = x => x.OrderBy(u => u.Id),
            PageIndex = index,
            PageSize = size
        };
        if (!string.IsNullOrEmpty(name))
        {
            spec.Conditions.Add(x => x.Name.Contains(name) || x.UserName.Contains(name));
        }

        return repository.GetListAsync<User, UserInfo>(spec, x => x.ToDto());
    }

    public async Task<string> UpdateAsync(UserInfo user)
    {
        var entity = user.ToEntity();
        await using var transaction = await repository.BeginTransactionAsync();
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

                repository.Update(entity);
            }
            else
            {
                entity.Password = await SecretHasher.HashAsync(user.RawPassword);
                entity.Name = !string.IsNullOrEmpty(entity.Name) ? entity.Name : "";
                await repository.AddAsync(user);
            }

            await repository.SaveChangesAsync().ConfigureAwait(false);
            await transaction.CommitAsync().ConfigureAwait(false);
            return "OK";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync().ConfigureAwait(false);
            return ex.ToString();
        }
        finally
        {
            repository.ClearChangeTracker();
        }
    }

    public async Task<IEnumerable<UserInfo>> FindUserByUserNameAsync(string userName) =>
        await repository
            .GetListAsync<User, UserInfo>(x => x.UserName == userName, x => x.ToDto())
            .ConfigureAwait(false);
}
