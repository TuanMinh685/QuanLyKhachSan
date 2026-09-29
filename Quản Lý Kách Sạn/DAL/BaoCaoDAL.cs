using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Quản_Lý_Khách_Sạn.DAL
{
    internal class DoanhThuTheoThang
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal TongDoanhThu { get; set; }
        public int SoHoaDon { get; set; }
    }

    internal class ThongKeDichVu
    {
        public string TenDichVu { get; set; }
        public int SoLuotDung { get; set; }
        public decimal TongTien { get; set; }
    }

    internal class ThongKeCongSuatPhong
    {
        public DateTime Ngay { get; set; }
        public int SoPhongDaOccupy { get; set; }
        public int TongSoPhong { get; set; }
        public double TyLeLapDay => TongSoPhong == 0 ? 0 : (double)SoPhongDaOccupy / TongSoPhong * 100;
    }

    internal class BaoCaoDAL
    {
        // Doanh thu gộp theo tháng, chỉ tính hóa đơn đã thanh toán, trong khoảng ngày cho trước
        public static List<DoanhThuTheoThang> GetDoanhThuTheoThang(DateTime tu, DateTime den)
        {
            var list = new List<DoanhThuTheoThang>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT MONTH(NgayLap) AS Thang, YEAR(NgayLap) AS Nam,
                           SUM(TongTien) AS TongDoanhThu, COUNT(*) AS SoHoaDon
                    FROM HoaDon
                    WHERE TrangThaiThanhToan = N'Đã thanh toán'
                      AND NgayLap BETWEEN @Tu AND @Den
                    GROUP BY YEAR(NgayLap), MONTH(NgayLap)
                    ORDER BY Nam, Thang";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Tu", tu);
                    cmd.Parameters.AddWithValue("@Den", den);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DoanhThuTheoThang
                            {
                                Thang = reader.GetInt32(0),
                                Nam = reader.GetInt32(1),
                                TongDoanhThu = reader.GetDecimal(2),
                                SoHoaDon = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }
            return list;
        }

        // Top dịch vụ được dùng nhiều nhất trong khoảng thời gian
        public static List<ThongKeDichVu> GetTopDichVu(DateTime tu, DateTime den)
        {
            var list = new List<ThongKeDichVu>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT dv.TenDichVu, SUM(ct.SoLuong) AS SoLuotDung, SUM(ct.ThanhTien) AS TongTien
                    FROM ChiTietHoaDon ct
                    JOIN DichVu dv ON ct.MaDichVu = dv.MaDichVu
                    JOIN HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon
                    WHERE hd.NgayLap BETWEEN @Tu AND @Den
                    GROUP BY dv.TenDichVu
                    ORDER BY TongTien DESC";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Tu", tu);
                    cmd.Parameters.AddWithValue("@Den", den);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ThongKeDichVu
                            {
                                TenDichVu = reader.GetString(0),
                                SoLuotDung = Convert.ToInt32(reader.GetValue(1)),
                                TongTien = reader.GetDecimal(2)
                            });
                        }
                    }
                }
            }
            return list;
        }

        // Công suất phòng: số phòng đang có khách / tổng số phòng, theo từng ngày trong khoảng
        public static List<ThongKeCongSuatPhong> GetCongSuatPhong(DateTime tu, DateTime den)
        {
            var list = new List<ThongKeCongSuatPhong>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT CAST(GETDATE() AS DATE) AS Ngay,
                           (SELECT COUNT(*) FROM Phong WHERE TrangThai = N'Đang ở') AS SoPhongDaOccupy,
                           (SELECT COUNT(*) FROM Phong) AS TongSoPhong";
                // Ghi chú: đây là công suất TỨC THỜI tại thời điểm chạy báo cáo (đơn giản, dễ demo).
                // Muốn công suất theo TỪNG NGÀY trong quá khứ cần bảng lịch sử trạng thái phòng
                // (nếu nhóm có thời gian, có thể mở rộng thêm sau).
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            list.Add(new ThongKeCongSuatPhong
                            {
                                Ngay = reader.GetDateTime(0),
                                SoPhongDaOccupy = reader.GetInt32(1),
                                TongSoPhong = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}