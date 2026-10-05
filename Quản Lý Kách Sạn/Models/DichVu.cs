namespace QuanLyKhachSan.Models
{
    public class DichVu
    {
        public string MaDichVu { get; set; } = string.Empty;
        public string TenDichVu { get; set; } = string.Empty;
        public string LoaiDichVu { get; set; } = string.Empty; // Ăn uống, Giặt ủi, Đưa đón, Khác
        public decimal DonGia { get; set; }
        public string DonViTinh { get; set; } = "Lần";
        public bool TrangThai { get; set; } = true; // Còn phục vụ / Ngưng
    }
}