using System.Security.Cryptography;
// ReSharper disable MemberCanBePrivate.Global

namespace SmokeTestLogin.Data.Utils
{
    public static class SecretHasher
    {
        private const int _saltSize = 16; // 128 bits
        private const int _keySize = 32; // 256 bits
        private const int _iterations = 500;
        private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

        private const char segmentDelimiter = ':';

        /// <summary>
        /// Hash specific string, seed is embed directly to hash string result
        /// </summary>
        /// <param name="input">string to hash</param>
        /// <returns></returns>
        public static string Hash(string input)
        {
            var salt = RandomNumberGenerator.GetBytes(_saltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                _iterations,
                _algorithm,
                _keySize
            );
            return string.Join(
                segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                _iterations,
                _algorithm
            );
        }

        /// <summary>
        /// Async version of <see cref="Hash(string)"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<string> HashAsync(string input) => await Task.FromResult(Hash(input));

        /// <summary>
        /// Verify a hash string and a original string is equal
        /// </summary>
        /// <param name="input">original string</param>
        /// <param name="hashString">hash string to compare</param>
        /// <returns>true if two string is equal, otherwise false</returns>
        public static bool Verify(string input, string hashString)
        {
            var segments = hashString.Split(segmentDelimiter);
            var hash = Convert.FromHexString(segments[0]);
            var salt = Convert.FromHexString(segments[1]);
            var iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new(segments[3]);
            var inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }

        /// <summary>
        /// Async version of <see cref="Verify(string, string)"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hashString"></param>
        /// <returns></returns>
        public static async Task<bool> VerifyAsync(string input, string hashString) =>
            await Task.FromResult(Verify(input, hashString));
    }
}