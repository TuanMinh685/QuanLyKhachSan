-- ============================================================================
-- SCRIPT BẢO MẬT MỨC DÒNG (RLS), VIEW SECURITY INVOKER & INDEX KHÓA NGOẠI
-- Đồ án Quản Lý Khách Sạn An Nhiên (PostgreSQL / Supabase)
-- ============================================================================

-- 0. Khởi tạo các Role nghiệp vụ nếu chưa có
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'role_quan_tri_ks') THEN
        CREATE ROLE role_quan_tri_ks WITH LOGIN PASSWORD 'Admin@Hotel2026';
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'role_le_tan_ks') THEN
        CREATE ROLE role_le_tan_ks WITH LOGIN PASSWORD 'LeTan@Hotel2026';
    END IF;
END $$;

-- 1. Bật RLS (Row Level Security) và thiết lập Policy phân quyền theo Role xác thực
--    (Chặn truy cập trái phép từ role ẩn danh 'anon', chỉ cho phép kết nối xác thực của hệ thống)

-- 1.1 Bảng danh mục cấu hình (Admin toàn quyền; Lễ tân & Kết nối ứng dụng được phép đọc/tác nghiệp)
ALTER TABLE loaiphong ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap loaiphong" ON loaiphong;
DROP POLICY IF EXISTS "rls_loaiphong_role_policy" ON loaiphong;
CREATE POLICY "rls_loaiphong_role_policy" ON loaiphong FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks'));

ALTER TABLE phong ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap phong" ON phong;
DROP POLICY IF EXISTS "rls_phong_role_policy" ON phong;
CREATE POLICY "rls_phong_role_policy" ON phong FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'));

ALTER TABLE dichvu ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap dichvu" ON dichvu;
DROP POLICY IF EXISTS "rls_dichvu_role_policy" ON dichvu;
CREATE POLICY "rls_dichvu_role_policy" ON dichvu FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks'));

ALTER TABLE phuongthucthanhtoan ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap phuongthucthanhtoan" ON phuongthucthanhtoan;
DROP POLICY IF EXISTS "rls_phuongthucthanhtoan_role_policy" ON phuongthucthanhtoan;
CREATE POLICY "rls_phuongthucthanhtoan_role_policy" ON phuongthucthanhtoan FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks'));

ALTER TABLE voucher ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap voucher" ON voucher;
DROP POLICY IF EXISTS "rls_voucher_role_policy" ON voucher;
CREATE POLICY "rls_voucher_role_policy" ON voucher FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'));

-- 1.2 Bảng giao dịch lễ tân (KhachHang, DatPhong, HoaDon, ChiTietHoaDon, PhieuDatCoc)
ALTER TABLE khachhang ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap khachhang" ON khachhang;
DROP POLICY IF EXISTS "rls_khachhang_role_policy" ON khachhang;
CREATE POLICY "rls_khachhang_role_policy" ON khachhang FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'));

ALTER TABLE datphong ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap datphong" ON datphong;
DROP POLICY IF EXISTS "rls_datphong_role_policy" ON datphong;
CREATE POLICY "rls_datphong_role_policy" ON datphong FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'));

ALTER TABLE hoadon ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap hoadon" ON hoadon;
DROP POLICY IF EXISTS "rls_hoadon_role_policy" ON hoadon;
CREATE POLICY "rls_hoadon_role_policy" ON hoadon FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'));

ALTER TABLE chitiethoadon ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap chitiethoadon" ON chitiethoadon;
DROP POLICY IF EXISTS "rls_chitiethoadon_role_policy" ON chitiethoadon;
CREATE POLICY "rls_chitiethoadon_role_policy" ON chitiethoadon FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'));

ALTER TABLE phieudatcoc ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rls_phieudatcoc_role_policy" ON phieudatcoc;
CREATE POLICY "rls_phieudatcoc_role_policy" ON phieudatcoc FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'));

-- 1.3 Bảng nhân sự, tài khoản & nhật ký bảo mật (Phân quyền chặt cho Quản trị hệ thống)
ALTER TABLE nhanvien ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap nhanvien" ON nhanvien;
DROP POLICY IF EXISTS "rls_nhanvien_role_policy" ON nhanvien;
CREATE POLICY "rls_nhanvien_role_policy" ON nhanvien FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks'));

ALTER TABLE taikhoan ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap taikhoan" ON taikhoan;
DROP POLICY IF EXISTS "rls_taikhoan_admin_policy" ON taikhoan;
CREATE POLICY "rls_taikhoan_admin_policy" ON taikhoan FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks'));

ALTER TABLE nhatkyhethong ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rls_nhatkyhethong_role_policy" ON nhatkyhethong;
CREATE POLICY "rls_nhatkyhethong_role_policy" ON nhatkyhethong FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'));

ALTER TABLE nhatkybaomat ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rls_nhatkybaomat_role_policy" ON nhatkybaomat;
CREATE POLICY "rls_nhatkybaomat_role_policy" ON nhatkybaomat FOR ALL
    USING (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks'))
    WITH CHECK (current_user IN ('postgres', 'authenticated', 'service_role', 'role_quan_tri_ks', 'role_le_tan_ks'));

-- 2. Thiết lập Security Invoker cho toàn bộ View để thực thi đúng quyền RLS của người gọi
ALTER VIEW IF EXISTS vw_letankhachhangtongquan SET (security_invoker = true);
ALTER VIEW IF EXISTS vw_khaithacdoanhthutheothang SET (security_invoker = true);
ALTER VIEW IF EXISTS vw_khaithaccongsuatloaiphong SET (security_invoker = true);
ALTER VIEW IF EXISTS vw_khaithactopdichvu SET (security_invoker = true);
ALTER VIEW IF EXISTS vw_khaithackhachhangvip SET (security_invoker = true);
ALTER VIEW IF EXISTS vw_khaithacdashboardtonghop SET (security_invoker = true);

-- 3. Tạo Index cho toàn bộ các khóa ngoại (Unindexed foreign keys)
CREATE INDEX IF NOT EXISTS ix_chitiethoadon_mahoadon ON chitiethoadon(mahoadon);
CREATE INDEX IF NOT EXISTS ix_chitiethoadon_madichvu ON chitiethoadon(madichvu);
CREATE INDEX IF NOT EXISTS ix_datphong_makh ON datphong(makh);
CREATE INDEX IF NOT EXISTS ix_datphong_maphong ON datphong(maphong);
CREATE INDEX IF NOT EXISTS ix_datphong_manhanvien ON datphong(manhanvien);
CREATE INDEX IF NOT EXISTS ix_hoadon_madatphong ON hoadon(madatphong);
CREATE INDEX IF NOT EXISTS ix_hoadon_mapttt ON hoadon(mapttt);
CREATE INDEX IF NOT EXISTS ix_hoadon_mavoucher ON hoadon(mavoucher);
CREATE INDEX IF NOT EXISTS ix_phong_maloai ON phong(maloai);
CREATE INDEX IF NOT EXISTS ix_taikhoan_manhanvien ON taikhoan(manhanvien);
CREATE INDEX IF NOT EXISTS ix_phieudatcoc_madatphong ON phieudatcoc(madatphong);
CREATE INDEX IF NOT EXISTS ix_phieudatcoc_manhanvien ON phieudatcoc(manhanvien);
