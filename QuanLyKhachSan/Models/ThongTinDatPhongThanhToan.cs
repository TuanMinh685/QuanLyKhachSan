using System;

namespace Quản_Lý_Khách_Sạn.Models
{
    public class ThongTinDatPhongThanhToan
    {
        public int MaDatPhong { get; set; }
        public string MaDat { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public string SoPhong { get; set; } = string.Empty;
        public DateTime NgayNhanDuKien { get; set; }
        public DateTime NgayTraDuKien { get; set; }
        public decimal GiaPhongMoiDem { get; set; }
        public decimal TongTienDatPhong { get; set; }
        public decimal TienCoc { get; set; }
        public int SoDem => Math.Max(1, (NgayTraDuKien.Date - NgayNhanDuKien.Date).Days);
        public decimal TienPhong => TongTienDatPhong > 0 ? TongTienDatPhong : (SoDem * GiaPhongMoiDem);
        public string MaHienThi => string.IsNullOrWhiteSpace(MaDat) ? MaDatPhong.ToString() : MaDat;

        public override string ToString() => $"{MaHienThi} - Phòng {SoPhong} ({TenKhachHang})";
    }
}
