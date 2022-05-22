using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace CryptoExamensExempel.BlockCiphers.AES
{
    internal class AES
    {
        // Work in progress
        public static void AesEncrypt()
        {
            using (FileStream fs = new FileStream("cryptoTest.txt", FileMode.OpenOrCreate))
            {
                using (Aes aes = Aes.Create())
                {
                    //Till skillna från Random är RandomNumberGenerator kryptografiskt stark.
                    var rnd = RandomNumberGenerator.Create();
                    byte[] keyByteArray = new byte[16];
                    //rnd.NextBytes(keyByteArray);
                    rnd.GetBytes(keyByteArray);
                    var keyString = Encoding.UTF8.GetString(keyByteArray, 0, keyByteArray.Length);
                    Console.WriteLine("Nycekln i form av en 'byte array':   " + keyString);

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
