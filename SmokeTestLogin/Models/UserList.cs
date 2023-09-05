using SmokeTestLogin.Logic.Models;

namespace SmokeTestLogin.Web.Models;

public class UserList
{
    public int UserPerPage { get; set; } = 15;
    public IList<UserInfo> Users { get; set; }
    public int CurrentIndex { get; set; }
    public int TotalCount { get; init; }

    public int PageCount => (int)Math.Ceiling((double)TotalCount / UserPerPage);

    public UserList(IList<UserInfo> users, int totalCount)
    {
        Users = users;
        TotalCount = totalCount;
    }

    public UserList(IList<UserInfo> users, int currentIndex, int totalCount, int? userPerPage = null) : this(users,
        totalCount)
    {
        CurrentIndex = currentIndex;
        UserPerPage = userPerPage ?? 15;
    }
}