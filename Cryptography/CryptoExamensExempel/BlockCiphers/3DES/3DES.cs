using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace CryptoExamensExempel.BlockCiphers._3DES
{
    public class _3DES
    {
        private const string key = "nyckel";
        
        public string Encrypt(string plainText)
        {
            using (var des = TripleDES.Create())
            {
                des.IV = new byte[8];
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[0]);
                des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);

                using(MemoryStream ms = new MemoryStream(plainText.Length * 2)){

                    using(CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        encStream.Write(plainBytes, 0, plainBytes.Length);
            
                        encStream.FlushFinalBlock();
            
                        byte[] encryptedBytes = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(encryptedBytes, 0, (int)ms.Length);
                        
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            } 
        }

        public string Decrypt(string encryptedBase64)
        {
            using (var des = TripleDES.Create())
            {
                 des.IV = new byte[8];
                 PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[0]);
                 des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
                 byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);

                using(MemoryStream ms = new MemoryStream(encryptedBase64.Length))
                {
                    using (CryptoStream decStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                        decStream.FlushFinalBlock();
                        byte[] plainBytes = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(plainBytes, 0, (int)ms.Length);

                        return Encoding.UTF8.GetString(plainBytes);
                    }
                } 
            }
        }
    
    }
}
