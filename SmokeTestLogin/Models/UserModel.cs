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
    public abstract class Constant
    {
        public const string VALID_USER_NAME = "hai";
        public const string VALID_PASS = "Hai@1234";
    }

    public record UserInfo
    {
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Password *")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "UserName *")]
        public string UserName { get; set; } = string.Empty;
        public long Id { get; set; } = 0;

        public static implicit operator UserInfo(User? user) =>
            user is not null
            ? new() { Id = user.Id, Name = user.Name, UserName = user.UserName, Password = user.Password }
            : new();
        public static implicit operator User(UserInfo info) =>
            new() { Id = info.Id, Name = info.Name, UserName = info.UserName, _passwordUnHash = info.Password, Password = info.Password };
    }
}
