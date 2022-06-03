using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace CryptoExamensExempel.BlockCiphers._3DES
{
    public static class _3DES
    {   
        public static (byte[], long, string, long, TimeSpan) TripleDesCrypto(string plainText, ref List<string> listOfResult)
        {
            (byte[], long) encrypted;
            (string, long) decrypted;
            TimeSpan keyElapsed;

            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            using (TripleDES tripDes = TripleDES.Create())
            {
                stopwatch.Stop();
                keyElapsed = stopwatch.Elapsed;
                Console.WriteLine("Key and IV creation {0}", keyElapsed);

                encrypted = Encrypt(plainText, tripDes.Key, tripDes.IV, ref listOfResult);

                decrypted = Decrypt(encrypted.Item1, tripDes.Key, tripDes.IV, ref listOfResult);
            }

            return (encrypted.Item1, encrypted.Item2, decrypted.Item1, decrypted.Item2, keyElapsed);
        }
        

        public static (byte[], long) Encrypt(string plainText, byte[] key, byte[] iv, ref List<string> listOfResult)
        {
            byte[] encrypted;

            Stopwatch stopwatch = Stopwatch.StartNew();

            using (var des = TripleDES.Create())
            {
                des.Key = key;
                des.IV = iv;


                ICryptoTransform encryptor = des.CreateEncryptor(des.Key, des.IV);
              

                using (MemoryStream ms = new MemoryStream(plainText.Length)){

                    using(CryptoStream encStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {

                        using (StreamWriter sw = new StreamWriter(encStream))
                        {
                            sw.Write(plainText);
                        }

                        encrypted = ms.ToArray();
                
                    }
                }
            }
            stopwatch.Stop();
            listOfResult.Add("Milliseconds to encrypt: " + stopwatch.Elapsed);
            Console.WriteLine("Milliseconds to encrypt: " + stopwatch.Elapsed);
            
            return (encrypted, stopwatch.ElapsedMilliseconds);
        }

        public static (string, long) Decrypt(byte[] cipherText, byte[] key, byte[] iv, ref List<string> listOfResult)
        {
            string? decryptedPlainText = null;

            Stopwatch stopwatch = Stopwatch.StartNew();


            using (var des = TripleDES.Create())
            {
                des.IV = iv;
                des.Key = key;

                ICryptoTransform encryptor = des.CreateDecryptor(des.Key, des.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Read))
                    {

                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decryptedPlainText = sr.ReadToEnd();
                        }                        
                    }
                } 
            }

            stopwatch.Stop();
            listOfResult.Add("Milliseconds to decrypt: " + stopwatch.Elapsed);
            Console.WriteLine("Milliseconds to decrypt: " + stopwatch.Elapsed);

            return (decryptedPlainText, stopwatch.ElapsedMilliseconds);
        }
    
    }
}
