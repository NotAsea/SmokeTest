using Bogus;
using Bogus.DataSets;
using Cysharp.Text;

namespace SmokeTestLogin.Data.Utils;

public static class SeedData
{
    /// <summary>
    ///     <b>if you accidentally delete Local.db file or want reset database, be sure change path at File.OpenWrite </b>
    /// </summary>
    /// <returns></returns>
    public static async Task<IEnumerable<User>> Seed()
    {
        const string path = @"D:\C#\SmokeTestLogin\PasswordGen.md";
        var collectUserGenPass = new List<KeyValuePair<string, string>>();
        var faker = new Faker<User>()
            .Ignore(x => x.Id)
            .RuleFor(x => x.Name, (f, _) => f.Person.FullName)
            .RuleFor(x => x.UserName, (f, _) => f.Person.UserName)
            .RuleFor(x => x.Password, (f, _) => f.Internet.PasswordEx(6, 10));

        var users = faker.Generate(50);
        var i = 1;
        users.ForEach(u =>
        {
            u.Id = i++;
            collectUserGenPass.Add(new(u.UserName, u.Password));
            u.Password = SecretHasher.Hash(u.Password);
        });

        if (File.Exists(path))
            File.Delete(path);
        await using var file = File.OpenWrite(path);
        await using var writer = new StreamWriter(file);
        await writer.WriteLineAsync(
            "| User Name            | Password   |\r\n|----------------------|------------|"
        );
        foreach (var val in collectUserGenPass)
        {
            await writer.WriteLineAsync(ZString.Format("|{0}|{1}|", val.Key, val.Value));
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
        using var s = ZString.CreateStringBuilder(true);
        s.Append(r.Char('a', 'z'));
        if (s.Length < maxLength)
        {
            if (includeUppercase)
            {
                s.Append(r.Char('A', 'Z'));
            }
            if (includeNumber)
            {
                s.Append(r.Char('0', '9'));
            }
            if (includeSymbol)
            {
                s.Append(r.Char('#', '&'));
            }
        }

        if (s.Length < minLength)
        {
            s.Append(r.String2(minLength - s.Length)); // pad up to min
        }

        if (s.Length < maxLength)
        {
            s.Append(r.String2(r.Number(0, maxLength - s.Length))); // random extra padding in range min..max
        }

        var chars = s.ToString();
        var charsShuffled = r.Shuffle(chars).ToArray();

        return new string(charsShuffled);
    }
}
