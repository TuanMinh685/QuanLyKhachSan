# Phần I: Tài khoản & Phòng — Ứng dụng Windows (WinForms + SQL Server)

Chức năng đã code:
- Đăng nhập / Đăng ký (mật khẩu băm bằng PBKDF2, không dùng thư viện ngoài)
- Phân quyền Admin / Nhân viên
- Quản lý tài khoản (chỉ Admin): danh sách, thêm, khóa/mở khóa
- Quản lý loại phòng (chỉ Admin): CRUD đầy đủ
- Quản lý phòng: Admin CRUD đầy đủ; Nhân viên xem + đổi trạng thái
- Khai báo giấy tờ khách khi check-in (bản rút gọn, xem ghi chú trong code)

Database: **SQL Server** (quản lý bằng **SSMS**), kết nối qua `Microsoft.Data.SqlClient`.


## ⚠️ Lưu ý quan trọng — đọc trước khi build

Code này được viết **hoàn toàn bằng tay, không qua Visual Studio Designer**,
và môi trường soạn thảo hiện tại **không có .NET SDK / SQL Server để chạy thử
thực tế**. Nghĩa là: khả năng cao code và schema đúng, nhưng **có thể có vài
lỗi nhỏ** chỉ phát hiện được khi bạn build trên Visual Studio hoặc chạy file
SQL trên SSMS thật.

→ Nếu gặp lỗi, **copy nguyên văn thông báo lỗi** (Error List trong Visual
Studio, hoặc thông báo đỏ trong SSMS) gửi lại cho mình, mình sẽ sửa ngay.

## Cách cài đặt

### A. Cài SQL Server + SSMS (nếu chưa có)
1. Tải **SQL Server 2022 Express** (miễn phí): tìm "SQL Server Express download".
2. Tải **SSMS (SQL Server Management Studio)** (miễn phí, riêng biệt với SQL Server).
3. Cài SQL Server trước, sau đó cài SSMS.
4. Mở SSMS, ở ô "Server name" gõ `localhost` (hoặc `localhost\SQLEXPRESS` nếu
   bạn cài kiểu Named Instance), chọn **Windows Authentication**, bấm Connect.

### B. Tạo database
1. Trong SSMS, mở file `hotel_management_schema_sqlserver.sql` (**dùng file
   này, không dùng file MySQL cũ**) bằng menu File → Open → File.
2. Bấm **Execute** (hoặc phím F5) để chạy toàn bộ script — sẽ tự tạo database
   `quan_ly_khach_san` cùng toàn bộ bảng, ràng buộc, và view.
3. Trong Object Explorer, refresh và kiểm tra database `quan_ly_khach_san`
   đã xuất hiện với đầy đủ bảng.

### C. Mở và chạy project C#
1. **Cài Visual Studio 2022** (bản Community, miễn phí), khi cài chọn thêm
   workload **".NET desktop development"**.
2. Double-click file `QuanLyKhachSan_Phan1.csproj` để mở bằng Visual Studio.
3. **Khôi phục NuGet package**: Visual Studio thường tự khôi phục. Nếu không,
   chuột phải vào tên project → **Restore NuGet Packages**. Package cần có:
   `Microsoft.Data.SqlClient`.
4. **Kiểm tra chuỗi kết nối** trong `Helpers/DatabaseHelper.cs`:
   - Nếu bạn đăng nhập SSMS bằng **Windows Authentication** (phổ biến nhất
     khi tự cài trên máy cá nhân) → giữ nguyên, không cần sửa gì.
   - Nếu bạn dùng **SQL Server Authentication** (đăng nhập bằng user `sa`
     hoặc user tự tạo) → đổi sang dòng "Cách 2" đã có sẵn trong file, điền
     đúng user/mật khẩu.
   - Nếu SQL Server cài dạng Named Instance (`SQLEXPRESS`) → sửa
     `Server=localhost` thành `Server=localhost\SQLEXPRESS`.
5. **Build** (Ctrl+Shift+B) để kiểm tra lỗi trước, sau đó **Run** (F5).
6. **Tạo tài khoản Admin đầu tiên**:
   - Vào màn hình đăng nhập → bấm "Đăng ký" → tạo 1 tài khoản (mặc định `NhanVien`).
   - Trong SSMS, mở New Query trên database `quan_ly_khach_san`, chạy:
     ```sql
     UPDATE tai_khoan SET vai_tro = 'Admin' WHERE ten_dang_nhap = N'ten_ban_da_dang_ky';
     ```
   - Đăng nhập lại để thấy đầy đủ menu Admin (Loại phòng, Tài khoản).
7. Vào **Loại phòng → Thêm loại phòng** trước, sau đó mới **Phòng → Thêm phòng**.

## Cấu trúc project
```
QuanLyKhachSan_Phan1/
├── QuanLyKhachSan_Phan1.csproj
├── Program.cs
├── hotel_management_schema_sqlserver.sql   # Chay file nay trong SSMS
├── Helpers/
│   ├── DatabaseHelper.cs     # Ket noi SQL Server (Microsoft.Data.SqlClient)
│   ├── Session.cs             # Luu thong tin dang nhap hien tai
│   ├── MatKhauHelper.cs       # Bam/kiem tra mat khau (PBKDF2)
│   └── GiaoDien.cs            # Style dung chung cho cac control
└── Forms/
    ├── FormDangNhap.cs
    ├── FormDangKy.cs
    ├── FormChinh.cs            # Form chinh, co menu dieu huong
    ├── FormDanhSachTaiKhoan.cs
    ├── FormThemTaiKhoan.cs
    ├── FormDanhSachLoaiPhong.cs
    ├── FormThemSuaLoaiPhong.cs
    ├── FormDanhSachPhong.cs
    ├── FormThemSuaPhong.cs
    └── FormCheckIn.cs
```

## Nếu gặp lỗi thường gặp

- **"The type or namespace 'Microsoft.Data.SqlClient' could not be found"**
  → Package chưa được khôi phục. Restore NuGet Packages (bước C.3).

- **"Cannot open database 'quan_ly_khach_san' requested by the login"**
  → Bạn chưa chạy file `hotel_management_schema_sqlserver.sql` trong SSMS,
  hoặc chạy nhưng bị lỗi giữa chừng. Mở lại file, chạy lại toàn bộ (F5).

- **"A network-related or instance-specific error... (provider: Named Pipes...)"**
  → SQL Server chưa chạy, hoặc sai tên server. Mở "SQL Server Configuration
  Manager" kiểm tra dịch vụ SQL Server đang Running; kiểm tra tên instance
  (mặc định Express là `localhost\SQLEXPRESS`).

- **"Login failed for user..."**
  → Đang dùng sai kiểu xác thực. Xem lại bước C.4 (Windows Authentication
  hay SQL Server Authentication) và sửa đúng trong `DatabaseHelper.cs`.

- **Lỗi khi chạy file SQL: "may cause cycles or multiple cascade paths"**
  → Đây là lỗi đặc trưng của SQL Server (khác MySQL) khi 1 bảng bị xóa/sửa
  dây chuyền qua nhiều đường tới cùng 1 bảng con. File
  `hotel_management_schema_sqlserver.sql` đã được xử lý sẵn để tránh lỗi này —
  nếu bạn tự sửa thêm ràng buộc khóa ngoại mới, tránh dùng `ON UPDATE CASCADE`.
