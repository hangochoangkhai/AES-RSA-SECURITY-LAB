
using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("===== CHUONG TRINH MA HOA AES-256 =====");

        Console.Write("Nhap van ban can ma hoa: ");
        string plainText = Console.ReadLine() ?? "";

        using Aes aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        aes.GenerateKey();
        aes.GenerateIV();

        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        byte[] encryptedBytes;

        using (ICryptoTransform encryptor =
               aes.CreateEncryptor(aes.Key, aes.IV))
        {
            encryptedBytes = encryptor.TransformFinalBlock(
                plainBytes, 0, plainBytes.Length);
        }

        string encryptedText = Convert.ToBase64String(encryptedBytes);

        Console.WriteLine("\n===== KET QUA MA HOA =====");
        Console.WriteLine("Key (Base64): " + Convert.ToBase64String(aes.Key));
        Console.WriteLine("IV (Base64): " + Convert.ToBase64String(aes.IV));
        Console.WriteLine("Ciphertext: " + encryptedText);

        byte[] decryptedBytes;

        using (ICryptoTransform decryptor =
               aes.CreateDecryptor(aes.Key, aes.IV))
        {
            decryptedBytes = decryptor.TransformFinalBlock(
                encryptedBytes, 0, encryptedBytes.Length);
        }

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

