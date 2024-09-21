using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ConsoleAppTesting.Clases
{
    internal class Password
    {
        public (string Hash, string Salt) HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16); // 16 Bytes

            string hashed = HashMachie(password, saltBytes);

            return (hashed, Convert.ToBase64String(saltBytes));
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            string hashed = HashMachie(password, saltBytes);

            return hashed == hashedPassword;
        }

        private string HashMachie(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32)); // 32 Bytes or 64 Bytes
        }
    }
}
