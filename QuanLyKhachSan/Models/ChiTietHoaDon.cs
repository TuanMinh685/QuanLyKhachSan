namespace Quản_Lý_Khách_Sạn.Models
{
    public class ChiTietHoaDon
    {
        public int MaCTHD { get; set; }
        public string MaHoaDon { get; set; } = string.Empty;
        public string MaDichVu { get; set; } = string.Empty;
        public string TenDichVu { get; set; } = string.Empty;
        public decimal SoLuong { get; set; } = 1;
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
