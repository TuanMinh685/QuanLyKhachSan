using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class VoucherDAL
    {
        public List<Voucher> GetAll()
        {
            var list = new List<Voucher>();
            string sql = "SELECT * FROM Voucher ORDER BY MaVoucher DESC";
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
                            list.Add(MapVoucher(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi lấy danh sách Voucher: " + ex.Message);
            }
            return list;
        }

        // Bổ sung hàm GetById(int)
        public Voucher GetById(int id)
        {
            string sql = "SELECT TOP 1 * FROM Voucher WHERE MaVoucher = @Id";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapVoucher(reader);
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        // Hỗ trợ kiểu nullable int?
        public Voucher GetById(int? id)
        {
            if (!id.HasValue) return null;
            return GetById(id.Value);
        }

        public Voucher GetByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            string sql = "SELECT TOP 1 * FROM Voucher WHERE Code = @code";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@code", code);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapVoucher(reader);
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        public bool Insert(Voucher v)
        {
            string sql = @"INSERT INTO Voucher (Code, TenChuongTrinh, GiaTri, LoaiGiam, DonToiThieu, NgayBatDau, NgayHetHan, SoLuong, TrangThai)
                           VALUES (@Code, @Ten, @GiaTri, @LoaiGiam, @DonToiThieu, @NgayBD, @NgayKT, @SoLuong, @TrangThai)";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Code", (string.IsNullOrEmpty(v.Code) || v.Code == "0") ? "VC" + DateTime.Now.ToString("yyMMddHHmmss") : v.Code);
                    cmd.Parameters.AddWithValue("@Ten", string.IsNullOrEmpty(v.TenChuongTrinh) ? "Khuyến mãi" : v.TenChuongTrinh);
                    cmd.Parameters.AddWithValue("@GiaTri", v.GiaTri);
                    cmd.Parameters.AddWithValue("@LoaiGiam", v.LoaiGiam ?? "PhanTram");
                    cmd.Parameters.AddWithValue("@DonToiThieu", v.DonToiThieu);
                    cmd.Parameters.AddWithValue("@NgayBD", v.NgayBatDau);
                    cmd.Parameters.AddWithValue("@NgayKT", v.NgayHetHan);
                    cmd.Parameters.AddWithValue("@SoLuong", v.SoLuong);
                    cmd.Parameters.AddWithValue("@TrangThai", v.TrangThai);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi lưu vào SQL: " + ex.Message, "SQL Error");
                return false;
            }
        }

        public bool Update(Voucher v)
        {
            string sql = @"UPDATE Voucher SET GiaTri=@GiaTri, LoaiGiam=@LoaiGiam, DonToiThieu=@DonToiThieu, 
                           NgayBatDau=@NgayBD, NgayHetHan=@NgayKT, SoLuong=@SoLuong, TrangThai=@TrangThai 
                           WHERE MaVoucher=@MaVoucher";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaVoucher", v.MaVoucher ?? 0);
                    cmd.Parameters.AddWithValue("@GiaTri", v.GiaTri);
                    cmd.Parameters.AddWithValue("@LoaiGiam", v.LoaiGiam ?? "PhanTram");
                    cmd.Parameters.AddWithValue("@DonToiThieu", v.DonToiThieu);
                    cmd.Parameters.AddWithValue("@NgayBD", v.NgayBatDau);
                    cmd.Parameters.AddWithValue("@NgayKT", v.NgayHetHan);
                    cmd.Parameters.AddWithValue("@SoLuong", v.SoLuong);
                    cmd.Parameters.AddWithValue("@TrangThai", v.TrangThai);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi cập nhật SQL: " + ex.Message, "SQL Error");
                return false;
            }
        }

        public bool Delete(int id)
        {
            string sql = "DELETE FROM Voucher WHERE MaVoucher = @Id";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi xóa SQL: " + ex.Message, "SQL Error");
                return false;
            }
        }

        public bool Delete(int? id)
        {
            if (!id.HasValue) return false;
            return Delete(id.Value);
        }

        private Voucher MapVoucher(SqlDataReader reader)
        {
            var v = new Voucher();
            int? maVoucher = null;
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string col = reader.GetName(i).ToLower();
                object val = reader.GetValue(i);
                if (val == DBNull.Value) continue;

                if (col == "mavoucher") { maVoucher = Convert.ToInt32(val); v.MaVoucher = maVoucher; }
                else if (col == "code") v.Code = val.ToString() ?? "";
                else if (col == "tenchuongtrinh") v.TenChuongTrinh = val.ToString() ?? "";
                else if (col == "giatri") v.GiaTri = Convert.ToDecimal(val);
                else if (col == "loaigiam") v.LoaiGiam = val.ToString() ?? "PhanTram";
                else if (col == "dontoithieu") v.DonToiThieu = Convert.ToDecimal(val);
                else if (col == "ngaybatdau") v.NgayBatDau = Convert.ToDateTime(val);
                else if (col == "ngayhethan") v.NgayHetHan = Convert.ToDateTime(val);
                else if (col == "soluong") v.SoLuong = Convert.ToInt32(val);
                else if (col == "trangthai") v.TrangThai = Convert.ToBoolean(val);
            }
            if (maVoucher.HasValue) v.MaVoucher = maVoucher;   // Voucher.Code là số sẽ ghi đè Id -> đặt lại Id đúng
            return v;
        }
    }
}