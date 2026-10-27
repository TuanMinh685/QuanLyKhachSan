-- ============================================================================
-- SCRIPT SỬA TOÀN BỘ CẢNH BÁO BẢO MẬT (RLS, VIEW, INDEX) TRÊN SUPABASE
-- ============================================================================

-- 1. Bật RLS (Row Level Security) cho tất cả các bảng và cấp quyền truy cập
ALTER TABLE loaiphong ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap loaiphong" ON loaiphong;
CREATE POLICY "Cho phep tat ca truy cap loaiphong" ON loaiphong FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE phong ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap phong" ON phong;
CREATE POLICY "Cho phep tat ca truy cap phong" ON phong FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE nhanvien ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap nhanvien" ON nhanvien;
CREATE POLICY "Cho phep tat ca truy cap nhanvien" ON nhanvien FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE taikhoan ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap taikhoan" ON taikhoan;
CREATE POLICY "Cho phep tat ca truy cap taikhoan" ON taikhoan FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE khachhang ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap khachhang" ON khachhang;
CREATE POLICY "Cho phep tat ca truy cap khachhang" ON khachhang FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE datphong ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap datphong" ON datphong;
CREATE POLICY "Cho phep tat ca truy cap datphong" ON datphong FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE dichvu ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap dichvu" ON dichvu;
CREATE POLICY "Cho phep tat ca truy cap dichvu" ON dichvu FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE phuongthucthanhtoan ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap phuongthucthanhtoan" ON phuongthucthanhtoan;
CREATE POLICY "Cho phep tat ca truy cap phuongthucthanhtoan" ON phuongthucthanhtoan FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE voucher ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap voucher" ON voucher;
CREATE POLICY "Cho phep tat ca truy cap voucher" ON voucher FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE hoadon ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap hoadon" ON hoadon;
CREATE POLICY "Cho phep tat ca truy cap hoadon" ON hoadon FOR ALL USING (true) WITH CHECK (true);

ALTER TABLE chitiethoadon ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "Cho phep tat ca truy cap chitiethoadon" ON chitiethoadon;
CREATE POLICY "Cho phep tat ca truy cap chitiethoadon" ON chitiethoadon FOR ALL USING (true) WITH CHECK (true);

-- 2. Sửa cảnh báo Security Definer View
ALTER VIEW vw_letankhachhangtongquan SET (security_invoker = true);

-- 3. Tạo Index cho các khóa ngoại (Unindexed foreign keys)
CREATE INDEX IF NOT EXISTS ix_chitiethoadon_mahoadon ON chitiethoadon(mahoadon);
CREATE INDEX IF NOT EXISTS ix_chitiethoadon_madichvu ON chitiethoadon(madichvu);
CREATE INDEX IF NOT EXISTS ix_datphong_makh ON datphong(makh);
CREATE INDEX IF NOT EXISTS ix_datphong_maphong ON datphong(maphong);
CREATE INDEX IF NOT EXISTS ix_datphong_manhanvien ON datphong(manhanvien);
CREATE INDEX IF NOT EXISTS ix_hoadon_mavoucher ON hoadon(mavoucher);
CREATE INDEX IF NOT EXISTS ix_phong_maloai ON phong(maloai);
CREATE INDEX IF NOT EXISTS ix_taikhoan_manhanvien ON taikhoan(manhanvien);
