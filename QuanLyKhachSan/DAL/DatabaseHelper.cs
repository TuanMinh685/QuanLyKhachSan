using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public static class DatabaseHelper
    {
        // CHUỖI KẾT NỐI DUY NHẤT của cả dự án. Máy nào khác (LocalDB, tên instance khác...)
        // thì đặt biến môi trường QLKS_CONNECTION thay vì sửa code rồi commit.
        private static readonly string connectionString =
            Environment.GetEnvironmentVariable("QLKS_CONNECTION")
            ?? @"Server=.;Database=QuanLyKhachSan;User Id=sa;Password=12345;TrustServerCertificate=True;";
        // Thêm lại hàm GetConnection bị thiếu tên ở code cũ
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}