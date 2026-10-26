# AES-RSA SECURITY LAB

## 1. Giới thiệu

Dự án thực hành lập trình mã hóa và giải mã dữ liệu bằng hai thuật toán AES và RSA, sử dụng ngôn ngữ C# trên nền tảng .NET.

Dự án gồm hai ứng dụng Console độc lập:

- **AES-App:** Thực hiện mã hóa và giải mã văn bản bằng AES-256.
- **RSA-App:** Tạo cặp khóa Public Key và Private Key, thực hiện mã hóa và giải mã văn bản bằng RSA-2048.

## 2. Công nghệ sử dụng

- Ngôn ngữ: C#
- Nền tảng: .NET 10
- Công cụ phát triển: Visual Studio Code
- Thư viện: System.Security.Cryptography

## 3. Cấu trúc thư mục

```text
AES-RSA-SECURITY-LAB/
├── AES-App/
│   ├── AES-App.csproj
│   └── Program.cs
├── RSA-App/
│   ├── RSA-App.csproj
│   └── Program.cs
├── .gitignore
└── README.md
```

## 4. Chạy chương trình AES

Mở Terminal tại thư mục gốc của dự án:

```powershell
cd AES-App
dotnet run
```

Nhập văn bản cần mã hóa. Chương trình sẽ hiển thị khóa, IV, bản mã và kết quả giải mã.

## 5. Chạy chương trình RSA

Mở Terminal tại thư mục gốc của dự án:

```powershell
cd RSA-App
dotnet run
```

Chương trình sẽ tạo cặp khóa RSA-2048, mã hóa văn bản bằng Public Key và giải mã bằng Private Key.

## 6. Kết quả kiểm thử

Hai chương trình đã được chạy thử thành công:

- AES-256: Văn bản sau giải mã trùng với văn bản ban đầu.
- RSA-2048: Văn bản sau giải mã trùng với văn bản ban đầu.
- Cả hai chương trình đều hiển thị thông báo kiểm tra dữ liệu thành công.

## 7. Kết luận

Bài thực hành minh họa cách sử dụng thuật toán mã hóa đối xứng AES và thuật toán mã hóa bất đối xứng RSA trong ứng dụng C#.

AES phù hợp với mã hóa dữ liệu có kích thước lớn, trong khi RSA thường được sử dụng để bảo vệ khóa, xác thực và hỗ trợ trao đổi khóa an toàn.
