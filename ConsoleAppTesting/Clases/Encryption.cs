
using System.Security.Cryptography;
using System.Text;

namespace ConsoleAppTesting.Clases
{
    internal class Encryption
    {
        private static readonly string Key = "TuClaveDeEncriptacionSegura12345"; // Debe tener al menos 32 caracteres
        private static readonly string IV = "TuIVDeEncriptaci"; // Debe tener el tamaño de bloque del algoritmo, por ejemplo, 16 bytes para AES

        public static string Encrypt(string plainText)
        {
            //using (Aes aes = Aes.Create())
            //{
            //    aes.Key = Encoding.UTF8.GetBytes(Key);
            //    aes.IV = Encoding.UTF8.GetBytes(IV); // IV debe ser único por cada operación de cifrado en producción

            //    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            //    using (var ms = new MemoryStream())
            //    {
            //        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            //        {
            //            using (var sw = new StreamWriter(cs))
            //            {
            //                sw.Write(plainText);
            //            }
            //        }

            //        byte[] encrypted = ms.ToArray();
            //        return Convert.ToBase64String(encrypted);
            //    }
            //}

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);

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

        public static string Decrypt(string encryptedText)
        {
            //byte[] cipherBytes = Convert.FromBase64String(encryptedText);

            //using (Aes aes = Aes.Create())
            //{
            //    aes.Key = Encoding.UTF8.GetBytes(Key);
            //    aes.IV = Encoding.UTF8.GetBytes(IV); // Debe coincidir con el IV usado en el cifrado

            //    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            //    using (var ms = new MemoryStream(cipherBytes))
            //    {
            //        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            //        {
            //            using (var sr = new StreamReader(cs))
            //            {
            //                return sr.ReadToEnd();
            //            }
            //        }
            //    }
            //}

            byte[] cipherBytes = Convert.FromBase64String(encryptedText);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }

    }
}
