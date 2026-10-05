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
            string sql = "SELECT * FROM Voucher";
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
            catch { }
            return list;
        }

        public Voucher GetById(int id)
        {
            return GetByCode(id.ToString());
        }

        public Voucher GetById(int? id)
        {
            if (!id.HasValue) return null;
            return GetById(id.Value);
        }

        public Voucher GetByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            string sql = "SELECT TOP 1 * FROM Voucher WHERE MaVoucher = @code OR Code = @code";
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
            string sql = @"INSERT INTO Voucher (GiaTri, LoaiGiam, DonToiThieu, NgayHetHan, SoLuong, TrangThai)
                           VALUES (@GiaTri, @LoaiGiam, @DonToiThieu, @NgayHetHan, @SoLuong, @TrangThai)";
            return ExecuteCmd(sql, v);
        }

        public bool Update(Voucher v)
        {
            string sql = @"UPDATE Voucher SET GiaTri=@GiaTri, LoaiGiam=@LoaiGiam, DonToiThieu=@DonToiThieu, 
                           NgayHetHan=@NgayHetHan, SoLuong=@SoLuong, TrangThai=@TrangThai 
                           WHERE MaVoucher=@MaVoucher";
            return ExecuteCmd(sql, v);
        }

        public bool Delete(int id)
        {
            return Delete(id.ToString());
        }

        public bool Delete(int? id)
        {
            if (!id.HasValue) return false;
            return Delete(id.Value);
        }

        public bool Delete(string code)
        {
            string sql = "DELETE FROM Voucher WHERE MaVoucher = @code OR Code = @code";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@code", code);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch { return false; }
        }

        private bool ExecuteCmd(string sql, Voucher v)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaVoucher", v.MaVoucher.HasValue ? (object)v.MaVoucher.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@GiaTri", v.GiaTri);
                    cmd.Parameters.AddWithValue("@LoaiGiam", v.LoaiGiam ?? "PhanTram");
                    cmd.Parameters.AddWithValue("@DonToiThieu", v.DonToiThieu);
                    cmd.Parameters.AddWithValue("@NgayHetHan", v.NgayHetHan);
                    cmd.Parameters.AddWithValue("@SoLuong", v.SoLuong);
                    cmd.Parameters.AddWithValue("@TrangThai", v.TrangThai);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch { return false; }
        }

        private Voucher MapVoucher(SqlDataReader reader)
        {
            var v = new Voucher();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string col = reader.GetName(i).ToLower();
                object val = reader.GetValue(i);
                if (val == DBNull.Value) continue;

                if (col == "mavoucher" || col == "id")
                {
                    if (int.TryParse(val.ToString(), out int idVal))
                        v.MaVoucher = idVal;
                }
                else if (col == "code") v.Code = val.ToString();
                else if (col == "giatri" || col == "giatrigiam") v.GiaTri = Convert.ToDecimal(val);
                else if (col == "loaigiam" || col == "loaigiamgia") v.LoaiGiam = val.ToString();
                else if (col == "dontoithieu" || col == "dongiatoithieu") v.DonToiThieu = Convert.ToDecimal(val);
                else if (col == "ngayhethan" || col == "ngayketthuc") v.NgayHetHan = Convert.ToDateTime(val);
                else if (col == "soluong") v.SoLuong = Convert.ToInt32(val);
                else if (col == "trangthai" || col == "conhieuluc") v.TrangThai = Convert.ToBoolean(val);
            }
            return v;
        }
    }
}