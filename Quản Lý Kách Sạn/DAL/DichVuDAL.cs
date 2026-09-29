using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class DichVuDAL
    {
        // 1. Lấy toàn bộ danh sách dịch vụ
        public List<DichVu> GetAll()
        {
            List<DichVu> list = new List<DichVu>();
            string query = "SELECT MaDichVu, TenDichVu, DonGia, LoaiDichVu FROM DichVu";

            using (DataTable dt = DatabaseHelper.ExecuteQuery(query))
            {
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new DichVu
                        {
                            MaDichVu = row.Table.Columns.Contains("MaDichVu") ? row["MaDichVu"]?.ToString() ?? "" : "",
                            TenDichVu = row.Table.Columns.Contains("TenDichVu") ? row["TenDichVu"]?.ToString() ?? "" : "",
                            DonGia = row.Table.Columns.Contains("DonGia") && row["DonGia"] != DBNull.Value ? Convert.ToDecimal(row["DonGia"]) : 0,
                            LoaiDichVu = row.Table.Columns.Contains("LoaiDichVu") ? row["LoaiDichVu"]?.ToString() ?? "" : ""
                        });
                    }
                }
            }
            return list;
        }

        // 2. Thêm dịch vụ mới
        public bool Insert(DichVu dv)
        {
            string query = "INSERT INTO DichVu(MaDichVu, TenDichVu, DonGia, LoaiDichVu) VALUES(@MaDichVu, @TenDichVu, @DonGia, @LoaiDichVu)";
            SqlParameter[] parameters = {
                new SqlParameter("@MaDichVu", dv.MaDichVu),
                new SqlParameter("@TenDichVu", dv.TenDichVu),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@LoaiDichVu", (object)dv.LoaiDichVu ?? DBNull.Value)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // 3. Cập nhật thông tin dịch vụ
        public bool Update(DichVu dv)
        {
            string query = "UPDATE DichVu SET TenDichVu = @TenDichVu, DonGia = @DonGia, LoaiDichVu = @LoaiDichVu WHERE MaDichVu = @MaDichVu";
            SqlParameter[] parameters = {
                new SqlParameter("@MaDichVu", dv.MaDichVu),
                new SqlParameter("@TenDichVu", dv.TenDichVu),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@LoaiDichVu", (object)dv.LoaiDichVu ?? DBNull.Value)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // 4. Xóa dịch vụ theo mã
        public bool Delete(string maDichVu)
        {
            string query = "DELETE FROM DichVu WHERE MaDichVu = @MaDichVu";
            SqlParameter[] parameters = {
                new SqlParameter("@MaDichVu", maDichVu)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}