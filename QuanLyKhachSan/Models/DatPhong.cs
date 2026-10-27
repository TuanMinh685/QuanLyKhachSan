using System;

namespace Quản_Lý_Khách_Sạn.Models
{
    public class DatPhong
    {
        public int MaDatPhong { get; set; }
        public string MaDat { get; set; } = string.Empty;
        public int MaKH { get; set; }
        public int MaPhong { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime NgayTra { get; set; }
        public int SoDem { get; set; }
        public int SoNguoiLon { get; set; } = 1;
        public int SoTreEm { get; set; } = 0;
        public decimal GiaMotDem { get; set; }
        public decimal PhuThu { get; set; }
        public decimal ThueVaPhi { get; set; }
        public decimal TongTien { get; set; }
        public decimal TienCoc { get; set; }
        public string TrangThai { get; set; } = "Đã đặt";
        public DateTime? NgayTao { get; set; }
        public int? MaNhanVien { get; set; }
    }
}
