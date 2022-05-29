using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExamensExempel.Extensions
{
    public static class CryptoExtension
    {
        private static int encryptDecryptLoops;
        private static decimal totalMillisecondsEncrypt;
        private static decimal totalMillisecondsDecrypt;

        public static int EncryptDecryptLoops
        {
            get { return encryptDecryptLoops; }
            set { encryptDecryptLoops = value; }
        }

        public static decimal AverageMillisecondsEncrypt
        {
            get { return totalMillisecondsEncrypt / encryptDecryptLoops; }
        }

        public static decimal TotalMillisecondsEncrypt
        {
            get { return totalMillisecondsEncrypt; }
            set { totalMillisecondsEncrypt += value; }
        }

        public static decimal AverageMillisecondsDecrypt
        {
            get { return totalMillisecondsDecrypt / encryptDecryptLoops; }
        }

        public static decimal TotalMillisecondsDecrypt
        {
            get { return totalMillisecondsDecrypt; }
            set { totalMillisecondsDecrypt += value; }
        }
    }
}
