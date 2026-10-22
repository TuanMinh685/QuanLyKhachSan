namespace Quản_Lý_Khách_Sạn.Models
{
    public class DichVu
    {
        public string MaDichVu { get; set; } = string.Empty;
        public string TenDichVu { get; set; } = string.Empty;
        public string LoaiDichVu { get; set; } = "Ăn uống";
        public decimal DonGia { get; set; }
        public string DonViTinh { get; set; } = "Lần";
        public bool TrangThai { get; set; } = true;
        public string TrangThaiHienThi => TrangThai ? "● Đang phục vụ" : "● Tạm ngưng";
    }
}
