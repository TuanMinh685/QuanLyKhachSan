/* =====================================================================
   QUẢN LÝ KHÁCH SẠN — SCRIPT DATABASE DUY NHẤT
   Chạy trong SSMS (F5). Script tự xóa & tạo lại database QuanLyKhachSan,
   nên CHỈ chạy khi chấp nhận mất dữ liệu cũ.
   Tên bảng/cột khớp với code trong QuanLyKhachSan/DAL.
   ===================================================================== */
USE master;
GO
IF DB_ID(N'QuanLyKhachSan') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyKhachSan SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyKhachSan;
END
GO
CREATE DATABASE QuanLyKhachSan;
GO
USE QuanLyKhachSan;
GO

/* ---------- 1. Loại phòng ---------- */
CREATE TABLE LoaiPhong (
    MaLoai        INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai       NVARCHAR(50)  NOT NULL,
    GiaMotDem     DECIMAL(18,2) NOT NULL,
    SoNguoiToiDa  INT           NOT NULL
);

/* ---------- 2. Phòng ---------- */
CREATE TABLE Phong (
    MaPhong     INT IDENTITY(1,1) PRIMARY KEY,
    SoPhong     NVARCHAR(10)  NOT NULL UNIQUE,
    Tang        INT           NOT NULL,
    MaLoai      INT           NOT NULL REFERENCES LoaiPhong(MaLoai),
    MoTaGiuong  NVARCHAR(100) NULL,
    DienTich    DECIMAL(6,2)  NULL,
    TinhTrang   NVARCHAR(20)  NOT NULL DEFAULT N'Hoạt động'   -- Hoạt động / Bảo trì
);

/* ---------- 3. Nhân viên & tài khoản ---------- */
CREATE TABLE NhanVien (
    MaNhanVien   INT IDENTITY(1,1) PRIMARY KEY,
    HoTen        NVARCHAR(100) NOT NULL,
    ChucVu       NVARCHAR(50)  NOT NULL,       -- Lễ tân / Quản lý / Buồng phòng
    SoDienThoai  NVARCHAR(15)  NULL
);

CREATE TABLE TaiKhoan (
    MaTaiKhoan   INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap  NVARCHAR(50)  NOT NULL UNIQUE,
    MatKhau      NVARCHAR(255) NOT NULL,       -- lưu mật khẩu đã băm, không lưu thô
    VaiTro       NVARCHAR(20)  NOT NULL,       -- Admin / LeTan
    MaNhanVien   INT           NOT NULL REFERENCES NhanVien(MaNhanVien)
);

/* ---------- 4. Khách hàng ---------- */
CREATE TABLE KhachHang (
    MaKH            INT IDENTITY(1,1) PRIMARY KEY,
    -- Mã hiển thị "KH000123" tự sinh từ MaKH
    MaKHHienThi     AS (N'KH' + RIGHT(N'000000' + CAST(MaKH AS NVARCHAR(10)), 6)) PERSISTED,
    HoTen           NVARCHAR(100) NOT NULL,
    SoDienThoai     VARCHAR(20)   NOT NULL,
    GioiTinh        NVARCHAR(10)  NULL,
    NgaySinh        DATE          NULL,
    QuocTich        NVARCHAR(50)  NOT NULL DEFAULT N'Việt Nam',
    Email           VARCHAR(100)  NULL,
    DiaChi          NVARCHAR(255) NULL,
    CCCD            VARCHAR(30)   NULL,        -- CCCD / CMND / Hộ chiếu
    GhiChu          NVARCHAR(500) NULL,
    DongYNhanUuDai  BIT           NOT NULL DEFAULT 0,
    NgayTao         DATETIME      NOT NULL DEFAULT GETDATE(),
    NgayCapNhat     DATETIME      NULL
);
-- Số giấy tờ không được trùng (cho phép nhiều khách chưa khai báo = NULL)
CREATE UNIQUE INDEX UX_KhachHang_CCCD ON KhachHang(CCCD) WHERE CCCD IS NOT NULL;

/* ---------- 5. Đặt phòng (gồm cả nhận/trả phòng thực tế) ---------- */
CREATE TABLE DatPhong (
    MaDatPhong     INT IDENTITY(1,1) PRIMARY KEY,
    MaDat          NVARCHAR(20)  NOT NULL UNIQUE,            -- DP-yyMMdd-STT
    MaKH           INT           NOT NULL REFERENCES KhachHang(MaKH),
    MaPhong        INT           NOT NULL REFERENCES Phong(MaPhong),
    NgayNhan       DATE          NOT NULL,
    NgayTra        DATE          NOT NULL,
    SoDem          AS (CASE WHEN DATEDIFF(DAY, NgayNhan, NgayTra) < 1 THEN 1
                            ELSE DATEDIFF(DAY, NgayNhan, NgayTra) END) PERSISTED,
    SoNguoiLon     INT           NOT NULL DEFAULT 1,
    SoTreEm        INT           NOT NULL DEFAULT 0,
    GiaMotDem      DECIMAL(18,2) NOT NULL,
    PhuThu         DECIMAL(18,2) NOT NULL DEFAULT 0,
    ThueVaPhi      DECIMAL(18,2) NOT NULL DEFAULT 0,
    TongTien       DECIMAL(18,2) NOT NULL,
    TienCoc        DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai      NVARCHAR(20)  NOT NULL DEFAULT N'Đã đặt', -- Đã đặt / Đang ở / Đã trả / Đã hủy
    MaNhanVien     INT           NULL REFERENCES NhanVien(MaNhanVien),
    GioNhanThucTe  DATETIME      NULL,
    GioTraThucTe   DATETIME      NULL,
    NgayHuy        DATETIME      NULL,
    LyDoHuy        NVARCHAR(200) NULL,
    NgayTao        DATETIME      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CK_DatPhong_TrangThai CHECK (TrangThai IN (N'Đã đặt', N'Đang ở', N'Đã trả', N'Đã hủy'))
);
CREATE INDEX IX_DatPhong_Phong_Ngay ON DatPhong(MaPhong, NgayNhan, NgayTra);

/* ---------- 6. Dịch vụ ---------- */
CREATE TABLE DichVu (
    MaDichVu    NVARCHAR(20)  NOT NULL PRIMARY KEY,           -- ví dụ DV001
    TenDichVu   NVARCHAR(100) NOT NULL,
    LoaiDichVu  NVARCHAR(30)  NOT NULL DEFAULT N'Ăn uống',
    DonGia      DECIMAL(18,2) NOT NULL,
    DonViTinh   NVARCHAR(20)  NOT NULL DEFAULT N'Lần',
    TrangThai   BIT           NOT NULL DEFAULT 1
);

/* ---------- 7. Phương thức thanh toán ---------- */
CREATE TABLE PhuongThucThanhToan (
    MaPhuongThuc   INT IDENTITY(1,1) PRIMARY KEY,
    TenPhuongThuc  NVARCHAR(50)  NOT NULL UNIQUE,
    MoTa           NVARCHAR(200) NULL,
    KichHoat       BIT           NOT NULL DEFAULT 1
);
INSERT INTO PhuongThucThanhToan (TenPhuongThuc) VALUES (N'Tiền mặt'), (N'Chuyển khoản'), (N'Thẻ');

/* ---------- 8. Voucher ---------- */
CREATE TABLE Voucher (
    MaVoucher       INT IDENTITY(1,1) PRIMARY KEY,
    Code            NVARCHAR(30)  NOT NULL,
    TenChuongTrinh  NVARCHAR(100) NOT NULL DEFAULT N'Khuyến mãi',
    LoaiGiam        NVARCHAR(10)  NOT NULL CHECK (LoaiGiam IN (N'PhanTram', N'SoTien')),
    GiaTri          DECIMAL(18,2) NOT NULL,
    DonToiThieu     DECIMAL(18,2) NOT NULL DEFAULT 0,
    NgayBatDau      DATE          NOT NULL,
    NgayHetHan      DATE          NOT NULL,
    SoLuong         INT           NULL,                       -- NULL = không giới hạn
    TrangThai       BIT           NOT NULL DEFAULT 1
);
CREATE UNIQUE INDEX UX_Voucher_Code ON Voucher(Code);

/* ---------- 9. Hóa đơn ----------
   MaHoaDon (HD...) và MaDatPhong do form hóa đơn nhập/chọn dạng chuỗi,
   nên để NVARCHAR, chưa gắn khóa ngoại. MaPTTT là mã ngắn (TM, CK, ...). */
CREATE TABLE HoaDon (
    MaHoaDon       NVARCHAR(20)  NOT NULL PRIMARY KEY,
    MaDatPhong     NVARCHAR(30)  NULL,
    NgayLap        DATETIME      NOT NULL DEFAULT GETDATE(),
    NgayThanhToan  DATETIME      NULL,
    TienPhong      DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienDichVu     DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienGiamGia    DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienDatCoc     DECIMAL(18,2) NOT NULL DEFAULT 0,
    TongTien       DECIMAL(18,2) NOT NULL DEFAULT 0,
    MaPTTT         NVARCHAR(10)  NOT NULL DEFAULT N'TM',
    MaVoucher      INT           NULL REFERENCES Voucher(MaVoucher),
    TrangThai      NVARCHAR(30)  NOT NULL DEFAULT N'Đã thanh toán'
);

/* ---------- 10. Chi tiết hóa đơn (dòng dịch vụ) ---------- */
CREATE TABLE ChiTietHoaDon (
    MaCTHD     INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon   NVARCHAR(20)  NOT NULL REFERENCES HoaDon(MaHoaDon),
    MaDichVu   NVARCHAR(20)  NOT NULL REFERENCES DichVu(MaDichVu),
    SoLuong    INT           NOT NULL DEFAULT 1,
    DonGia     DECIMAL(18,2) NOT NULL,
    ThanhTien  DECIMAL(18,2) NOT NULL
);
GO

/* ---------- View cho màn hình Khách hàng ----------
   Nhóm khách: VIP >= 10 lượt lưu trú, Thân thiết >= 3 lượt, còn lại Mới.
   (Ngưỡng tạm đặt — nhóm chỉnh lại nếu có quy định khác.) */
CREATE VIEW vw_LeTanKhachHangTongQuan AS
SELECT  k.MaKH                AS MaKhachHang,
        k.MaKHHienThi         AS MaKH,
        k.HoTen,
        k.SoDienThoai         AS DienThoai,
        k.GioiTinh, k.NgaySinh, k.QuocTich, k.Email, k.DiaChi,
        k.CCCD                AS SoGiayTo,
        k.GhiChu, k.DongYNhanUuDai, k.NgayTao, k.NgayCapNhat,
        ISNULL(t.SoLan, 0)    AS SoLanLuuTru,
        t.GanNhat             AS LanLuuTruGanNhat,
        CASE WHEN ISNULL(t.SoLan, 0) >= 10 THEN N'VIP'
             WHEN ISNULL(t.SoLan, 0) >= 3  THEN N'Thân thiết'
             ELSE N'Mới' END  AS NhomKhach
FROM KhachHang k
LEFT JOIN (SELECT MaKH, COUNT(*) AS SoLan, MAX(NgayNhan) AS GanNhat
           FROM DatPhong
           WHERE TrangThai IN (N'Đang ở', N'Đã trả')
           GROUP BY MaKH) t ON t.MaKH = k.MaKH;
GO

/* ---------- Dữ liệu mẫu để chạy thử ---------- */
INSERT INTO LoaiPhong (TenLoai, GiaMotDem, SoNguoiToiDa) VALUES
 (N'Standard', 500000, 2), (N'Deluxe', 800000, 3), (N'Suite', 1500000, 4);
INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich) VALUES
 (N'P101', 1, 1, N'1 giường đôi', 22), (N'P102', 1, 1, N'2 giường đơn', 22),
 (N'P201', 2, 2, N'1 giường king', 30), (N'P202', 2, 2, N'2 giường đôi', 32),
 (N'P301', 3, 3, N'1 giường king + sofa', 45);
INSERT INTO NhanVien (HoTen, ChucVu) VALUES (N'Quản trị hệ thống', N'Quản lý');
INSERT INTO DichVu (MaDichVu, TenDichVu, LoaiDichVu, DonGia, DonViTinh) VALUES
 (N'DV001', N'Ăn sáng buffet', N'Ăn uống', 120000, N'Suất'),
 (N'DV002', N'Giặt ủi', N'Tiện ích', 50000, N'Kg'),
 (N'DV003', N'Đưa đón sân bay', N'Di chuyển', 300000, N'Lượt');
GO
