using System;
using System.Collections.Generic;
using System.Data;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class ChiTietHoaDonDAL
    {
        public DataTable LayChiTietTheoHoaDon(string maHoaDon)
        {
            string sql = @"
                SELECT 
                    ct.MaCTHD,
                    ct.MaDichVu AS ""Mã DV"",
                    dv.TenDichVu AS ""Tên Dịch Vụ"",
                    ct.SoLuong AS ""SL"",
                    ct.DonGia AS ""Đơn Giá"",
                    ct.ThanhTien AS ""Thành Tiền""
                FROM ChiTietHoaDon ct
                INNER JOIN DichVu dv ON ct.MaDichVu = dv.MaDichVu
                WHERE ct.MaHoaDon = @MaHD
                ORDER BY ct.MaCTHD";

            SqlParameter[] pars = { new SqlParameter("@MaHD", maHoaDon) };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        public DataTable LayDanhSachPhongDangO()
        {
            // Lấy danh sách các phòng đang có khách lưu trú (trạng thái 'Đang ở' hoặc 'Đã đặt' đang nhận phòng)
            string sql = @"
                SELECT 
                    dp.MaDatPhong,
                    dp.MaDat,
                    p.MaPhong,
                    p.SoPhong,
                    lp.TenLoai,
                    kh.MaKH,
                    kh.HoTen,
                    kh.SoDienThoai,
                    COALESCE(dp.TongTien, 0) AS TongTienPhong,
                    COALESCE(dp.TienCoc, 0) AS TienCoc
                FROM DatPhong dp
                JOIN Phong p ON dp.MaPhong = p.MaPhong
                JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
                JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                WHERE dp.TrangThai IN ('Đang ở', 'Đã đặt')
                ORDER BY CASE WHEN dp.TrangThai = 'Đang ở' THEN 0 ELSE 1 END, p.SoPhong";

            return DatabaseHelper.ExecuteQuery(sql);
        }

        public DataTable LayChiTietTheoDatPhong(string maDatPhong)
        {
            string sql = @"
                SELECT 
                    ct.MaCTHD,
                    ct.MaHoaDon,
                    ct.MaDichVu,
                    dv.TenDichVu,
                    COALESCE(dv.DonViTinh, '') AS DonViTinh,
                    ct.SoLuong,
                    ct.DonGia,
                    ct.ThanhTien
                FROM ChiTietHoaDon ct
                JOIN DichVu dv ON ct.MaDichVu = dv.MaDichVu
                JOIN HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon
                WHERE (hd.MaDatPhong = @MaDP OR hd.MaDatPhong = @MaDPRaw)
                  AND hd.TrangThai != 'Đã hủy'
                ORDER BY ct.MaCTHD DESC";

            string raw = maDatPhong.Replace("DP-", "");
            SqlParameter[] pars = {
                new SqlParameter("@MaDP", maDatPhong),
                new SqlParameter("@MaDPRaw", raw)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        public bool GhiNhanDichVuChoPhong(string maDatPhong, int maPhong, string maDichVu, decimal soLuong, decimal donGia, decimal tienPhongMacDinh = 0, decimal tienCoc = 0)
        {
            try
            {
                // 1. Kiểm tra xem đã có hóa đơn mở (Chưa thanh toán) cho mã đặt phòng này chưa
                string sqlCheckHD = @"SELECT MaHoaDon FROM HoaDon WHERE (MaDatPhong = @MaDP OR MaDatPhong = @MaDPRaw) AND TrangThai != 'Đã hủy' LIMIT 1";
                string raw = maDatPhong.Replace("DP-", "");
                var dt = DatabaseHelper.ExecuteQuery(sqlCheckHD, new[] {
                    new SqlParameter("@MaDP", maDatPhong),
                    new SqlParameter("@MaDPRaw", raw)
                });

                string maHD = "";
                if (dt.Rows.Count > 0)
                {
                    maHD = dt.Rows[0]["MaHoaDon"].ToString();
                }
                else
                {
                    // Tạo hóa đơn tạm chờ thanh toán
                    maHD = "HD" + DateTime.Now.ToString("yyMMddHHmmss");
                    string sqlTaoHD = @"
                        INSERT INTO HoaDon (MaHoaDon, MaDatPhong, NgayLap, TienPhong, TienDichVu, TienGiamGia, TienDatCoc, TongTien, MaPTTT, TrangThai)
                        VALUES (@MaHD, @MaDP, CURRENT_TIMESTAMP, @TienPhong, 0, 0, @TienCoc, @TongTien, 'TM', 'Chưa thanh toán')";

                    decimal tongTien = Math.Max(0, tienPhongMacDinh - tienCoc);
                    DatabaseHelper.ExecuteNonQuery(sqlTaoHD, new[] {
                        new SqlParameter("@MaHD", maHD),
                        new SqlParameter("@MaDP", maDatPhong),
                        new SqlParameter("@TienPhong", tienPhongMacDinh),
                        new SqlParameter("@TienCoc", tienCoc),
                        new SqlParameter("@TongTien", tongTien)
                    });
                }

                // 2. Thêm vào ChiTietHoaDon
                decimal thanhTien = soLuong * donGia;
                string sqlThemCT = @"
                    INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, SoLuong, DonGia, ThanhTien)
                    VALUES (@MaHD, @MaDV, @SL, @DonGia, @ThanhTien)";

                DatabaseHelper.ExecuteNonQuery(sqlThemCT, new[] {
                    new SqlParameter("@MaHD", maHD),
                    new SqlParameter("@MaDV", maDichVu),
                    new SqlParameter("@SL", soLuong),
                    new SqlParameter("@DonGia", donGia),
                    new SqlParameter("@ThanhTien", thanhTien)
                });

                // 3. Cập nhật lại tổng tiền dịch vụ trên HoaDon
                string sqlCapNhatHD = @"
                    UPDATE HoaDon 
                    SET TienDichVu = (SELECT COALESCE(SUM(ThanhTien), 0) FROM ChiTietHoaDon WHERE MaHoaDon = @MaHD),
                        TongTien = TienPhong + (SELECT COALESCE(SUM(ThanhTien), 0) FROM ChiTietHoaDon WHERE MaHoaDon = @MaHD) - TienGiamGia - TienDatCoc
                    WHERE MaHoaDon = @MaHD";

                DatabaseHelper.ExecuteNonQuery(sqlCapNhatHD, new[] { new SqlParameter("@MaHD", maHD) });

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi ghi nhận dịch vụ: " + ex.Message, "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        public bool XoaChiTiet(int maCTHD)
        {
            try
            {
                // Lấy MaHoaDon trước khi xóa
                string sqlGetHD = "SELECT MaHoaDon FROM ChiTietHoaDon WHERE MaCTHD = @id";
                object res = DatabaseHelper.ExecuteScalar(sqlGetHD, new[] { new SqlParameter("@id", maCTHD) });
                if (res == null) return false;
                string maHD = res.ToString();

                string sqlXoa = "DELETE FROM ChiTietHoaDon WHERE MaCTHD = @id";
                int affected = DatabaseHelper.ExecuteNonQuery(sqlXoa, new[] { new SqlParameter("@id", maCTHD) });

                if (affected > 0)
                {
                    // Cập nhật lại tổng tiền trên HoaDon
                    string sqlCapNhat = @"
                        UPDATE HoaDon 
                        SET TienDichVu = (SELECT COALESCE(SUM(ThanhTien), 0) FROM ChiTietHoaDon WHERE MaHoaDon = @MaHD),
                            TongTien = TienPhong + (SELECT COALESCE(SUM(ThanhTien), 0) FROM ChiTietHoaDon WHERE MaHoaDon = @MaHD) - TienGiamGia - TienDatCoc
                        WHERE MaHoaDon = @MaHD";

                    DatabaseHelper.ExecuteNonQuery(sqlCapNhat, new[] { new SqlParameter("@MaHD", maHD) });
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool LuuChiTiet(string maHoaDon, string maDichVu, decimal soLuong, decimal donGia)
        {
            decimal thanhTien = soLuong * donGia;

            string sql = @"
                INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, SoLuong, DonGia, ThanhTien)
                VALUES (@MaHD, @MaDV, @SL, @DonGia, @ThanhTien)";

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHD", maHoaDon);
                    cmd.Parameters.AddWithValue("@MaDV", maDichVu);
                    cmd.Parameters.AddWithValue("@SL", soLuong);
                    cmd.Parameters.AddWithValue("@DonGia", donGia);
                    cmd.Parameters.AddWithValue("@ThanhTien", thanhTien);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
