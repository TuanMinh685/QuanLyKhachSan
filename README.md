# Quản Lý Khách Sạn (WinForms + SQL Server)

Ứng dụng Windows Forms (.NET 10), kiến trúc 3 lớp: `Forms` → `BLL` → `DAL` (+ `Models`).

## Cấu trúc
```
QuanLyKhachSan.slnx          # mở file này bằng Visual Studio
QuanLyKhachSan/              # project duy nhất
  Forms/  BLL/  DAL/  Models/  UITheme.cs  Program.cs
Database/QLKS.sql            # script database duy nhất
```

## Chạy thử
1. Cài SQL Server Express + SSMS. Mở `Database/QLKS.sql` trong SSMS, bấm F5.
   Script **xóa và tạo lại** database `QuanLyKhachSan` kèm dữ liệu mẫu (5 phòng, 3 loại phòng, 3 dịch vụ).
2. Mở `QuanLyKhachSan.slnx` → Restore NuGet → F5.
3. Chuỗi kết nối mặc định: `Server=.;Database=QuanLyKhachSan;User Id=sa;Password=12345` (đăng nhập SQL Server bằng `sa`).
   Máy khác (tên instance khác, mật khẩu khác, LocalDB…) thì **đặt biến môi trường `QLKS_CONNECTION`**,
   đừng sửa `DAL/DatabaseHelper.cs` rồi commit.

## Quy ước nhóm
- Mỗi người một nhánh theo chức năng (`feature/dat-phong`, `feature/hoa-don`…), tạo Pull Request vào `main`, không push thẳng.
- Trước khi làm: `git pull`. Commit nhỏ, message rõ.
- Mọi truy cập database đi qua `DAL/DatabaseHelper.cs` (một chuỗi kết nối duy nhất).
- Đổi cấu trúc bảng thì sửa `Database/QLKS.sql` và báo cả nhóm.
- Không commit `bin/`, `obj/`, `.vs/`, `*.user` (đã có trong `.gitignore`).

## Tên bảng/cột (đã thống nhất)
Lấy theo code phần Lễ tân (nhiều cột nhất): `LoaiPhong(MaLoai, TenLoai, GiaMotDem, SoNguoiToiDa)`,
`Phong(..., MaLoai, TinhTrang)`, `KhachHang(MaKH, MaKHHienThi, SoDienThoai, CCCD, ...)`,
`DatPhong(MaDatPhong, MaDat, MaKH, NgayNhan, NgayTra, SoDem, ...)`.
Phần Hóa đơn/Dịch vụ/Voucher giữ mã chuỗi (`HD...`, `DV001`) như code hiện tại.
Các DAL của Báo cáo và Thanh toán đã được sửa theo bộ tên này.

## Đã kiểm tra bằng cách nào
Mình không có .NET và SQL Server để chạy thật. Thay vào đó đã tách toàn bộ 45 câu SQL trong `DAL/`
và đối chiếu tên bảng/cột với `QLKS.sql` bằng công cụ phân tích T-SQL: không còn câu nào gọi cột không tồn tại.
**Chưa kiểm tra được:** build C#, chạy script trên SQL Server thật, hành vi khi chạy.
Nếu build hoặc chạy script báo lỗi, gửi nguyên văn thông báo lỗi để sửa tiếp.

## Còn dang dở / lỗi đã biết
- `Program.cs` mở thẳng `frmTrangChu`, **chưa có đăng nhập thật**; bảng `TaiKhoan` chưa có dữ liệu và `TaiKhoanBLL/DAL`, `NhanVienDAL` còn rỗng.
- `frmCheckIn`, `frmCheckOut`, `frmLoaiPhong`, `frmDanhSachPhong`, `frmNhanVien` mới là form trống; `Models/DatPhong`, `LoaiPhong`, `TaiKhoan`… là lớp rỗng.
- `frmHoaDon` đang dùng danh sách đặt phòng **giả** ("DP01 - Phòng P206…") thay vì đọc từ database, nên hóa đơn chưa nối với đơn đặt phòng thật.
  Vì vậy `HoaDon.MaDatPhong` đang là chuỗi, chưa có khóa ngoại.
- `ThanhToanDAL.GetDanhSachChuaThanhToan()` đã sửa cho khớp schema nhưng chưa form nào gọi.
- Ngưỡng nhóm khách (VIP ≥ 10 lượt, Thân thiết ≥ 3 lượt) do mình tạm đặt trong view `vw_LeTanKhachHangTongQuan`; nhóm chỉnh lại nếu có quy định khác.
- `Models/Voucher.cs` có thuộc tính `Code` trả về Id thay vì mã thật (gây lỗi lưu/sửa voucher). Đã vá ở `VoucherDAL`, nhưng nên viết lại model cho gọn.
- Mật khẩu trong `TaiKhoan` phải băm (PBKDF2), chưa có code băm trong project này.


## Phân quyền tài khoản
- Đăng nhập lấy `VaiTro` từ bảng `TaiKhoan` và lưu vào `Session` trong bộ nhớ.
- Admin nhìn thấy và truy cập Quản lý tài khoản, Voucher, Báo cáo.
- Lễ tân không nhìn thấy các mục trên; các hàm mở Voucher/Báo cáo và form tài khoản cũng kiểm tra quyền.
- Đăng xuất xóa thông tin phiên trước khi đóng trang chủ.
