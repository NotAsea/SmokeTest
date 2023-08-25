using Bogus;
using SmokeTestLogin.Data.Entities;

namespace SmokeTestLogin.Data.Utils
{
    public static class SeedData
    {
        public static IEnumerable<User> Seed()
        {
            var faker = new Faker<User>().Ignore(x => x.Id)
                .RuleFor(x => x.Name, (f, _) => f.Person.FullName)
                .RuleFor(x => x.UserName, (f, _) => f.Person.UserName)
                .RuleFor(x => x.Password, (f, _) => f.Internet.Password())
                .RuleFor(x => x._passwordUnHash, (_, u) => u.Password);
            foreach (var i in Enumerable.Range(1, 50))
            {
                var user = faker.Generate();
                user.Id = i;
                user.Password = SecretHasher.Hash(user.Password);
                yield return user;
            }
        }
    }
}
