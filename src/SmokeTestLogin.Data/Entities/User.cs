// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace SmokeTestLogin.Data.Entities;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    ///     this is our real field to store Hash Password, the Hash is used can be seen at
    ///     <see cref="Utils.SecretHasher.HashAsync(string)" />
    /// </summary>
    public string Password { get; set; } = string.Empty;

    public bool IsActived { get; set; } = true;
}