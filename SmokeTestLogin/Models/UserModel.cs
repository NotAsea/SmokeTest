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
}
