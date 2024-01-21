namespace SmokeTestLogin.Logic.Models;

public record UserInfo
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;

    [Display(Name = "Password *")]
    public string Password { get; init; } = string.Empty;

    [Display(Name = "UserName *")]
    public string UserName { get; init; } = string.Empty;

    public string RawPassword { get; init; } = string.Empty;
    public bool IsActivated { get; init; }
}

public static class UserConvert
{
    public static UserInfo ToDto(this User? user) =>
        user is not null
            ? new UserInfo
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Password = user.Password,
                IsActivated = user.IsActived
            }
            : new UserInfo();

    public static User ToEntity(this UserInfo info) =>
        new()
        {
            Id = info.Id,
            Name = info.Name,
            UserName = info.UserName,
            IsActived = info.IsActivated
        };
}
