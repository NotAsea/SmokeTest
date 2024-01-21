namespace SmokeTestLogin.Web.Models;

public record UserList
{
    public UserList(IList<UserInfo> users, int totalCount)
    {
        Users = users;
        TotalCount = totalCount;
    }

    public UserList(
        IList<UserInfo> users,
        int currentIndex,
        int totalCount,
        int? userPerPage = null
    )
        : this(users, totalCount)
    {
        CurrentIndex = currentIndex;
        UserPerPage = userPerPage ?? 15;
    }

    public int UserPerPage { get; init; } = 15;
    public IList<UserInfo> Users { get; init; }
    public int CurrentIndex { get; init; }
    public long TotalCount { get; init; }

    public int PageCount => (int)Math.Ceiling((double)TotalCount / UserPerPage);
}