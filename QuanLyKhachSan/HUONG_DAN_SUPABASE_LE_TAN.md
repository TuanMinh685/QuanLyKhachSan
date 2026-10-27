# Khách hàng, đặt phòng và đặt cọc trên Supabase

Ứng dụng dùng PostgreSQL qua Npgsql 10.0.3. Không cần SQL Server cục bộ.

## Cập nhật cấu trúc dùng chung

Đã áp dụng bảng `PhieuDatCoc` lên Supabase của nhóm ngày 23/10/2026 và xác nhận kết nối thành công. Các máy còn lại dùng cùng database không cần chạy lại migration.

Khi triển khai lên database mới, chạy `Database/Migrations/002_Supabase_PhieuDatCoc.sql` một lần trong Supabase SQL Editor (schema `public`). Script có thể chạy lại; chỉ bổ sung bảng phiếu thu/hoàn cọc và bật RLS cho bảng này. Ứng dụng desktop dùng kết nối PostgreSQL trực tiếp. Không tự mở quyền REST cho bảng giao dịch.

Không dùng `001_PhieuDatCoc.sql`: đây là script SQL Server cũ. Không chạy lại script khởi tạo có `DROP TABLE` trên dữ liệu nhóm đang sử dụng.

Script mới không dựng lại phiếu thu cho đơn cũ: tiền cọc cũ vẫn ở `DatPhong.TienCoc`, còn phương thức, người thu và thời điểm thu chưa biết thì không tự điền.

## Chạy ứng dụng

1. Restore NuGet và Build solution trong Visual Studio.
2. Bấm F5, đăng nhập bằng tài khoản nhóm đang dùng.
3. Mở Khách hàng, thử tìm hồ sơ và xem lịch sử. Chuyển sang Đặt phòng để tìm phòng theo ngày.
4. Khi tạo đơn, xác nhận khoản cọc thực nhận. Đơn và phiếu thu cùng thành công hoặc cùng hoàn tác khi có lỗi.
5. Khi hủy, nhập lý do và số hoàn. Khi thanh toán, hộp thoại mới chuyển cả chi tiết dịch vụ và số cọc sang cùng giao dịch lưu hóa đơn/trả phòng.

Múi giờ phiên PostgreSQL được đặt về `Asia/Ho_Chi_Minh` sau mỗi lần mở kết nối vì pooler có thể bỏ qua giá trị startup. Khóa `pg_advisory_xact_lock(20261023, 1)` dùng chung cho tạo/nhận/trả/hủy/thanh toán; các thành viên cần dùng cùng phiên bản để bảo đảm phối hợp khóa.

## Kiểm tra

```powershell
dotnet build QuanLyKhachSan.csproj --no-restore
dotnet build Verification/BookingVerification.csproj --no-restore
dotnet Verification/bin/Debug/net10.0-windows/BookingVerification.dll --probe
```

`--probe` chỉ đọc dữ liệu. `--integration` tạo schema ngẫu nhiên `qlks_verify_<GUID>`, kiểm tra search_path đã tách biệt trước khi tạo bảng, dùng dữ liệu mẫu riêng và xóa schema đó trong finally. Không ghi các bảng nghiệp vụ trong `public`; không chạy lệnh xóa/tạo database.

Tùy chọn `--apply-deposit-migration` áp dụng script 002 vào schema `public` của database đang cấu hình. Đây là lệnh thay đổi cấu trúc dùng chung, chỉ dùng khi triển khai phần cọc lên một database chưa được cập nhật.

Các kiểu `SqlConnection`/`SqlCommand` còn thấy ở một số module của nhóm là alias Npgsql trong `GlobalUsings.cs`. Phần khách hàng, đặt phòng, lưu trú, phiếu cọc và DAL hóa đơn dùng trực tiếp Npgsql; package Microsoft.Data.SqlClient đã được loại bỏ.
