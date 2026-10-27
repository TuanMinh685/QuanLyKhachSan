using System;
using System.Collections.Generic;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.DAL
{ 
    public class ThanhToanDAL
    {
        // Lấy danh sách các lượt đặt phòng CHƯA có hóa đơn (cần thanh toán) từ Supabase
        public static List<ThongTinDatPhongThanhToan> GetDanhSachChuaThanhToan()
        {
            var list = new List<ThongTinDatPhongThanhToan>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT dp.MaDatPhong, dp.MaDat, kh.HoTen, p.SoPhong,
                           dp.NgayNhan, dp.NgayTra,
                           dp.GiaMotDem, COALESCE(dp.TongTien, 0) AS TongTien, COALESCE(dp.TienCoc, 0) AS TienCoc
                    FROM DatPhong dp
                    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                    JOIN Phong p ON dp.MaPhong = p.MaPhong
                    WHERE dp.TrangThai IN ('Đang ở', 'Đã trả')
                      AND NOT EXISTS (
                          SELECT 1 FROM HoaDon hd 
                          WHERE (hd.MaDatPhong = CAST(dp.MaDatPhong AS TEXT) OR hd.MaDatPhong = dp.MaDat)
                            AND hd.TrangThai = 'Đã thanh toán'
                      )
                    ORDER BY dp.NgayNhan DESC, dp.MaDatPhong DESC";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ThongTinDatPhongThanhToan
                            {
                                MaDatPhong = reader.GetInt32(0),
                                MaDat = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                TenKhachHang = reader.GetString(2),
                                SoPhong = reader.GetString(3),
                                NgayNhanDuKien = reader.GetValue(4) is DateOnly d1 ? d1.ToDateTime(TimeOnly.MinValue) : reader.GetDateTime(4),
                                NgayTraDuKien = reader.GetValue(5) is DateOnly d2 ? d2.ToDateTime(TimeOnly.MinValue) : reader.GetDateTime(5),
                                GiaPhongMoiDem = reader.GetDecimal(6),
                                TongTienDatPhong = reader.GetDecimal(7),
                                TienCoc = reader.GetDecimal(8)
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
