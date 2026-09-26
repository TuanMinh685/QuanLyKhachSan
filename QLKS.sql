CREATE DATABASE QuanLyKhachSan;
GO
USE QuanLyKhachSan;
GO

-- 1. Loại phòng
CREATE TABLE LoaiPhong (
    MaLoaiPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiPhong NVARCHAR(50) NOT NULL,
    GiaPhong DECIMAL(18,2) NOT NULL,
    SucChua INT NOT NULL
);

-- 2. Phòng
CREATE TABLE Phong (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    SoPhong NVARCHAR(10) NOT NULL UNIQUE,
    Tang INT NOT NULL,
    MaLoaiPhong INT NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Trống', -- Trống/Đang ở/Đang dọn/Bảo trì
    FOREIGN KEY (MaLoaiPhong) REFERENCES LoaiPhong(MaLoaiPhong)
);

-- 3. Khách hàng
CREATE TABLE KhachHang (
    MaKhachHang INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    CCCD NVARCHAR(20) NOT NULL UNIQUE,
    SoDienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    DiaChi NVARCHAR(200)
);

-- 4. Nhân viên
CREATE TABLE NhanVien (
    MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    ChucVu NVARCHAR(50) NOT NULL, -- Lễ tân / Quản lý / Buồng phòng
    SoDienThoai NVARCHAR(15)
);

-- 5. Tài khoản đăng nhập
CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(255) NOT NULL, -- lưu mật khẩu đã hash
    VaiTro NVARCHAR(20) NOT NULL, -- Admin / LeTan
    MaNhanVien INT NOT NULL,
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- 6. Đặt phòng (gồm luôn check-in/check-out thực tế)
CREATE TABLE DatPhong (
    MaDatPhong INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT NOT NULL,
    MaPhong INT NOT NULL,
    NgayDat DATETIME NOT NULL DEFAULT GETDATE(),
    NgayNhanDuKien DATE NOT NULL,
    NgayTraDuKien DATE NOT NULL,
    NgayCheckInThucTe DATETIME NULL,
    NgayCheckOutThucTe DATETIME NULL,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Đã đặt', -- Đã đặt/Đang ở/Đã trả/Đã hủy
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);

-- 7. Dịch vụ
CREATE TABLE DichVu (
    MaDichVu INT IDENTITY(1,1) PRIMARY KEY,
    TenDichVu NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL
);

-- 8. Hóa đơn
CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaDatPhong INT NOT NULL,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThaiThanhToan NVARCHAR(20) NOT NULL DEFAULT N'Chưa thanh toán',
    FOREIGN KEY (MaDatPhong) REFERENCES DatPhong(MaDatPhong)
);

-- 9. Chi tiết hóa đơn (tiền phòng + từng dịch vụ)
CREATE TABLE ChiTietHoaDon (
    MaChiTiet INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon INT NOT NULL,
    MaDichVu INT NULL, -- NULL nếu dòng này là tiền phòng
    NoiDung NVARCHAR(100) NOT NULL,
    SoLuong INT NOT NULL DEFAULT 1,
    DonGia DECIMAL(18,2) NOT NULL,
    ThanhTien AS (SoLuong * DonGia) PERSISTED,
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaDichVu) REFERENCES DichVu(MaDichVu)
);
GO