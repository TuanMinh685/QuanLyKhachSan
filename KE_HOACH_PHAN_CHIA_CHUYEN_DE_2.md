# KẾ HOẠCH PHÂN CHIA CÔNG VIỆC CHUYÊN ĐỀ 2 (3 THÀNH VIÊN)
**HỌC PHẦN:** CHUYÊN ĐỀ 2 - PHÁT TRIỂN ỨNG DỤNG QUẢN LÝ (C# WINFORMS .NET 10 + POSTGRESQL/SUPABASE)  
**TRƯỜNG:** ĐẠI HỌC VĂN HIẾN - KHOA CÔNG NGHỆ THÔNG TIN  
**GIẢNG VIÊN HƯỚNG DẪN:** THẦY LƯƠNG THÁI HIỀN  
**ĐỀ TÀI:** XÂY DỰNG PHẦN MỀM QUẢN LÝ KHÁCH SẠN AN NHIÊN THEO KIẾN TRÚC 3 TẦNG (3-TIER)  
**DANH SÁCH THÀNH VIÊN:**  
1. **Tuấn Minh** (Trưởng nhóm - Thành viên 1)
2. **Nhân** (Thành viên 2)
3. **Thanh Phương** (Thành viên 3)

---

## I. MỤC TIÊU VÀ NGUYÊN TẮC PHÂN CHIA
1. **Khối lượng cân đối:** Phân chia rõ ràng cả 3 tầng kiến trúc phần mềm:
   - **Tầng Giao diện (Presentation / GUI):** Windows Forms hiện đại, phẳng, chuẩn UX khách sạn.
   - **Tầng Nghiệp vụ (Business Logic Layer - BLL):** Kiểm soát logic, tính toán chi phí, xử lý ngoại lệ.
   - **Tầng Truy xuất Dữ liệu (Data Access Layer - DAL):** Tương tác cơ sở dữ liệu qua Npgsql, xử lý Transaction và Advisory Lock.
2. **Báo cáo Word đồng nhất:** Từng thành viên chịu trách nhiệm viết chuyên sâu về các chức năng mình code, chụp ảnh màn hình giao diện (Screenshots) và bảng Test Cases thực tế.
3. **Bảo vệ độc lập:** Đảm bảo khi Thầy Lương Thái Hiền chỉ định bất kỳ thành viên nào demo hoặc trả lời code C#, thành viên đó đều làm chủ được module của mình.

---

## II. BẢNG PHÂN CHIA NHIỆM VỤ CHI TIẾT THEO TỪNG THÀNH VIÊN

### 👤 1. THÀNH VIÊN 1: TUẤN MINH (Lead Developer / Core Architecture & Front-Desk Reservation)
* **Vai trò:** Trưởng nhóm phụ trách cấu trúc giải pháp (Solution Architecture), luồng kết nối CSDL Supabase, phân hệ bàn làm việc lễ tân, đặt phòng, lưu trú, khách hàng và đặt cọc.
* **Phần Kỹ thuật Lập trình C# WinForms phụ trách:**
  - Thiết lập kiến trúc dự án 3-Tier, cấu hình chuỗi kết nối Npgsql pooler (`DatabaseHelper.cs`, `Session.cs`, `SqlCompat.cs`).
  - Lập trình **Tầng DAL & BLL Lễ tân - Đặt phòng - Khách hàng**:
    - `LeTanDatPhongDAL.cs`, `LeTanDatPhongBLL.cs`: Tìm kiếm phòng trống theo khoảng ngày, tính tiền cọc 30%, tạo đơn đặt phòng, cơ chế khóa chống race-condition `pg_advisory_xact_lock`.
    - `LeTanLuuTruDAL.cs`, `LeTanLuuTruBLL.cs`: Thủ tục Check-in nhận phòng, chuyển trạng thái buồng phòng, ghi nhận giờ thực tế.
    - `KhachHangDAL.cs`, `KhachHangBLL.cs`: Tra cứu CCCD, quản lý thông tin khách hàng, lịch sử lưu trú.
    - `PhieuDatCocDAL.cs`: Quản lý phiếu thu cọc và thủ tục hoàn cọc khi hủy phòng.
  - Lập trình **Giao diện người dùng (Forms / UserControls)**:
    - `ucBanLamViecLeTan.cs`: Bàn làm việc lễ tân trung tâm, sơ đồ buồng phòng trực quan theo màu sắc trạng thái (Trống, Đang ở, Chờ dọn, Bảo trì).
    - `ucDatPhong.cs`, `frmChonDatPhong.cs`: Giao diện đặt phòng chuyên nghiệp, chọn ngày đến/ngày đi.
    - `frmKhachHang.cs`, `frmChonKhach.cs`, `frmThemKhachNhanhModal.cs`: Quản lý hồ sơ và thêm khách nhanh tại quầy.
    - `frmHoanCoc.cs`: Giao diện xử lý hủy phòng và hoàn cọc theo chính sách.
  - Xây dựng module kiểm thử tự động giao dịch (`Verification/BookingVerification.csproj`).
* **Phần Báo cáo Word Chuyên đề 2 phụ trách viết:**
  - **Trang bìa chính/phụ, Lời cảm ơn, Lời cam đoan, Mục lục, Danh mục hình/bảng**.
  - **Bảng phân công nhiệm vụ Chuyên đề 2 & Tỷ lệ đóng góp**.
  - **CHƯƠNG 1: TỔNG QUAN YÊU CẦU & ĐẶC TẢ HỆ THỐNG PHẦN MỀM** *(~6 trang)*:
    - Khảo sát quy trình vận hành lễ tân khách sạn An Nhiên.
    - Sơ đồ ca sử dụng tổng quát (Use Case Diagram), đặc tả các Use Case chính (Đặt phòng, Check-in, Quản lý cọc).
  - **CHƯƠNG 2: THIẾT KẾ KIẾN TRÚC PHẦN MỀM 3 TẦNG (3-TIER)** *(~8 trang)*:
    - Mô hình phân tầng: GUI ➔ BLL ➔ DAL ➔ Database PostgreSQL.
    - Sơ đồ lớp chi tiết (Class Diagram) của phân hệ Đặt phòng & Khách hàng.
    - Giải pháp đồng bộ múi giờ `Asia/Ho_Chi_Minh` và kỹ thuật khóa giao dịch `pg_advisory_xact_lock`.
  - **Mục 3.1 & 3.2 (Trong Chương 3): HIỆN THỰC PHÂN HỆ LỄ TÂN & ĐẶT PHÒNG** *(~8 trang)*:
    - Hình ảnh chụp màn hình Bàn làm việc Lễ tân, Sơ đồ phòng theo tầng.
    - Quy trình đặt phòng mới, kiểm tra phòng trống thực tế và tạo phiếu thu cọc.
    - Quy trình làm thủ tục Check-in và cập nhật buồng phòng.
  - **Tổng hợp & Định dạng hoàn thiện file Word (Format Master)**.

---

### 👤 2. THÀNH VIÊN 2: NHÂN (Core Backend & System Administration, Security & Inventory)
* **Vai trò:** Phụ trách phân hệ Quản trị hệ thống, danh mục cơ sở vật chất (phòng/loại phòng), quản trị nhân sự, dịch vụ phụ trợ, khuyến mãi voucher và bảo mật phần mềm.
* **Phần Kỹ thuật Lập trình C# WinForms phụ trách:**
  - Lập trình **Bảo mật & Xác thực tài khoản**:
    - `BaoMatHelper.cs`: Thuật toán băm mật khẩu PBKDF2 kết hợp Salt ngẫu nhiên chống tấn công từ điển / Rainbow Table.
    - `TaiKhoanDAL.cs`, `TaiKhoanBLL.cs`: Xác thực đăng nhập, phân quyền vai trò (Admin / Lễ tân), đổi mật khẩu, cơ chế khóa tài khoản khi có sự cố.
  - Lập trình **Tầng DAL & BLL Danh mục & Nhân sự**:
    - `PhongDAL.cs`, `PhongBLL.cs`, `LoaiPhongDAL.cs`: Quản lý danh mục phòng, loại phòng, sức chứa, đơn giá niêm yết theo đêm.
    - `NhanVienDAL.cs`: Quản lý hồ sơ nhân viên khách sạn, chức vụ, số điện thoại, ngày vào làm.
    - `DichVuDAL.cs`: Quản lý danh mục dịch vụ (Minibar, Giặt là, Ăn uống, Spa, Xe đưa đón).
    - `VoucherDAL.cs`, `VoucherBLL.cs`: Quản lý chương trình khuyến mãi, điều kiện áp dụng, kiểm soát số lượng phát hành và hạn dùng.
  - Lập trình **Giao diện người dùng (Forms / Modals)**:
    - `frmDangNhap.cs`, `frmDoiMatKhauModal.cs`, `frmKhoaManHinhModal.cs`: Form đăng nhập chuyên nghiệp, form khóa màn hình nhanh bảo vệ ca trực khi lễ tân rời quầy.
    - `frmDanhSachPhong.cs`, `frmPhongModal.cs`, `frmChiTietPhongModal.cs`: Quản trị buồng phòng.
    - `frmLoaiPhong.cs`, `frmLoaiPhongModal.cs`: Quản trị danh mục loại phòng và giá.
    - `frmNhanVien.cs`, `frmTaiKhoan.cs`, `frmTaiKhoanModal.cs`: Phân hệ quản trị nhân sự và tài khoản.
    - `frmDichVu.cs`, `frmDichVuModal.cs`: Quản trị danh mục dịch vụ.
    - `frmVoucher.cs`, `frmVoucherModal.cs`: Quản trị mã giảm giá.
* **Phần Báo cáo Word Chuyên đề 2 phụ trách viết:**
  - **Mục 3.3 & 3.4 (Trong Chương 3): HIỆN THỰC PHÂN HỆ QUẢN TRỊ DANH MỤC & NHÂN SỰ** *(~9 trang)*:
    - Mô tả các biểu mẫu quản lý phòng, loại phòng, nhân viên, dịch vụ và voucher.
    - Chụp ảnh màn hình thao tác CRUD (Thêm, Sửa, Xóa, Tìm kiếm, Đổi trạng thái).
    - Sơ đồ Class Diagram của các lớp `Phong`, `LoaiPhong`, `NhanVien`, `DichVu`, `Voucher`.
  - **Mục 5.1 (Trong Chương 5): AN TOÀN & BẢO MẬT HỆ THỐNG ỨNG DỤNG** *(~5 trang)*:
    - Phân tích cơ chế mã hóa PBKDF2 (Password-Based Key Derivation Function 2) với SHA-256.
    - Cơ chế phòng chống SQL Injection thông qua Parameterized Queries của thư viện Npgsql.
    - Chức năng Khóa màn hình tạm thời (Lock Screen) và phân quyền chức năng theo phiên làm việc `Session`.

---

### 👤 3. THÀNH VIÊN 3: THANH PHƯƠNG (UI/UX Theme, Multi-channel Checkout & BI Analytics)
* **Vai trò:** Phụ trách thiết kế trải nghiệm người dùng (UI/UX Theme), phân hệ quyết toán hóa đơn (Check-out), tích hợp đa kênh thanh toán điện tử (QR Code) và báo cáo thống kê doanh thu.
* **Phần Kỹ thuật Lập trình C# WinForms phụ trách:**
  - Thiết kế **Hệ thống Giao diện & Chủ đề (UI Theme Master)**:
    - `UITheme.cs`: Xây dựng bộ quy chuẩn màu sắc hiện đại (Primary, Dark, Accent, Card, Status Colors), bo góc nút bấm, hiệu ứng hover, font chuẩn Flat UI.
    - `frmTrangChu.cs`: Form điều hướng trung tâm (Sidebar Dashboard), chuyển đổi linh hoạt giữa các phân hệ, thanh trạng thái ca trực và đồng hồ thời gian thực.
  - Lập trình **Tầng DAL & BLL Hóa đơn & Quyết toán**:
    - `HoaDonDAL.cs`, `HoaDonBLL.cs`, `ChiTietHoaDonDAL.cs`: Nghiệp vụ xuất hóa đơn, tự động cộng tiền phòng theo số đêm, tiền dịch vụ phụ trợ, khấu trừ tiền đặt cọc và trừ giá trị voucher giảm giá.
    - `ThanhToanDAL.cs`, `PhuongThucThanhToanDAL.cs`: Quản lý các phương thức thanh toán.
    - `BaoCaoDAL.cs`: Truy vấn dữ liệu phân tích doanh thu, tỷ lệ lấp đầy phòng, xếp hạng dịch vụ từ các View CSDL phục vụ hiển thị lên giao diện.
  - Lập trình **Giao diện người dùng (Forms / Modals)**:
    - `frmHoaDon.cs`, `frmChiTietHoaDonModal.cs`: Giao diện danh sách và chi tiết hóa đơn.
    - `frmThanhToanModal.cs`, `frmThanhToanTongDatPhongModal.cs`: Hộp thoại quyết toán thanh toán chuyên nghiệp khi khách Check-out.
    - `frmVNPAYQR.cs`, `frmMoMoQR.cs`, `frmApplePayModal.cs`: Tích hợp hiển thị mã QR thanh toán động VNPAY (chuẩn VietQR/EMVCo), Ví điện tử MoMo, Apple Pay và Thẻ tín dụng/POS.
    - `frmBaoCao.cs`: Giao diện báo cáo thống kê trực quan, lọc doanh thu theo ngày/tháng/năm, biểu đồ công suất phòng.
* **Phần Báo cáo Word Chuyên đề 2 phụ trách viết:**
  - **Mục 3.5 & 3.6 (Trong Chương 3): HIỆN THỰC PHÂN HỆ QUYẾT TOÁN & THANH TOÁN ĐA KÊNH** *(~9 trang)*:
    - Quy trình tính toán hóa đơn quyết toán (Tiền phòng + Tiền dịch vụ - Cọc - Voucher).
    - Giải pháp thanh toán không dùng tiền mặt: Tích hợp mã QR chuẩn EMVCo VNPAY, Ví MoMo, Apple Pay và tiền mặt.
    - Chụp ảnh màn hình các bước thanh toán, in/xem trước hóa đơn dịch vụ.
  - **CHƯƠNG 4: PHÂN HỆ BÁO CÁO THỐNG KÊ & PHÂN TÍCH DOANH THU** *(~7 trang)*:
    - Giao diện báo cáo doanh thu kinh doanh của khách sạn theo thời gian.
    - Biểu đồ thống kê tỷ lệ buồng phòng và xếp hạng dịch vụ mang lại doanh thu cao.
    - Chụp ảnh màn hình số liệu thống kê thực tế được tải từ cơ sở dữ liệu Supabase.
  - **Mục 5.2 (Trong Chương 5): KIỂM THỬ PHẦN MỀM (SOFTWARE TESTING)** *(~6 trang)*:
    - Xây dựng ma trận Test Cases (Kiểm thử chức năng đăng nhập, đặt phòng, áp mã voucher, thanh toán QR, hủy phòng).
    - Bảng ghi nhận kết quả kiểm thử thực tế và đánh giá độ ổn định.
  - **KẾT LUẬN & HƯỚNG PHÁT TRIỂN ỨNG DỤNG** *(~3 trang)*: Tổng kết các tính năng đạt được, định hướng phát triển ứng dụng di động (Mobile App) hoặc Web Booking Engine.
  - **Tài liệu tham khảo**.

---

## III. MA TRẬN PHÂN CHIA BÁO CÁO WORD CHUYÊN ĐỀ 2

| Chương / Mục trong Báo cáo Word Chuyên đề 2 | Người phụ trách chính | Số trang dự kiến |
| :--- | :---: | :---: |
| **Phần đầu** (Bìa chính, Bìa phụ, Lời cảm ơn, Mục lục, Danh mục hình/bảng) | **Tuấn Minh** | 4 – 5 trang |
| **Bảng phân công nhiệm vụ thành viên & Tỷ lệ hoàn thành** | **Tuấn Minh** | 1 trang |
| **Chương 1: Tổng quan bài toán & Đặc tả hệ thống phần mềm** (Use Case, Nghiệp vụ) | **Tuấn Minh** | 6 – 8 trang |
| **Chương 2: Thiết kế kiến trúc phần mềm 3 tầng (3-Tier)** (DAL - BLL - GUI, Class Diagram) | **Tuấn Minh** | 8 – 10 trang |
| **Mục 3.1 & 3.2: Phân hệ Lễ tân, Sơ đồ phòng, Đặt phòng & Lưu trú** | **Tuấn Minh** | 8 – 10 trang |
| **Mục 3.3 & 3.4: Phân hệ Quản trị Danh mục (Phòng, Dịch vụ, Voucher) & Nhân sự** | **Nhân** | 8 – 10 trang |
| **Mục 3.5 & 3.6: Phân hệ Hóa đơn Quyết toán & Tích hợp Thanh toán QR (VNPAY/MoMo)** | **Thanh Phương** | 8 – 10 trang |
| **Chương 4: Phân hệ Báo cáo thống kê & Biểu đồ doanh thu** | **Thanh Phương** | 6 – 8 trang |
| **Mục 5.1: An toàn & Bảo mật phần mềm** (Mã hóa PBKDF2, SQL Injection, Khóa màn hình) | **Nhân** | 5 – 6 trang |
| **Mục 5.2: Kiểm thử phần mềm (Testing & Test Cases)** | **Thanh Phương** | 5 – 6 trang |
| **Chương 6: Kết luận & Hướng phát triển phần mềm** | **Thanh Phương** + Cả nhóm | 3 – 4 trang |
| **TỔNG CỘNG ĐỘ DÀI QUYỂN BÁO CÁO CHUYÊN ĐỀ 2** | **Cả 3 thành viên** | **~60 – 75 trang** |

---

## IV. BẢNG ĐÁNH GIÁ ĐÓNG GÓP CHUYÊN ĐỀ 2 (NỘP CHO GIẢNG VIÊN)

| STT | Họ và Tên | Vai trò | Nhiệm vụ chính phụ trách | Đánh giá hoàn thành | Tỷ lệ đóng góp |
| :---: | :--- | :---: | :--- | :---: | :---: |
| 1 | **Tuấn Minh** | Trưởng nhóm / Lead Dev | Kiến trúc 3-Tier, Phân hệ Lễ tân, Đặt phòng, Lưu trú, Quản lý Khách hàng, Phiếu cọc, Tổng hợp Word | Hoàn thành xuất sắc | **100%** |
| 2 | **Nhân** | Core Backend / Security | Phân hệ Quản trị phòng, Loại phòng, Dịch vụ, Voucher, Quản lý Nhân sự & Tài khoản, Bảo mật PBKDF2 | Hoàn thành xuất sắc | **100%** |
| 3 | **Thanh Phương** | UI/UX & Fintech / Reporting | Thiết kế Theme UI, Phân hệ Quyết toán Hóa đơn, Tích hợp cổng QR VNPAY/MoMo/Apple Pay, Báo cáo thống kê | Hoàn thành xuất sắc | **100%** |

---

## V. KỊCH BẢN PHÂN CHIA THUYẾT TRÌNH & DEMO CHUYÊN ĐỀ 2 (15 - 20 PHÚT)

Khi nhóm bước vào bảo vệ **Chuyên đề 2** trước **Thầy Lương Thái Hiền**:

1. **Tuấn Minh (Phút 1 – 6):**
   - Giới thiệu tổng quan đề tài, giải thích mô hình kiến trúc 3-Tier (DAL - BLL - GUI) kết nối PostgreSQL Supabase.
   - Chạy ứng dụng (F5 Visual Studio): Giới thiệu màn hình Dashboard `frmTrangChu`.
   - Demo phân hệ Lễ tân:
     - Mở Bàn làm việc lễ tân (`ucBanLamViecLeTan`), giới thiệu sơ đồ phòng trực quan đổi màu theo trạng thái.
     - Thao tác: Tạo đơn đặt phòng mới, chọn ngày, tính tiền cọc 30% và xuất phiếu cọc.
     - Thao tác: Tiến hành Check-in nhận phòng cho khách và chứng minh phòng chuyển sang trạng thái "Đang ở".

2. **Nhân (Phút 7 – 12):**
   - Thuyết trình về cơ chế bảo mật và quản trị dữ liệu của phần mềm.
   - Demo chức năng:
     - Đăng nhập với tài khoản Admin để hiển thị menu quản trị; thử đăng nhập tài khoản Lễ tân để cho thấy hệ thống ẩn các menu nhạy cảm.
     - Demo form Khóa màn hình tạm thời (`frmKhoaManHinhModal`) để lễ tân bàn giao hoặc rời quầy.
     - Demo quản trị buồng phòng: Thêm mới phòng, sửa đơn giá loại phòng.
     - Demo quản lý dịch vụ và phát hành mã Voucher giảm giá mới.

3. **Thanh Phương (Phút 13 – 18):**
   - Thuyết trình về phong cách thiết kế UI/UX hiện đại của phần mềm và giải pháp thanh toán điện tử không dùng tiền mặt.
   - Demo phân hệ Thanh toán & Quyết toán:
     - Thực hiện Check-out cho một phòng đang ở: Chọn dịch vụ minibar đã dùng, áp mã voucher vừa tạo ở phần của Nhân.
     - Hệ thống tự động trừ tiền cọc đã thu ở phần của Tuấn Minh và tính ra số tiền cần thanh toán cuối cùng.
     - Bật hộp thoại thanh toán QR: Trình chiếu mã QR VNPAY/MoMo động hiển thị đúng số tiền cần thanh toán.
     - Hoàn tất xuất hóa đơn.
     - Mở màn hình Báo cáo (`frmBaoCao`) cho thấy doanh thu và lượt phòng vừa thanh toán được cập nhật tức thì lên biểu đồ.

4. **Cả nhóm (Phút 18 – 20+):**
   - Lắng nghe nhận xét và trả lời câu hỏi vấn đáp chuyên môn của Thầy Lương Thái Hiền.

---

## VI. KẾ HOẠCH PHÂN CHIA VÀ QUẢN TRỊ TRÊN GITHUB (GIT WORKFLOW)

### 1. Thông tin Kho lưu trữ Git (Git Repository)
* **URL Repository:** `https://github.com/TuanMinh685/QuanLyKhachSan.git`
* **Mô hình phân nhánh (Git Branching Strategy):**
  - Nhánh `main`: Nhánh chính tích hợp toàn bộ Solution C# WinForms (.slnx, .csproj). Chỉ merge sau khi build thành công (`Build Succeeded`) và không có conflict.
  - Nhánh `tuanminh` (hoặc `feature/le-tan-dat-phong`): Tuấn Minh phụ trách kiến trúc 3-Tier, kết nối Npgsql, bàn làm việc lễ tân, đặt phòng, lưu trú, khách hàng, phiếu cọc.
  - Nhánh `nhan` (hoặc `feature/phan-quan-ly-phong-tai-khoan`): Nhân phụ trách bảo mật PBKDF2, quản lý phòng, loại phòng, nhân viên, dịch vụ, voucher, form khóa màn hình.
  - Nhánh `thanhphuong` (hoặc `xuanhong` / `feature/giao-dien-thanh-toan-bao-cao`): Thanh Phương phụ trách UI Theme chuẩn, Dashboard, Quyết toán hóa đơn, cổng QR VNPAY/MoMo/Apple Pay, Báo cáo thống kê.

### 2. Bảng phân công Commit & Quản lý File C# trên Git

| Thành viên | GitHub Account | Nhánh Git | Thư mục & File C# phụ trách commit | Quy ước Commit mẫu |
| :--- | :--- | :---: | :--- | :--- |
| **Tuấn Minh** | `@TuanMinh685` | `tuanminh` ➔ PR `main` | • `Program.cs`, `Session.cs`, `GlobalUsings.cs`<br>• `DAL/DatabaseHelper.cs`, `SqlCompat.cs`<br>• `DAL/LeTanDatPhongDAL.cs`, `BLL/LeTanDatPhongBLL.cs`<br>• `DAL/LeTanLuuTruDAL.cs`, `BLL/LeTanLuuTruBLL.cs`<br>• `DAL/KhachHangDAL.cs`, `BLL/KhachHangBLL.cs`<br>• `DAL/PhieuDatCocDAL.cs`<br>• `Forms/ucBanLamViecLeTan.cs`, `ucDatPhong.cs`<br>• `Forms/frmKhachHang.cs`, `frmHoanCoc.cs`<br>• `Verification/BookingVerification.csproj` | `feat(core): setup 3-tier architecture and Npgsql pooler`<br>`feat(reservation): build ucBanLamViecLeTan room matrix`<br>`feat(deposit): implement check-in and deposit flow`<br>`fix(concurrency): add pg_advisory_xact_lock` |
| **Nhân** | `@nhan2546789` | `nhan` ➔ PR `main` | • `BLL/BaoMatHelper.cs` (PBKDF2 hash salt)<br>• `DAL/TaiKhoanDAL.cs`, `BLL/TaiKhoanBLL.cs`<br>• `DAL/NhanVienDAL.cs`, `Forms/frmNhanVien.cs`<br>• `DAL/PhongDAL.cs`, `BLL/PhongBLL.cs`, `DAL/LoaiPhongDAL.cs`<br>• `DAL/DichVuDAL.cs`, `Forms/frmDichVu.cs`<br>• `DAL/VoucherDAL.cs`, `BLL/VoucherBLL.cs`, `Forms/frmVoucher.cs`<br>• `Forms/frmDangNhap.cs`, `frmKhoaManHinhModal.cs`<br>• `Forms/frmDanhSachPhong.cs`, `frmLoaiPhong.cs` | `feat(security): implement PBKDF2 password hashing`<br>`feat(admin): build room and room type management`<br>`feat(voucher): add voucher CRUD and discount rules`<br>`feat(lockscreen): add modal lock screen for shift handover` |
| **Thanh Phương** | `@xuanhong2004` / `@thanhphuong` | `xuanhong` / `thanhphuong` ➔ PR `main` | • `UITheme.cs` (Flat modern styling, color palette)<br>• `Forms/frmTrangChu.cs` (Sidebar, status bar)<br>• `DAL/HoaDonDAL.cs`, `BLL/HoaDonBLL.cs`<br>• `DAL/ChiTietHoaDonDAL.cs`<br>• `DAL/ThanhToanDAL.cs`, `PhuongThucThanhToanDAL.cs`<br>• `Forms/frmHoaDon.cs`, `frmThanhToanModal.cs`<br>• `Forms/frmVNPAYQR.cs`, `frmMoMoQR.cs`, `frmApplePayModal.cs`<br>• `DAL/BaoCaoDAL.cs`, `Forms/frmBaoCao.cs` | `feat(ui): design modern UITheme and responsive controls`<br>`feat(checkout): calculate invoice with deposit and voucher deduction`<br>`feat(payment): integrate dynamic VNPAY and MoMo QR codes`<br>`feat(reports): build revenue report and room occupancy charts` |

### 3. Quy trình làm việc và kiểm soát xung đột (Merge Conflict Prevention)
1. **Quy tắc độc lập phân tầng:**
   - Mỗi người làm việc trên file Form và DAL/BLL của riêng mình theo bảng phân công, tránh sửa chéo vào cùng một file `.Designer.cs` để không bị xung đột giao diện kéo thả.
   - Các hàm dùng chung (như `DatabaseHelper`, `UITheme`, `Session`) khi cần sửa phải trao đổi trước với trưởng nhóm.
2. **Quy trình đẩy code (Push & Pull Request):**
   ```bash
   # Bước 1: Trước khi code mỗi ngày, kéo code mới nhất từ main về nhánh mình
   git checkout <nhanh_cua_ban>
   git pull origin main

   # Bước 2: Code, test chạy F5 không lỗi trên Visual Studio
   git add .
   git commit -m "feat(phan_he): chi tiet thay doi"
   git push origin <nhanh_cua_ban>

   # Bước 3: Lên GitHub tạo Pull Request vào main
   # Bước 4: Trưởng nhóm review, kiểm tra build và merge PR
   ```
3. **Minh chứng Git đưa vào Báo cáo Word:**
   - Chụp ảnh lịch sử commit (`git log --graph --oneline`) hoặc biểu đồ **GitHub Network Graph / Contributors**.
   - Bảng tổng hợp các Pull Request đã thực hiện thành công (PR #1, PR #2, PR #3...) đưa vào **Chương 4 (Mục Đánh giá kết quả thực nghiệm)** để chứng minh nhóm làm việc nghiêm túc, đúng tiến độ.

