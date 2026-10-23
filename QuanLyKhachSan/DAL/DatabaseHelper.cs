using System;
using System.Data;
using System.Configuration;
using Npgsql;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class DatabaseHelper
    {
        // Chuỗi kết nối trực tiếp đến Supabase PostgreSQL Cloud (Pooler Session mode IPv4)
        private static string connectionString = ConfigurationManager.ConnectionStrings["HotelDB"]?.ConnectionString
            ?? "Host=aws-0-ap-northeast-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.xfudgdoksedzyqinnkup;Password=TuanMinh@2004;SSL Mode=Require;Trust Server Certificate=true;Timeout=30;Command Timeout=60;";

        static DatabaseHelper()
        {
            try
            {
                ExecuteNonQuery(@"
                    ALTER TABLE Phong ADD COLUMN IF NOT EXISTS HinhAnh TEXT;
                    ALTER TABLE Phong ADD COLUMN IF NOT EXISTS TienNghi TEXT;
                    ALTER TABLE Phong ADD COLUMN IF NOT EXISTS GhiChu TEXT;
                    ALTER TABLE ChiTietHoaDon ALTER COLUMN SoLuong TYPE NUMERIC(10,2);
                ");
            }
            catch { }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
