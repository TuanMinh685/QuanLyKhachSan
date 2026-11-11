-- ============================================================================
-- TRƯỜNG ĐẠI HỌC VĂN HIẾN - KHOA CÔNG NGHỆ THÔNG TIN
-- HỌC PHẦN: CHUYÊN ĐỀ 1: KHAI THÁC HỆ CƠ SỞ DỮ LIỆU (MÃ HP: INT51309 - 6 TÍN CHỈ)
-- GIẢNG VIÊN HƯỚNG DẪN: THẦY LƯƠNG THÁI HIỀN
-- ĐỀ TÀI: KHAI THÁC & QUẢN TRỊ CSDL HỆ THỐNG QUẢN LÝ KHÁCH SẠN AN NHIÊN
-- HỆ QUẢN TRỊ CSDL: POSTGRESQL 15+ / SUPABASE
-- ============================================================================

-- ============================================================================
-- PHẦN 1: BỔ SUNG CẤU TRÚC PHỤ TRỢ & CHUẨN HÓA RÀNG BUỘC TOÀN VẸN (SCHEMA FIXES)
-- ============================================================================

-- 1.1 Bảng lưu vết lịch sử biến động nghiệp vụ (Audit Log hệ thống)
CREATE TABLE IF NOT EXISTS NhatKyHeThong (
    MaNhatKy        SERIAL PRIMARY KEY,
    LoaiHanhDong    VARCHAR(50)   NOT NULL, -- 'CHECK_IN', 'CHECK_OUT', 'DAT_PHONG', 'HUY_PHONG', 'AP_VOUCHER'
    BangTacDong     VARCHAR(50)   NOT NULL,
    KhoaChinh       VARCHAR(50)   NOT NULL,
    GiaTriCu        TEXT          NULL,
    GiaTriMoi       TEXT          NULL,
    NguoiThucHien   VARCHAR(100)  DEFAULT CURRENT_USER,
    ThoiGian        TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MoTaChiTiet     TEXT          NULL
);

-- 1.2 Bảng nhật ký bảo mật phục vụ BaoMatHelper.GhiNhatKy trong ứng dụng C#
CREATE TABLE IF NOT EXISTS NhatKyBaoMat (
    MaNhatKy        SERIAL PRIMARY KEY,
    TaiKhoan        VARCHAR(50)   NOT NULL,
    HanhDong        VARCHAR(100)  NOT NULL,
    ChiTiet         TEXT          NULL,
    MayTram         VARCHAR(100)  NULL,
    ThoiGian        TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 1.3 Bảng quản lý sổ phiếu đặt cọc và hoàn cọc
CREATE TABLE IF NOT EXISTS PhieuDatCoc (
    MaPhieu         SERIAL PRIMARY KEY,
    MaDatPhong      INT           NOT NULL REFERENCES DatPhong(MaDatPhong) ON DELETE CASCADE,
    LoaiGiaoDich    VARCHAR(10)   NOT NULL CHECK (LoaiGiaoDich IN ('Thu', 'Hoan')),
    SoTien          NUMERIC(18,2) NOT NULL CHECK (SoTien >= 0),
    PhuongThuc      VARCHAR(10)   NOT NULL DEFAULT 'TM',
    MaNhanVien      INT           NULL REFERENCES NhanVien(MaNhanVien),
    NgayGiaoDich    TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    GhiChu          VARCHAR(200)  NULL
);

-- 1.4 Bổ sung cột GioTraThucTe / GioNhanThucTe nếu bảng cũ chưa có
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'datphong' AND column_name = 'gionhanthucte') THEN
        ALTER TABLE DatPhong ADD COLUMN GioNhanThucTe TIMESTAMP NULL;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'datphong' AND column_name = 'giotrathucte') THEN
        ALTER TABLE DatPhong ADD COLUMN GioTraThucTe TIMESTAMP NULL;
    END IF;
END $$;

-- 1.5 Chuẩn hóa từ điển trạng thái phòng (Phong.TinhTrang chỉ nhận 'Hoạt động' hoặc 'Bảo trì')
UPDATE Phong
SET TinhTrang = 'Hoạt động'
WHERE TinhTrang NOT IN ('Hoạt động', 'Bảo trì');

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_phong_tinhtrang') THEN
        ALTER TABLE Phong ADD CONSTRAINT ck_phong_tinhtrang CHECK (TinhTrang IN ('Hoạt động', 'Bảo trì'));
    END IF;
END $$;

-- 1.6 Chuẩn hóa bảng PhuongThucThanhToan (thêm cột mã viết tắt MaPTTT) và tạo FK từ HoaDon
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'phuongthucthanhtoan' AND column_name = 'mapttt') THEN
        ALTER TABLE PhuongThucThanhToan ADD COLUMN MaPTTT VARCHAR(10);
    END IF;
END $$;

UPDATE PhuongThucThanhToan SET MaPTTT = 'TM'       WHERE TenPhuongThuc ILIKE '%Tiền mặt%' AND MaPTTT IS NULL;
UPDATE PhuongThucThanhToan SET MaPTTT = 'VNPAY'    WHERE TenPhuongThuc ILIKE '%VNPAY%' AND MaPTTT IS NULL;
UPDATE PhuongThucThanhToan SET MaPTTT = 'MOMO'     WHERE TenPhuongThuc ILIKE '%MoMo%' AND MaPTTT IS NULL;
UPDATE PhuongThucThanhToan SET MaPTTT = 'APPLEPAY' WHERE TenPhuongThuc ILIKE '%Apple%' AND MaPTTT IS NULL;
UPDATE PhuongThucThanhToan SET MaPTTT = 'CK'       WHERE TenPhuongThuc ILIKE '%Chuyển khoản%' AND MaPTTT IS NULL;
UPDATE PhuongThucThanhToan SET MaPTTT = 'POS'      WHERE TenPhuongThuc ILIKE '%Thẻ%' AND MaPTTT IS NULL;
UPDATE PhuongThucThanhToan SET MaPTTT = 'PT' || MaPhuongThuc::TEXT WHERE MaPTTT IS NULL;

-- Khử trùng MaPTTT nếu có nhiều dòng cùng loại trên CSDL hiện hành
WITH dup_pt AS (
    SELECT MaPhuongThuc, MaPTTT,
           ROW_NUMBER() OVER (PARTITION BY MaPTTT ORDER BY MaPhuongThuc) AS rn
    FROM PhuongThucThanhToan
)
UPDATE PhuongThucThanhToan pt
SET MaPTTT = 'PT' || pt.MaPhuongThuc::TEXT
FROM dup_pt d
WHERE pt.MaPhuongThuc = d.MaPhuongThuc AND d.rn > 1;

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'uq_phuongthucthanhtoan_mapttt') THEN
        ALTER TABLE PhuongThucThanhToan ADD CONSTRAINT uq_phuongthucthanhtoan_mapttt UNIQUE (MaPTTT);
    END IF;
END $$;

INSERT INTO PhuongThucThanhToan (MaPTTT, TenPhuongThuc, MoTa, KichHoat)
SELECT v.MaPTTT, v.TenPhuongThuc, v.MoTa, TRUE
FROM (VALUES
    ('TM',       'Tiền mặt',              'Thanh toán tiền mặt tại quầy lễ tân'),
    ('CK',       'Chuyển khoản',          'Chuyển khoản ngân hàng nội địa'),
    ('POS',      'Thẻ',                   'Quẹt thẻ ngân hàng qua máy POS'),
    ('VNPAY',    'VNPAY-QR',              'Quét mã QR cổng VNPAY chuẩn EMVCo'),
    ('MOMO',     'Ví MoMo',               'Thanh toán qua ví điện tử MoMo QR'),
    ('APPLEPAY', 'Apple Pay Contactless', 'Thanh toán chạm NFC Apple Pay')
) AS v(MaPTTT, TenPhuongThuc, MoTa)
WHERE NOT EXISTS (
    SELECT 1 FROM PhuongThucThanhToan pt
    WHERE pt.MaPTTT = v.MaPTTT OR pt.TenPhuongThuc = v.TenPhuongThuc
);

-- 1.7 Đồng bộ dữ liệu HoaDon.MaDatPhong sang mã chuẩn DatPhong.MaDat và tạo FOREIGN KEY vật lý
UPDATE HoaDon hd
SET MaDatPhong = dp.MaDat
FROM DatPhong dp
WHERE hd.MaDatPhong = dp.MaDatPhong::TEXT
  AND hd.MaDatPhong <> dp.MaDat;

UPDATE HoaDon hd
SET MaDatPhong = NULL
WHERE hd.MaDatPhong IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM DatPhong dp WHERE dp.MaDat = hd.MaDatPhong);

UPDATE HoaDon
SET MaPTTT = 'TM'
WHERE MaPTTT IS NULL
   OR NOT EXISTS (SELECT 1 FROM PhuongThucThanhToan pt WHERE pt.MaPTTT = HoaDon.MaPTTT);

-- Đồng bộ ThanhTien và TienDichVu giữa ChiTietHoaDon và HoaDon cho dữ liệu lịch sử
UPDATE ChiTietHoaDon
SET ThanhTien = COALESCE(SoLuong, 1) * COALESCE(DonGia, 0)
WHERE ThanhTien IS NULL OR ThanhTien <> COALESCE(SoLuong, 1) * COALESCE(DonGia, 0);

UPDATE HoaDon hd
SET TienDichVu = COALESCE((SELECT SUM(ct.ThanhTien) FROM ChiTietHoaDon ct WHERE ct.MaHoaDon = hd.MaHoaDon), 0),
    TongTien = GREATEST(0, TienPhong + COALESCE((SELECT SUM(ct.ThanhTien) FROM ChiTietHoaDon ct WHERE ct.MaHoaDon = hd.MaHoaDon), 0) - TienGiamGia - TienDatCoc);

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fk_hoadon_datphong') THEN
        ALTER TABLE HoaDon
        ADD CONSTRAINT fk_hoadon_datphong
        FOREIGN KEY (MaDatPhong) REFERENCES DatPhong(MaDat)
        ON UPDATE CASCADE ON DELETE SET NULL;
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fk_hoadon_phuongthucthanhtoan') THEN
        ALTER TABLE HoaDon
        ADD CONSTRAINT fk_hoadon_phuongthucthanhtoan
        FOREIGN KEY (MaPTTT) REFERENCES PhuongThucThanhToan(MaPTTT)
        ON UPDATE CASCADE;
    END IF;
END $$;

-- 1.8 Làm sạch dữ liệu trùng và thiết lập ràng buộc UNIQUE trên Voucher.Code, TaiKhoan.MaNhanVien, KhachHang.SoDienThoai
WITH dup_kh AS (
    SELECT MaKH, SoDienThoai,
           ROW_NUMBER() OVER (PARTITION BY SoDienThoai ORDER BY MaKH) AS rn
    FROM KhachHang
)
UPDATE KhachHang kh
SET SoDienThoai = LEFT(kh.SoDienThoai, 14) || '_' || kh.MaKH::TEXT
FROM dup_kh d
WHERE kh.MaKH = d.MaKH AND d.rn > 1;

WITH dup_vc AS (
    SELECT MaVoucher, Code,
           ROW_NUMBER() OVER (PARTITION BY Code ORDER BY MaVoucher) AS rn
    FROM Voucher
)
UPDATE Voucher vc
SET Code = LEFT(vc.Code, 22) || '_' || vc.MaVoucher::TEXT
FROM dup_vc d
WHERE vc.MaVoucher = d.MaVoucher AND d.rn > 1;

WITH dup_tk AS (
    SELECT MaTaiKhoan, MaNhanVien,
           ROW_NUMBER() OVER (PARTITION BY MaNhanVien ORDER BY MaTaiKhoan) AS rn
    FROM TaiKhoan
)
DELETE FROM TaiKhoan tk
USING dup_tk d
WHERE tk.MaTaiKhoan = d.MaTaiKhoan AND d.rn > 1;

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'uq_voucher_code') THEN
        ALTER TABLE Voucher ADD CONSTRAINT uq_voucher_code UNIQUE (Code);
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'uq_taikhoan_manhanvien') THEN
        ALTER TABLE TaiKhoan ADD CONSTRAINT uq_taikhoan_manhanvien UNIQUE (MaNhanVien);
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'uq_khachhang_sodienthoai') THEN
        ALTER TABLE KhachHang ADD CONSTRAINT uq_khachhang_sodienthoai UNIQUE (SoDienThoai);
    END IF;
END $$;


-- ============================================================================
-- PHẦN 2: LẬP TRÌNH TRIGGER TỰ ĐỘNG HÓA & TOÀN VẸN DỮ LIỆU (TRIGGERS)
-- ============================================================================

-- ----------------------------------------------------------------------------
-- TRIGGER 1: Tự động ghi nhận mốc thời gian lưu trú & nhật ký khi đổi trạng thái đơn
-- Nghiệp vụ:
--  - Giữ Phong.TinhTrang = 'Hoạt động' (tránh xung đột từ điển với ứng dụng C#).
--  - Tự động ghi nhận GioNhanThucTe, GioTraThucTe, NgayHuy và lưu vết NhatKyHeThong.
-- ----------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_trg_DongBoTrangThaiPhong()
RETURNS TRIGGER AS $$
BEGIN
    -- 1. Khách nhận phòng (Check-in)
    IF (NEW.TrangThai = 'Đang ở' AND (OLD.TrangThai IS NULL OR OLD.TrangThai <> 'Đang ở')) THEN
        UPDATE Phong 
        SET TinhTrang = 'Hoạt động'
        WHERE MaPhong = NEW.MaPhong AND TinhTrang <> 'Bảo trì';

        NEW.GioNhanThucTe := COALESCE(NEW.GioNhanThucTe, CURRENT_TIMESTAMP);

        INSERT INTO NhatKyHeThong (LoaiHanhDong, BangTacDong, KhoaChinh, GiaTriCu, GiaTriMoi, MoTaChiTiet)
        VALUES ('CHECK_IN', 'DatPhong', NEW.MaDatPhong::TEXT, OLD.TrangThai, NEW.TrangThai, 
                'Khách đã nhận phòng thành công tại phòng mã ' || NEW.MaPhong);
    END IF;

    -- 2. Khách trả phòng (Check-out)
    IF (NEW.TrangThai = 'Đã trả' AND (OLD.TrangThai IS NULL OR OLD.TrangThai <> 'Đã trả')) THEN
        UPDATE Phong 
        SET TinhTrang = 'Hoạt động'
        WHERE MaPhong = NEW.MaPhong AND TinhTrang <> 'Bảo trì';

        NEW.GioTraThucTe := COALESCE(NEW.GioTraThucTe, CURRENT_TIMESTAMP);

        INSERT INTO NhatKyHeThong (LoaiHanhDong, BangTacDong, KhoaChinh, GiaTriCu, GiaTriMoi, MoTaChiTiet)
        VALUES ('CHECK_OUT', 'DatPhong', NEW.MaDatPhong::TEXT, OLD.TrangThai, NEW.TrangThai, 
                'Khách đã hoàn tất trả phòng, phòng sẵn sàng đón khách tiếp theo.');
    END IF;

    -- 3. Hủy đơn đặt phòng
    IF (NEW.TrangThai = 'Đã hủy' AND (OLD.TrangThai IS NULL OR OLD.TrangThai <> 'Đã hủy')) THEN
        UPDATE Phong 
        SET TinhTrang = 'Hoạt động'
        WHERE MaPhong = NEW.MaPhong AND TinhTrang <> 'Bảo trì';

        NEW.NgayHuy := COALESCE(NEW.NgayHuy, CURRENT_TIMESTAMP);

        INSERT INTO NhatKyHeThong (LoaiHanhDong, BangTacDong, KhoaChinh, GiaTriCu, GiaTriMoi, MoTaChiTiet)
        VALUES ('HUY_PHONG', 'DatPhong', NEW.MaDatPhong::TEXT, OLD.TrangThai, NEW.TrangThai, 
                'Hủy đơn đặt phòng. Lý do: ' || COALESCE(NEW.LyDoHuy, 'Không nêu'));
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_DongBoTrangThaiPhong ON DatPhong;
CREATE TRIGGER trg_DongBoTrangThaiPhong
BEFORE UPDATE OF TrangThai ON DatPhong
FOR EACH ROW
EXECUTE FUNCTION fn_trg_DongBoTrangThaiPhong();


-- ----------------------------------------------------------------------------
-- TRIGGER 2: Tự động kiểm tra điều kiện, trừ số lượng Voucher (chống trừ kép 2 lần)
-- Nghiệp vụ:
--  - Chỉ thực thi khi Hóa đơn ở trạng thái 'Đã thanh toán' và có MaVoucher.
--  - Kiểm tra khóa chống trừ kép qua NhatKyHeThong để đảm bảo mỗi hóa đơn chỉ trừ đúng 1 lượt.
-- ----------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_trg_XuLyVoucherHoaDon()
RETURNS TRIGGER AS $$
DECLARE
    v_SoLuong INT;
    v_NgayBatDau DATE;
    v_HetHan DATE;
    v_DonToiThieu NUMERIC(18,2);
    v_TrangThai BOOLEAN;
BEGIN
    IF NEW.MaVoucher IS NOT NULL AND NEW.TrangThai = 'Đã thanh toán' THEN
        -- Nếu hóa đơn này đã từng được trừ Voucher rồi thì bỏ qua (tránh trừ kép 2 lần)
        IF EXISTS (
            SELECT 1 FROM NhatKyHeThong
            WHERE LoaiHanhDong = 'AP_VOUCHER' AND KhoaChinh = NEW.MaHoaDon
        ) THEN
            RETURN NEW;
        END IF;

        SELECT SoLuong, NgayBatDau, NgayHetHan, COALESCE(DonToiThieu, 0), TrangThai 
        INTO v_SoLuong, v_NgayBatDau, v_HetHan, v_DonToiThieu, v_TrangThai
        FROM Voucher 
        WHERE MaVoucher = NEW.MaVoucher
        FOR UPDATE;

        IF NOT FOUND THEN
            RAISE EXCEPTION 'Mã voucher không tồn tại trong hệ thống!';
        END IF;

        IF v_TrangThai = FALSE THEN
            RAISE EXCEPTION 'Mã voucher này hiện đang bị tạm khóa hoặc đã hết hiệu lực!';
        END IF;

        IF CURRENT_DATE < v_NgayBatDau OR CURRENT_DATE > v_HetHan THEN
            RAISE EXCEPTION 'Mã voucher nằm ngoài thời hạn sử dụng (% đến %)!', v_NgayBatDau, v_HetHan;
        END IF;

        IF v_SoLuong IS NOT NULL AND v_SoLuong <= 0 THEN
            RAISE EXCEPTION 'Mã voucher này đã hết lượt sử dụng!';
        END IF;

        IF (COALESCE(NEW.TienPhong, 0) + COALESCE(NEW.TienDichVu, 0)) < v_DonToiThieu THEN
            RAISE EXCEPTION 'Tổng tiền hóa đơn chưa đạt mức tối thiểu (%) để áp dụng voucher!', v_DonToiThieu;
        END IF;

        -- Trừ đúng 1 lượt sử dụng
        IF v_SoLuong IS NOT NULL THEN
            UPDATE Voucher
            SET SoLuong = SoLuong - 1,
                TrangThai = CASE WHEN (SoLuong - 1) <= 0 THEN FALSE ELSE TRUE END
            WHERE MaVoucher = NEW.MaVoucher;
        END IF;

        INSERT INTO NhatKyHeThong (LoaiHanhDong, BangTacDong, KhoaChinh, GiaTriCu, GiaTriMoi, MoTaChiTiet)
        VALUES ('AP_VOUCHER', 'HoaDon', NEW.MaHoaDon, NULL, NEW.MaVoucher::TEXT, 
                'Đã áp dụng voucher cho hóa đơn ' || NEW.MaHoaDon || '. Số lượng còn lại: ' || COALESCE((v_SoLuong - 1)::TEXT, 'Vô hạn'));
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_XuLyVoucherHoaDon ON HoaDon;
CREATE TRIGGER trg_XuLyVoucherHoaDon
AFTER INSERT OR UPDATE OF TrangThai, MaVoucher ON HoaDon
FOR EACH ROW
EXECUTE FUNCTION fn_trg_XuLyVoucherHoaDon();


-- ----------------------------------------------------------------------------
-- TRIGGER 3A (BEFORE) & 3B (AFTER): Tự động tính Thành tiền ChiTietHoaDon & cập nhật HoaDon
-- Nghiệp vụ:
--  - BEFORE INSERT OR UPDATE: Tự điền NEW.DonGia từ DichVu (nếu thiếu) và tính NEW.ThanhTien.
--  - AFTER INSERT OR UPDATE OR DELETE: Tính lại tổng TienDichVu và TongTien trên HoaDon,
--    trả về RETURN OLD khi DELETE và RETURN NEW khi INSERT/UPDATE đúng chuẩn PostgreSQL.
-- ----------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_trg_BeforeChiTietHoaDon()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.DonGia IS NULL OR NEW.DonGia <= 0 THEN
        SELECT DonGia INTO NEW.DonGia FROM DichVu WHERE MaDichVu = NEW.MaDichVu;
    END IF;
    NEW.SoLuong := COALESCE(NULLIF(NEW.SoLuong, 0), 1);
    NEW.ThanhTien := NEW.SoLuong * COALESCE(NEW.DonGia, 0);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_BeforeChiTietHoaDon ON ChiTietHoaDon;
CREATE TRIGGER trg_BeforeChiTietHoaDon
BEFORE INSERT OR UPDATE ON ChiTietHoaDon
FOR EACH ROW
EXECUTE FUNCTION fn_trg_BeforeChiTietHoaDon();

CREATE OR REPLACE FUNCTION fn_trg_CapNhatTienDichVuHoaDon()
RETURNS TRIGGER AS $$
DECLARE
    v_MaHD VARCHAR(20);
    v_TongTienDV NUMERIC(18,2);
BEGIN
    v_MaHD := COALESCE(NEW.MaHoaDon, OLD.MaHoaDon);

    -- Tính lại tổng tiền dịch vụ của hóa đơn
    SELECT COALESCE(SUM(ThanhTien), 0) INTO v_TongTienDV
    FROM ChiTietHoaDon
    WHERE MaHoaDon = v_MaHD;

    -- Cập nhật ngược lại bảng HoaDon
    UPDATE HoaDon
    SET TienDichVu = v_TongTienDV,
        TongTien = GREATEST(0, (TienPhong + v_TongTienDV - TienGiamGia - TienDatCoc))
    WHERE MaHoaDon = v_MaHD;

    IF TG_OP = 'DELETE' THEN
        RETURN OLD;
    ELSE
        RETURN NEW;
    END IF;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_CapNhatTienDichVuHoaDon ON ChiTietHoaDon;
CREATE TRIGGER trg_CapNhatTienDichVuHoaDon
AFTER INSERT OR UPDATE OR DELETE ON ChiTietHoaDon
FOR EACH ROW
EXECUTE FUNCTION fn_trg_CapNhatTienDichVuHoaDon();


-- ----------------------------------------------------------------------------
-- TRIGGER 4: Ngăn chặn đặt phòng trùng lịch (Anti Double-Booking Trigger)
-- ----------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_trg_KiemTraTrungLichDatPhong()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.TrangThai IN ('Đã đặt', 'Đang ở') AND EXISTS (
        SELECT 1 FROM DatPhong
        WHERE MaPhong = NEW.MaPhong
          AND TrangThai IN ('Đã đặt', 'Đang ở')
          AND MaDatPhong <> COALESCE(NEW.MaDatPhong, 0)
          AND (NEW.NgayNhan < NgayTra AND NEW.NgayTra > NgayNhan)
    ) THEN
        RAISE EXCEPTION 'Xung đột lịch: Phòng % đã có đơn đặt trong khoảng từ % đến %!', 
            NEW.MaPhong, NEW.NgayNhan, NEW.NgayTra;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_KiemTraTrungLichDatPhong ON DatPhong;
CREATE TRIGGER trg_KiemTraTrungLichDatPhong
BEFORE INSERT OR UPDATE OF MaPhong, NgayNhan, NgayTra, TrangThai ON DatPhong
FOR EACH ROW
EXECUTE FUNCTION fn_trg_KiemTraTrungLichDatPhong();


-- ============================================================================
-- PHẦN 3: HÀM & THỦ TỤC NGHIỆP VỤ NÂNG CAO (STORED PROCEDURES & FUNCTIONS)
-- ============================================================================

-- ----------------------------------------------------------------------------
-- FUNCTION 1: Tìm kiếm phòng khả dụng theo khoảng ngày & tiêu chí lọc
-- ----------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_TimPhongTrong(
    p_NgayNhan DATE,
    p_NgayTra  DATE,
    p_SoNguoi  INT DEFAULT 1,
    p_MaLoai   INT DEFAULT NULL
)
RETURNS TABLE (
    MaPhong       INT,
    SoPhong       VARCHAR(20),
    Tang          INT,
    MaLoai        INT,
    TenLoai       VARCHAR(50),
    GiaMotDem     NUMERIC(18,2),
    SoNguoiToiDa  INT,
    MoTaGiuong    VARCHAR(100),
    DienTich      NUMERIC(6,2),
    TinhTrang     VARCHAR(30)
) AS $$
BEGIN
    IF p_NgayNhan >= p_NgayTra THEN
        RAISE EXCEPTION 'Ngày nhận phòng (%) phải trước ngày trả phòng (%)!', p_NgayNhan, p_NgayTra;
    END IF;

    RETURN QUERY
    SELECT 
        p.MaPhong,
        p.SoPhong,
        p.Tang,
        p.MaLoai,
        lp.TenLoai,
        lp.GiaMotDem,
        lp.SoNguoiToiDa,
        p.MoTaGiuong,
        p.DienTich,
        p.TinhTrang
    FROM Phong p
    JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
    WHERE p.TinhTrang = 'Hoạt động'
      AND (p_MaLoai IS NULL OR p.MaLoai = p_MaLoai)
      AND (lp.SoNguoiToiDa >= p_SoNguoi)
      AND NOT EXISTS (
          SELECT 1 
          FROM DatPhong dp
          WHERE dp.MaPhong = p.MaPhong
            AND dp.TrangThai IN ('Đã đặt', 'Đang ở')
            AND (p_NgayNhan < dp.NgayTra AND p_NgayTra > dp.NgayNhan)
      )
    ORDER BY p.Tang, p.SoPhong;
END;
$$ LANGUAGE plpgsql;


-- ----------------------------------------------------------------------------
-- FUNCTION 2: Tính toán chi tiết chi phí đặt phòng (Ước tính giá & Tiền cọc 30%)
-- ----------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_TinhToanChiPhiDatPhong(
    p_GiaMotDem   NUMERIC(18,2),
    p_NgayNhan    DATE,
    p_NgayTra     DATE,
    p_SoNguoiLon  INT DEFAULT 1,
    p_SoTreEm     INT DEFAULT 0,
    p_PhanTramVAT NUMERIC DEFAULT 0.10
)
RETURNS TABLE (
    SoDem         INT,
    TienPhongGoc  NUMERIC(18,2),
    PhuThuNguoi   NUMERIC(18,2),
    ThueVAT       NUMERIC(18,2),
    TongChiPhi    NUMERIC(18,2),
    TienCocDeXuat NUMERIC(18,2)
) AS $$
DECLARE
    v_SoDem INT;
    v_TienGoc NUMERIC(18,2);
    v_PhuThu NUMERIC(18,2) := 0;
    v_Thue NUMERIC(18,2) := 0;
    v_Tong NUMERIC(18,2) := 0;
BEGIN
    v_SoDem := GREATEST(1, (p_NgayTra - p_NgayNhan));
    v_TienGoc := p_GiaMotDem * v_SoDem;

    IF p_SoNguoiLon > 2 THEN
        v_PhuThu := v_PhuThu + ((p_SoNguoiLon - 2) * 150000 * v_SoDem);
    END IF;

    IF p_SoTreEm > 0 THEN
        v_PhuThu := v_PhuThu + (p_SoTreEm * 80000 * v_SoDem);
    END IF;

    v_Thue := (v_TienGoc + v_PhuThu) * p_PhanTramVAT;
    v_Tong := v_TienGoc + v_PhuThu + v_Thue;

    RETURN QUERY
    SELECT 
        v_SoDem,
        v_TienGoc,
        v_PhuThu,
        v_Thue,
        v_Tong,
        ROUND(v_Tong * 0.30, -3);
END;
$$ LANGUAGE plpgsql;


-- ----------------------------------------------------------------------------
-- PROCEDURE 1: Quy trình Check-in nhận phòng an toàn (Transaction ACID)
-- ----------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE sp_ThucHienCheckIn(
    p_MaDatPhong INT,
    p_MaNhanVien INT DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_TrangThai VARCHAR(20);
    v_NgayNhan DATE;
    v_MaPhong INT;
    v_SoPhong VARCHAR(20);
    v_TinhTrangPhong VARCHAR(30);
    v_CCCD VARCHAR(50);
BEGIN
    SELECT dp.TrangThai, dp.NgayNhan, dp.MaPhong, p.SoPhong, p.TinhTrang, kh.CCCD
    INTO v_TrangThai, v_NgayNhan, v_MaPhong, v_SoPhong, v_TinhTrangPhong, v_CCCD
    FROM DatPhong dp
    JOIN Phong p ON dp.MaPhong = p.MaPhong
    LEFT JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    WHERE dp.MaDatPhong = p_MaDatPhong
    FOR UPDATE;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Không tìm thấy đơn đặt phòng có mã: %', p_MaDatPhong;
    END IF;

    IF v_TrangThai <> 'Đã đặt' THEN
        RAISE EXCEPTION 'Đơn phòng đang ở trạng thái "%", không thể thực hiện nhận phòng!', v_TrangThai;
    END IF;

    IF v_TinhTrangPhong = 'Bảo trì' THEN
        RAISE EXCEPTION 'Phòng % hiện đang Bảo trì, không thể nhận phòng!', v_SoPhong;
    END IF;

    IF v_CCCD IS NULL OR btrim(v_CCCD) = '' THEN
        RAISE EXCEPTION 'Khách chưa có CCCD/hộ chiếu. Hãy cập nhật hồ sơ khách hàng trước khi nhận phòng.';
    END IF;

    IF v_NgayNhan > CURRENT_DATE THEN
        RAISE EXCEPTION 'Chưa đến ngày nhận phòng (Ngày nhận: %). Chỉ nhận phòng từ ngày nhận trở đi!', v_NgayNhan;
    END IF;

    IF EXISTS (SELECT 1 FROM DatPhong WHERE MaPhong = v_MaPhong AND TrangThai = 'Đang ở' AND MaDatPhong <> p_MaDatPhong) THEN
        RAISE EXCEPTION 'Phòng % hiện đang có khách lưu trú chưa trả phòng!', v_SoPhong;
    END IF;

    UPDATE DatPhong
    SET TrangThai = 'Đang ở',
        GioNhanThucTe = COALESCE(GioNhanThucTe, CURRENT_TIMESTAMP),
        MaNhanVien = COALESCE(p_MaNhanVien, MaNhanVien)
    WHERE MaDatPhong = p_MaDatPhong;

    UPDATE Phong
    SET TinhTrang = 'Hoạt động'
    WHERE MaPhong = v_MaPhong AND TinhTrang <> 'Bảo trì';

    RAISE NOTICE 'Đã thực hiện Check-in thành công cho đơn đặt phòng % (Phòng %)', p_MaDatPhong, v_SoPhong;
END;
$$;


-- ----------------------------------------------------------------------------
-- PROCEDURE 2: Quy trình Check-out, tính toán hóa đơn, kiểm tra Voucher & hoàn cọc dư
-- Hỗ trợ đầy đủ dịch vụ tại quầy (p_DichVuJson) và kết nối trực tiếp với WinForms C#
-- ----------------------------------------------------------------------------
DROP PROCEDURE IF EXISTS sp_ThucHienCheckOut(INT, VARCHAR, INT, VARCHAR, INT);
DROP PROCEDURE IF EXISTS sp_ThucHienCheckOut(INT, VARCHAR, INT, VARCHAR, INT, JSONB, NUMERIC, NUMERIC, NUMERIC);

CREATE OR REPLACE PROCEDURE sp_ThucHienCheckOut(
    p_MaDatPhong        INT,
    p_MaPTTT            VARCHAR(10) DEFAULT 'TM',
    p_MaVoucher         INT DEFAULT NULL,
    INOUT p_MaHoaDon    VARCHAR(20) DEFAULT NULL,
    p_MaNhanVien        INT DEFAULT 1,
    p_DichVuJson        JSONB DEFAULT NULL,
    p_TienGiamGia       NUMERIC(18,2) DEFAULT NULL,
    INOUT p_TongThucThu NUMERIC(18,2) DEFAULT 0,
    INOUT p_HoanCocDu   NUMERIC(18,2) DEFAULT 0
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_MaDat VARCHAR(20);
    v_MaPhong INT;
    v_SoPhong VARCHAR(20);
    v_MaNV INT;
    v_TienPhong NUMERIC(18,2);
    v_TienCoc NUMERIC(18,2);
    v_TienDV NUMERIC(18,2) := 0;
    v_TienGiam NUMERIC(18,2) := 0;
    v_LoaiGiam VARCHAR(10);
    v_GiaTriVoucher NUMERIC(18,2);
    v_DonToiThieu NUMERIC(18,2);
    v_NgayBD DATE;
    v_NgayKT DATE;
    v_SoLuongVC INT;
    v_TrangThaiVC BOOLEAN;
    v_PhuongThucCoc VARCHAR(10);
BEGIN
    -- 1. Lấy thông tin đơn đặt phòng và khóa dòng FOR UPDATE
    SELECT dp.MaDat, dp.MaPhong, p.SoPhong, COALESCE(dp.TongTien, 0), COALESCE(dp.TienCoc, 0), COALESCE(p_MaNhanVien, dp.MaNhanVien, 1)
    INTO v_MaDat, v_MaPhong, v_SoPhong, v_TienPhong, v_TienCoc, v_MaNV
    FROM DatPhong dp
    JOIN Phong p ON dp.MaPhong = p.MaPhong
    WHERE dp.MaDatPhong = p_MaDatPhong AND dp.TrangThai IN ('Đang ở', 'Đã trả')
    FOR UPDATE;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Không tìm thấy đơn đặt phòng đang ở có mã: %!', p_MaDatPhong;
    END IF;

    IF EXISTS (
        SELECT 1 FROM HoaDon
        WHERE MaDatPhong IN (v_MaDat, p_MaDatPhong::TEXT)
          AND TrangThai = 'Đã thanh toán'
          AND (p_MaHoaDon IS NULL OR MaHoaDon <> p_MaHoaDon)
    ) THEN
        RAISE EXCEPTION 'Đơn đặt phòng % đã được thanh toán trước đó!', v_MaDat;
    END IF;

    -- 2. Sinh mã hóa đơn nếu chưa truyền vào (HDYYYYMMDDXXXX)
    IF p_MaHoaDon IS NULL OR btrim(p_MaHoaDon) = '' THEN
        p_MaHoaDon := 'HD' || TO_CHAR(CURRENT_TIMESTAMP, 'YYYYMMDD') || LPAD(p_MaDatPhong::TEXT, 4, '0');
    END IF;

    -- 3. Tạo trước bản ghi HoaDon đích
    INSERT INTO HoaDon (
        MaHoaDon, MaDatPhong, NgayLap, NgayThanhToan,
        TienPhong, TienDichVu, TienGiamGia, TienDatCoc, TongTien,
        MaPTTT, MaVoucher, TrangThai
    )
    VALUES (
        p_MaHoaDon, v_MaDat, CURRENT_TIMESTAMP, NULL,
        v_TienPhong, 0, 0, v_TienCoc, 0,
        COALESCE(p_MaPTTT, 'TM'), NULL, 'Chưa thanh toán'
    )
    ON CONFLICT (MaHoaDon) DO NOTHING;

    -- 4. Xử lý dịch vụ phát sinh (On-the-spot Services):
    IF p_DichVuJson IS NOT NULL AND jsonb_typeof(p_DichVuJson) = 'array' AND jsonb_array_length(p_DichVuJson) > 0 THEN
        DELETE FROM ChiTietHoaDon WHERE MaHoaDon = p_MaHoaDon;

        INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, SoLuong, DonGia, ThanhTien)
        SELECT
            p_MaHoaDon,
            x->>'MaDichVu',
            COALESCE((x->>'SoLuong')::INT, 1),
            COALESCE((x->>'DonGia')::NUMERIC(18,2), 0),
            COALESCE((x->>'ThanhTien')::NUMERIC(18,2), COALESCE((x->>'SoLuong')::INT, 1) * COALESCE((x->>'DonGia')::NUMERIC(18,2), 0))
        FROM jsonb_array_elements(p_DichVuJson) AS x;
    ELSE
        UPDATE ChiTietHoaDon
        SET MaHoaDon = p_MaHoaDon
        WHERE MaHoaDon IN (
            SELECT MaHoaDon FROM HoaDon
            WHERE MaDatPhong IN (v_MaDat, p_MaDatPhong::TEXT)
              AND TrangThai = 'Chưa thanh toán'
              AND MaHoaDon <> p_MaHoaDon
        );
    END IF;

    DELETE FROM HoaDon
    WHERE MaDatPhong IN (v_MaDat, p_MaDatPhong::TEXT)
      AND TrangThai = 'Chưa thanh toán'
      AND MaHoaDon <> p_MaHoaDon;

    SELECT COALESCE(SUM(ThanhTien), 0) INTO v_TienDV
    FROM ChiTietHoaDon
    WHERE MaHoaDon = p_MaHoaDon;

    -- 5. Kiểm tra đầy đủ điều kiện Voucher & tính giảm giá
    IF p_TienGiamGia IS NOT NULL AND p_TienGiamGia > 0 THEN
        v_TienGiam := p_TienGiamGia;
    ELSIF p_MaVoucher IS NOT NULL THEN
        SELECT LoaiGiam, GiaTri, COALESCE(DonToiThieu, 0), NgayBatDau, NgayHetHan, SoLuong, TrangThai
        INTO v_LoaiGiam, v_GiaTriVoucher, v_DonToiThieu, v_NgayBD, v_NgayKT, v_SoLuongVC, v_TrangThaiVC
        FROM Voucher
        WHERE MaVoucher = p_MaVoucher
        FOR UPDATE;

        IF NOT FOUND THEN
            RAISE EXCEPTION 'Mã khuyến mãi (Voucher %) không tồn tại!', p_MaVoucher;
        END IF;

        IF v_TrangThaiVC = FALSE THEN
            RAISE EXCEPTION 'Mã khuyến mãi hiện đang bị khóa hoặc ngừng áp dụng!';
        END IF;

        IF CURRENT_DATE < v_NgayBD OR CURRENT_DATE > v_NgayKT THEN
            RAISE EXCEPTION 'Mã khuyến mãi nằm ngoài thời hạn áp dụng (% đến %)!', v_NgayBD, v_NgayKT;
        END IF;

        IF v_SoLuongVC IS NOT NULL AND v_SoLuongVC <= 0 THEN
            RAISE EXCEPTION 'Mã khuyến mãi đã hết lượt sử dụng!';
        END IF;

        IF (v_TienPhong + v_TienDV) < v_DonToiThieu THEN
            RAISE EXCEPTION 'Tổng tiền phòng & dịch vụ (%) chưa đạt mức đơn tối thiểu (%) của Voucher!',
                (v_TienPhong + v_TienDV), v_DonToiThieu;
        END IF;

        IF v_LoaiGiam = 'PhanTram' THEN
            v_TienGiam := LEAST((v_TienPhong + v_TienDV) * (v_GiaTriVoucher / 100.0), (v_TienPhong + v_TienDV));
        ELSE
            v_TienGiam := LEAST(v_GiaTriVoucher, (v_TienPhong + v_TienDV));
        END IF;
    END IF;

    -- 6. Tính tổng tiền thực thu và hoàn cọc dư
    p_TongThucThu := GREATEST(0, (v_TienPhong + v_TienDV - v_TienCoc - v_TienGiam));
    p_HoanCocDu   := GREATEST(0, v_TienCoc - (v_TienPhong + v_TienDV - v_TienGiam));

    -- 7. Cập nhật chốt hóa đơn 'Đã thanh toán' (kích hoạt trg_XuLyVoucherHoaDon trên Supabase)
    UPDATE HoaDon
    SET MaDatPhong    = v_MaDat,
        NgayThanhToan = CURRENT_TIMESTAMP,
        TienPhong     = v_TienPhong,
        TienDichVu    = v_TienDV,
        TienGiamGia   = v_TienGiam,
        TienDatCoc    = v_TienCoc,
        TongTien      = p_TongThucThu,
        MaPTTT        = COALESCE(p_MaPTTT, 'TM'),
        MaVoucher     = p_MaVoucher,
        TrangThai     = 'Đã thanh toán'
    WHERE MaHoaDon = p_MaHoaDon;

    -- Tránh trừ kép: nếu trigger chưa có thì thủ tục tự cập nhật an toàn
    IF p_MaVoucher IS NOT NULL AND NOT EXISTS (
        SELECT 1 FROM pg_trigger t
        JOIN pg_class c ON c.oid = t.tgrelid
        WHERE c.relname = 'hoadon' AND t.tgname = 'trg_xulyvoucherhoadon' AND t.tgenabled <> 'D'
    ) THEN
        UPDATE Voucher
        SET SoLuong = CASE WHEN SoLuong IS NOT NULL THEN SoLuong - 1 ELSE NULL END,
            TrangThai = CASE WHEN SoLuong IS NOT NULL AND (SoLuong - 1) <= 0 THEN FALSE ELSE TrangThai END
        WHERE MaVoucher = p_MaVoucher AND (SoLuong IS NULL OR SoLuong > 0);
    END IF;

    -- 8. Ghi sổ hoàn cọc dư vào bảng PhieuDatCoc
    IF p_HoanCocDu > 0 THEN
        v_PhuongThucCoc := CASE
            WHEN COALESCE(p_MaPTTT, 'TM') IN ('CK', 'VNPAY', 'MOMO') THEN 'CK'
            WHEN COALESCE(p_MaPTTT, 'TM') IN ('POS', 'APPLEPAY') THEN 'POS'
            ELSE 'TM'
        END;

        INSERT INTO PhieuDatCoc (MaDatPhong, LoaiGiaoDich, SoTien, PhuongThuc, MaNhanVien, GhiChu)
        VALUES (
            p_MaDatPhong,
            'Hoan',
            p_HoanCocDu,
            v_PhuongThucCoc,
            v_MaNV,
            'Hoàn cọc dư khi thanh toán ' || p_MaHoaDon || ' qua ' || COALESCE(p_MaPTTT, 'TM')
        );
    END IF;

    -- 9. Cập nhật đơn đặt phòng sang 'Đã trả' và giữ Phong.TinhTrang = 'Hoạt động'
    UPDATE DatPhong
    SET TrangThai = 'Đã trả',
        GioTraThucTe = COALESCE(GioTraThucTe, CURRENT_TIMESTAMP)
    WHERE MaDatPhong = p_MaDatPhong;

    UPDATE Phong
    SET TinhTrang = 'Hoạt động'
    WHERE MaPhong = v_MaPhong AND TinhTrang <> 'Bảo trì';

    RAISE NOTICE 'Đã Check-out và xuất hóa đơn % thành công cho phòng %. Tổng thu: % VNĐ (Hoàn cọc dư: % VNĐ)', 
        p_MaHoaDon, v_SoPhong, p_TongThucThu, p_HoanCocDu;
END;
$$;

-- Overload 5 tham số nguyên bản đúng đặc tả Mục 4.3.4 Báo Cáo Chuyên Đề 1 của Tuấn Minh
CREATE OR REPLACE PROCEDURE sp_ThucHienCheckOut(
    p_MaDatPhong     INT,
    p_MaPTTT         VARCHAR(10),
    p_MaVoucher      INT,
    INOUT p_MaHoaDon VARCHAR(20) DEFAULT NULL,
    p_MaNhanVien     INT DEFAULT 1
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_TongThucThu NUMERIC(18,2) := 0;
    v_HoanCocDu   NUMERIC(18,2) := 0;
BEGIN
    CALL sp_ThucHienCheckOut(
        p_MaDatPhong,
        p_MaPTTT,
        p_MaVoucher,
        p_MaHoaDon,
        p_MaNhanVien,
        NULL::jsonb,
        NULL::numeric,
        v_TongThucThu,
        v_HoanCocDu
    );
END;
$$;


-- ============================================================================
-- PHẦN 4: KHAI THÁC & THỐNG KÊ DỮ LIỆU ĐA CHIỀU (ANALYTIC VIEWS)
-- ============================================================================

-- ----------------------------------------------------------------------------
-- VIEW 1: Doanh thu kinh doanh theo tháng & cơ cấu thu chi
-- ----------------------------------------------------------------------------
CREATE OR REPLACE VIEW vw_KhaiThacDoanhThuTheoThang AS
SELECT 
    TO_CHAR(COALESCE(hd.NgayThanhToan, hd.NgayLap), 'YYYY-MM') AS ThangNam,
    COUNT(hd.MaHoaDon)                                         AS TongSoHoaDon,
    SUM(hd.TienPhong)                                          AS DoanhThuTienPhong,
    SUM(hd.TienDichVu)                                         AS DoanhThuDichVu,
    SUM(hd.TienGiamGia)                                        AS TongTienUuDai,
    SUM(hd.TongTien)                                           AS DoanhThuThucThu,
    ROUND(AVG(hd.TongTien), 2)                                 AS GiaTriHoaDonTrungBinh,
    COUNT(CASE WHEN hd.MaPTTT IN ('VNPAY', 'MOMO', 'APPLEPAY', 'CK', 'POS') THEN 1 END) AS SoGiaoDichKhongTienMat
FROM HoaDon hd
WHERE hd.TrangThai = 'Đã thanh toán'
GROUP BY TO_CHAR(COALESCE(hd.NgayThanhToan, hd.NgayLap), 'YYYY-MM')
ORDER BY ThangNam DESC;


-- ----------------------------------------------------------------------------
-- VIEW 2: Tỷ lệ lấp đầy phòng (Occupancy Rate) theo từng Loại phòng
-- (Đã sửa Lỗi 1.3: Đếm phòng đang có khách dựa trên đơn lưu trú 'Đang ở' trong DatPhong)
-- ----------------------------------------------------------------------------
CREATE OR REPLACE VIEW vw_KhaiThacCongSuatLoaiPhong AS
SELECT 
    lp.MaLoai,
    lp.TenLoai,
    lp.GiaMotDem,
    COUNT(DISTINCT p.MaPhong) AS TongSoPhong,
    COUNT(DISTINCT CASE WHEN dp_active.MaPhong IS NOT NULL THEN p.MaPhong END) AS SoPhongDangCoKhach,
    COUNT(DISTINCT CASE WHEN p.TinhTrang = 'Bảo trì' THEN p.MaPhong END) AS SoPhongChoDon,
    COUNT(DISTINCT CASE WHEN p.TinhTrang = 'Hoạt động' AND dp_active.MaPhong IS NULL THEN p.MaPhong END) AS SoPhongSanSangDonKhach,
    COALESCE(ROUND(
        COUNT(DISTINCT CASE WHEN dp_active.MaPhong IS NOT NULL THEN p.MaPhong END)::NUMERIC / 
        NULLIF(COUNT(DISTINCT p.MaPhong), 0) * 100, 
        2
    ), 0.00) AS TyLeLapDayPhanTram
FROM LoaiPhong lp
LEFT JOIN Phong p ON lp.MaLoai = p.MaLoai
LEFT JOIN (
    SELECT DISTINCT MaPhong
    FROM DatPhong
    WHERE TrangThai = 'Đang ở'
) dp_active ON dp_active.MaPhong = p.MaPhong
GROUP BY lp.MaLoai, lp.TenLoai, lp.GiaMotDem
ORDER BY lp.MaLoai;


-- ----------------------------------------------------------------------------
-- VIEW 3: Xếp hạng Dịch vụ phụ trợ được khách hàng ưa chuộng nhất
-- (Đã sửa Lỗi 1.5: Chỉ cộng dịch vụ thuộc các hóa đơn có TrangThai = 'Đã thanh toán')
-- ----------------------------------------------------------------------------
CREATE OR REPLACE VIEW vw_KhaiThacTopDichVu AS
SELECT 
    dv.MaDichVu,
    dv.TenDichVu,
    dv.LoaiDichVu,
    dv.DonGia,
    COALESCE(SUM(ct.SoLuong), 0)                             AS TongSoLuongDaBan,
    COALESCE(SUM(ct.ThanhTien), 0)                           AS TongDoanhThuDichVu,
    DENSE_RANK() OVER (ORDER BY COALESCE(SUM(ct.ThanhTien), 0) DESC) AS XepHangDoanhThu
FROM DichVu dv
LEFT JOIN (
    ChiTietHoaDon ct
    INNER JOIN HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon AND hd.TrangThai = 'Đã thanh toán'
) ON dv.MaDichVu = ct.MaDichVu
GROUP BY dv.MaDichVu, dv.TenDichVu, dv.LoaiDichVu, dv.DonGia
ORDER BY TongDoanhThuDichVu DESC;


-- ----------------------------------------------------------------------------
-- VIEW 4: Phân khúc khách hàng VIP & Thân thiết (RFM Analytics)
-- (Đã sửa Lỗi 1.6: Loại trừ đơn 'Đã hủy' và dùng COUNT(DISTINCT dp.MaDatPhong))
-- ----------------------------------------------------------------------------
CREATE OR REPLACE VIEW vw_KhaiThacKhachHangVIP AS
SELECT 
    kh.MaKH,
    kh.MaKHHienThi,
    kh.HoTen,
    kh.SoDienThoai,
    kh.QuocTich,
    COUNT(DISTINCT dp.MaDatPhong)                            AS SoLanDatPhong,
    MAX(dp.NgayNhan)                                         AS LanDatPhongGanNhat,
    COALESCE(SUM(hd.TongTien), 0)                            AS TongChiTieu,
    CASE 
        WHEN COALESCE(SUM(hd.TongTien), 0) >= 15000000 THEN '💎 VIP Kim Cương'
        WHEN COALESCE(SUM(hd.TongTien), 0) >= 7000000  THEN '🥇 VIP Vàng'
        WHEN COUNT(DISTINCT dp.MaDatPhong) >= 3        THEN '🥈 Thân thiết'
        ELSE 'Khách mới'
    END AS HangThanhVien
FROM KhachHang kh
LEFT JOIN DatPhong dp ON kh.MaKH = dp.MaKH AND dp.TrangThai IN ('Đang ở', 'Đã trả')
LEFT JOIN HoaDon hd ON (hd.MaDatPhong = dp.MaDat OR CAST(dp.MaDatPhong AS TEXT) = hd.MaDatPhong) AND hd.TrangThai = 'Đã thanh toán'
GROUP BY kh.MaKH, kh.MaKHHienThi, kh.HoTen, kh.SoDienThoai, kh.QuocTich
ORDER BY TongChiTieu DESC;


-- ----------------------------------------------------------------------------
-- VIEW 5: Bảng tổng quan Dashboard cho Giám đốc / Lễ tân ca trực
-- (Đã sửa Lỗi 1.3: Đếm PhongDangO từ DatPhong.TrangThai = 'Đang ở' và lọc HoaDon 'Đã thanh toán')
-- ----------------------------------------------------------------------------
CREATE OR REPLACE VIEW vw_KhaiThacDashboardTongHop AS
SELECT 
    (SELECT COUNT(*) FROM Phong)                                                                 AS TongSoPhong,
    (SELECT COUNT(DISTINCT MaPhong) FROM DatPhong WHERE TrangThai = 'Đang ở')                    AS PhongDangO,
    (SELECT COUNT(*) FROM Phong WHERE TinhTrang = 'Bảo trì')                                     AS PhongChoDon,
    (SELECT COUNT(*) FROM DatPhong WHERE TrangThai = 'Đã đặt' AND NgayNhan = CURRENT_DATE)       AS CheckInHomNay,
    (SELECT COUNT(*) FROM DatPhong WHERE TrangThai = 'Đang ở' AND NgayTra = CURRENT_DATE)        AS CheckOutHomNay,
    (SELECT COALESCE(SUM(TongTien), 0) FROM HoaDon WHERE TrangThai = 'Đã thanh toán' AND CAST(NgayThanhToan AS DATE) = CURRENT_DATE) AS DoanhThuHomNay,
    (SELECT COALESCE(SUM(TongTien), 0) FROM HoaDon WHERE TrangThai = 'Đã thanh toán' AND DATE_TRUNC('month', NgayThanhToan) = DATE_TRUNC('month', CURRENT_DATE)) AS DoanhThuThangNay;


-- ============================================================================
-- PHẦN 5: TỐI ƯU HÓA TRUY VẤN VỚI CHỈ MỤC (INDEXING & PERFORMANCE TUNING)
-- ============================================================================

CREATE INDEX IF NOT EXISTS IX_DatPhong_NgayNhan_NgayTra 
    ON DatPhong (NgayNhan, NgayTra, TrangThai);

CREATE INDEX IF NOT EXISTS IX_DatPhong_Phong_TrangThai 
    ON DatPhong (MaPhong, TrangThai);

CREATE INDEX IF NOT EXISTS IX_KhachHang_SDT_HoTen 
    ON KhachHang (SoDienThoai, HoTen);

CREATE INDEX IF NOT EXISTS IX_HoaDon_NgayThanhToan_TrangThai 
    ON HoaDon (NgayThanhToan, TrangThai, TongTien);

CREATE INDEX IF NOT EXISTS IX_ChiTietHoaDon_MaHD 
    ON ChiTietHoaDon (MaHoaDon, MaDichVu);

CREATE INDEX IF NOT EXISTS IX_Voucher_Code_HieuLuc 
    ON Voucher (Code, TrangThai, NgayHetHan);


-- ============================================================================
-- PHẦN 6: BẢO MẬT PHÂN QUYỀN (RBAC & ROW-LEVEL SECURITY)
-- (Đã sửa Lỗi 1.7: Thay bảng LichSuLuuTru bằng PhieuDatCoc, NhatKyHeThong, NhatKyBaoMat)
-- ============================================================================

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'role_quan_tri_ks') THEN
        CREATE ROLE role_quan_tri_ks WITH LOGIN PASSWORD 'Admin@Hotel2026';
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'role_le_tan_ks') THEN
        CREATE ROLE role_le_tan_ks WITH LOGIN PASSWORD 'LeTan@Hotel2026';
    END IF;
END $$;

GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO role_quan_tri_ks;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO role_quan_tri_ks;
GRANT ALL PRIVILEGES ON ALL FUNCTIONS IN SCHEMA public TO role_quan_tri_ks;

GRANT SELECT, INSERT, UPDATE ON DatPhong, KhachHang, HoaDon, ChiTietHoaDon, Phong, PhieuDatCoc, NhatKyHeThong, NhatKyBaoMat TO role_le_tan_ks;
GRANT SELECT ON LoaiPhong, DichVu, Voucher, PhuongThucThanhToan, vw_KhaiThacDoanhThuTheoThang, vw_KhaiThacCongSuatLoaiPhong, vw_KhaiThacTopDichVu, vw_KhaiThacKhachHangVIP, vw_KhaiThacDashboardTongHop TO role_le_tan_ks;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO role_le_tan_ks;
GRANT EXECUTE ON FUNCTION fn_TimPhongTrong, fn_TinhToanChiPhiDatPhong TO role_le_tan_ks;
GRANT EXECUTE ON PROCEDURE sp_ThucHienCheckIn(INT, INT) TO role_le_tan_ks;
GRANT EXECUTE ON PROCEDURE sp_ThucHienCheckOut(INT, VARCHAR, INT, VARCHAR, INT) TO role_le_tan_ks;
GRANT EXECUTE ON PROCEDURE sp_ThucHienCheckOut(INT, VARCHAR, INT, VARCHAR, INT, JSONB, NUMERIC, NUMERIC, NUMERIC) TO role_le_tan_ks;

REVOKE ALL ON TaiKhoan FROM role_le_tan_ks;
