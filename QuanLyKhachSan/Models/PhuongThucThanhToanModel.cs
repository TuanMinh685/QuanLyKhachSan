namespace Quản_Lý_Khách_Sạn.Models
{
    public class PhuongThucThanhToanModel
    {
        public int MaPhuongThuc { get; set; }
        public string TenPhuongThuc { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;
        public bool KichHoat { get; set; }
        public string TrangThaiHienThi => KichHoat ? "● Đang hoạt động" : "● Tạm khóa";
    }
}
