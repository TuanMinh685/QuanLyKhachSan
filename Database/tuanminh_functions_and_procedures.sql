-- ============================================================================
-- TRƯỜNG ĐẠI HỌC VĂN HIẾN - KHOA CÔNG NGHỆ THÔNG TIN
-- HỌC PHẦN: CHUYÊN ĐỀ 1 - KHAI THÁC HỆ CƠ SỞ DỮ LIỆU (MÃ HP: INT51309 - 6 TÍN CHỈ)
-- GIẢNG VIÊN HƯỚNG DẪN: ThS. LƯƠNG THÁI HIỀN (Email: hienlt@vhu.edu.vn)
-- ĐỀ TÀI: KHAI THÁC & QUẢN TRỊ CSDL HỆ THỐNG QUẢN LÝ KHÁCH SẠN AN NHIÊN
-- THÀNH VIÊN PHỤ TRÁCH: LÊ TRẦN TUẤN MINH (TRƯỞNG NHÓM - MSSV: 221A010993)
-- NHIỆM VỤ: THỦ TỤC & HÀM NGHIỆP VỤ GIAO DỊCH AN TOÀN (TRANSACTION ACID)
-- HỆ QUẢN TRỊ CSDL: POSTGRESQL 15+ / SUPABASE CLOUD
-- ============================================================================

-- ============================================================================
-- PHẦN 0: ĐẢM BẢO BẢNG PHỤ TRỢ (NHẬT KÝ HỆ THỐNG & PHIẾU ĐẶT CỌC) TỒN TẠI
-- ============================================================================
CREATE TABLE IF NOT EXISTS NhatKyHeThong (
    MaNhatKy        SERIAL PRIMARY KEY,
    LoaiHanhDong    VARCHAR(50)   NOT NULL,
    BangTacDong     VARCHAR(50)   NOT NULL,
    KhoaChinh       VARCHAR(50)   NOT NULL,
    GiaTriCu        TEXT          NULL,
    GiaTriMoi       TEXT          NULL,
    NguoiThucHien   VARCHAR(100)  DEFAULT CURRENT_USER,
    ThoiGian        TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MoTaChiTiet     TEXT          NULL
);

CREATE TABLE IF NOT EXISTS PhieuDatCoc (
    MaPhieu         SERIAL PRIMARY KEY,
    MaDatPhong      INT           NOT NULL REFERENCES DatPhong(MaDatPhong) ON DELETE CASCADE,
    LoaiGiaoDich    VARCHAR(10)   NOT NULL CHECK (LoaiGiaoDich IN ('Thu', 'Hoan')),
    SoTien          NUMERIC(18,2) NOT NULL CHECK (SoTien >= 0),
    PhuongThuc      VARCHAR(10)   NOT NULL DEFAULT 'TM',
    NgayGiaoDich    TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MaNhanVien      INT           NULL REFERENCES NhanVien(MaNhanVien),
    GhiChu          TEXT          NULL
);


-- ============================================================================
-- PHẦN 1: HÀM NGHIỆP VỤ (FUNCTIONS)
-- ============================================================================

-- ----------------------------------------------------------------------------
-- FUNCTION 1: Tìm kiếm phòng trống thông minh theo khoảng ngày & sức chứa
-- Nghiệp vụ:
--  - Đồng bộ từ điển trạng thái phòng với ứng dụng C#: chỉ lọc các phòng có
--    trạng thái vận hành vật lý p.TinhTrang = 'Hoạt động' (không bảo trì).
--  - Kiểm tra không giao thoa (Overlap) với bất kỳ đơn đặt phòng nào đang ở
--    trạng thái 'Đã đặt' hoặc 'Đang ở' trong khoảng [p_NgayNhan, p_NgayTra).
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
--  2. Kiểm tra tính hợp lệ của ngày nhận và trạng thái bảo trì của phòng.
--  3. Yêu cầu bắt buộc khách phải có CCCD/hộ chiếu trước khi nhận phòng.
--  4. Chuyển trạng thái đơn sang 'Đang ở', ghi nhận giờ nhận thực tế.
--  5. Giữ Phong.TinhTrang = 'Hoạt động' (tránh xung đột từ điển gây kẹt phòng).
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
    -- 1. Kiểm tra sự tồn tại và khóa dòng dữ liệu
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

    -- 2. Kiểm tra trạng thái đơn đặt
    IF v_TrangThai <> 'Đã đặt' THEN
        RAISE EXCEPTION 'Đơn phòng đang ở trạng thái "%", không thể thực hiện nhận phòng!', v_TrangThai;
    END IF;

    -- 3. Kiểm tra buồng phòng có đang bảo trì không
    IF v_TinhTrangPhong = 'Bảo trì' THEN
        RAISE EXCEPTION 'Phòng % hiện đang Bảo trì, không thể nhận phòng!', v_SoPhong;
    END IF;

    -- 4. Bắt buộc có giấy tờ tùy thân CCCD/Hộ chiếu
    IF v_CCCD IS NULL OR btrim(v_CCCD) = '' THEN
        RAISE EXCEPTION 'Khách chưa có CCCD/hộ chiếu. Hãy cập nhật hồ sơ khách hàng trước khi nhận phòng.';
    END IF;

    -- 5. Kiểm tra ngày nhận phòng
    IF v_NgayNhan > CURRENT_DATE THEN
        RAISE EXCEPTION 'Chưa đến ngày nhận phòng (Ngày nhận: %). Chỉ được nhận phòng từ ngày nhận trở đi!', v_NgayNhan;
    END IF;

    -- 6. Phòng hiện tại đã có khách chưa trả phòng
    IF EXISTS (
        SELECT 1 FROM DatPhong
        WHERE MaPhong = v_MaPhong AND TrangThai = 'Đang ở' AND MaDatPhong <> p_MaDatPhong
    ) THEN
        RAISE EXCEPTION 'Phòng % hiện đang có khách lưu trú chưa trả phòng!', v_SoPhong;
    END IF;

    -- 7. Cập nhật trạng thái nhận phòng
    UPDATE DatPhong
    SET TrangThai = 'Đang ở',
        GioNhanThucTe = COALESCE(GioNhanThucTe, CURRENT_TIMESTAMP),
        MaNhanVien = COALESCE(p_MaNhanVien, MaNhanVien)
    WHERE MaDatPhong = p_MaDatPhong;

    -- Giữ phòng 'Hoạt động'
    UPDATE Phong
    SET TinhTrang = 'Hoạt động'
    WHERE MaPhong = v_MaPhong AND TinhTrang <> 'Bảo trì';

    RAISE NOTICE 'Đã thực hiện Check-in thành công cho đơn đặt phòng % (Phòng %)', p_MaDatPhong, v_SoPhong;
END;
$$;


-- ----------------------------------------------------------------------------
-- PROCEDURE 2: Quy trình Check-out, quyết toán hóa đơn và hoàn cọc dư (ACID)
-- Nghiệp vụ:
--  1. Khóa dòng đơn đặt phòng ('Đang ở') bằng FOR UPDATE và kiểm tra trùng thanh toán.
--  2. Tự động sinh mã hóa đơn duy nhất chuẩn HDYYYYMMDDXXXX nếu chưa truyền vào.
--  3. Hỗ trợ nhận mảng dịch vụ trực tiếp tại quầy từ WinForms C# (p_DichVuJson)
--     hoặc kế thừa từ các hóa đơn tạm ('Chưa thanh toán').
--  4. Kiểm tra đầy đủ điều kiện Voucher (TrangThai, NgayBatDau, NgayHetHan, SoLuong, DonToiThieu).
--  5. Tính tổng thực thu & ghi nhận phiếu hoàn cọc dư vào PhieuDatCoc (nếu cọc > tổng phí).
--  6. Lưu HoaDon, chuyển DatPhong sang 'Đã trả' và giữ Phong.TinhTrang = 'Hoạt động'.
-- ----------------------------------------------------------------------------
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
    -- 1. Lấy thông tin đơn đặt phòng và khóa dòng dữ liệu
    SELECT dp.MaDat, dp.MaPhong, p.SoPhong, COALESCE(dp.TongTien, 0), COALESCE(dp.TienCoc, 0), COALESCE(p_MaNhanVien, dp.MaNhanVien, 1)
    INTO v_MaDat, v_MaPhong, v_SoPhong, v_TienPhong, v_TienCoc, v_MaNV
    FROM DatPhong dp
    JOIN Phong p ON dp.MaPhong = p.MaPhong
    WHERE dp.MaDatPhong = p_MaDatPhong AND dp.TrangThai IN ('Đang ở', 'Đã trả')
    FOR UPDATE;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Không tìm thấy đơn đặt phòng đang lưu trú có mã: %!', p_MaDatPhong;
    END IF;

    -- Kiểm tra đơn đã có hóa đơn thanh toán trước đó chưa
    IF EXISTS (
        SELECT 1 FROM HoaDon
        WHERE (MaDatPhong = v_MaDat OR MaDatPhong = p_MaDatPhong::TEXT)
          AND TrangThai = 'Đã thanh toán'
          AND (p_MaHoaDon IS NULL OR MaHoaDon <> p_MaHoaDon)
    ) THEN
        RAISE EXCEPTION 'Đơn đặt phòng % đã được thanh toán trước đó!', v_MaDat;
    END IF;

    -- 2. Sinh mã hóa đơn nếu chưa truyền vào (HDYYYYMMDDXXXX)
    IF p_MaHoaDon IS NULL OR btrim(p_MaHoaDon) = '' THEN
        p_MaHoaDon := 'HD' || TO_CHAR(CURRENT_TIMESTAMP, 'YYYYMMDD') || LPAD(p_MaDatPhong::TEXT, 4, '0');
    END IF;

    -- 3. Đảm bảo bản ghi HoaDon đích tồn tại trước khi cập nhật chi tiết
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
        -- Chuyển các chi tiết dịch vụ từ hóa đơn tạm ('Chưa thanh toán') của đơn này sang p_MaHoaDon
        UPDATE ChiTietHoaDon
        SET MaHoaDon = p_MaHoaDon
        WHERE MaHoaDon IN (
            SELECT MaHoaDon FROM HoaDon
            WHERE (MaDatPhong = v_MaDat OR MaDatPhong = p_MaDatPhong::TEXT)
              AND TrangThai = 'Chưa thanh toán'
              AND MaHoaDon <> p_MaHoaDon
        );
    END IF;

    -- Xóa các hóa đơn tạm dư thừa của đơn này
    DELETE FROM HoaDon
    WHERE (MaDatPhong = v_MaDat OR MaDatPhong = p_MaDatPhong::TEXT)
      AND TrangThai = 'Chưa thanh toán'
      AND MaHoaDon <> p_MaHoaDon;

    -- Tính tổng tiền dịch vụ phát sinh
    SELECT COALESCE(SUM(ThanhTien), 0) INTO v_TienDV
    FROM ChiTietHoaDon
    WHERE MaHoaDon = p_MaHoaDon;

    -- 5. Kiểm tra đầy đủ điều kiện và tính giảm giá Voucher (nếu có)
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

    -- 6. Tổng tiền thực tế cần thu và tiền hoàn cọc dư
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

    -- Tránh trừ kép: nếu trigger trg_xulyvoucherhoadon chưa được kích hoạt thì thủ tục tự cập nhật an toàn
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

    -- 8. Ghi sổ hoàn cọc dư vào bảng PhieuDatCoc (đồng bộ với HoaDonDAL.LuuHoaDonVaCheckOut)
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
-- PHẦN 3: KỊCH BẢN KIỂM THỬ VÀ DEMO CHO TUẤN MINH BẢO VỆ ĐỒ ÁN
-- ============================================================================

-- 1. Demo Hàm tìm phòng trống:
SELECT * FROM fn_TimPhongTrong(CURRENT_DATE + 1, CURRENT_DATE + 4, 2, NULL);

-- 2. Demo Hàm tính chi phí dự kiến & tiền cọc 30%:
SELECT * FROM fn_TinhToanChiPhiDatPhong(800000, CURRENT_DATE, CURRENT_DATE + 3, 2, 1, 0.10);
