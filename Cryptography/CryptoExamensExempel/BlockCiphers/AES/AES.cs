using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Diagnostics;

namespace CryptoExamensExempel.BlockCiphers.AES
{
    public class AES
    {
        string path = File.ReadAllText(@"C:\Users\menta\Source\Repos\CryptoExamens\Cryptography\CryptoExamensExempel\LOTR.txt"); //'Path' är egentligen filen (Cleartext)

        public string EncryptAes(int oneortwo)
        {
            Stopwatch sw = Stopwatch.StartNew();
            byte[] encrypted;
            string decrypted;

            sw.Start();

            using(Aes aes = Aes.Create()) // OBS! Man kan ej separera Encrypted och Decrypted om dem inte använder sig av samma Key och IV!
            {
                encrypted = EncryptStringToBytes(path, aes.Key, aes.IV);
                decrypted = DecryptStringFromBytes(encrypted, aes.Key, aes.IV);
            }
            sw.Stop();

            if (oneortwo == 1) //Om du vill ha Encrypterad Text, skriv Console.Write(test.EncryptAes(1)) i Program.cs!
            {
                return $"AES Encryption & Decryption Time: {sw.ElapsedMilliseconds} \n AES Encryption Desc: {System.Text.Encoding.Default.GetString(encrypted)}"; //Encrypted är fortfarande bytes.
            }
            else //Om du vill ha Decrypted Text, skriv Console.Write(test.EncryptAes(2)) i Program.cs!
            return $"AES Encryption & Decryption Time: {sw.ElapsedMilliseconds} \n Decrypted Text: {decrypted}";
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");
            
            byte[] encrypted;

            using(Aes aesEncrypt = Aes.Create())
            {
                aesEncrypt.Key = key;
                aesEncrypt.IV = iv;

                ICryptoTransform encryptor = aesEncrypt.CreateEncryptor(aesEncrypt.Key, aesEncrypt.IV);

                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
