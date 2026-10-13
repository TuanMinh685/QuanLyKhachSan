using System;

namespace Quản_Lý_Khách_Sạn.Models
{
    /// <summary>
    /// Thực thể Khách hàng – ánh xạ 1-1 với view vw_KhachHangTongQuan
    /// (gồm các cột của bảng KhachHang + 3 cột thống kê tính sẵn).
    /// </summary>
    public class KhachHang
    {
        // ====== Cột của bảng KhachHang ======
        public int MaKhachHang { get; set; }                 // khóa chính (INT, tự tăng) – dùng để JOIN
        public string MaKH { get; set; } = string.Empty;     // mã hiển thị "KH002184" – do SQL tự sinh, chỉ đọc
        public string HoTen { get; set; } = string.Empty;
        public string DienThoai { get; set; } = string.Empty;
        public string GioiTinh { get; set; }                 // "Nam" / "Nữ" / "Khác" / null
        public DateTime? NgaySinh { get; set; }
        public string QuocTich { get; set; } = "Việt Nam";
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SoGiayTo { get; set; }                 // CCCD / CMND / Hộ chiếu
        public string GhiChu { get; set; }
        public bool DongYNhanUuDai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        // ====== Cột thống kê (chỉ đọc – lấy từ view, không ghi ngược xuống DB) ======
        public int SoLanLuuTru { get; set; }
        public DateTime? LanLuuTruGanNhat { get; set; }
        public string NhomKhach { get; set; } = "Mới";       // VIP / Thân thiết / Mới
    }
}
