
using System.Security.Cryptography;

namespace ConsoleAppTesting.Utils
{
    internal class Encryption
    {
        //private static readonly string Key = "TuClaveDeEncriptacionSegura12345"; // Debe tener al menos 32 caracteres
        //private static readonly string IV = "TuIVDeEncriptaci"; // Debe tener el tamaño de bloque del algoritmo, por ejemplo, 16 bytes para AES

        public static string Encrypt(string plainText, string key, string iv)
        {
            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(key);
            aes.IV = Convert.FromBase64String(iv);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            byte[] encrypted = ms.ToArray();
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string encryptedText, string key, string iv)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);

            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(key);
            aes.IV = Convert.FromBase64String(iv);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }

    }
}
