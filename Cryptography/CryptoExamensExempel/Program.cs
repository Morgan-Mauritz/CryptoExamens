// See https://aka.ms/new-console-template for more information

using CryptoExamensExempel.Extensions;
using CryptoExamensExempel.BlockCiphers.AES;
using System.Text;

Console.WriteLine("Hello, World!");

// För att köra AES2
//string plainText = File.ReadAllText(@"C:\Users\Ny Användare\source\repos\consoleEncryptTest\consoleEncryptTest\LOTR.txt"); //Ändra till rätt plats!

//for (int cryptoLoops = 0; cryptoLoops < 50; cryptoLoops++)
//{
//    //Räkna bort första loopen? Outlier.
//    var result = AES2.AesCrypto(plainText);
//    CryptoExtension.TotalMillisecondsEncrypt = result.Item2;
//    Console.WriteLine("Total milliseconds encrypt: " + CryptoExtension.TotalMillisecondsEncrypt);
//    CryptoExtension.TotalMillisecondsDecrypt = result.Item4;
//    Console.WriteLine("Total milliseconds decrypt: " + CryptoExtension.TotalMillisecondsDecrypt);
//    CryptoExtension.EncryptDecryptLoops++;
//    Console.WriteLine("Loops: " + CryptoExtension.EncryptDecryptLoops);
//}

//Console.WriteLine("Encryption average milliseconds: " + CryptoExtension.AverageMillisecondsEncrypt);
//Console.WriteLine("Decryption average milliseconds: " + CryptoExtension.AverageMillisecondsDecrypt);


//// Print encryption
////var encryption = AES2.AesCrypto(plainText);
////Console.WriteLine($"Encrypted: {Encoding.UTF8.GetString(encryption.Item1)}");
////Console.WriteLine("Encrypted: " + string.Join("", encryption.Item1));
////Console.WriteLine("Decrypted: " + encryption.Item3);