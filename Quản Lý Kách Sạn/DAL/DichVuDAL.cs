using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn.DAL;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyKhachSan.DAL
{
    public class DichVuDAL
    {
        public List<DichVu> GetAll(string keyword = "", string loai = "")
        {
            var list = new List<DichVu>();
            string sql = "SELECT * FROM DichVu WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(keyword))
                sql += " AND (MaDichVu LIKE @kw OR TenDichVu LIKE @kw)";
            if (!string.IsNullOrWhiteSpace(loai) && loai != "Tất cả")
                sql += " AND LoaiDichVu = @loai";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                if (!string.IsNullOrWhiteSpace(loai) && loai != "Tất cả")
                    cmd.Parameters.AddWithValue("@loai", loai);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DichVu
                        {
                            MaDichVu = reader["MaDichVu"].ToString()!,
                            TenDichVu = reader["TenDichVu"].ToString()!,
                            LoaiDichVu = reader["LoaiDichVu"].ToString()!,
                            DonGia = Convert.ToDecimal(reader["DonGia"]),
                            DonViTinh = reader["DonViTinh"].ToString()!,
                            TrangThai = Convert.ToBoolean(reader["TrangThai"])
                        });
                    }
                }
            }
            return list;
        }

        public bool Insert(DichVu dv)
        {
            string sql = "INSERT INTO DichVu (MaDichVu, TenDichVu, LoaiDichVu, DonGia, DonViTinh, TrangThai) " +
                         "VALUES (@Ma, @Ten, @Loai, @Gia, @DVT, @TrangThai)";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Ma", dv.MaDichVu);
                cmd.Parameters.AddWithValue("@Ten", dv.TenDichVu);
                cmd.Parameters.AddWithValue("@Loai", dv.LoaiDichVu);
                cmd.Parameters.AddWithValue("@Gia", dv.DonGia);
                cmd.Parameters.AddWithValue("@DVT", dv.DonViTinh);
                cmd.Parameters.AddWithValue("@TrangThai", dv.TrangThai);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(DichVu dv)
        {
            string sql = "UPDATE DichVu SET TenDichVu=@Ten, LoaiDichVu=@Loai, DonGia=@Gia, " +
                         "DonViTinh=@DVT, TrangThai=@TrangThai WHERE MaDichVu=@Ma";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Ma", dv.MaDichVu);
                cmd.Parameters.AddWithValue("@Ten", dv.TenDichVu);
                cmd.Parameters.AddWithValue("@Loai", dv.LoaiDichVu);
                cmd.Parameters.AddWithValue("@Gia", dv.DonGia);
                cmd.Parameters.AddWithValue("@DVT", dv.DonViTinh);
                cmd.Parameters.AddWithValue("@TrangThai", dv.TrangThai);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}