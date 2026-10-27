using System;
using System.Collections.Generic;
// using Microsoft.Data.SqlClient; // migrated to Supabase Npgsql
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class DichVuDAL
    {
        public List<DichVu> GetAll()
        {
            var list = new List<DichVu>();
            string sql = "SELECT * FROM DichVu ORDER BY MaDichVu ASC";
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
                            var dv = new DichVu();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string col = reader.GetName(i).ToLower();
                                object val = reader.GetValue(i);
                                if (val == DBNull.Value) continue;

                                if (col == "madichvu") dv.MaDichVu = val.ToString() ?? "";
                                else if (col == "tendichvu") dv.TenDichVu = val.ToString() ?? "";
                                else if (col == "loaidichvu") dv.LoaiDichVu = val.ToString() ?? "Ăn uống";
                                else if (col == "dongia") dv.DonGia = Convert.ToDecimal(val);
                                else if (col == "donvitinh") dv.DonViTinh = val.ToString() ?? "Lần";
                                else if (col == "trangthai") dv.TrangThai = Convert.ToBoolean(val);
                            }
                            list.Add(dv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi lấy danh sách Dịch Vụ: " + ex.Message);
            }
            return list;
        }

        public bool Insert(DichVu dv)
        {
            string sql = @"INSERT INTO DichVu (MaDichVu, TenDichVu, LoaiDichVu, DonGia, DonViTinh, TrangThai)
                           VALUES (@MaDV, @TenDV, @LoaiDV, @DonGia, @DVT, @TrangThai)";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDV", dv.MaDichVu);
                    cmd.Parameters.AddWithValue("@TenDV", dv.TenDichVu);
                    cmd.Parameters.AddWithValue("@LoaiDV", dv.LoaiDichVu ?? "Ăn uống");
                    cmd.Parameters.AddWithValue("@DonGia", dv.DonGia);
                    cmd.Parameters.AddWithValue("@DVT", dv.DonViTinh ?? "Lần");
                    cmd.Parameters.AddWithValue("@TrangThai", dv.TrangThai);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi thêm dịch vụ: " + ex.Message, "SQL Error");
                return false;
            }
        }

        // Bổ sung hàm Update
        public bool Update(DichVu dv)
        {
            string sql = @"UPDATE DichVu 
                           SET TenDichVu = @TenDV, LoaiDichVu = @LoaiDV, DonGia = @DonGia, DonViTinh = @DVT, TrangThai = @TrangThai
                           WHERE MaDichVu = @MaDV";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDV", dv.MaDichVu);
                    cmd.Parameters.AddWithValue("@TenDV", dv.TenDichVu);
                    cmd.Parameters.AddWithValue("@LoaiDV", dv.LoaiDichVu ?? "Ăn uống");
                    cmd.Parameters.AddWithValue("@DonGia", dv.DonGia);
                    cmd.Parameters.AddWithValue("@DVT", dv.DonViTinh ?? "Lần");
                    cmd.Parameters.AddWithValue("@TrangThai", dv.TrangThai);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi cập nhật dịch vụ: " + ex.Message, "SQL Error");
                return false;
            }
        }

        public bool Delete(string maDV)
        {
            string sql = "DELETE FROM DichVu WHERE MaDichVu = @MaDV";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDV", maDV);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi xóa dịch vụ: " + ex.Message, "SQL Error");
                return false;
            }
        }
    }
}