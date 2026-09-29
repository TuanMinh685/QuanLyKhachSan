namespace Quản_Lý_Khách_Sạn.Models
{
    public class ChiTietHoaDon
    {
        public string MaHoaDon { get; set; } = "";
        public string MaDichVu { get; set; } = "";
        public string NoiDung { get; set; } = "";
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}