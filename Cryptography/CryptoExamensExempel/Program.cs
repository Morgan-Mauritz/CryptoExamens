// See https://aka.ms/new-console-template for more information

using CryptoExamensExempel.Extensions;
using CryptoExamensExempel.BlockCiphers.AES;
using CryptoExamensExempel.BlockCiphers._3DES; 
using System.Text;


var baseDirectory = Directory.GetCurrentDirectory();
var workingDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;

using (var fs = new FileStream(workingDirectory + "/dummy16.txt", FileMode.Create, FileAccess.Write, FileShare.None))
{
    fs.SetLength(16000000);
}
using (var fs = new FileStream(workingDirectory + "/dummy32.txt", FileMode.Create, FileAccess.Write, FileShare.None))
{
    fs.SetLength(32000000);
}
using (var fs = new FileStream(workingDirectory + "/dummy64.txt", FileMode.Create, FileAccess.Write, FileShare.None))
{
    fs.SetLength(64000000);
}
using (var fs = new FileStream(workingDirectory + "/dummy128.txt", FileMode.Create, FileAccess.Write, FileShare.None))
{
    fs.SetLength(128000000);
}

var plainText = File.ReadAllText($"{workingDirectory}/dummy128.txt");



for (int cryptoLoops = 0; cryptoLoops < 1; cryptoLoops++)
{
    //Räkna bort första loopen? Outlier.
    var result = _3DES.TrippleDesCrypto(plainText);
    CryptoExtension.TotalMillisecondsEncrypt = result.Item2;
    Console.WriteLine("Total milliseconds encrypt: " + CryptoExtension.TotalMillisecondsEncrypt);
    CryptoExtension.TotalMillisecondsDecrypt = result.Item4;
    Console.WriteLine("Total milliseconds decrypt: " + CryptoExtension.TotalMillisecondsDecrypt);
    CryptoExtension.EncryptDecryptLoops++;
    Console.WriteLine("Loops: " + CryptoExtension.EncryptDecryptLoops);
}

//var trippleDesEncryption = _3DES.TrippleDesCrypto(plainText);
//Console.WriteLine("Encrypted text: {0}", Encoding.UTF8.GetString(trippleDesEncryption.Item1));
//Console.WriteLine("Decrypted text: {0}", trippleDesEncryption.Item3);
Console.WriteLine("Encryption average milliseconds: {0}", CryptoExtension.AverageMillisecondsEncrypt);
Console.WriteLine("Decryption average milliseconds: {0}", CryptoExtension.AverageMillisecondsDecrypt);

CryptoExtension.Reset();

for (int cryptoLoops = 0; cryptoLoops < 1; cryptoLoops++)
{
    //Räkna bort första loopen? Outlier.
    var result = AES2.AesCrypto(plainText);
    CryptoExtension.TotalMillisecondsEncrypt = result.Item2;
    Console.WriteLine("Total milliseconds encrypt: " + CryptoExtension.TotalMillisecondsEncrypt);
    CryptoExtension.TotalMillisecondsDecrypt = result.Item4;
    Console.WriteLine("Total milliseconds decrypt: " + CryptoExtension.TotalMillisecondsDecrypt);
    CryptoExtension.EncryptDecryptLoops++;
    Console.WriteLine("Loops: " + CryptoExtension.EncryptDecryptLoops);
}

//var aesEncryption = AES2.AesCrypto(plainText);
//Console.WriteLine("Encrypted text: {0}", Encoding.UTF8.GetString(aesEncryption.Item1));
//Console.WriteLine("Decrypted text: {0}", aesEncryption.Item3);
Console.WriteLine("Encryption average milliseconds: " + CryptoExtension.AverageMillisecondsEncrypt);
Console.WriteLine("Decryption average milliseconds: " + CryptoExtension.AverageMillisecondsDecrypt);

// Print encryption
//var encryption = AES2.AesCrypto(plainText);
//Console.WriteLine($"Encrypted: {Encoding.UTF8.GetString(encryption.Item1)}");
//Console.WriteLine("Encrypted: " + string.Join("", encryption.Item1));
//Console.WriteLine("Decrypted: " + encryption.Item3);