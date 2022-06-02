using System.Security.Cryptography;
using System.Diagnostics;

namespace CryptoExamensExempel.BlockCiphers.AES
{
    public static class AES2
    {
        public static (byte[], long, string, long) AesCrypto(string plainText, ref List<string> listOfResults)
        {
            (byte[], long) encrypted;
            (string, long) decrypted;

            using (Aes aes = Aes.Create())
            {
                encrypted = EncryptStringToBytes(plainText, aes.Key, aes.IV, ref listOfResults);

                decrypted = DecryptStringFromBytes(encrypted.Item1, aes.Key, aes.IV, ref listOfResults);
            }

            return (encrypted.Item1, encrypted.Item2, decrypted.Item1, decrypted.Item2);
        }

        private static (byte[], long) EncryptStringToBytes(string plainText, byte[] key, byte[] iv, ref List<string> listOfResults)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            byte[] encrypted;

            Stopwatch stopwatch = Stopwatch.StartNew();

            using (Aes aesEncrypt = Aes.Create())
            {
                aesEncrypt.Key = key;
                aesEncrypt.IV = iv;

                ICryptoTransform encryptor = aesEncrypt.CreateEncryptor(aesEncrypt.Key, aesEncrypt.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        encrypted = ms.ToArray();
                    }
                }
            }

            stopwatch.Stop();
            listOfResults.Add("Milliseconds to encrypt: " + stopwatch.Elapsed);
            Console.WriteLine("Milliseconds to encrypt: " + stopwatch.Elapsed);

            return (encrypted, stopwatch.ElapsedMilliseconds);
        }

        private static (string, long) DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv, ref List<string> listOfResults)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");

            string? decryptedPlainText = null;

            Stopwatch stopwatch = Stopwatch.StartNew();

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decryptedPlainText = sr.ReadToEnd();
                        }
                    }
                }
            }

            stopwatch.Stop();
            listOfResults.Add("Milliseconds to decrypt: " + stopwatch.Elapsed);
            Console.WriteLine("Milliseconds to decrypt: " + stopwatch.Elapsed);
            return (decryptedPlainText, stopwatch.ElapsedMilliseconds);
        }
    }
}
