using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn.DAL;
using System;
using System.Data;

namespace QuanLyKhachSan.DAL
{
    public class BaoCaoDAL
    {
        public DataTable GetDoanhThuTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"SELECT CAST(NgayThanhToan AS DATE) AS Ngay, 
                                  SUM(TienPhong) AS TongTienPhong, 
                                  SUM(TienDichVu) AS TongTienDichVu, 
                                  SUM(TongTien) AS TongDoanhThu
                           FROM HoaDon
                           WHERE NgayThanhToan BETWEEN @TuNgay AND @DenNgay AND TrangThai = N'Đã thanh toán'
                           GROUP BY CAST(NgayThanhToan AS DATE)
                           ORDER BY Ngay ASC";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", denNgay);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}