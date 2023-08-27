using Bogus;
using Bogus.DataSets;
using SmokeTestLogin.Data.Entities;
using System;

namespace SmokeTestLogin.Data.Utils
{
    public static class SeedData
    {
        public static IEnumerable<User> Seed()
        {
            var faker = new Faker<User>().Ignore(x => x.Id)
                .RuleFor(x => x.Name, (f, _) => f.Person.FullName)
                .RuleFor(x => x.UserName, (f, _) => f.Person.UserName)
                .RuleFor(x => x.Password, (f, _) => f.Internet.PasswordEx(6, 10))
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
    public static class PassWordGenExtension
    {
        public static string PasswordEx(
            this Internet internet,
            int minLength,
            int maxLength,
            bool includeUppercase = true,
            bool includeNumber = true,
            bool includeSymbol = true)
        {

            if (internet == null) throw new ArgumentNullException(nameof(internet));
            if (minLength < 1) throw new ArgumentOutOfRangeException(nameof(minLength));
            if (maxLength < minLength) throw new ArgumentOutOfRangeException(nameof(maxLength));

            var r = internet.Random;
            var s = "";

            s += r.Char('a', 'z').ToString();
            if (s.Length < maxLength) if (includeUppercase) s += r.Char('A', 'Z').ToString();
            if (s.Length < maxLength) if (includeNumber) s += r.Char('0', '9').ToString();
            if (s.Length < maxLength) if (includeSymbol) s += r.Char('#', '&').ToString();
            if (s.Length < minLength) s += r.String2(minLength - s.Length);                // pad up to min
            if (s.Length < maxLength) s += r.String2(r.Number(0, maxLength - s.Length));   // random extra padding in range min..max

            var chars = s.ToArray();
            var charsShuffled = r.Shuffle(chars).ToArray();

            return new string(charsShuffled);
        }
    }
}
