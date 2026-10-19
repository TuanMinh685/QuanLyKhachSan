using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Quản_Lý_Khách_Sạn.DAL
{ 
    internal class ThongTinDatPhongThanhToan
    {
        public int MaDatPhong { get; set; }
        public string MaDat { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public string SoPhong { get; set; } = string.Empty;
        public DateTime NgayNhanDuKien { get; set; }
        public DateTime NgayTraDuKien { get; set; }
        public decimal GiaPhongMoiDem { get; set; }
        public decimal TongTienDatPhong { get; set; }
        public decimal TienCoc { get; set; }
        public int SoDem => Math.Max(1, (NgayTraDuKien.Date - NgayNhanDuKien.Date).Days);
        public decimal TienPhong => TongTienDatPhong > 0 ? TongTienDatPhong : (SoDem * GiaPhongMoiDem);
        public string MaHienThi => string.IsNullOrWhiteSpace(MaDat) ? MaDatPhong.ToString() : MaDat;

        public override string ToString() => $"{MaHienThi} - Phòng {SoPhong} ({TenKhachHang})";
    }

    internal class ThanhToanDAL
    {
        // Lấy danh sách các lượt đặt phòng CHƯA có hóa đơn (cần thanh toán)
        // Chỉ lấy đơn đã nhận phòng / đã trả phòng mà chưa có hóa đơn.
        // Cột TienCoc đã có sẵn trong Database/QLKS.sql.
        public static List<ThongTinDatPhongThanhToan> GetDanhSachChuaThanhToan()
        {
            var list = new List<ThongTinDatPhongThanhToan>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT dp.MaDatPhong, dp.MaDat, kh.HoTen, p.SoPhong,
                           dp.NgayNhan, dp.NgayTra,
                           dp.GiaMotDem, ISNULL(dp.TongTien, 0) AS TongTien, ISNULL(dp.TienCoc, 0) AS TienCoc
                    FROM DatPhong dp
                    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                    JOIN Phong p ON dp.MaPhong = p.MaPhong
                    LEFT JOIN HoaDon hd ON hd.MaDatPhong IN (CAST(dp.MaDatPhong AS NVARCHAR(30)), dp.MaDat)
                    WHERE hd.MaHoaDon IS NULL
                      AND dp.TrangThai IN (N'Đang ở', N'Đã trả')
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
                                NgayNhanDuKien = reader.GetDateTime(4),
                                NgayTraDuKien = reader.GetDateTime(5),
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