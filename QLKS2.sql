USE QuanLyKhachSan;
GO

-- Phương thức thanh toán
CREATE TABLE PhuongThucThanhToan (
    MaPhuongThuc INT IDENTITY(1,1) PRIMARY KEY,
    TenPhuongThuc NVARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO PhuongThucThanhToan (TenPhuongThuc)
VALUES (N'Tiền mặt'), (N'Chuyển khoản'), (N'Thẻ');

-- Voucher
CREATE TABLE Voucher (
    MaVoucher INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(30) NOT NULL UNIQUE,
    LoaiGiam NVARCHAR(10) NOT NULL CHECK (LoaiGiam IN (N'PhanTram', N'SoTien')),
    GiaTri DECIMAL(18,2) NOT NULL,
    DonToiThieu DECIMAL(18,2) NOT NULL DEFAULT 0,
    NgayBatDau DATE NOT NULL,
    NgayHetHan DATE NOT NULL,
    SoLuong INT NULL,              -- NULL = không giới hạn
    TrangThai BIT NOT NULL DEFAULT 1
);

-- Bổ sung cột cho hóa đơn
ALTER TABLE HoaDon ADD
    MaVoucher INT NULL,
    MaPhuongThuc INT NULL,
    GiamGia DECIMAL(18,2) NOT NULL DEFAULT 0;
GO

ALTER TABLE HoaDon ADD CONSTRAINT FK_HoaDon_Voucher
    FOREIGN KEY (MaVoucher) REFERENCES Voucher(MaVoucher);
ALTER TABLE HoaDon ADD CONSTRAINT FK_HoaDon_PhuongThuc
    FOREIGN KEY (MaPhuongThuc) REFERENCES PhuongThucThanhToan(MaPhuongThuc);
GO

-- Dịch vụ: thêm loại dịch vụ
ALTER TABLE DichVu ADD LoaiDichVu NVARCHAR(30) NOT NULL DEFAULT N'Khác';
GO