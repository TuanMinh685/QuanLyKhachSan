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

        public static NpgsqlConnection GetConnection()
        {
            // Dùng múi giờ khách sạn cho CURRENT_DATE và các cột timestamp hiện có.
            var settings = new NpgsqlConnectionStringBuilder(connectionString) { Timezone = "Asia/Ho_Chi_Minh" };
            var conn = new NpgsqlConnection(settings.ConnectionString);
            // Pooler có thể bỏ qua Timezone/SearchPath trong startup parameters.
            // Đặt lại trên từng phiên được mở, kể cả khi lấy kết nối từ pool.
            conn.StateChange += (_, e) =>
            {
                if (e.OriginalState != ConnectionState.Closed || e.CurrentState != ConnectionState.Open) return;
                using var setup = new NpgsqlCommand("SELECT set_config('TimeZone', 'Asia/Ho_Chi_Minh', false)", conn);
                setup.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(settings.SearchPath))
                {
                    setup.CommandText = "SELECT set_config('search_path', @Path, false)";
                    setup.Parameters.AddWithValue("@Path", settings.SearchPath);
                    setup.ExecuteNonQuery();
                }
            };
            return conn;
        }

        public static int ExecuteNonQuery(string query, NpgsqlParameter[] parameters = null)
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
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

        public static DataTable ExecuteQuery(string query, NpgsqlParameter[] parameters = null)
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static object ExecuteScalar(string query, NpgsqlParameter[] parameters = null)
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
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
