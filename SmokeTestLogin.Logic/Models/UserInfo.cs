using System.ComponentModel.DataAnnotations;
using SmokeTestLogin.Data.Entities;

namespace SmokeTestLogin.Logic.Models;

public record UserInfo
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Password *")] public string Password { get; set; } = string.Empty;

    [Display(Name = "UserName *")] public string UserName { get; set; } = string.Empty;

    public string RawPassword { get; set; } = string.Empty;
    public bool IsActivated { get; set; }
}

public static class UserConvert
{
    public static UserInfo ToDto(this User? user)
    {
        return user is not null
            ? new UserInfo
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Password = user.Password,
                IsActivated = user.IsActived
            }
            : new UserInfo();
    }

    public static User ToEntity(this UserInfo info)
    {
        return new User
        {
            Id = info.Id,
            Name = info.Name,
            UserName = info.UserName,
            IsActived = info.IsActivated
        };
    }
}