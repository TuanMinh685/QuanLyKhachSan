using System;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn
{
    public static class SeedDataHelper
    {
        public static void KiemTraVaNapDuLieuMau()
        {
            try
            {
                // 1. Thêm các Loại Phòng cao cấp mới
                string sqlLoai = @"
                    INSERT INTO LoaiPhong (TenLoai, GiaMotDem, SoNguoiToiDa)
                    VALUES 
                        ('Superior City View', 750000, 2),
                        ('Executive Ocean Suite', 2200000, 4),
                        ('Family Connecting Room', 1800000, 5),
                        ('Presidential Penthouse VIP', 4500000, 6),
                        ('Bungalow Garden View', 1250000, 3)
                    ON CONFLICT DO NOTHING;
                ";
                DatabaseHelper.ExecuteNonQuery(sqlLoai);

                // 2. Thêm Buồng Phòng mới đầy đủ Tiện Nghi & Hình Ảnh Unsplash cao cấp
                string sqlPhong = @"
                    INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich, TinhTrang, HinhAnh, TienNghi, GhiChu)
                    SELECT 'P103', 1, MaLoai, '1 giường đôi King', 28, 'Hoạt động', 'https://images.unsplash.com/photo-1590490360182-c33d57733427?auto=format&fit=crop&w=800&q=80', 'Wifi 6 tốc độ cao, Smart TV 55 inch, Minibar, Điều hòa 2 chiều, Ban công thoáng mát', 'Phòng hướng vườn yên tĩnh'
                    FROM LoaiPhong WHERE TenLoai = 'Superior City View' LIMIT 1
                    ON CONFLICT (SoPhong) DO NOTHING;

                    INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich, TinhTrang, HinhAnh, TienNghi, GhiChu)
                    SELECT 'P104', 1, MaLoai, '2 giường đơn Twin', 28, 'Hoạt động', 'https://images.unsplash.com/photo-1566665797739-1674de7a421a?auto=format&fit=crop&w=800&q=80', 'Wifi 6, Smart TV 4K, Tủ lạnh mini, Bình đun siêu tốc', 'Gần sảnh lễ tân'
                    FROM LoaiPhong WHERE TenLoai = 'Superior City View' LIMIT 1
                    ON CONFLICT (SoPhong) DO NOTHING;

                    INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich, TinhTrang, HinhAnh, TienNghi, GhiChu)
                    SELECT 'P203', 2, MaLoai, '1 giường King + 1 giường đơn', 38, 'Hoạt động', 'https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=800&q=80', 'Wifi 6, Bồn tắm nằm Jacuzzi, Smart TV 65 inch, Ban công view biển', 'Tầng cao thoáng mát'
                    FROM LoaiPhong WHERE TenLoai = 'Deluxe' LIMIT 1
                    ON CONFLICT (SoPhong) DO NOTHING;

                    INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich, TinhTrang, HinhAnh, TienNghi, GhiChu)
                    SELECT 'P204', 2, MaLoai, '2 giường Queen cao cấp', 42, 'Hoạt động', 'https://images.unsplash.com/photo-1618773928121-c32242e63f39?auto=format&fit=crop&w=800&q=80', 'Wifi 6, Máy pha cà phê, Bồn tắm massage, Minibar miễn phí', 'View trực diện hồ bơi'
                    FROM LoaiPhong WHERE TenLoai = 'Bungalow Garden View' LIMIT 1
                    ON CONFLICT (SoPhong) DO NOTHING;

                    INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich, TinhTrang, HinhAnh, TienNghi, GhiChu)
                    SELECT 'P302', 3, MaLoai, '2 giường King thông nhau', 65, 'Hoạt động', 'https://images.unsplash.com/photo-1611892440504-42a792e24d32?auto=format&fit=crop&w=800&q=80', '2 Phòng ngủ nối thông, 2 phòng tắm riêng, Smart TV 4K mỗi phòng, Sofa bed', 'Phù hợp gia đình có trẻ nhỏ'
                    FROM LoaiPhong WHERE TenLoai = 'Family Connecting Room' LIMIT 1
                    ON CONFLICT (SoPhong) DO NOTHING;

                    INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich, TinhTrang, HinhAnh, TienNghi, GhiChu)
                    SELECT 'P401', 4, MaLoai, '1 giường Hoàng Gia Super King', 75, 'Hoạt động', 'https://images.unsplash.com/photo-1578683010236-d716f9a3f461?auto=format&fit=crop&w=800&q=80', 'Phòng khách riêng biệt, Bồn tắm dát vàng, View toàn cảnh đại dương, Rượu vang chào đón', 'Phòng VIP tầng cao'
                    FROM LoaiPhong WHERE TenLoai = 'Executive Ocean Suite' LIMIT 1
                    ON CONFLICT (SoPhong) DO NOTHING;

                    INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich, TinhTrang, HinhAnh, TienNghi, GhiChu)
                    SELECT 'P501', 5, MaLoai, 'Giường Hoàng Gia + Phòng tiệc riêng', 120, 'Hoạt động', 'https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=800&q=80', 'Hồ bơi vô cực mini riêng, Bếp ăn sang trọng, Rạp chiếu phim tại gia, Quản gia riêng 24/7', 'Phòng Tổng Thống cao cấp nhất'
                    FROM LoaiPhong WHERE TenLoai = 'Presidential Penthouse VIP' LIMIT 1
                    ON CONFLICT (SoPhong) DO NOTHING;

                    INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich, TinhTrang, HinhAnh, TienNghi, GhiChu)
                    SELECT 'P205', 2, MaLoai, '1 giường King', 32, 'Bảo trì', 'https://images.unsplash.com/photo-1611892440504-42a792e24d32?auto=format&fit=crop&w=800&q=80', 'Đang thay mới sàn gỗ và điều hòa trung tâm', 'Dự kiến hoàn tất tuần tới'
                    FROM LoaiPhong WHERE TenLoai = 'Deluxe' LIMIT 1
                    ON CONFLICT (SoPhong) DO NOTHING;
                ";
                DatabaseHelper.ExecuteNonQuery(sqlPhong);

                // 3. Thêm Nhiều Dịch Vụ phong phú (Ăn uống, Giặt ủi, Đưa đón, Thư giãn)
                string sqlDV = @"
                    INSERT INTO DichVu (MaDichVu, TenDichVu, LoaiDichVu, DonGia, DonViTinh, TrangThai)
                    VALUES
                        ('DV010', 'Bò Wagyu A5 sốt tiêu đen', 'Ăn uống', 650000, 'Phần', TRUE),
                        ('DV011', 'Set trà chiều quý tộc Anh', 'Ăn uống', 280000, 'Set', TRUE),
                        ('DV012', 'Rượu vang đỏ Chile cao cấp', 'Ăn uống', 750000, 'Chai', TRUE),
                        ('DV013', 'Nước ép cam tươi nguyên chất', 'Ăn uống', 45000, 'Ly', TRUE),
                        ('DV014', 'Đĩa hoa quả tươi VIP chào đón', 'Ăn uống', 150000, 'Đĩa', TRUE),
                        ('DV015', 'Lẩu hải sản nấm tươi An Nhiên', 'Ăn uống', 520000, 'Nồi', TRUE),
                        ('DV020', 'Giặt thường quần áo', 'Giặt ủi', 45000, 'Kg', TRUE),
                        ('DV021', 'Giặt hấp áo vest / đầm dạ hội', 'Giặt ủi', 120000, 'Bộ', TRUE),
                        ('DV022', 'Giặt nhanh hỏa tốc 2 giờ', 'Giặt ủi', 85000, 'Kg', TRUE),
                        ('DV023', 'Ủi phẳng quần áo công sở', 'Giặt ủi', 30000, 'Cái', TRUE),
                        ('DV030', 'Xe Limousine 9 chỗ đón sân bay', 'Đưa đón', 550000, 'Lượt', TRUE),
                        ('DV031', 'Đưa đón xe riêng 4 chỗ nội thành', 'Đưa đón', 350000, 'Lượt', TRUE),
                        ('DV032', 'Thuê xe tay ga cao cấp SH 150i', 'Đưa đón', 250000, 'Ngày', TRUE),
                        ('DV033', 'Thuê xe đạp đôi dạo bờ biển', 'Đưa đón', 50000, 'Giờ', TRUE),
                        ('DV040', 'Liệu trình Spa đá muối Himalaya', 'Khác', 450000, 'Suất', TRUE),
                        ('DV041', 'Massage chân thảo dược 60 phút', 'Khác', 220000, 'Suất', TRUE),
                        ('DV042', 'Setup hoa tươi và nến kỷ niệm', 'Khác', 500000, 'Gói', TRUE),
                        ('DV043', 'Kê thêm giường phụ (Extra Bed)', 'Khác', 300000, 'Đêm', TRUE)
                    ON CONFLICT (MaDichVu) DO UPDATE 
                    SET TenDichVu = EXCLUDED.TenDichVu, 
                        LoaiDichVu = EXCLUDED.LoaiDichVu, 
                        DonGia = EXCLUDED.DonGia, 
                        DonViTinh = EXCLUDED.DonViTinh;
                ";
                DatabaseHelper.ExecuteNonQuery(sqlDV);

                // 4. Thêm Nhiều Mã Giảm Giá / Voucher Khuyến Mãi Hấp Dẫn
                string sqlVoucher = @"
                    INSERT INTO Voucher (Code, TenChuongTrinh, LoaiGiam, GiaTri, DonToiThieu, NgayBatDau, NgayHetHan, SoLuong, TrangThai)
                    VALUES
                        ('ANNHIEN10', 'Ưu đãi thành viên mới giảm 10%', 'PhanTram', 10, 800000, '2026-01-01', '2026-12-31', 100, TRUE),
                        ('VIP20', 'Tri ân khách hàng thân thiết VIP', 'PhanTram', 20, 1500000, '2026-01-01', '2026-12-31', 50, TRUE),
                        ('WEEKEND200K', 'Kỳ nghỉ cuối tuần rực rỡ', 'SoTien', 200000, 1000000, '2026-01-01', '2026-12-31', 200, TRUE),
                        ('GIAM500K', 'Đặc quyền phòng Suite & Penthouse', 'SoTien', 500000, 2500000, '2026-01-01', '2026-12-31', 30, TRUE),
                        ('HONEYMOON', 'Tuần trăng mật ngọt ngào', 'PhanTram', 15, 1200000, '2026-01-01', '2026-12-31', 40, TRUE),
                        ('EARLYBIRD', 'Đặt phòng sớm nhận ngay ưu đãi', 'PhanTram', 12, 700000, '2026-01-01', '2026-12-31', 150, TRUE),
                        ('SUMMER300K', 'Hè sôi động cùng An Nhiên Hotel', 'SoTien', 300000, 1800000, '2026-01-01', '2026-12-31', 80, TRUE),
                        ('CORPORATE15', 'Ưu đãi đối tác & công tác đoàn', 'PhanTram', 15, 2000000, '2026-01-01', '2026-12-31', 60, TRUE)
                    ON CONFLICT DO NOTHING;
                ";
                DatabaseHelper.ExecuteNonQuery(sqlVoucher);

                // 5. Thêm đầy đủ các Phương Thức Thanh Toán hiện đại (MoMo, Apple Pay, VietQR, Tiền mặt, Thẻ POS)
                string sqlPTTT = @"
                    DO $$
                    BEGIN
                        IF NOT EXISTS (SELECT 1 FROM PhuongThucThanhToan WHERE TenPhuongThuc ILIKE '%MoMo%') THEN
                            INSERT INTO PhuongThucThanhToan (TenPhuongThuc, MoTa, KichHoat)
                            VALUES ('Ví điện tử MoMo', 'Quét mã QR MoMo hoặc thanh toán qua ứng dụng Ví MoMo', TRUE);
                        END IF;

                        IF NOT EXISTS (SELECT 1 FROM PhuongThucThanhToan WHERE TenPhuongThuc ILIKE '%Apple Pay%') THEN
                            INSERT INTO PhuongThucThanhToan (TenPhuongThuc, MoTa, KichHoat)
                            VALUES ('Apple Pay / Contactless', 'Chạm thanh toán Apple Pay, Google Wallet hoặc thẻ chip qua POS', TRUE);
                        END IF;

                        -- Cập nhật nâng cấp VietQR cũ lên chuẩn VNPAY-QR / EMVCo Quốc tế
                        UPDATE PhuongThucThanhToan 
                        SET TenPhuongThuc = 'VNPAY-QR (Chuẩn EMVCo Quốc Tế)',
                            MoTa = 'Cổng thanh toán Quốc tế & Quốc gia VNPAY-QR chuẩn EMVCo (40+ Ngân hàng, Ví điện tử, Visa, Master)'
                        WHERE TenPhuongThuc ILIKE '%VietQR%';

                        IF NOT EXISTS (SELECT 1 FROM PhuongThucThanhToan WHERE TenPhuongThuc ILIKE '%VNPAY%' OR TenPhuongThuc ILIKE '%EMVCo%') THEN
                            INSERT INTO PhuongThucThanhToan (TenPhuongThuc, MoTa, KichHoat)
                            VALUES ('VNPAY-QR (Chuẩn EMVCo Quốc Tế)', 'Cổng thanh toán Quốc tế & Quốc gia VNPAY-QR chuẩn EMVCo (40+ Ngân hàng, Ví điện tử, Visa, Master)', TRUE);
                        END IF;

                        IF NOT EXISTS (SELECT 1 FROM PhuongThucThanhToan WHERE TenPhuongThuc ILIKE '%Tiền mặt%') THEN
                            INSERT INTO PhuongThucThanhToan (TenPhuongThuc, MoTa, KichHoat)
                            VALUES ('Tiền mặt', 'Thanh toán tiền mặt trực tiếp tại quầy lễ tân', TRUE);
                        END IF;

                        IF NOT EXISTS (SELECT 1 FROM PhuongThucThanhToan WHERE TenPhuongThuc ILIKE '%Thẻ POS%' OR TenPhuongThuc ILIKE '%Visa%') THEN
                            INSERT INTO PhuongThucThanhToan (TenPhuongThuc, MoTa, KichHoat)
                            VALUES ('Thẻ POS (Visa/Master/ATM)', 'Cà thẻ nội địa Napas hoặc thẻ quốc tế Visa, MasterCard qua máy POS', TRUE);
                        END IF;
                    END $$;
                ";
                DatabaseHelper.ExecuteNonQuery(sqlPTTT);
            }
            catch { }
        }
    }
}
