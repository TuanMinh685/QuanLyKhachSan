using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.BLL
{
    public class NghiepVuException : Exception
    {
        public NghiepVuException(string message) : base(message) { }
    }

    /// <summary>Kết quả tính tiền của một đơn đặt phòng.</summary>
    public class ChiPhiDatPhong
    {
        public int SoDem { get; set; }
        public decimal GiaMotDem { get; set; }
        public decimal TienPhong { get; set; }
        public decimal PhuThu { get; set; }
        public decimal ThueVaPhi { get; set; }
        public decimal TongTien { get; set; }
        public decimal CocToiThieu { get; set; }
    }

    /// <summary>
    /// Nghiệp vụ lễ tân: tìm phòng trống, tính tiền, tạo đơn đặt phòng.
    /// Quy tắc tính tiền nằm ở MỘT chỗ duy nhất (class này).
    /// </summary>
    public static class LeTanDatPhongBLL
    {
        public const decimal TyLeThue = 0.10m;           // thuế & phí dịch vụ 10%
        public const decimal TyLeCocToiThieu = 0.30m;    // đặt cọc tối thiểu 30%

        // ---------------- Tính tiền ----------------
        public static int TinhSoDem(DateTime nhan, DateTime tra)
        {
            return (tra.Date - nhan.Date).Days;
        }

        public static ChiPhiDatPhong TinhChiPhi(decimal giaMotDem, DateTime nhan, DateTime tra)
        {
            int soDem = TinhSoDem(nhan, tra);
            if (soDem < 1 || giaMotDem <= 0)
                throw new NghiepVuException("Ngày trả phải sau ngày nhận và giá phòng phải lớn hơn 0.");
            decimal tienPhong = giaMotDem * soDem;
            decimal phuThu = 0;                                          // bản đầu chưa có quy tắc phụ thu
            decimal thue = Math.Round((tienPhong + phuThu) * TyLeThue, 0);
            decimal tong = tienPhong + phuThu + thue;

            return new ChiPhiDatPhong
            {
                SoDem = soDem,
                GiaMotDem = giaMotDem,
                TienPhong = tienPhong,
                PhuThu = phuThu,
                ThueVaPhi = thue,
                TongTien = tong,
                CocToiThieu = Math.Ceiling(tong * TyLeCocToiThieu)
            };
        }

        // ---------------- Kiểm tra dữ liệu ----------------
        public static decimal DocTienCoc(string text)
        {
            string s = (text ?? "").Trim();
            if (!Regex.IsMatch(s, @"^(?:[0-9]+|[0-9]{1,3}(?:\.[0-9]{3})+)$")
                || !decimal.TryParse(s.Replace(".", ""), NumberStyles.None, CultureInfo.InvariantCulture, out decimal tien))
                throw new NghiepVuException("Nhập tiền cọc bằng số nguyên VNĐ, ví dụ 165000 hoặc 165.000.");
            return tien;
        }

        public static void KiemTraTienCoc(decimal tienCoc, ChiPhiDatPhong chiPhi)
        {
            if (tienCoc != decimal.Truncate(tienCoc))
                throw new NghiepVuException("Tiền cọc phải là số nguyên VNĐ.");
            if (tienCoc < chiPhi.CocToiThieu)
                throw new NghiepVuException($"Tiền cọc tối thiểu là {chiPhi.CocToiThieu.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))} đ (30%).");
            if (tienCoc > chiPhi.TongTien)
                throw new NghiepVuException("Tiền cọc không được vượt quá tổng tiền đặt phòng.");
        }

        public static void KiemTraPhuongThuc(string phuongThuc)
        {
            if (phuongThuc != "TM" && phuongThuc != "CK" && phuongThuc != "POS")
                throw new NghiepVuException("Vui lòng chọn phương thức thu/hoàn tiền.");
        }

        private static void KiemTraSoNguoi(int nguoiLon, int treEm)
        {
            if (nguoiLon < 1 || treEm < 0 || (long)nguoiLon + treEm > int.MaxValue)
                throw new NghiepVuException("Phải có ít nhất 1 người lớn; số trẻ em không được âm.");
        }

        private static void KiemTraNgay(DateTime nhan, DateTime tra)
        {
            if (nhan.Date < DateTime.Today)
                throw new NghiepVuException("Ngày nhận phòng không được ở quá khứ.");
            if (tra.Date <= nhan.Date)
                throw new NghiepVuException("Ngày trả phòng phải sau ngày nhận phòng ít nhất 1 ngày.");
        }

        // ---------------- Truy vấn ----------------
        public static DataTable LayLoaiPhong()
        {
            return LeTanDatPhongDAL.LayLoaiPhong();
        }

        /// <param name="maLoai">0 = tất cả loại phòng</param>
        public static DataTable TimPhongTrong(DateTime nhan, DateTime tra, int nguoiLon, int treEm, int maLoai)
        {
            KiemTraNgay(nhan, tra);
            KiemTraSoNguoi(nguoiLon, treEm);

            // Sức chứa tính cả trẻ em (quy ước của đề tài)
            return LeTanDatPhongDAL.TimPhongTrong(nhan, tra, nguoiLon + treEm,
                                                  maLoai == 0 ? (int?)null : maLoai);
        }

        public static DataTable TimKhach(string tuKhoa)
        {
            return LeTanDatPhongDAL.TimKhach((tuKhoa ?? "").Trim());
        }

        // ---------------- Đặt phòng ----------------
        /// <returns>Mã đặt phòng, ví dụ DP-261008-001</returns>
        public static string TaoDatPhong(int maKH, int maPhong, DateTime nhan, DateTime tra,
                                         int nguoiLon, int treEm, decimal tienCoc, string phuongThuc, int? maNhanVien, decimal giaDaXacNhan)
        {
            if (maKH <= 0) throw new NghiepVuException("Vui lòng chọn khách đặt phòng.");
            if (maPhong <= 0) throw new NghiepVuException("Vui lòng chọn phòng.");
            KiemTraNgay(nhan, tra);
            KiemTraSoNguoi(nguoiLon, treEm);
            KiemTraPhuongThuc(phuongThuc);
            if (!maNhanVien.HasValue || maNhanVien.Value <= 0)
                throw new NghiepVuException("Không xác định được nhân viên thu cọc. Vui lòng đăng nhập lại.");

            // Lấy giá từ CSDL (không tin giá do giao diện gửi lên), rồi tính tiền
            decimal? gia = LeTanDatPhongDAL.LayGiaPhong(maPhong);
            if (gia == null)
                throw new NghiepVuException("Phòng không tồn tại hoặc đang bảo trì.");
            if (gia.Value != giaDaXacNhan)
                throw new NghiepVuException("Giá phòng vừa thay đổi. Hãy tìm lại phòng để xác nhận giá và tiền cọc mới.");

            ChiPhiDatPhong cp = TinhChiPhi(gia.Value, nhan, tra);
            KiemTraTienCoc(tienCoc, cp);

            string maDat = LeTanDatPhongDAL.TaoDatPhong(maKH, maPhong, nhan, tra, nguoiLon, treEm,
                                                        cp.GiaMotDem, cp.PhuThu, cp.ThueVaPhi, cp.TongTien,
                                                        tienCoc, phuongThuc, maNhanVien.Value);
            if (maDat == null)
                throw new NghiepVuException(
                    "Phòng này vừa được đặt trong khoảng ngày đã chọn. Vui lòng chọn phòng khác.");

            return maDat;
        }
    }
}
