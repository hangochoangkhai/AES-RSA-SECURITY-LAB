
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

public static class AesFileService
{
    // Ma hoa file bang AES-256-CBC
    public static async Task EncryptFile(
        string inputPath,
        string outputPath,
        byte[] key)
    {
        using Aes aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = key;
        aes.GenerateIV();

        await using FileStream input = new FileStream(
            inputPath, FileMode.Open, FileAccess.Read);

        await using FileStream output = new FileStream(
            outputPath, FileMode.CreateNew, FileAccess.Write);

        // Luu IV 16 byte o dau file ma hoa
        await output.WriteAsync(aes.IV);

        using ICryptoTransform encryptor =
            aes.CreateEncryptor();

        using CryptoStream cryptoStream = new CryptoStream(
            output, encryptor, CryptoStreamMode.Write,
            leaveOpen: true);

        await input.CopyToAsync(cryptoStream);
        cryptoStream.FlushFinalBlock();
    }

    // Giai ma file AES-256-CBC
    public static async Task DecryptFile(
        string inputPath,
        string outputPath,
        byte[] key)
    {
        using Aes aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = key;

        await using FileStream input = new FileStream(
            inputPath, FileMode.Open, FileAccess.Read);

        if (input.Length < 32)
            throw new InvalidDataException(
                "File ma hoa khong hop le!");

        // Doc IV 16 byte tu dau file
        byte[] iv = new byte[16];
        await input.ReadExactlyAsync(iv);
        aes.IV = iv;

        using ICryptoTransform decryptor =
            aes.CreateDecryptor();

        using CryptoStream cryptoStream = new CryptoStream(
            input, decryptor, CryptoStreamMode.Read);

        await using FileStream output = new FileStream(
            outputPath, FileMode.CreateNew, FileAccess.Write);

        await cryptoStream.CopyToAsync(output);
    }
}
