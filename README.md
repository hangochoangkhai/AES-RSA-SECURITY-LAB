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


AES-RSA-SECURITY-LAB/
├── AES-App/
│   ├── AES-App.csproj
│   ├── Program.cs
│   ├── AesFileService.cs
│   └── test.txt
├── RSA-App/
│   ├── RSA-App.csproj
│   └── Program.cs
├── Benchmark-App/
│   ├── Benchmark-App.csproj
│   └── Program.cs
├── .gitignore
└── README.md


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

### 5.1. Chạy chương trình Benchmark

Mở Terminal tại thư mục gốc của dự án và thực hiện:

```powershell
dotnet run --project Benchmark-App -c Release
```

Chương trình sẽ thực hiện 1.000 lần mã hóa và giải mã dữ liệu ngắn bằng AES-256-CBC và RSA-2048-OAEP-SHA256.

Kết quả hiển thị bao gồm:

- Thời gian mã hóa AES trung bình.
- Thời gian giải mã AES trung bình.
- Thời gian mã hóa RSA trung bình.
- Thời gian giải mã RSA trung bình.
- Kết quả kiểm tra dữ liệu sau giải mã.

Các giá trị thời gian được tính bằng mili giây (ms).
## 6. Kết quả kiểm thử

Hai chương trình đã được chạy thử thành công:

- AES-256: Văn bản sau giải mã trùng với văn bản ban đầu.
- RSA-2048: Văn bản sau giải mã trùng với văn bản ban đầu.
- Cả hai chương trình đều hiển thị thông báo kiểm tra dữ liệu thành công.

## 7. Kết luận

Bài thực hành minh họa cách sử dụng thuật toán mã hóa đối xứng AES và thuật toán mã hóa bất đối xứng RSA trong ứng dụng C#.

AES phù hợp với mã hóa dữ liệu có kích thước lớn, trong khi RSA thường được sử dụng để bảo vệ khóa, xác thực và hỗ trợ trao đổi khóa an toàn.
## 8. Cơ sở lý thuyết thuật toán DES và AES

### 8.1. Thuật toán DES

DES (Data Encryption Standard) là thuật toán mã hóa đối xứng, sử dụng cùng một khóa bí mật để mã hóa và giải mã dữ liệu.

**Đặc điểm chính:**

- Kích thước khối dữ liệu: 64 bit.
- Độ dài khóa hiệu dụng: 56 bit.
- Số vòng xử lý: 16 vòng.
- Cấu trúc thuật toán: mạng Feistel.

**Quy trình mã hóa DES:**

1. Dữ liệu đầu vào được chia thành các khối 64 bit.
2. Mỗi khối được thực hiện phép hoán vị ban đầu (Initial Permutation).
3. Khối dữ liệu được chia thành hai nửa trái và phải, mỗi nửa 32 bit.
4. Thuật toán thực hiện 16 vòng Feistel với các khóa con được tạo từ khóa chính.
5. Sau các vòng xử lý, hai nửa được kết hợp và thực hiện phép hoán vị cuối cùng để tạo bản mã.

**Quy trình giải mã DES:**

Quá trình giải mã sử dụng cùng cấu trúc Feistel như quá trình mã hóa, nhưng các khóa con được áp dụng theo thứ tự ngược lại.

**Hạn chế của DES:**

Do khóa hiệu dụng chỉ dài 56 bit, DES không còn đủ an toàn trước các cuộc tấn công vét cạn bằng thiết bị hiện đại. Vì vậy, DES không được khuyến nghị sử dụng để bảo vệ dữ liệu mới.

### 8.2. Thuật toán AES

AES (Advanced Encryption Standard) là thuật toán mã hóa đối xứng theo khối, được sử dụng rộng rãi trong các hệ thống bảo mật hiện đại.

**Đặc điểm chính:**

- Kích thước khối dữ liệu: 128 bit.
- Độ dài khóa hỗ trợ: 128, 192 hoặc 256 bit.
- Số vòng xử lý tương ứng: 10, 12 hoặc 14 vòng.
- Dự án sử dụng AES-256 với khóa 256 bit và 14 vòng xử lý.

**Quy trình mã hóa AES:**

1. **Key Expansion:** Tạo các khóa vòng từ khóa bí mật ban đầu.
2. **AddRoundKey:** Kết hợp trạng thái dữ liệu với khóa vòng bằng phép XOR.
3. **SubBytes:** Thay thế các byte bằng bảng S-box.
4. **ShiftRows:** Dịch vòng các hàng của ma trận trạng thái.
5. **MixColumns:** Biến đổi các cột để tăng khả năng khuếch tán dữ liệu.
6. **AddRoundKey:** Kết hợp dữ liệu với khóa vòng tương ứng.

Các bước biến đổi được thực hiện qua nhiều vòng. Ở vòng cuối cùng, AES không thực hiện MixColumns.

**Quy trình giải mã AES:**

Quá trình giải mã sử dụng các phép biến đổi nghịch đảo như InvShiftRows, InvSubBytes, InvMixColumns và AddRoundKey để khôi phục dữ liệu ban đầu.

### 8.3. Chế độ mã hóa AES-CBC trong dự án

Ứng dụng AES-App sử dụng:

- Thuật toán AES-256.
- Chế độ CBC (Cipher Block Chaining).
- Padding PKCS7.
- Khóa và IV được tạo ngẫu nhiên bằng thư viện System.Security.Cryptography.

Trong chế độ CBC, mỗi khối bản rõ được XOR với khối bản mã trước đó trước khi mã hóa. Khối đầu tiên sử dụng IV (Initialization Vector).

Quy trình thực hiện trong chương trình:

1. Nhập văn bản cần mã hóa.
2. Tạo khóa AES-256 và IV.
3. Chuyển văn bản thành mảng byte UTF-8.
4. Mã hóa dữ liệu bằng AES-CBC.
5. Chuyển bản mã sang Base64 để hiển thị.
6. Sử dụng khóa và IV tương ứng để giải mã.
7. So sánh văn bản giải mã với văn bản gốc.

Lưu ý: IV không cần giữ bí mật nhưng phải được sử dụng đúng với bản mã. Khóa AES phải được bảo vệ an toàn. Chế độ CBC không tự cung cấp khả năng kiểm tra tính toàn vẹn của dữ liệu; trong ứng dụng thực tế cần bổ sung cơ chế xác thực hoặc sử dụng chế độ mã hóa có xác thực như AES-GCM.

### 8.4. So sánh DES và AES

| Tiêu chí | DES | AES |
|---|---|---|
| Loại mã hóa | Đối xứng | Đối xứng |
| Kích thước khối | 64 bit | 128 bit |
| Độ dài khóa | 56 bit hiệu dụng | 128/192/256 bit |
| Số vòng | 16 | 10/12/14 |
| Cấu trúc | Feistel | Mạng thay thế – hoán vị |
| Mức độ an toàn hiện nay | Không còn phù hợp | An toàn khi triển khai đúng |
| Ứng dụng | Chủ yếu mang tính lịch sử | Bảo vệ dữ liệu hiện đại |

**Nhận xét:** AES có độ dài khóa lớn hơn, khả năng chống tấn công vét cạn tốt hơn và phù hợp hơn với các yêu cầu bảo mật hiện đại. Vì vậy, dự án lựa chọn AES-256 để triển khai chức năng mã hóa và giải mã dữ liệu.
## 9. Cơ sở lý thuyết thuật toán RSA

### 9.1. Giới thiệu thuật toán RSA

RSA (Rivest–Shamir–Adleman) là thuật toán mật mã bất đối xứng, sử dụng hai khóa khác nhau:

- **Public Key (khóa công khai):** Có thể chia sẻ cho người khác, dùng để mã hóa dữ liệu hoặc kiểm tra chữ ký số.
- **Private Key (khóa riêng):** Phải được bảo mật, dùng để giải mã dữ liệu hoặc tạo chữ ký số.

Độ an toàn của RSA dựa trên độ khó của bài toán phân tích một số nguyên lớn thành các thừa số nguyên tố.

Trong dự án, ứng dụng RSA-App sử dụng RSA-2048 thông qua thư viện `System.Security.Cryptography` của .NET.

### 9.2. Quy trình tạo cặp khóa RSA

Quá trình tạo khóa RSA về mặt toán học gồm các bước:

**Bước 1:** Chọn hai số nguyên tố lớn khác nhau, ký hiệu là p và q.

**Bước 2:** Tính:

n = p × q

**Bước 3:** Tính hàm Euler:

φ(n) = (p − 1) × (q − 1)

**Bước 4:** Chọn số nguyên e sao cho:

1 < e < φ(n)

và:

gcd(e, φ(n)) = 1

**Bước 5:** Tính số nguyên d thỏa mãn:

d × e ≡ 1 (mod φ(n))

**Bước 6:** Tạo hai khóa:

- Public Key: (e, n)
- Private Key: (d, n)

Trong triển khai thực tế, khóa riêng còn có thể chứa các tham số bổ sung để tối ưu hóa quá trình giải mã.

**Ví dụ minh họa với số nhỏ:**

Giả sử:

- p = 61
- q = 53
- n = 61 × 53 = 3233
- φ(n) = 60 × 52 = 3120
- e = 17
- d = 2753

Khi đó:

- Public Key = (17, 3233)
- Private Key = (2753, 3233)

Đây chỉ là ví dụ toán học để giải thích nguyên lý, không đủ an toàn cho ứng dụng thực tế.

### 9.3. Quy trình mã hóa và giải mã RSA

**Mã hóa:**

Người gửi sử dụng Public Key của người nhận để mã hóa thông điệp.

Công thức RSA cơ bản:

C = M^e mod n

Trong đó:

- M: Thông điệp được biểu diễn dưới dạng số nguyên.
- C: Bản mã.
- e, n: Thành phần của Public Key.

**Giải mã:**

Người nhận sử dụng Private Key để khôi phục thông điệp:

M = C^d mod n

Trong đó d là thành phần bí mật của Private Key.

Trong ứng dụng thực tế, RSA cần sử dụng cơ chế đệm mã hóa an toàn, chẳng hạn RSA-OAEP, thay vì áp dụng trực tiếp công thức toán học trên dữ liệu tùy ý.

### 9.4. Mô hình xác thực người gửi

Mục tiêu của mô hình là giúp người nhận kiểm tra thông điệp có thực sự được ký bởi người gửi hay không.

Quy trình:

1. Người gửi tạo giá trị băm của thông điệp.
2. Người gửi sử dụng Private Key của mình để tạo chữ ký số.
3. Người gửi chuyển thông điệp và chữ ký cho người nhận.
4. Người nhận sử dụng Public Key của người gửi để kiểm tra chữ ký.
5. Nếu chữ ký hợp lệ, người nhận có cơ sở xác nhận thông điệp không bị thay đổi và được ký bằng khóa riêng tương ứng.

Điều kiện quan trọng là Public Key phải được xác thực đúng là thuộc về người gửi.

### 9.5. Mô hình xác thực người nhận

Mục tiêu là xác minh người nhận có quyền sở hữu Private Key tương ứng với Public Key đã được xác thực.

Một phương pháp là sử dụng cơ chế thử thách – phản hồi:

1. Người gửi tạo một giá trị thử thách ngẫu nhiên.
2. Người gửi mã hóa giá trị đó bằng Public Key của người nhận.
3. Người nhận sử dụng Private Key để giải mã.
4. Người nhận gửi lại phản hồi chứng minh đã giải mã được thử thách.
5. Người gửi kiểm tra phản hồi để xác nhận khả năng sở hữu Private Key.

Trong thực tế, giao thức cần chống phát lại, bảo vệ giá trị thử thách và xác thực nguồn gốc Public Key.

### 9.6. Mô hình xác thực cả người gửi và người nhận

Mô hình này kết hợp chữ ký số và mã hóa khóa công khai.

Quy trình:

1. Người gửi tạo thông điệp.
2. Người gửi ký thông điệp bằng Private Key của mình.
3. Người gửi mã hóa thông điệp và chữ ký bằng cơ chế bảo mật phù hợp với Public Key của người nhận.
4. Người nhận sử dụng Private Key để giải mã.
5. Người nhận kiểm tra chữ ký bằng Public Key của người gửi.
6. Hai bên thực hiện xác thực khóa và, nếu cần, sử dụng cơ chế thử thách – phản hồi để chứng minh quyền sở hữu khóa riêng.

**Kết quả:**

- Chữ ký số giúp xác thực người gửi và kiểm tra tính toàn vẹn.
- Mã hóa giúp bảo mật thông điệp cho người nhận dự kiến.
- Cơ chế chứng minh sở hữu khóa riêng giúp xác thực người nhận.

Lưu ý: Chỉ mã hóa bằng Public Key của người nhận chưa tự động chứng minh danh tính người nhận. Việc xác thực cần được thiết kế ở mức giao thức.

### 9.7. Ứng dụng của RSA

RSA được sử dụng trong nhiều cơ chế bảo mật, bao gồm:

- Chữ ký số.
- Xác thực danh tính và chứng chỉ số.
- Bảo vệ hoặc vận chuyển khóa mã hóa đối xứng trong một số giao thức.
- Bảo mật thông tin trong các ứng dụng sử dụng cơ chế mật mã lai.

RSA thường không được sử dụng để mã hóa trực tiếp dữ liệu có kích thước lớn vì giới hạn kích thước thông điệp và chi phí tính toán cao.
## 10. So sánh thuật toán AES và RSA

### 10.1. Sự khác biệt giữa AES và RSA

AES và RSA đều được sử dụng trong bảo mật thông tin nhưng có nguyên lý hoạt động khác nhau.

| Tiêu chí | AES | RSA |
|---|---|---|
| Loại thuật toán | Mã hóa đối xứng | Mã hóa bất đối xứng |
| Khóa sử dụng | Một khóa bí mật | Public Key và Private Key |
| Kích thước khóa trong dự án | 256 bit | 2048 bit |
| Tốc độ xử lý | Thường nhanh hơn | Thường chậm hơn |
| Dữ liệu phù hợp | Văn bản, tệp tin, dữ liệu lớn | Thông điệp nhỏ, khóa mã hóa |
| Ứng dụng | Mã hóa dữ liệu | Chữ ký số, bảo vệ khóa |

### 10.2. So sánh tốc độ mã hóa và giải mã

AES được thiết kế để xử lý dữ liệu theo khối và thường có hiệu suất cao, đặc biệt khi phần cứng hỗ trợ tăng tốc AES.

RSA sử dụng các phép toán số học trên số nguyên lớn, vì vậy chi phí tính toán thường cao hơn.

Để so sánh tốc độ, có thể xây dựng chương trình kiểm thử thực hiện:

1. Chuẩn bị dữ liệu đầu vào phù hợp với cả hai thuật toán.
2. Tạo khóa AES-256 và RSA-2048.
3. Đo thời gian mã hóa bằng công cụ `Stopwatch` của .NET.
4. Đo thời gian giải mã.
5. Lặp lại nhiều lần để tính thời gian trung bình.
6. Kiểm tra dữ liệu sau giải mã trùng với dữ liệu ban đầu.

Kết quả phụ thuộc vào cấu hình máy tính, kích thước dữ liệu, cơ chế đệm RSA, chế độ AES và số lần thử nghiệm.

**Nhận xét:** AES thường phù hợp hơn để mã hóa dữ liệu dung lượng lớn. RSA phù hợp với các tác vụ sử dụng khóa công khai, đặc biệt là bảo vệ khóa phiên và chữ ký số.

### 10.3. Kết quả thực nghiệm

Chương trình `Benchmark-App` được xây dựng bằng C# trên nền tảng .NET 10, sử dụng lớp `Stopwatch` để đo thời gian thực hiện các thao tác mã hóa và giải mã.

**Điều kiện kiểm thử:**

- Thuật toán AES: AES-256-CBC, padding PKCS7.
- Thuật toán RSA: RSA-2048, padding OAEP-SHA256.
- Dữ liệu kiểm thử: 27 byte.
- Số lần thực hiện: 1.000 lần cho mỗi thao tác.
- Cấu hình chạy: Release.
- Thời gian tạo khóa không được tính vào kết quả đo.

**Bảng 10.1. Kết quả đo thời gian trung bình**

| Thao tác | Thời gian trung bình (ms) |
|---|---:|
| AES mã hóa | 0,000752 |
| AES giải mã | 0,000856 |
| RSA mã hóa | 0,017157 |
| RSA giải mã | 0,685656 |

**Nhận xét:**

Trong lần kiểm thử này:

- Thời gian mã hóa RSA lớn hơn khoảng 22,8 lần so với AES.
- Thời gian giải mã RSA lớn hơn khoảng 801 lần so với AES.
- Cả hai thuật toán đều khôi phục chính xác dữ liệu ban đầu.

Kết quả cho thấy AES có thời gian xử lý thấp hơn RSA đối với dữ liệu ngắn được sử dụng trong thử nghiệm.

Đây là kết quả của một lần chạy benchmark trên máy thực hiện, không phải tỷ lệ tốc độ cố định giữa hai thuật toán. Việc đo thông lượng xử lý dữ liệu lớn cần một thử nghiệm riêng với phương pháp phù hợp.

**Kết luận:** AES phù hợp để mã hóa dữ liệu dung lượng lớn, trong khi RSA phù hợp hơn với các chức năng mật mã khóa công khai như bảo vệ khóa phiên và chữ ký số.

## 11. Mô hình kết hợp RSA và AES

### 11.1. Giới thiệu

Mã hóa lai (Hybrid Encryption) là phương pháp kết hợp ưu điểm của mã hóa đối xứng và mã hóa bất đối xứng.

Trong mô hình này:

- AES được sử dụng để mã hóa nội dung dữ liệu.
- RSA được sử dụng để bảo vệ khóa AES khi chuyển cho người nhận.

### 11.2. Quy trình mã hóa kết hợp

**Phía người gửi:**

1. Tạo ngẫu nhiên một khóa AES-256.
2. Sử dụng khóa AES để mã hóa dữ liệu.
3. Sử dụng Public Key RSA của người nhận để mã hóa khóa AES bằng RSA-OAEP.
4. Gửi bản mã dữ liệu, khóa AES đã được mã hóa và các tham số cần thiết cho người nhận.

**Phía người nhận:**

1. Nhận dữ liệu đã mã hóa và khóa AES đã được RSA bảo vệ.
2. Sử dụng Private Key RSA để giải mã khóa AES.
3. Sử dụng khóa AES vừa khôi phục để giải mã dữ liệu.
4. Kiểm tra tính toàn vẹn và tính xác thực của dữ liệu theo cơ chế được sử dụng.

### 11.3. Ưu điểm

- Tận dụng tốc độ xử lý dữ liệu của AES.
- Giảm nhu cầu chia sẻ trực tiếp khóa AES bí mật.
- Cho phép sử dụng RSA để bảo vệ khóa phiên.
- Phù hợp với việc mã hóa tệp tin và trao đổi dữ liệu.

### 11.4. Lưu ý về bảo mật

Trong ứng dụng thực tế, nên ưu tiên chế độ mã hóa có xác thực như AES-GCM để bảo vệ cả tính bí mật và tính toàn vẹn của dữ liệu.

Public Key RSA của người nhận phải được xác thực để tránh nguy cơ bị thay thế khóa.

Mô hình kết hợp RSA và AES không tự động xác thực danh tính người gửi; nếu cần, phải bổ sung chữ ký số hoặc cơ chế xác thực phù hợp.

### 11.5. Kết luận

AES và RSA không thay thế hoàn toàn cho nhau mà có thể phối hợp trong cùng một hệ thống.

AES đảm nhiệm mã hóa dữ liệu hiệu quả, còn RSA hỗ trợ bảo vệ khóa và các chức năng mật mã khóa công khai.

Việc kết hợp đúng cách hai thuật toán giúp xây dựng hệ thống bảo mật vừa có hiệu suất tốt vừa đáp ứng các yêu cầu an toàn thông tin.