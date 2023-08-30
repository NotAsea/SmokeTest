using System.ComponentModel.DataAnnotations;

namespace SmokeTestLogin.Logic.Models
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
}