namespace SmokeTestLogin.Logic.Models;

public record UserModel
{
    [Required(ErrorMessage = "UserName cannot null")]
    public string UserName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Password cannot null")]
    public string Password { get; init; } = string.Empty;

    public string ReturnUrl { get; init; } = string.Empty;
}