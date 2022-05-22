using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections;
using System.Diagnostics;

namespace CryptoExamensExempel.StreamCiphers.OneTimePad
{
    public class OneTimePad
    {
        private byte[] _key { get; set; }
        private Stopwatch watch { get; set; }
        public OneTimePad(int clearTextLength)
        {
            _key = new byte[clearTextLength];
            RandomNumberGenerator rngCryptoServiceProvider = RandomNumberGenerator.Create();
            rngCryptoServiceProvider.GetBytes(_key);
        }

        public (string encryptedString, TimeSpan timer) Encrypt(string clearText)
        {
            watch = new Stopwatch();
            watch.Start();
            string encryptedString = "";

            for (int j = 0; j < clearText.Length; j++)
            {
                byte clearTextByte = Convert.ToByte(clearText[j]);
                byte keyByte = Convert.ToByte(_key[j]);

                BitArray clearTextBits = new BitArray(new byte[] { clearTextByte });
                BitArray keyBits = new BitArray(new byte[] { keyByte });

                BitArray encryptedBits = new BitArray(8);


                for (int i = 0; i < 8; i++)
                {
                    if (clearTextBits[i] == keyBits[i]) { encryptedBits[i] = false; }
                    else { encryptedBits[i] = true; }
                }

                byte[] testttttt = new byte[2];
                encryptedBits.CopyTo(testttttt, 0);

                encryptedString += BitConverter.ToChar(testttttt, 0); 
                bool stop = true;
            }
            watch.Stop();

            return (encryptedString, watch.Elapsed);
        }
        public (string decryptedString, TimeSpan timer) Decrypt(string encryptedString)
        {
            watch = new Stopwatch();
            watch.Start();

            string decryptedString = "";

            for (int j = 0; j < encryptedString.Length; j++)
            {
                byte encryptedTextByte = Convert.ToByte(encryptedString[j]);
                byte keyByte = Convert.ToByte(_key[j]);

                BitArray encryptedTextBits = new BitArray(new byte[] { encryptedTextByte });
                BitArray keyBits = new BitArray(new byte[] { keyByte });

                BitArray decryptedBits = new BitArray(8);

                for (int i = 0; i < 8; i++)
                {
                    if (encryptedTextBits[i] == keyBits[i]) { decryptedBits[i] = false; }
                    else { decryptedBits[i] = true; }
                }

                byte[] testttttt = new byte[2];
                decryptedBits.CopyTo(testttttt, 0);

                decryptedString += BitConverter.ToChar(testttttt, 0);
            }

            watch.Stop();
            return (decryptedString, watch.Elapsed);
        }
    }
}
