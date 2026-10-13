using System;
using System.Globalization;

namespace Quản_Lý_Khách_Sạn.Models
{
    /// <summary>
    /// Một dòng trong tab "Lịch sử lưu trú" của khách hàng
    /// (mỗi dòng = một lượt đặt phòng đã nhận hoặc đã trả).
    /// </summary>
    public class LichSuLuuTru
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");

        public int MaDatPhong { get; set; }
        public string SoPhong { get; set; } = string.Empty;
        public string TenLoaiPhong { get; set; } = string.Empty;
        public DateTime NgayNhan { get; set; }
        public DateTime NgayTra { get; set; }
        public int SoDem { get; set; }
        public string TrangThai { get; set; } = string.Empty;   // "Đã nhận phòng" | "Đã trả phòng"
        public decimal TongTien { get; set; }                   // = GiaPhongDem x SoDem (tiền phòng)

        // ====== Thuộc tính hiển thị: DataGridView đọc trực tiếp qua DataPropertyName ======
        public string PhongHienThi => $"Phòng {SoPhong} • {TenLoaiPhong}";
        public string ThoiGianHienThi => $"{NgayNhan:dd/MM/yyyy} – {NgayTra:dd/MM/yyyy} • {SoDem} đêm";
        public string TrangThaiHienThi => TrangThai == "Đã nhận phòng" ? "Đang lưu trú" : TrangThai;
        public string TongTienHienThi => TongTien.ToString("N0", Vi) + " đ";
    }
}
