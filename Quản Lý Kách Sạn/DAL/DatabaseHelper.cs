using Microsoft.Data.SqlClient;

namespace Quản_Lý_Kách_Sạn.DAL
{
    internal class DatabaseHelper
    {
        private static readonly string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=QuanLyKhachSan;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}