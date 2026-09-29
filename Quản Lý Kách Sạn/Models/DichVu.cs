namespace Quản_Lý_Khách_Sạn.Models
{
    public class DichVu
    {
        public string MaDichVu { get; set; } = string.Empty;
        public string TenDichVu { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public string LoaiDichVu { get; set; } = string.Empty;
    }
}