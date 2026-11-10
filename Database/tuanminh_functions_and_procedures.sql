-- ============================================================================
-- TRƯỜNG ĐẠI HỌC VĂN HIẾN - KHOA CÔNG NGHỆ THÔNG TIN
-- HỌC PHẦN: CHUYÊN ĐỀ 1 - KHAI THÁC HỆ CƠ SỞ DỮ LIỆU (MÃ HP: INT51309 - 6 TÍN CHỈ)
-- GIẢNG VIÊN HƯỚNG DẪN: THẦY LƯƠNG THÁI HIỀN
-- ĐỀ TÀI: KHAI THÁC & QUẢN TRỊ CSDL HỆ THỐNG QUẢN LÝ KHÁCH SẠN AN NHIÊN
-- THÀNH VIÊN PHỤ TRÁCH: LÊ TRẦN TUẤN MINH (TRƯỞNG NHÓM - NHÁNH tuanminh)
-- NHIỆM VỤ: THỦ TỤC & HÀM NGHIỆP VỤ GIAO DỊCH AN TOÀN (TRANSACTION ACID)
-- HỆ QUẢN TRỊ CSDL: POSTGRESQL 15+ / SUPABASE CLOUD
-- ============================================================================

-- ============================================================================
-- PHẦN 1: HÀM NGHIỆP VỤ (FUNCTIONS)
-- ============================================================================

-- ----------------------------------------------------------------------------
-- FUNCTION 1: Tìm kiếm phòng trống thông minh theo khoảng ngày & sức chứa
-- Nghiệp vụ: Lọc các phòng đang ở trạng thái Hoạt động/Trống, kiểm tra không bị
-- giao thoa với bất kỳ đơn đặt phòng nào đang hoạt động trong khoảng ngày đó.
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
    -- Kiểm tra logic ngày hợp lệ
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
    WHERE p.TinhTrang IN ('Hoạt động', 'Trống', 'Đã dọn dẹp')
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
-- FUNCTION 2: Tính toán chi tiết chi phí đặt phòng (Ước tính giá phòng & tiền cọc 30%)
-- Nghiệp vụ: Tính số đêm lưu trú thực tế, phụ thu người lớn/trẻ em, tính thuế VAT
-- và đề xuất mức cọc 30% làm tròn nghìn để thu cọc khách hàng.
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

    -- Phụ thu nếu trên 2 người lớn (150.000 đ/người/đêm)
    IF p_SoNguoiLon > 2 THEN
        v_PhuThu := v_PhuThu + ((p_SoNguoiLon - 2) * 150000 * v_SoDem);
    END IF;

    -- Phụ thu trẻ em (80.000 đ/bé/đêm)
    IF p_SoTreEm > 0 THEN
        v_PhuThu := v_PhuThu + (p_SoTreEm * 80000 * v_SoDem);
    END IF;

    -- Thuế và phí
    v_Thue := (v_TienGoc + v_PhuThu) * p_PhanTramVAT;
    v_Tong := v_TienGoc + v_PhuThu + v_Thue;

    RETURN QUERY
    SELECT 
        v_SoDem,
        v_TienGoc,
        v_PhuThu,
        v_Thue,
        v_Tong,
        ROUND(v_Tong * 0.30, -3); -- Tiền cọc 30% làm tròn nghìn
END;
$$ LANGUAGE plpgsql;


-- ============================================================================
-- PHẦN 2: THỦ TỤC NGHIỆP VỤ GIAO DỊCH AN TOÀN (STORED PROCEDURES - ACID)
-- ============================================================================

-- ----------------------------------------------------------------------------
-- PROCEDURE 1: Quy trình Check-in nhận phòng an toàn (Transaction ACID)
-- Nghiệp vụ:
--  1. Kiểm tra đơn đặt phòng và khóa dòng dữ liệu (FOR UPDATE) chống tranh chấp.
--  2. Kiểm tra tính hợp lệ của ngày nhận (không được nhận trước ngày ghi trên đơn).
--  3. Kiểm tra phòng có đang bị khách khác ở không.
--  4. Chuyển trạng thái đơn sang 'Đang ở', ghi nhận giờ nhận thực tế.
--  5. Đổi trạng thái phòng sang 'Đang sử dụng'.
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
BEGIN
    -- 1. Kiểm tra sự tồn tại và khóa dòng dữ liệu
    SELECT dp.TrangThai, dp.NgayNhan, dp.MaPhong, p.SoPhong
    INTO v_TrangThai, v_NgayNhan, v_MaPhong, v_SoPhong
    FROM DatPhong dp
    JOIN Phong p ON dp.MaPhong = p.MaPhong
    WHERE dp.MaDatPhong = p_MaDatPhong
    FOR UPDATE;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Không tìm thấy đơn đặt phòng có mã: %', p_MaDatPhong;
    END IF;

    IF v_TrangThai <> 'Đã đặt' THEN
        RAISE EXCEPTION 'Đơn phòng đang ở trạng thái "%", không thể thực hiện nhận phòng!', v_TrangThai;
    END IF;

    IF v_NgayNhan > CURRENT_DATE THEN
        RAISE EXCEPTION 'Chưa đến ngày nhận phòng (Ngày nhận: %). Chỉ nhận phòng từ ngày nhận trở đi!', v_NgayNhan;
    END IF;

    -- 2. Kiểm tra phòng có đang bị khách khác ở không
    IF EXISTS (SELECT 1 FROM DatPhong WHERE MaPhong = v_MaPhong AND TrangThai = 'Đang ở' AND MaDatPhong <> p_MaDatPhong) THEN
        RAISE EXCEPTION 'Phòng % hiện đang có khách lưu trú chưa trả phòng!', v_SoPhong;
    END IF;

    -- 3. Cập nhật trạng thái nhận phòng
    UPDATE DatPhong
    SET TrangThai = 'Đang ở',
        GioNhanThucTe = CURRENT_TIMESTAMP,
        MaNhanVien = COALESCE(p_MaNhanVien, MaNhanVien)
    WHERE MaDatPhong = p_MaDatPhong;

    -- 4. Đồng bộ trạng thái buồng phòng
    UPDATE Phong
    SET TinhTrang = 'Đang sử dụng'
    WHERE MaPhong = v_MaPhong;

    RAISE NOTICE 'Đã thực hiện Check-in thành công cho đơn đặt phòng % (Phòng %)', p_MaDatPhong, v_SoPhong;
END;
$$;


-- ----------------------------------------------------------------------------
-- PROCEDURE 2: Quy trình Check-out, quyết toán hóa đơn và đóng buồng phòng
-- Nghiệp vụ:
--  1. Kiểm tra đơn đặt phòng 'Đang ở' và khóa dòng dữ liệu (FOR UPDATE).
--  2. Tự động sinh mã hóa đơn duy nhất chuẩn HD-YYYYMMDD-XXXX.
--  3. Tính tổng tiền dịch vụ phát sinh từ bảng ChiTietHoaDon.
--  4. Tính số tiền giảm giá từ Voucher (theo % hoặc số tiền cố định).
--  5. Tính tổng thu thực tế: TienPhong + TienDichVu - TienDatCoc - TienGiamGia.
--  6. Ghi nhận hóa đơn vào bảng HoaDon, chuyển trạng thái đơn sang 'Đã trả'.
--  7. Chuyển phòng sang 'Chờ dọn dẹp' để buồng phòng làm việc.
-- ----------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE sp_ThucHienCheckOut(
    p_MaDatPhong   INT,
    p_MaPTTT       VARCHAR(10),
    p_MaVoucher    INT,
    INOUT p_MaHoaDon VARCHAR(20) DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_MaDat VARCHAR(20);
    v_MaPhong INT;
    v_SoPhong VARCHAR(20);
    v_TienPhong NUMERIC(18,2);
    v_TienCoc NUMERIC(18,2);
    v_TienDV NUMERIC(18,2) := 0;
    v_TienGiam NUMERIC(18,2) := 0;
    v_TongCanThu NUMERIC(18,2) := 0;
BEGIN
    -- 1. Lấy thông tin đơn đặt phòng
    SELECT dp.MaDat, dp.MaPhong, p.SoPhong, COALESCE(dp.TongTien, 0), COALESCE(dp.TienCoc, 0)
    INTO v_MaDat, v_MaPhong, v_SoPhong, v_TienPhong, v_TienCoc
    FROM DatPhong dp
    JOIN Phong p ON dp.MaPhong = p.MaPhong
    WHERE dp.MaDatPhong = p_MaDatPhong AND dp.TrangThai = 'Đang ở'
    FOR UPDATE;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Không tìm thấy đơn đặt phòng đang ở có mã: %!', p_MaDatPhong;
    END IF;

    -- 2. Sinh mã hóa đơn nếu chưa truyền vào (HDYYYYMMDDXXXX)
    IF p_MaHoaDon IS NULL OR p_MaHoaDon = '' THEN
        p_MaHoaDon := 'HD' || TO_CHAR(CURRENT_TIMESTAMP, 'YYYYMMDD') || LPAD(p_MaDatPhong::TEXT, 4, '0');
    END IF;

    -- 3. Tính tiền dịch vụ phát sinh (nếu có chi tiết tạm gán)
    SELECT COALESCE(SUM(ThanhTien), 0) INTO v_TienDV
    FROM ChiTietHoaDon
    WHERE MaHoaDon = p_MaHoaDon;

    -- 4. Tính giảm giá voucher (nếu có)
    IF p_MaVoucher IS NOT NULL THEN
        SELECT 
            CASE 
                WHEN LoaiGiam = 'PhanTram' THEN LEAST(v_TienPhong * (GiaTri / 100), v_TienPhong)
                ELSE LEAST(GiaTri, v_TienPhong)
            END
        INTO v_TienGiam
        FROM Voucher
        WHERE MaVoucher = p_MaVoucher AND TrangThai = TRUE;
    END IF;

    -- 5. Tổng tiền thực tế cần thanh toán
    v_TongCanThu := GREATEST(0, (v_TienPhong + v_TienDV - v_TienCoc - v_TienGiam));

    -- 6. Ghi nhận hóa đơn thanh toán
    INSERT INTO HoaDon (
        MaHoaDon, MaDatPhong, NgayLap, NgayThanhToan,
        TienPhong, TienDichVu, TienGiamGia, TienDatCoc, TongTien,
        MaPTTT, MaVoucher, TrangThai
    )
    VALUES (
        p_MaHoaDon, v_MaDat, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP,
        v_TienPhong, v_TienDV, v_TienGiam, v_TienCoc, v_TongCanThu,
        COALESCE(p_MaPTTT, 'TM'), p_MaVoucher, 'Đã thanh toán'
    )
    ON CONFLICT (MaHoaDon) DO UPDATE 
    SET NgayThanhToan = CURRENT_TIMESTAMP,
        TongTien = EXCLUDED.TongTien,
        TrangThai = 'Đã thanh toán';

    -- 7. Cập nhật đơn đặt phòng sang 'Đã trả'
    UPDATE DatPhong
    SET TrangThai = 'Đã trả',
        GioTraThucTe = CURRENT_TIMESTAMP
    WHERE MaDatPhong = p_MaDatPhong;

    -- 8. Cập nhật phòng sang 'Chờ dọn dẹp'
    UPDATE Phong
    SET TinhTrang = 'Chờ dọn dẹp'
    WHERE MaPhong = v_MaPhong;

    RAISE NOTICE 'Đã Check-out và xuất hóa đơn % thành công cho phòng %. Tổng thu: % VNĐ', 
        p_MaHoaDon, v_SoPhong, v_TongCanThu;
END;
$$;


-- ============================================================================
-- PHẦN 3: KỊCH BẢN KIỂM THỬ VÀ DEMO CHO TUẤN MINH BẢO VỆ ĐỒ ÁN
-- ============================================================================

-- 1. Demo Hàm tìm phòng trống:
-- Tìm các phòng trống từ ngày mai trong 3 ngày tới cho 2 khách
SELECT * FROM fn_TimPhongTrong(CURRENT_DATE + 1, CURRENT_DATE + 4, 2, NULL);

-- 2. Demo Hàm tính chi phí dự kiến & tiền cọc 30%:
-- Tính tiền phòng đơn giá 800.000 đ, 3 đêm, 2 người lớn, 1 trẻ em
SELECT * FROM fn_TinhToanChiPhiDatPhong(800000, CURRENT_DATE, CURRENT_DATE + 3, 2, 1, 0.10);

-- 3. Demo Thủ tục Check-in nhận phòng:
-- CALL sp_ThucHienCheckIn(p_MaDatPhong := 1, p_MaNhanVien := 1);

-- 4. Demo Thủ tục Check-out xuất hóa đơn:
-- DO $$
-- DECLARE v_HD VARCHAR(20);
-- BEGIN
--     CALL sp_ThucHienCheckOut(1, 'VNPAY', NULL, v_HD);
--     RAISE NOTICE 'Mã hóa đơn vừa sinh: %', v_HD;
-- END $$;
