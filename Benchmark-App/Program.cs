
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

const int iterations = 1000;
byte[] data = Encoding.UTF8.GetBytes("Du lieu kiem thu AES va RSA");

Console.WriteLine("=== SO SANH TOC DO AES-256 VA RSA-2048 ===");
Console.WriteLine($"So lan lap: {iterations}");
Console.WriteLine($"Kich thuoc du lieu: {data.Length} bytes");
Console.WriteLine();

// AES-256-CBC
using Aes aes = Aes.Create();
aes.KeySize = 256;
aes.Mode = CipherMode.CBC;
aes.Padding = PaddingMode.PKCS7;
aes.GenerateKey();
aes.GenerateIV();

byte[] aesEncrypted = aes.EncryptCbc(data, aes.IV, PaddingMode.PKCS7);

Stopwatch sw = Stopwatch.StartNew();

for (int i = 0; i < iterations; i++)
{
    aes.EncryptCbc(data, aes.IV, PaddingMode.PKCS7);
}

sw.Stop();
double aesEncryptMs = sw.Elapsed.TotalMilliseconds / iterations;

sw.Restart();

byte[] aesDecrypted = Array.Empty<byte>();

for (int i = 0; i < iterations; i++)
{
    aesDecrypted = aes.DecryptCbc(
        aesEncrypted, aes.IV, PaddingMode.PKCS7);
}

sw.Stop();
double aesDecryptMs = sw.Elapsed.TotalMilliseconds / iterations;

// RSA-2048-OAEP-SHA256
using RSA rsa = RSA.Create(2048);
RSAEncryptionPadding padding = RSAEncryptionPadding.OaepSHA256;

byte[] rsaEncrypted = rsa.Encrypt(data, padding);

sw.Restart();

for (int i = 0; i < iterations; i++)
{
    rsa.Encrypt(data, padding);
}

sw.Stop();
double rsaEncryptMs = sw.Elapsed.TotalMilliseconds / iterations;

sw.Restart();

byte[] rsaDecrypted = Array.Empty<byte>();

for (int i = 0; i < iterations; i++)
{
    rsaDecrypted = rsa.Decrypt(rsaEncrypted, padding);
}

sw.Stop();
double rsaDecryptMs = sw.Elapsed.TotalMilliseconds / iterations;

Console.WriteLine("Thoi gian trung binh moi lan (ms):");
Console.WriteLine("-----------------------------------");
Console.WriteLine($"AES ma hoa  : {aesEncryptMs:F6} ms");
Console.WriteLine($"AES giai ma: {aesDecryptMs:F6} ms");
Console.WriteLine($"RSA ma hoa  : {rsaEncryptMs:F6} ms");
Console.WriteLine($"RSA giai ma: {rsaDecryptMs:F6} ms");

Console.WriteLine();
Console.WriteLine("Kiem tra ket qua:");

Console.WriteLine(
    $"AES: {(data.SequenceEqual(aesDecrypted) ? "THANH CONG" : "THAT BAI")}");

Console.WriteLine(
    $"RSA: {(data.SequenceEqual(rsaDecrypted) ? "THANH CONG" : "THAT BAI")}");
