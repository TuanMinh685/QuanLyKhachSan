namespace QuanLyKhachSan_Phan1.Helpers
{
    /// <summary>
    /// Luu thong tin tai khoan dang dang nhap trong suot phien lam viec cua ung dung.
    /// </summary>
    public static class Session
    {
        public static int Id { get; set; }
        public static string TenDangNhap { get; set; } = "";
        public static string HoTen { get; set; } = "";
        public static string VaiTro { get; set; } = "";

        public static bool LaAdmin => VaiTro == "Admin";

        public static void DangXuat()
        {
            Id = 0;
            TenDangNhap = "";
            HoTen = "";
            VaiTro = "";
        }
    }
}
