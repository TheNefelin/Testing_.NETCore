using ConsoleAppTesting.Models.Dtos;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleAppTesting.Utils
{
    internal class Cryptex
    {
        public static DataEncryptedDTO Encrypt(string plainText, string pass)
        {
            using var aes = Aes.Create();
            aes.Key = GetAesKey(pass);
            aes.IV = RandomNumberGenerator.GetBytes(16);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            byte[] encrypted = ms.ToArray();

            DataEncryptedDTO data = new DataEncryptedDTO()
            {
                Encrypted = Convert.ToBase64String(encrypted),
                IV = Convert.ToBase64String(aes.IV),
            };

            return data;
        }

        public static string Decrypt(string encryptedText, string pass, string iv)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);

            using var aes = Aes.Create();
            aes.Key = GetAesKey(pass);
            aes.IV = Convert.FromBase64String(iv);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }

        private static byte[] GetAesKey(string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            while (keyBytes.Length < 32)
                keyBytes = keyBytes.Concat(keyBytes).ToArray(); // Concatenamos la clave
         
            return keyBytes.Take(32).ToArray();
        }
    }
}
