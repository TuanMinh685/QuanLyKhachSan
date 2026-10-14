USE QuanLyKhachSan;
GO

-- Bổ sung 2 cột MoTa và KichHoat để form Phương Thức Thanh Toán không báo lỗi "Invalid column name"
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PhuongThucThanhToan')
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.PhuongThucThanhToan') AND name = 'MoTa')
        ALTER TABLE dbo.PhuongThucThanhToan ADD MoTa NVARCHAR(255);

    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.PhuongThucThanhToan') AND name = 'KichHoat')
        ALTER TABLE dbo.PhuongThucThanhToan ADD KichHoat BIT DEFAULT 1;

    EXEC('UPDATE dbo.PhuongThucThanhToan SET KichHoat = 1 WHERE KichHoat IS NULL;');
END
GO