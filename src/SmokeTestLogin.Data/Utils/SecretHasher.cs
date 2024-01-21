using System.Security.Cryptography;

// ReSharper disable MemberCanBePrivate.Global

namespace SmokeTestLogin.Data.Utils;

public static class SecretHasher
{
    private const int SaltSize = 16; // 128 bits
    private const int KeySize = 32; // 256 bits
    private const int Iterations = 500;

    private const char SegmentDelimiter = ':';
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    /// <summary>
    ///     Hash specific string, seed is embed directly to hash string result
    /// </summary>
    /// <param name="input">string to hash</param>
    /// <returns></returns>
    public static string Hash(string input)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, Iterations, Algorithm, KeySize);
        return string.Join(
            SegmentDelimiter,
            Convert.ToHexString(hash),
            Convert.ToHexString(salt),
            Iterations,
            Algorithm
        );
    }

    /// <summary>
    ///     Async version of <see cref="Hash(string)" />
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static ValueTask<string> HashAsync(string input) => ValueTask.FromResult(Hash(input));

    /// <summary>
    ///     Verify a hash string and an original string is equal
    /// </summary>
    /// <param name="input">original string</param>
    /// <param name="hashString">hash string to compare</param>
    /// <returns>true if two string is equal, otherwise false</returns>
    public static bool Verify(string input, string hashString)
    {
        var segments = hashString.Split(SegmentDelimiter);
        var hash = Convert.FromHexString(segments[0]);
        var salt = Convert.FromHexString(segments[1]);
        var iterations = int.Parse(segments[2]);
        HashAlgorithmName algorithm = new(segments[3]);
        var inputHash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, hash.Length);
        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }

    /// <summary>
    ///     Async version of <see cref="Verify(string, string)" />
    /// </summary>
    /// <param name="input"></param>
    /// <param name="hashString"></param>
    /// <returns></returns>
    public static ValueTask<bool> VerifyAsync(string input, string hashString) =>
        ValueTask.FromResult(Verify(input, hashString));
}
