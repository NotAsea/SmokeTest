namespace SmokeTestLogin.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// this is our real field to store Hash Password, the Hash is used can be see at <see cref="Utils.SecretHasher.HashAsync(string)"/>
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// this field store raw password, which in real world is bad practice, but since we use <see cref="Bogus.Faker"/> 
        /// to generate random pasword, and for the sake of demo, store raw password in database here is acceptable
        /// </summary>
        public string _passwordUnHash { get; set; } = string.Empty;
    }
}
