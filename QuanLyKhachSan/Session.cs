namespace Quản_Lý_Khách_Sạn
{
    public static class Session
    {
        public static string TenDangNhap { get; set; } = "";
        public static string VaiTro { get; set; } = "";
        public static bool LaAdmin => string.Equals(VaiTro, "Admin", System.StringComparison.OrdinalIgnoreCase);
        public static void DangXuat() { TenDangNhap = ""; VaiTro = ""; }
    }
}
