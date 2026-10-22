-- ============================================================================
-- SCRIPT TẠO DATABASE VÀ DỮ LIỆU MẪU CHO SUPABASE (POSTGRESQL)
-- Đồ án Quản Lý Khách Sạn
-- Tương thích 100% với Supabase SQL Editor
-- ============================================================================

-- 0. Xóa bảng cũ nếu tồn tại (theo thứ tự khóa ngoại)
DROP VIEW IF EXISTS vw_LeTanKhachHangTongQuan CASCADE;
DROP TABLE IF EXISTS ChiTietHoaDon CASCADE;
DROP TABLE IF EXISTS HoaDon CASCADE;
DROP TABLE IF EXISTS Voucher CASCADE;
DROP TABLE IF EXISTS PhuongThucThanhToan CASCADE;
DROP TABLE IF EXISTS DichVu CASCADE;
DROP TABLE IF EXISTS DatPhong CASCADE;
DROP TABLE IF EXISTS KhachHang CASCADE;
DROP TABLE IF EXISTS TaiKhoan CASCADE;
DROP TABLE IF EXISTS NhanVien CASCADE;
DROP TABLE IF EXISTS Phong CASCADE;
DROP TABLE IF EXISTS LoaiPhong CASCADE;

-- ============================================================================
-- 1. LOẠI PHÒNG
-- ============================================================================
CREATE TABLE LoaiPhong (
    MaLoai          SERIAL PRIMARY KEY,
    TenLoai         VARCHAR(50)   NOT NULL,
    GiaMotDem       NUMERIC(18,2) NOT NULL,
    SoNguoiToiDa    INT           NOT NULL DEFAULT 2
);

-- ============================================================================
-- 2. PHÒNG
-- ============================================================================
CREATE TABLE Phong (
    MaPhong         SERIAL PRIMARY KEY,
    SoPhong         VARCHAR(20)   NOT NULL UNIQUE,
    Tang            INT           NOT NULL,
    MaLoai          INT           NOT NULL REFERENCES LoaiPhong(MaLoai),
    MoTaGiuong      VARCHAR(100)  NULL,
    DienTich        NUMERIC(6,2)  NULL,
    TinhTrang       VARCHAR(30)   NOT NULL DEFAULT 'Hoạt động'
);

-- ============================================================================
-- 3. NHÂN VIÊN & TÀI KHOẢN
-- ============================================================================
CREATE TABLE NhanVien (
    MaNhanVien      SERIAL PRIMARY KEY,
    HoTen           VARCHAR(100)  NOT NULL,
    ChucVu          VARCHAR(50)   NOT NULL,
    SoDienThoai     VARCHAR(20)   NOT NULL,
    TrangThai       VARCHAR(20)   NOT NULL DEFAULT 'Đang làm việc'
);

CREATE TABLE TaiKhoan (
    MaTaiKhoan      SERIAL PRIMARY KEY,
    TenDangNhap     VARCHAR(50)   NOT NULL UNIQUE,
    MatKhau         VARCHAR(255)  NOT NULL,
    VaiTro          VARCHAR(20)   NOT NULL,
    MaNhanVien      INT           NOT NULL REFERENCES NhanVien(MaNhanVien)
);

-- ============================================================================
-- 4. KHÁCH HÀNG
-- ============================================================================
CREATE TABLE KhachHang (
    MaKH            SERIAL PRIMARY KEY,
    MaKHHienThi     VARCHAR(20) GENERATED ALWAYS AS ('KH' || LPAD(MaKH::TEXT, 6, '0')) STORED,
    HoTen           VARCHAR(100)  NOT NULL,
    SoDienThoai     VARCHAR(20)   NOT NULL,
    GioiTinh        VARCHAR(10)   NULL,
    NgaySinh        DATE          NULL,
    QuocTich        VARCHAR(50)   NOT NULL DEFAULT 'Việt Nam',
    Email           VARCHAR(100)  NULL,
    DiaChi          VARCHAR(255)  NULL,
    CCCD            VARCHAR(30)   NULL,
    GhiChu          VARCHAR(500)  NULL,
    DongYNhanUuDai  BOOLEAN       NOT NULL DEFAULT FALSE,
    NgayTao         TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    NgayCapNhat     TIMESTAMP     NULL
);
CREATE UNIQUE INDEX UX_KhachHang_CCCD ON KhachHang(CCCD) WHERE CCCD IS NOT NULL;

-- ============================================================================
-- 5. ĐẶT PHÒNG
-- ============================================================================
CREATE TABLE DatPhong (
    MaDatPhong      SERIAL PRIMARY KEY,
    MaDat           VARCHAR(20)   NOT NULL UNIQUE,
    MaKH            INT           NOT NULL REFERENCES KhachHang(MaKH),
    MaPhong         INT           NOT NULL REFERENCES Phong(MaPhong),
    NgayNhan        DATE          NOT NULL,
    NgayTra         DATE          NOT NULL,
    SoDem           INT GENERATED ALWAYS AS (GREATEST(1, (NgayTra - NgayNhan))) STORED,
    SoNguoiLon      INT           NOT NULL DEFAULT 1,
    SoTreEm         INT           NOT NULL DEFAULT 0,
    GiaMotDem       NUMERIC(18,2) NOT NULL,
    PhuThu          NUMERIC(18,2) NOT NULL DEFAULT 0,
    ThueVaPhi       NUMERIC(18,2) NOT NULL DEFAULT 0,
    TongTien        NUMERIC(18,2) NOT NULL,
    TienCoc         NUMERIC(18,2) NOT NULL DEFAULT 0,
    TrangThai       VARCHAR(20)   NOT NULL DEFAULT 'Đã đặt',
    MaNhanVien      INT           NULL REFERENCES NhanVien(MaNhanVien),
    GioNhanThucTe   TIMESTAMP     NULL,
    GioTraThucTe    TIMESTAMP     NULL,
    NgayHuy         TIMESTAMP     NULL,
    LyDoHuy         VARCHAR(200)  NULL,
    NgayTao         TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT CK_DatPhong_TrangThai CHECK (TrangThai IN ('Đã đặt', 'Đang ở', 'Đã trả', 'Đã hủy'))
);
CREATE INDEX IX_DatPhong_Phong_Ngay ON DatPhong(MaPhong, NgayNhan, NgayTra);

-- ============================================================================
-- 6. DỊCH VỤ
-- ============================================================================
CREATE TABLE DichVu (
    MaDichVu        VARCHAR(20)   NOT NULL PRIMARY KEY,
    TenDichVu       VARCHAR(100)  NOT NULL,
    LoaiDichVu      VARCHAR(30)   NOT NULL DEFAULT 'Ăn uống',
    DonGia          NUMERIC(18,2) NOT NULL,
    DonViTinh       VARCHAR(20)   NOT NULL DEFAULT 'Lần',
    TrangThai       BOOLEAN       NOT NULL DEFAULT TRUE
);

-- ============================================================================
-- 7. PHƯƠNG THỨC THANH TOÁN
-- ============================================================================
CREATE TABLE PhuongThucThanhToan (
    MaPhuongThuc    SERIAL PRIMARY KEY,
    TenPhuongThuc   VARCHAR(50)   NOT NULL UNIQUE,
    MoTa            VARCHAR(200)  NULL,
    KichHoat        BOOLEAN       NOT NULL DEFAULT TRUE
);

-- ============================================================================
-- 8. VOUCHER KHUYẾN MÃI
-- ============================================================================
CREATE TABLE Voucher (
    MaVoucher       SERIAL PRIMARY KEY,
    Code            VARCHAR(30)   NOT NULL,
    TenChuongTrinh  VARCHAR(100)  NOT NULL DEFAULT 'Khuyến mãi',
    LoaiGiam        VARCHAR(10)   NOT NULL CHECK (LoaiGiam IN ('PhanTram', 'SoTien')),
    GiaTri          NUMERIC(18,2) NOT NULL,
    DonToiThieu     NUMERIC(18,2) NOT NULL DEFAULT 0,
    NgayBatDau      DATE          NOT NULL,
    NgayHetHan      DATE          NOT NULL,
    SoLuong         INT           NULL,
    TrangThai       BOOLEAN       NOT NULL DEFAULT TRUE
);
CREATE UNIQUE INDEX UX_Voucher_Code ON Voucher(Code);

-- ============================================================================
-- 9. HÓA ĐƠN
-- ============================================================================
CREATE TABLE HoaDon (
    MaHoaDon        VARCHAR(20)   NOT NULL PRIMARY KEY,
    MaDatPhong      VARCHAR(30)   NULL,
    NgayLap         TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    NgayThanhToan   TIMESTAMP     NULL,
    TienPhong       NUMERIC(18,2) NOT NULL DEFAULT 0,
    TienDichVu      NUMERIC(18,2) NOT NULL DEFAULT 0,
    TienGiamGia     NUMERIC(18,2) NOT NULL DEFAULT 0,
    TienDatCoc      NUMERIC(18,2) NOT NULL DEFAULT 0,
    TongTien        NUMERIC(18,2) NOT NULL DEFAULT 0,
    MaPTTT          VARCHAR(10)   NOT NULL DEFAULT 'TM',
    MaVoucher       INT           NULL REFERENCES Voucher(MaVoucher),
    TrangThai       VARCHAR(30)   NOT NULL DEFAULT 'Đã thanh toán'
);

-- ============================================================================
-- 10. CHI TIẾT HÓA ĐƠN
-- ============================================================================
CREATE TABLE ChiTietHoaDon (
    MaCTHD          SERIAL PRIMARY KEY,
    MaHoaDon        VARCHAR(20)   NOT NULL REFERENCES HoaDon(MaHoaDon) ON DELETE CASCADE,
    MaDichVu        VARCHAR(20)   NOT NULL REFERENCES DichVu(MaDichVu),
    SoLuong         INT           NOT NULL DEFAULT 1,
    DonGia          NUMERIC(18,2) NOT NULL,
    ThanhTien       NUMERIC(18,2) NOT NULL
);

-- ============================================================================
-- 11. VIEW CHO LỄ TÂN & KHÁCH HÀNG
-- ============================================================================
CREATE OR REPLACE VIEW vw_LeTanKhachHangTongQuan AS
SELECT  k.MaKH                AS MaKhachHang,
        k.MaKHHienThi         AS MaKH,
        k.HoTen,
        k.SoDienThoai         AS DienThoai,
        k.GioiTinh, k.NgaySinh, k.QuocTich, k.Email, k.DiaChi,
        k.CCCD                AS SoGiayTo,
        k.GhiChu, k.DongYNhanUuDai, k.NgayTao, k.NgayCapNhat,
        COALESCE(t.SoLan, 0)  AS SoLanLuuTru,
        t.GanNhat             AS LanLuuTruGanNhat,
        CASE WHEN COALESCE(t.SoLan, 0) >= 10 THEN 'VIP'
             WHEN COALESCE(t.SoLan, 0) >= 3  THEN 'Thân thiết'
             ELSE 'Mới' END   AS NhomKhach
FROM KhachHang k
LEFT JOIN (SELECT MaKH, COUNT(*) AS SoLan, MAX(NgayNhan) AS GanNhat
           FROM DatPhong
           WHERE TrangThai IN ('Đang ở', 'Đã trả')
           GROUP BY MaKH) t ON t.MaKH = k.MaKH;


-- ============================================================================
-- 12. DỮ LIỆU MẪU (SEED DATA)
-- ============================================================================

-- Loại phòng & Phòng
INSERT INTO LoaiPhong (TenLoai, GiaMotDem, SoNguoiToiDa) VALUES
 ('Standard', 500000, 2),
 ('Deluxe', 800000, 3),
 ('Suite', 1500000, 4);

INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich) VALUES
 ('P101', 1, 1, '1 giường đôi', 22),
 ('P102', 1, 1, '2 giường đơn', 22),
 ('P201', 2, 2, '1 giường king', 30),
 ('P202', 2, 2, '2 giường đôi', 32),
 ('P301', 3, 3, '1 giường king + sofa', 45),
 ('P103', 1, 1, '1 giường đôi', 22),
 ('P104', 1, 1, '2 giường đơn', 22),
 ('P203', 2, 2, '1 giường king', 30),
 ('P204', 2, 2, '2 giường đôi', 32),
 ('P302', 3, 3, '1 giường king view biển', 48);

-- Nhân viên & Tài khoản
INSERT INTO NhanVien (HoTen, ChucVu, SoDienThoai) VALUES
 ('Quản trị hệ thống', 'Quản lý', '0901234567'),
 ('Trần Thị Lễ Tân', 'Lễ tân', '0907654321');

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNhanVien) VALUES
 ('admin', '123456', 'Admin', 1),
 ('letan01', '123456', 'LeTan', 2);

-- Phương thức thanh toán
INSERT INTO PhuongThucThanhToan (TenPhuongThuc) VALUES
 ('Tiền mặt'),
 ('Chuyển khoản'),
 ('Thẻ');

-- Dịch vụ
INSERT INTO DichVu (MaDichVu, TenDichVu, LoaiDichVu, DonGia, DonViTinh) VALUES
 ('DV001', 'Ăn sáng buffet', 'Ăn uống', 120000, 'Suất'),
 ('DV002', 'Giặt ủi', 'Tiện ích', 50000, 'Kg'),
 ('DV003', 'Đưa đón sân bay', 'Di chuyển', 300000, 'Lượt'),
 ('DV004', 'Nước suối khoáng', 'Đồ uống', 15000, 'Chai'),
 ('DV005', 'Bia Tiger lon', 'Đồ uống', 35000, 'Lon'),
 ('DV006', 'Spa massage trị liệu', 'Thư giãn', 350000, 'Suất'),
 ('DV007', 'Thuê xe máy Honda', 'Di chuyển', 150000, 'Ngày'),
 ('DV008', 'Phụ thu check-out muộn', 'Phụ thu', 200000, 'Giờ');

-- Voucher
INSERT INTO Voucher (Code, TenChuongTrinh, LoaiGiam, GiaTri, DonToiThieu, NgayBatDau, NgayHetHan, SoLuong, TrangThai) VALUES
 ('SUMMER2026', 'Mùa hè rực rỡ', 'PhanTram', 10, 1000000, '2026-01-01', '2026-12-31', 100, TRUE),
 ('VIPGUEST', 'Tri ân khách VIP', 'PhanTram', 15, 2000000, '2026-01-01', '2026-12-31', 50, TRUE),
 ('GIAM100K', 'Ưu đãi đặt sớm', 'SoTien', 100000, 500000, '2026-01-01', '2026-12-31', 200, TRUE);

-- Khách hàng
INSERT INTO KhachHang (HoTen, SoDienThoai, GioiTinh, NgaySinh, QuocTich, Email, DiaChi, CCCD, DongYNhanUuDai) VALUES
 ('Nguyễn Văn Hùng', '0912345678', 'Nam', '1988-05-12', 'Việt Nam', 'hung.nguyen@gmail.com', 'Ba Đình, Hà Nội', '001088012345', TRUE),
 ('Đỗ Thu Trang', '0987654321', 'Nữ', '1993-09-20', 'Việt Nam', 'trang.dothu@gmail.com', 'Quận 1, TP. Hồ Chí Minh', '079193005678', TRUE),
 ('Phạm Minh Tuấn', '0903112233', 'Nam', '1985-11-04', 'Việt Nam', 'tuan.pham@outlook.com', 'Hải Châu, Đà Nẵng', '048085009876', FALSE),
 ('Lê Hoàng Yến', '0934556677', 'Nữ', '1996-03-15', 'Việt Nam', 'yen.le@gmail.com', 'Ninh Kiều, Cần Thơ', '092196001234', TRUE),
 ('Trần Bảo Long', '0978998877', 'Nam', '1980-08-28', 'Việt Nam', 'long.tran@corp.vn', 'Ngô Quyền, Hải Phòng', '031080004567', TRUE),
 ('Vũ Thanh Thảo', '0945667788', 'Nữ', '1999-12-01', 'Việt Nam', 'thao.vu@gmail.com', 'Nha Trang, Khánh Hòa', '056199003456', FALSE);

-- Đặt phòng (3 Đang ở + 6 Đã trả + 2 Đã hủy)
INSERT INTO DatPhong (MaDat, MaKH, MaPhong, NgayNhan, NgayTra, SoNguoiLon, SoTreEm, GiaMotDem, TongTien, TienCoc, TrangThai, NgayTao) VALUES
 ('DP-261020-001', 1, 1, '2026-10-20', '2026-10-24', 2, 0, 500000, 2000000, 500000, 'Đang ở', '2026-10-20 09:00:00'),
 ('DP-261021-002', 2, 3, '2026-10-21', '2026-10-25', 2, 1, 800000, 3200000, 1000000, 'Đang ở', '2026-10-21 14:00:00'),
 ('DP-261022-003', 3, 5, '2026-10-22', '2026-10-26', 2, 2, 1500000, 6000000, 2000000, 'Đang ở', '2026-10-22 10:30:00'),
 ('DP-261001-004', 1, 3, '2026-10-01', '2026-10-03', 2, 0, 800000, 1600000, 500000, 'Đã trả', '2026-10-01 08:00:00'),
 ('DP-261005-005', 4, 1, '2026-10-05', '2026-10-07', 1, 0, 500000, 1000000, 300000, 'Đã trả', '2026-10-05 11:00:00'),
 ('DP-261008-006', 5, 5, '2026-10-08', '2026-10-11', 2, 0, 1500000, 4500000, 1500000, 'Đã trả', '2026-10-08 15:00:00'),
 ('DP-261012-007', 2, 2, '2026-10-12', '2026-10-14', 2, 0, 500000, 1000000, 300000, 'Đã trả', '2026-10-12 12:00:00'),
 ('DP-261015-008', 3, 4, '2026-10-15', '2026-10-17', 2, 1, 800000, 1600000, 500000, 'Đã trả', '2026-10-15 09:30:00'),
 ('DP-261018-009', 5, 5, '2026-10-18', '2026-10-20', 3, 0, 1500000, 3000000, 1000000, 'Đã trả', '2026-10-18 16:00:00'),
 ('DP-261010-010', 6, 2, '2026-10-10', '2026-10-12', 1, 0, 500000, 1000000, 0, 'Đã hủy', '2026-10-09 10:00:00'),
 ('DP-261016-011', 4, 3, '2026-10-16', '2026-10-18', 2, 0, 800000, 1600000, 0, 'Đã hủy', '2026-10-14 08:00:00');

-- Hóa đơn
INSERT INTO HoaDon (MaHoaDon, MaDatPhong, NgayLap, NgayThanhToan, TienPhong, TienDichVu, TienGiamGia, TienDatCoc, TongTien, MaPTTT, TrangThai) VALUES
 ('HD26100301', 'DP-261001-004', '2026-10-03 12:00:00', '2026-10-03 12:00:00', 1600000, 540000, 0, 500000, 1640000, 'TM', 'Đã thanh toán'),
 ('HD26100702', 'DP-261005-005', '2026-10-07 11:30:00', '2026-10-07 11:30:00', 1000000, 100000, 0, 300000, 800000, 'CK', 'Đã thanh toán'),
 ('HD26101103', 'DP-261008-006', '2026-10-11 12:15:00', '2026-10-11 12:15:00', 4500000, 1050000, 200000, 1500000, 3850000, 'CK', 'Đã thanh toán'),
 ('HD26101404', 'DP-261012-007', '2026-10-14 10:00:00', '2026-10-14 10:00:00', 1000000, 240000, 0, 300000, 940000, 'TM', 'Đã thanh toán'),
 ('HD26101705', 'DP-261015-008', '2026-10-17 12:30:00', '2026-10-17 12:30:00', 1600000, 470000, 100000, 500000, 1470000, 'POS', 'Đã thanh toán'),
 ('HD26102006', 'DP-261018-009', '2026-10-20 11:45:00', '2026-10-20 11:45:00', 3000000, 650000, 0, 1000000, 2650000, 'CK', 'Đã thanh toán');

-- Chi tiết hóa đơn
INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, SoLuong, DonGia, ThanhTien) VALUES
 ('HD26100301', 'DV001', 2, 120000, 240000),
 ('HD26100301', 'DV003', 1, 300000, 300000),
 ('HD26100702', 'DV002', 2, 50000, 100000),
 ('HD26101103', 'DV006', 2, 350000, 700000),
 ('HD26101103', 'DV007', 1, 150000, 150000),
 ('HD26101103', 'DV008', 1, 200000, 200000),
 ('HD26101404', 'DV001', 2, 120000, 240000),
 ('HD26101705', 'DV004', 4, 15000, 60000),
 ('HD26101705', 'DV005', 4, 35000, 140000),
 ('HD26101705', 'DV006', 1, 270000, 270000),
 ('HD26102006', 'DV003', 1, 300000, 300000),
 ('HD26102006', 'DV006', 1, 350000, 350000);

-- Thiết lập lại sequence cho các bảng có SERIAL để không bị xung đột ID khi INSERT sau này
SELECT setval('LoaiPhong_MaLoai_seq', (SELECT MAX(MaLoai) FROM LoaiPhong));
SELECT setval('Phong_MaPhong_seq', (SELECT MAX(MaPhong) FROM Phong));
SELECT setval('NhanVien_MaNhanVien_seq', (SELECT MAX(MaNhanVien) FROM NhanVien));
SELECT setval('TaiKhoan_MaTaiKhoan_seq', (SELECT MAX(MaTaiKhoan) FROM TaiKhoan));
SELECT setval('KhachHang_MaKH_seq', (SELECT MAX(MaKH) FROM KhachHang));
SELECT setval('DatPhong_MaDatPhong_seq', (SELECT MAX(MaDatPhong) FROM DatPhong));
SELECT setval('PhuongThucThanhToan_MaPhuongThuc_seq', (SELECT MAX(MaPhuongThuc) FROM PhuongThucThanhToan));
SELECT setval('Voucher_MaVoucher_seq', (SELECT MAX(MaVoucher) FROM Voucher));
SELECT setval('ChiTietHoaDon_MaCTHD_seq', (SELECT MAX(MaCTHD) FROM ChiTietHoaDon));
