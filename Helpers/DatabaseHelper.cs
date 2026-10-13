using Microsoft.Data.SqlClient;

namespace QuanLyKhachSan_Phan1.Helpers
{
    /// <summary>
    /// Chuoi ket noi da cau hinh theo may cua ban:
    /// Server "." (SQL Server mac dinh tren may nay), dang nhap bang tai khoan sa.
    /// </summary>
    public static class DatabaseHelper
    {
        private const string ConnectionString =
            "Server=.;Database=quan_ly_khach_san;User Id=sa;Password=12345;TrustServerCertificate=True;";

        public static SqlConnection LayKetNoi()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
