
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

class AesKeyData
{
    public string Key { get; set; } = "";
    public string IV { get; set; } = "";
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("===== CHUONG TRINH AES-256 =====");

        Console.Write("Nhap van ban can ma hoa: ");
        string plainText = Console.ReadLine() ?? "";

        // 1. Tao khoa AES-256 va IV
        using Aes aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        aes.GenerateKey();
        aes.GenerateIV();

        // 2. Luu khoa va IV ra file
        string keyFile = "aes-key.json";

        AesKeyData keyData = new AesKeyData
        {
            Key = Convert.ToBase64String(aes.Key),
            IV = Convert.ToBase64String(aes.IV)
        };

        string json = JsonSerializer.Serialize(
            keyData,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(keyFile, json);

        Console.WriteLine("\n===== LUU KHOA =====");
        Console.WriteLine("Da luu khoa va IV vao: " +
                          Path.GetFullPath(keyFile));

        // 3. Ma hoa van ban
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] encryptedBytes;

        using (ICryptoTransform encryptor =
               aes.CreateEncryptor())
        {
            encryptedBytes = encryptor.TransformFinalBlock(
                plainBytes, 0, plainBytes.Length);
        }

        string encryptedText =
            Convert.ToBase64String(encryptedBytes);

        Console.WriteLine("\n===== KET QUA MA HOA =====");
        Console.WriteLine("Ciphertext: " + encryptedText);

        // 4. Doc khoa va IV tu file
        string loadedJson = File.ReadAllText(keyFile);

        AesKeyData loadedKeyData =
            JsonSerializer.Deserialize<AesKeyData>(loadedJson)
            ?? throw new Exception("Khong doc duoc khoa!");

        byte[] loadedKey =
            Convert.FromBase64String(loadedKeyData.Key);

        byte[] loadedIV =
            Convert.FromBase64String(loadedKeyData.IV);

        if (loadedKey.Length != 32 || loadedIV.Length != 16)
        {
            throw new Exception("Kich thuoc khoa hoac IV khong hop le!");
        }

        Console.WriteLine("\n===== DOC KHOA =====");
        Console.WriteLine("Da doc khoa va IV tu file thanh cong!");

        // 5. Giai ma bang khoa doc tu file
        using Aes aesDecrypt = Aes.Create();

        aesDecrypt.Key = loadedKey;
        aesDecrypt.IV = loadedIV;
        aesDecrypt.Mode = CipherMode.CBC;
        aesDecrypt.Padding = PaddingMode.PKCS7;

        byte[] decryptedBytes;

        using (ICryptoTransform decryptor =
               aesDecrypt.CreateDecryptor())
        {
            decryptedBytes = decryptor.TransformFinalBlock(
                encryptedBytes, 0, encryptedBytes.Length);
        }

        string decryptedText =
            Encoding.UTF8.GetString(decryptedBytes);

        Console.WriteLine("\n===== KET QUA GIAI MA =====");
        Console.WriteLine("Van ban goc: " + plainText);
        Console.WriteLine("Van ban giai ma: " + decryptedText);

        // 6. Kiem tra
        Console.WriteLine("\n===== KIEM TRA =====");

        Console.WriteLine(
            plainText == decryptedText
                ? "THANH CONG: Du lieu khop nhau!"
                : "THAT BAI: Du lieu khong khop!"
        );

    Console.WriteLine("\n===== MA HOA FILE AES-256 =====");

Console.Write("Ban co muon thu ma hoa file test.txt? (y/n): ");
string choice = Console.ReadLine() ?? "";

if (choice.Trim().Equals("y", StringComparison.OrdinalIgnoreCase))
{
    try
    {
        string inputFile = "test.txt";
        string encryptedFile = "test.txt.aes";
        string decryptedFile = "test_decrypted.txt";

        // Su dung khoa AES-256 da tao trong chuong trinh
        byte[] fileKey = aes.Key;

        // Xoa file ket qua cu de cho phep thu lai
        if (System.IO.File.Exists(encryptedFile))
            System.IO.File.Delete(encryptedFile);

        if (System.IO.File.Exists(decryptedFile))
            System.IO.File.Delete(decryptedFile);

        AesFileService.EncryptFile(
            inputFile, encryptedFile, fileKey
        ).GetAwaiter().GetResult();

        Console.WriteLine("Ma hoa file thanh cong: " + encryptedFile);

        AesFileService.DecryptFile(
            encryptedFile, decryptedFile, fileKey
        ).GetAwaiter().GetResult();

        Console.WriteLine("Giai ma file thanh cong: " + decryptedFile);

        byte[] original = System.IO.File.ReadAllBytes(inputFile);
        byte[] restored = System.IO.File.ReadAllBytes(decryptedFile);

        Console.WriteLine(
            System.Security.Cryptography.CryptographicOperations
                .FixedTimeEquals(original, restored)
                ? "THANH CONG: Hai file giong nhau!"
                : "THAT BAI: Hai file khong khop!"
        );
    }
    catch (Exception ex)
    {
        Console.WriteLine("LOI: " + ex.Message);
    }
}
}
}
