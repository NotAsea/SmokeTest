using SmokeTestLogin.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmokeTestLogin.Web.Models
{
    public record UserModel
    {
        [Required(ErrorMessage = "UserName cannot null")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password cannot null")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = string.Empty;
    }

    public record UserInfo
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Password *")] public string Password { get; set; } = string.Empty;

        [Display(Name = "UserName *")] public string UserName { get; set; } = string.Empty;
        public string RawPassword { get; set; } = string.Empty;
        public bool IsActivated { get; set; }

        public static implicit operator UserInfo(User? user) =>
            user is not null
                ? new UserInfo
                {
                    Id = user.Id, Name = user.Name, UserName = user.UserName, Password = user.Password,
                    IsActivated = user.IsActived
                }
                : new UserInfo();

        public static implicit operator User(UserInfo info) =>
            new() { Id = info.Id, Name = info.Name, UserName = info.UserName, IsActived = info.IsActivated };
    }
}