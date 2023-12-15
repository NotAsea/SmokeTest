using Bogus;
using Bogus.DataSets;

namespace SmokeTestLogin.Data.Utils;

public static class SeedData
{
    /// <summary>
    ///     <b>if you accidentally delete Local.db file or want reset database, be sure change path at File.OpenWrite </b>
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<User> Seed()
    {
        var collectUserGenPass = new Dictionary<string, string>();
        var users = new List<User>();
        var faker = new Faker<User>()
            .Ignore(x => x.Id)
            .RuleFor(x => x.Name, (f, _) => f.Person.FullName)
            .RuleFor(x => x.UserName, (f, _) => f.Person.UserName)
            .RuleFor(x => x.Password, (f, _) => f.Internet.PasswordEx(6, 10));

        foreach (var i in Enumerable.Range(1, 50))
        {
            var user = faker.Generate();
            user.Id = i;
            collectUserGenPass.Add(user.UserName, user.Password);
            user.Password = SecretHasher.Hash(user.Password);
            users.Add(user);
        }

        using var file = File.OpenWrite(@"D:\C#\SmokeTestLogin\PasswordGen.Gen.md");
        using var writer = new StreamWriter(file);
        writer.WriteLine(
            "| User Name            | Password   |\r\n|----------------------|------------|"
        );
        foreach (var val in collectUserGenPass)
        {
            writer.WriteLine($"|{val.Key}|{val.Value}|");
        }

        return users;
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
        bool includeSymbol = true
    )
    {
        ArgumentNullException.ThrowIfNull(internet);
        ArgumentOutOfRangeException.ThrowIfLessThan(minLength, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(maxLength, minLength);

        var r = internet.Random;
        var s = "";

        s += r.Char('a', 'z').ToString();
        if (s.Length < maxLength)
        {
            if (includeUppercase)
            {
                s += r.Char('A', 'Z').ToString();
            }
        }

        if (s.Length < maxLength)
        {
            if (includeNumber)
            {
                s += r.Char('0', '9').ToString();
            }
        }

        if (s.Length < maxLength)
        {
            if (includeSymbol)
            {
                s += r.Char('#', '&').ToString();
            }
        }

        if (s.Length < minLength)
        {
            s += r.String2(minLength - s.Length); // pad up to min
        }

        if (s.Length < maxLength)
        {
            s += r.String2(r.Number(0, maxLength - s.Length)); // random extra padding in range min..max
        }

        var chars = s.ToArray();
        var charsShuffled = r.Shuffle(chars).ToArray();

        return new string(charsShuffled);
    }
}