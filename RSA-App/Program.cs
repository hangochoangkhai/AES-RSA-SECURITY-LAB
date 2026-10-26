
using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("===== CHUONG TRINH MA HOA RSA-2048 =====");

        Console.Write("Nhap van ban can ma hoa: ");
        string plainText = Console.ReadLine() ?? "";

        using RSA rsa = RSA.Create(2048);

        // Tao cap khoa RSA
        byte[] publicKey = rsa.ExportSubjectPublicKeyInfo();
        byte[] privateKey = rsa.ExportPkcs8PrivateKey();

        Console.WriteLine("\n===== TAO CAP KHOA =====");
        Console.WriteLine("Public Key da duoc tao.");
        Console.WriteLine("Private Key da duoc tao.");

        // Ma hoa bang Public Key
        using RSA rsaEncrypt = RSA.Create();
        rsaEncrypt.ImportSubjectPublicKeyInfo(publicKey, out _);

        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        byte[] encryptedBytes = rsaEncrypt.Encrypt(
            plainBytes,
            RSAEncryptionPadding.OaepSHA256
        );

        Console.WriteLine("\n===== KET QUA MA HOA =====");
        Console.WriteLine("Ciphertext (Base64):");
        Console.WriteLine(Convert.ToBase64String(encryptedBytes));

        // Giai ma bang Private Key
        using RSA rsaDecrypt = RSA.Create();
        rsaDecrypt.ImportPkcs8PrivateKey(privateKey, out _);

        byte[] decryptedBytes = rsaDecrypt.Decrypt(
            encryptedBytes,
            RSAEncryptionPadding.OaepSHA256
        );

        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

        Console.WriteLine("\n===== KET QUA GIAI MA =====");
        Console.WriteLine("Van ban goc: " + plainText);
        Console.WriteLine("Van ban giai ma: " + decryptedText);

        Console.WriteLine("\n===== KIEM TRA =====");
        Console.WriteLine(
            plainText == decryptedText
                ? "THANH CONG: Du lieu khop nhau!"
                : "THAT BAI: Du lieu khong khop!"
        );
    }
}

