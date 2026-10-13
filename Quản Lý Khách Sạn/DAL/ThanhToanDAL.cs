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
        // Lưu ý: nếu bảng DatPhong đã có cột TienCoc (do phần Đặt phòng thêm), câu SQL bên dưới
        // sẽ tự lấy đúng. Nếu báo lỗi "Invalid column name 'TienCoc'", nhờ bạn làm Phần II
        // chạy: ALTER TABLE DatPhong ADD TienCoc DECIMAL(18,2) NOT NULL DEFAULT 0;
        public static List<ThongTinDatPhongThanhToan> GetDanhSachChuaThanhToan()
        {
            var list = new List<ThongTinDatPhongThanhToan>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT dp.MaDatPhong, kh.HoTen, p.SoPhong,
                           dp.NgayNhanDuKien, dp.NgayTraDuKien,
                           lp.GiaPhong, ISNULL(dp.TienCoc, 0) AS TienCoc
                    FROM DatPhong dp
                    JOIN KhachHang kh ON dp.MaKhachHang = kh.MaKhachHang
                    JOIN Phong p ON dp.MaPhong = p.MaPhong
                    JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
                    LEFT JOIN HoaDon hd ON hd.MaDatPhong = dp.MaDatPhong
                    WHERE hd.MaHoaDon IS NULL";
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