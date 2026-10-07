using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class HoaDonDAL
    {
        public List<HoaDon> GetAll()
        {
            var list = new List<HoaDon>();
            string sql = "SELECT * FROM HoaDon ORDER BY NgayThanhToan DESC";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var hd = new HoaDon();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string col = reader.GetName(i).ToLower();
                                object val = reader.GetValue(i);
                                if (val == DBNull.Value) continue;

                                if (col == "mahoadon") hd.MaHoaDon = val.ToString() ?? "";
                                else if (col == "madatphong") hd.MaDatPhong = val.ToString() ?? "";
                                else if (col == "ngaylap") hd.NgayLap = Convert.ToDateTime(val);
                                else if (col == "ngaythanhtoan") hd.NgayThanhToan = Convert.ToDateTime(val);
                                else if (col == "tienphong") hd.TienPhong = Convert.ToDecimal(val);
                                else if (col == "tiendichvu") hd.TienDichVu = Convert.ToDecimal(val);
                                else if (col == "tiengiamgia") hd.TienGiamGia = Convert.ToDecimal(val);
                                else if (col == "tiendatcoc") hd.TienDatCoc = Convert.ToDecimal(val);
                                else if (col == "tongtien") hd.TongTien = Convert.ToDecimal(val);
                                else if (col == "mapttt") hd.MaPTTT = val.ToString() ?? "TM";
                                else if (col == "mavoucher") hd.MaVoucher = Convert.ToInt32(val);
                                else if (col == "trangthai") hd.TrangThai = val.ToString() ?? "Đã thanh toán";
                            }
                            list.Add(hd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi lấy danh sách Hóa Đơn: " + ex.Message);
            }
            return list;
        }

        public bool Insert(HoaDon hd)
        {
            string sql = @"INSERT INTO HoaDon (MaHoaDon, MaDatPhong, NgayLap, NgayThanhToan, TienPhong, TienDichVu, TienGiamGia, TienDatCoc, TongTien, MaPTTT, TrangThai)
                           VALUES (@MaHD, @MaDP, @NgayLap, @NgayTT, @TienPhong, @TienDV, @GiamGia, @DatCoc, @TongTien, @PTTT, @TrangThai)";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHD", hd.MaHoaDon);
                    cmd.Parameters.AddWithValue("@MaDP", hd.MaDatPhong ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                    cmd.Parameters.AddWithValue("@NgayTT", hd.NgayThanhToan);
                    cmd.Parameters.AddWithValue("@TienPhong", hd.TienPhong);
                    cmd.Parameters.AddWithValue("@TienDV", hd.TienDichVu);
                    cmd.Parameters.AddWithValue("@GiamGia", hd.TienGiamGia);
                    cmd.Parameters.AddWithValue("@DatCoc", hd.TienDatCoc);
                    cmd.Parameters.AddWithValue("@TongTien", hd.TongTien);
                    cmd.Parameters.AddWithValue("@PTTT", hd.MaPTTT ?? "TM");
                    cmd.Parameters.AddWithValue("@TrangThai", hd.TrangThai ?? "Đã thanh toán");

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi lưu Hóa Đơn: " + ex.Message, "SQL Error");
                return false;
            }
        }
    }
}