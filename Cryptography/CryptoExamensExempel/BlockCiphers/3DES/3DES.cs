using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExamensExempel.BlockCiphers._3DES
{
    public class _3DES
    {
        private const string key = "nyckel";

        public static byte[] Encrypt(string plainText)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.IV = new byte[8];
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[0]);
            des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
            MemoryStream ms = new MemoryStream(plainMessage.Length * 2);
            CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(),
            CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainMessage);
            encStream.Write(plainBytes, 0, plainBytes.Length);
            encStream.FlushFinalBlock();
            byte[] encryptedBytes = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(encryptedBytes, 0, (int)ms.Length);
            encStream.Close();
            return encryptedBytes;
        }

        public static string Decrypt(string encryptedBase64)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.IV = new byte[8];
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[0]);
            des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
            MemoryStream ms = new MemoryStream(encryptedBase64.Length);
            CryptoStream decStream = new CryptoStream(ms, des.CreateDecryptor(),
            CryptoStreamMode.Write);
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();
            byte[] plainBytes = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(plainBytes, 0, (int)ms.Length);
            decStream.Close();
            return Encoding.UTF8.GetString(plainBytes);
        }
    
    }
}
