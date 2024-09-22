using System.Data.SqlTypes;
using System.Security.Cryptography;

namespace ConsoleAppTesting.Utils
{
    internal class KeyGenerator
    {
        public static string Salt16()
        {
            byte[] saltByte = RandomNumberGenerator.GetBytes(16); // 128 bits (16 bytes)
            string salt = Convert.ToBase64String(saltByte);

            return salt;
        }

        public static string Salt32()
        {
            byte[] saltByte = RandomNumberGenerator.GetBytes(32); // 256 bits (32 bytes)
            string salt = Convert.ToBase64String(saltByte);

            return salt;
        }

        public static string Salt64()
        {
            byte[] saltByte = RandomNumberGenerator.GetBytes(64); // 512 bits (64 bytes)
            string salt = Convert.ToBase64String(saltByte);
            return salt;
        }
    }
}
