using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace CryptoExamensExempel.BlockCiphers.AES
{
    public class AES
    {
        const string path = @"C:\ExamenCrypto\CryptoExamens\Cryptography\CryptoExamensExempel\LOTR.txt";

        public byte[] EncryptAes()
        {
            Stopwatch sw = Stopwatch.StartNew();
            byte[] encrypted;
            
            sw.Start();

            using(Aes aes = Aes.Create())
            {
                encrypted = EncryptStringToBytes(path, aes.Key, aes.IV);
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            return encrypted;
        }

        private static byte[] EncryptStringToBytes(string original, byte[] key, byte[] iv)
        {
            if (original == null || original.Length <= 0)
                throw new ArgumentNullException("original");
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
                            sw.Write(original);
                        }

                        encrypted = ms.ToArray();
                    }
                }
            }

            return encrypted;
        }




        // Work in progress
        public static void AesEncrypt()
        {
            //var path = @"C:\ExamenCrypto\CryptoExamens\Cryptography\CryptoExamensExempel\LOTR.txt";
            var sw = new Stopwatch();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (Aes aes = Aes.Create())
                {                   
                    var rnd = RandomNumberGenerator.Create();
                    byte[] keyByteArray = new byte[16];
                    //rnd.NextBytes(keyByteArray);
                    rnd.GetBytes(keyByteArray);
                    var keyString = Encoding.UTF8.GetString(keyByteArray, 0, keyByteArray.Length);
                    //Console.WriteLine("Nycekln i form av en 'byte array':   " + keyString);

                    aes.Key = keyByteArray;
                    byte[] iv = aes.IV;

                    fs.Write(iv, 0, iv.Length);

                    using (CryptoStream cs = new CryptoStream(fs, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter encryptWriter = new StreamWriter(cs))
                        {
                            var cryptoStreamRes = cs.CopyTo;
                            var result = Encoding.UTF8.GetString(cs.);
                            encryptWriter.WriteLine("Encrypted AF!");
                            Console.WriteLine("Result: " + Encoding.UTF8.GetString());
                        }
                    }
                }

            }

        }
    }
}
