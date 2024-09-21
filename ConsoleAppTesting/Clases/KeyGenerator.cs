using System.Security.Cryptography;

namespace ConsoleAppTesting.Clases
{
    internal class KeyGenerator
    {
        public static string key16()
        {
            var key = new byte[16]; // 128 bits (16 bytes)
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(key);
            }

            string secretKey = Convert.ToBase64String(key);

            return secretKey;
        }

        public static string key32()
        {
            var key = new byte[32]; // 256 bits (32 bytes)
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(key);
            }

            string secretKey = Convert.ToBase64String(key);

            return secretKey;
        }

        public static string key64()
        {
            byte[] secretKey = RandomNumberGenerator.GetBytes(64); // 512 bits (64 bytes)
            return Convert.ToBase64String(secretKey);
        }
    }
}
