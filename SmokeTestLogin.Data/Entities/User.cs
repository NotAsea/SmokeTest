namespace SmokeTestLogin.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string _passwordUnHash { get; set; } = string.Empty;
    }
}
