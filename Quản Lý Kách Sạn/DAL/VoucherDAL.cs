using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class VoucherDAL
    {
        // 1. Lấy danh sách toàn bộ voucher
        public List<Voucher> GetAll()
        {
            List<Voucher> list = new List<Voucher>();
            string query = "SELECT * FROM Voucher";
            using (DataTable dt = DatabaseHelper.ExecuteQuery(query))
            {
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new Voucher
                        {
                            MaVoucher = row.Table.Columns.Contains("MaVoucher") && row["MaVoucher"] != DBNull.Value
                                ? Convert.ToInt32(row["MaVoucher"])
                                : (row.Table.Columns.Contains("Id") && row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0),
                            Code = row.Table.Columns.Contains("Code") ? row["Code"]?.ToString() ?? "" : "",
                            LoaiGiam = row.Table.Columns.Contains("LoaiGiam") ? row["LoaiGiam"]?.ToString() ?? "PhanTram" : "PhanTram",
                            GiaTri = row.Table.Columns.Contains("GiaTri") && row["GiaTri"] != DBNull.Value ? Convert.ToDecimal(row["GiaTri"]) : 0,
                            DonToiThieu = row.Table.Columns.Contains("DonToiThieu") && row["DonToiThieu"] != DBNull.Value ? Convert.ToDecimal(row["DonToiThieu"]) : 0,
                            SoLuong = row.Table.Columns.Contains("SoLuong") && row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : null,
                            NgayBatDau = row.Table.Columns.Contains("NgayBatDau") && row["NgayBatDau"] != DBNull.Value ? Convert.ToDateTime(row["NgayBatDau"]) : DateTime.Now,
                            NgayHetHan = row.Table.Columns.Contains("NgayHetHan") && row["NgayHetHan"] != DBNull.Value ? Convert.ToDateTime(row["NgayHetHan"]) : DateTime.MaxValue,
                            TrangThai = row.Table.Columns.Contains("TrangThai") && row["TrangThai"] != DBNull.Value && Convert.ToBoolean(row["TrangThai"])
                        });
                    }
                }
            }
            return list;
        }

        // 2. Thêm voucher mới
        public bool Insert(Voucher v)
        {
            string query = @"INSERT INTO Voucher(Code, LoaiGiam, GiaTri, DonToiThieu, SoLuong, NgayBatDau, NgayHetHan, TrangThai) 
                             VALUES(@Code, @LoaiGiam, @GiaTri, @DonToiThieu, @SoLuong, @NgayBatDau, @NgayHetHan, @TrangThai)";
            SqlParameter[] p = {
                new SqlParameter("@Code", v.Code),
                new SqlParameter("@LoaiGiam", v.LoaiGiam ?? "PhanTram"),
                new SqlParameter("@GiaTri", v.GiaTri),
                new SqlParameter("@DonToiThieu", v.DonToiThieu),
                new SqlParameter("@SoLuong", (object)v.SoLuong ?? DBNull.Value),
                new SqlParameter("@NgayBatDau", v.NgayBatDau),
                new SqlParameter("@NgayHetHan", v.NgayHetHan),
                new SqlParameter("@TrangThai", v.TrangThai)
            };
            return DatabaseHelper.ExecuteNonQuery(query, p) > 0;
        }

        // 3. Cập nhật voucher
        public bool Update(Voucher v)
        {
            string query = @"UPDATE Voucher 
                             SET Code=@Code, LoaiGiam=@LoaiGiam, GiaTri=@GiaTri, DonToiThieu=@DonToiThieu, 
                                 SoLuong=@SoLuong, NgayBatDau=@NgayBatDau, NgayHetHan=@NgayHetHan, TrangThai=@TrangThai 
                             WHERE MaVoucher=@MaVoucher";
            SqlParameter[] p = {
                new SqlParameter("@MaVoucher", v.MaVoucher),
                new SqlParameter("@Code", v.Code),
                new SqlParameter("@LoaiGiam", v.LoaiGiam ?? "PhanTram"),
                new SqlParameter("@GiaTri", v.GiaTri),
                new SqlParameter("@DonToiThieu", v.DonToiThieu),
                new SqlParameter("@SoLuong", (object)v.SoLuong ?? DBNull.Value),
                new SqlParameter("@NgayBatDau", v.NgayBatDau),
                new SqlParameter("@NgayHetHan", v.NgayHetHan),
                new SqlParameter("@TrangThai", v.TrangThai)
            };
            return DatabaseHelper.ExecuteNonQuery(query, p) > 0;
        }

        // 4. Xóa voucher theo mã số int (MaVoucher)
        public bool Delete(int maVoucher)
        {
            string query = "DELETE FROM Voucher WHERE MaVoucher = @Ma";
            SqlParameter[] p = { new SqlParameter("@Ma", maVoucher) };
            return DatabaseHelper.ExecuteNonQuery(query, p) > 0;
        }

        // 5. Xóa voucher theo mã chuỗi string (Code)
        public bool Delete(string code)
        {
            string query = "DELETE FROM Voucher WHERE Code = @Code";
            SqlParameter[] p = { new SqlParameter("@Code", code) };
            return DatabaseHelper.ExecuteNonQuery(query, p) > 0;
        }
    }
}