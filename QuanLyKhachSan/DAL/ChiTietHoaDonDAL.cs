using System;
using System.Collections.Generic;
using System.Data;
// using Microsoft.Data.SqlClient; // migrated to Supabase Npgsql

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class ChiTietHoaDonDAL
    {
        public DataTable LayChiTietTheoHoaDon(string maHoaDon)
        {
            string sql = @"
                SELECT 
                    ct.MaDichVu AS [Mã DV],
                    dv.TenDichVu AS [Tên Dịch Vụ],
                    ct.SoLuong AS [SL],
                    ct.DonGia AS [Đơn Giá],
                    ct.ThanhTien AS [Thành Tiền]
                FROM ChiTietHoaDon ct
                INNER JOIN DichVu dv ON ct.MaDichVu = dv.MaDichVu
                WHERE ct.MaHoaDon = @MaHD";

            SqlParameter[] pars = { new SqlParameter("@MaHD", maHoaDon) };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        public bool LuuChiTiet(string maHoaDon, string maDichVu, int soLuong, decimal donGia)
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