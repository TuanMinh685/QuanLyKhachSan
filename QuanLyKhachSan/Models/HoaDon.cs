using System;

namespace Quản_Lý_Khách_Sạn.Models
{
    public class HoaDon
    {
        public string MaHoaDon { get; set; } = "";
        public string MaDatPhong { get; set; } = "";
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public DateTime NgayThanhToan { get; set; } = DateTime.Now;
        public decimal TienPhong { get; set; }
        public decimal TienDichVu { get; set; }
        public decimal TienGiamGia { get; set; }
        public decimal TienDatCoc { get; set; }
        public decimal TongTien { get; set; }
        public string MaPTTT { get; set; } = "TM";
        public int? MaVoucher { get; set; }
        public string TrangThai { get; set; } = "Đã thanh toán";
    }
}