using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Quản_Lý_Khách_Sạn.DAL
{ 
    internal class ThongTinDatPhongThanhToan
    {
        public int MaDatPhong { get; set; }
        public string TenKhachHang { get; set; }
        public string SoPhong { get; set; }
        public DateTime NgayNhanDuKien { get; set; }
        public DateTime NgayTraDuKien { get; set; }
        public decimal GiaPhongMoiDem { get; set; }
        public decimal TienCoc { get; set; }
        public int SoDem => Math.Max(1, (NgayTraDuKien.Date - NgayNhanDuKien.Date).Days);
        public decimal TienPhong => SoDem * GiaPhongMoiDem;

        public override string ToString() => $"#{MaDatPhong} - {TenKhachHang} - Phòng {SoPhong}";
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
                    SELECT dp.MaDatPhong, kh.HoTen, p.SoPhong,
                           dp.NgayNhan, dp.NgayTra,
                           dp.GiaMotDem, ISNULL(dp.TienCoc, 0) AS TienCoc
                    FROM DatPhong dp
                    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                    JOIN Phong p ON dp.MaPhong = p.MaPhong
                    LEFT JOIN HoaDon hd ON hd.MaDatPhong IN (CAST(dp.MaDatPhong AS NVARCHAR(30)), dp.MaDat)
                    WHERE hd.MaHoaDon IS NULL
                      AND dp.TrangThai IN (N'Đang ở', N'Đã trả')";
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
                                TenKhachHang = reader.GetString(1),
                                SoPhong = reader.GetString(2),
                                NgayNhanDuKien = reader.GetDateTime(3),
                                NgayTraDuKien = reader.GetDateTime(4),
                                GiaPhongMoiDem = reader.GetDecimal(5),
                                TienCoc = reader.GetDecimal(6)
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}