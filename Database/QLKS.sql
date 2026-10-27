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
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
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
INSERT INTO NhanVien (HoTen, ChucVu, SoDienThoai) VALUES
 (N'Quản trị hệ thống', N'Quản lý', '0901234567'),
 (N'Trần Thị Lễ Tân', N'Lễ tân', '0907654321');
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNhanVien) VALUES
 (N'admin', N'123456', N'Admin', 1),
 (N'letan01', N'123456', N'LeTan', 2);
INSERT INTO DichVu (MaDichVu, TenDichVu, LoaiDichVu, DonGia, DonViTinh) VALUES
 (N'DV001', N'Ăn sáng buffet', N'Ăn uống', 120000, N'Suất'),
 (N'DV002', N'Giặt ủi', N'Tiện ích', 50000, N'Kg'),
 (N'DV003', N'Đưa đón sân bay', N'Di chuyển', 300000, N'Lượt');
GO


/* ---------- 11. Bổ sung phòng mẫu ---------- */
INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich) VALUES
 (N'P103', 1, 1, N'1 giường đôi', 22),
 (N'P104', 1, 1, N'2 giường đơn', 22),
 (N'P203', 2, 2, N'1 giường king', 30),
 (N'P204', 2, 2, N'2 giường đôi', 32),
 (N'P302', 3, 3, N'1 giường king view biển', 48);

/* ---------- 12. Bổ sung dịch vụ mẫu ---------- */
INSERT INTO DichVu (MaDichVu, TenDichVu, LoaiDichVu, DonGia, DonViTinh) VALUES
 (N'DV004', N'Nước suối khoáng', N'Đồ uống', 15000, N'Chai'),
 (N'DV005', N'Bia Tiger lon', N'Đồ uống', 35000, N'Lon'),
 (N'DV006', N'Spa massage trị liệu', N'Thư giãn', 350000, N'Suất'),
 (N'DV007', N'Thuê xe máy Honda', N'Di chuyển', 150000, N'Ngày'),
 (N'DV008', N'Phụ thu check-out muộn', N'Phụ thu', 200000, N'Giờ');

/* ---------- 13. Voucher khuyến mãi ---------- */
INSERT INTO Voucher (Code, TenChuongTrinh, LoaiGiam, GiaTri, DonToiThieu, NgayBatDau, NgayHetHan, SoLuong, TrangThai) VALUES
 (N'SUMMER2026', N'Mùa hè rực rỡ', N'PhanTram', 10, 1000000, '2026-01-01', '2026-12-31', 100, 1),
 (N'VIPGUEST', N'Tri ân khách VIP', N'PhanTram', 15, 2000000, '2026-01-01', '2026-12-31', 50, 1),
 (N'GIAM100K', N'Ưu đãi đặt sớm', N'SoTien', 100000, 500000, '2026-01-01', '2026-12-31', 200, 1);

/* ---------- 14. Khách hàng mẫu ---------- */
INSERT INTO KhachHang (HoTen, SoDienThoai, GioiTinh, NgaySinh, QuocTich, Email, DiaChi, CCCD, DongYNhanUuDai) VALUES
 (N'Nguyễn Văn Hùng', '0912345678', N'Nam', '1988-05-12', N'Việt Nam', 'hung.nguyen@gmail.com', N'Ba Đình, Hà Nội', '001088012345', 1),
 (N'Đỗ Thu Trang', '0987654321', N'Nữ', '1993-09-20', N'Việt Nam', 'trang.dothu@gmail.com', N'Quận 1, TP. Hồ Chí Minh', '079193005678', 1),
 (N'Phạm Minh Tuấn', '0903112233', N'Nam', '1985-11-04', N'Việt Nam', 'tuan.pham@outlook.com', N'Hải Châu, Đà Nẵng', '048085009876', 0),
 (N'Lê Hoàng Yến', '0934556677', N'Nữ', '1996-03-15', N'Việt Nam', 'yen.le@gmail.com', N'Ninh Kiều, Cần Thơ', '092196001234', 1),
 (N'Trần Bảo Long', '0978998877', N'Nam', '1980-08-28', N'Việt Nam', 'long.tran@corp.vn', N'Ngô Quyền, Hải Phòng', '031080004567', 1),
 (N'Vũ Thanh Thảo', '0945667788', N'Nữ', '1999-12-01', N'Việt Nam', 'thao.vu@gmail.com', N'Nha Trang, Khánh Hòa', '056199003456', 0);

/* ---------- 15. Đặt phòng mẫu ---------- */
-- 3 đơn ĐANG Ở (sẵn sàng chờ thanh toán trên màn hình Hóa đơn)
INSERT INTO DatPhong (MaDat, MaKH, MaPhong, NgayNhan, NgayTra, SoNguoiLon, SoTreEm, GiaMotDem, TongTien, TienCoc, TrangThai, NgayTao) VALUES
 (N'DP-261020-001', 1, 1, '2026-10-20', '2026-10-24', 2, 0, 500000, 2000000, 500000, N'Đang ở', '2026-10-20 09:00:00'),
 (N'DP-261021-002', 2, 3, '2026-10-21', '2026-10-25', 2, 1, 800000, 3200000, 1000000, N'Đang ở', '2026-10-21 14:00:00'),
 (N'DP-261022-003', 3, 5, '2026-10-22', '2026-10-26', 2, 2, 1500000, 6000000, 2000000, N'Đang ở', '2026-10-22 10:30:00');

-- 6 đơn ĐÃ TRẢ & ĐÃ HOÀN TẤT THANH TOÁN (cho Báo cáo Doanh thu & Thống kê)
INSERT INTO DatPhong (MaDat, MaKH, MaPhong, NgayNhan, NgayTra, SoNguoiLon, SoTreEm, GiaMotDem, TongTien, TienCoc, TrangThai, NgayTao) VALUES
 (N'DP-261001-004', 1, 3, '2026-10-01', '2026-10-03', 2, 0, 800000, 1600000, 500000, N'Đã trả', '2026-10-01 08:00:00'),
 (N'DP-261005-005', 4, 1, '2026-10-05', '2026-10-07', 1, 0, 500000, 1000000, 300000, N'Đã trả', '2026-10-05 11:00:00'),
 (N'DP-261008-006', 5, 5, '2026-10-08', '2026-10-11', 2, 0, 1500000, 4500000, 1500000, N'Đã trả', '2026-10-08 15:00:00'),
 (N'DP-261012-007', 2, 2, '2026-10-12', '2026-10-14', 2, 0, 500000, 1000000, 300000, N'Đã trả', '2026-10-12 12:00:00'),
 (N'DP-261015-008', 3, 4, '2026-10-15', '2026-10-17', 2, 1, 800000, 1600000, 500000, N'Đã trả', '2026-10-15 09:30:00'),
 (N'DP-261018-009', 5, 5, '2026-10-18', '2026-10-20', 3, 0, 1500000, 3000000, 1000000, N'Đã trả', '2026-10-18 16:00:00');

-- 2 đơn HỦY
INSERT INTO DatPhong (MaDat, MaKH, MaPhong, NgayNhan, NgayTra, SoNguoiLon, SoTreEm, GiaMotDem, TongTien, TienCoc, TrangThai, NgayTao) VALUES
 (N'DP-261010-010', 6, 2, '2026-10-10', '2026-10-12', 1, 0, 500000, 1000000, 0, N'Đã hủy', '2026-10-09 10:00:00'),
 (N'DP-261016-011', 4, 3, '2026-10-16', '2026-10-18', 2, 0, 800000, 1600000, 0, N'Đã hủy', '2026-10-14 08:00:00');

/* ---------- 16. Hóa đơn mẫu tương ứng các đơn Đã trả ---------- */
INSERT INTO HoaDon (MaHoaDon, MaDatPhong, NgayLap, NgayThanhToan, TienPhong, TienDichVu, TienGiamGia, TienDatCoc, TongTien, MaPTTT, TrangThai) VALUES
 (N'HD26100301', N'DP-261001-004', '2026-10-03 12:00:00', '2026-10-03 12:00:00', 1600000, 540000, 0, 500000, 1640000, N'TM', N'Đã thanh toán'),
 (N'HD26100702', N'DP-261005-005', '2026-10-07 11:30:00', '2026-10-07 11:30:00', 1000000, 100000, 0, 300000, 800000, N'CK', N'Đã thanh toán'),
 (N'HD26101103', N'DP-261008-006', '2026-10-11 12:15:00', '2026-10-11 12:15:00', 4500000, 1050000, 200000, 1500000, 3850000, N'CK', N'Đã thanh toán'),
 (N'HD26101404', N'DP-261012-007', '2026-10-14 10:00:00', '2026-10-14 10:00:00', 1000000, 240000, 0, 300000, 940000, N'TM', N'Đã thanh toán'),
 (N'HD26101705', N'DP-261015-008', '2026-10-17 12:30:00', '2026-10-17 12:30:00', 1600000, 470000, 100000, 500000, 1470000, N'POS', N'Đã thanh toán'),
 (N'HD26102006', N'DP-261018-009', '2026-10-20 11:45:00', '2026-10-20 11:45:00', 3000000, 650000, 0, 1000000, 2650000, N'CK', N'Đã thanh toán');

/* ---------- 17. Chi tiết hóa đơn (Dịch vụ sử dụng) ---------- */
INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, SoLuong, DonGia, ThanhTien) VALUES
 (N'HD26100301', N'DV001', 2, 120000, 240000),
 (N'HD26100301', N'DV003', 1, 300000, 300000),
 (N'HD26100702', N'DV002', 2, 50000, 100000),
 (N'HD26101103', N'DV006', 2, 350000, 700000),
 (N'HD26101103', N'DV007', 1, 150000, 150000),
 (N'HD26101103', N'DV008', 1, 200000, 200000),
 (N'HD26101404', N'DV001', 2, 120000, 240000),
 (N'HD26101705', N'DV004', 4, 15000, 60000),
 (N'HD26101705', N'DV005', 4, 35000, 140000),
 (N'HD26101705', N'DV006', 1, 270000, 270000),
 (N'HD26102006', N'DV003', 1, 300000, 300000),
 (N'HD26102006', N'DV006', 1, 350000, 350000);
GO
