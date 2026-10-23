-- Chạy một lần trên database QuanLyKhachSan; có thể chạy lại an toàn.
-- Không xóa bảng, database hay dữ liệu hiện có.
SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF OBJECT_ID(N'dbo.DatPhong', N'U') IS NULL
    THROW 50001, N'Hãy chọn đúng database QuanLyKhachSan có bảng DatPhong.', 1;

IF COL_LENGTH(N'dbo.DatPhong', N'TienCoc') IS NULL
    ALTER TABLE dbo.DatPhong ADD TienCoc DECIMAL(18,2) NOT NULL
        CONSTRAINT DF_DatPhong_TienCoc DEFAULT (0) WITH VALUES;

IF OBJECT_ID(N'dbo.PhieuDatCoc', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PhieuDatCoc (
        MaPhieu INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        MaDatPhong INT NOT NULL REFERENCES dbo.DatPhong(MaDatPhong),
        LoaiGiaoDich NVARCHAR(10) NOT NULL,
        SoTien DECIMAL(18,2) NOT NULL,
        PhuongThuc NVARCHAR(10) NOT NULL,
        MaNhanVien INT NOT NULL REFERENCES dbo.NhanVien(MaNhanVien),
        NgayGiaoDich DATETIME NOT NULL DEFAULT GETDATE(),
        GhiChu NVARCHAR(200) NULL,
        CONSTRAINT CK_PhieuDatCoc_Loai CHECK (LoaiGiaoDich IN (N'Thu', N'Hoan')),
        CONSTRAINT CK_PhieuDatCoc_SoTien CHECK ((LoaiGiaoDich = N'Thu' AND SoTien > 0) OR (LoaiGiaoDich = N'Hoan' AND SoTien >= 0)),
        CONSTRAINT CK_PhieuDatCoc_PhuongThuc CHECK (PhuongThuc IN (N'TM', N'CK', N'POS')),
        CONSTRAINT UQ_PhieuDatCoc_Don_Loai UNIQUE (MaDatPhong, LoaiGiaoDich)
    );
END;

COMMIT TRANSACTION;
