using System.Text.RegularExpressions;

namespace QuanLyKhachSan_Phan1.Helpers
{
    /// <summary>
    /// Các hàm kiểm tra dữ liệu nhập vào. Mỗi hàm trả về thông báo lỗi
    /// (tiếng Việt) nếu dữ liệu sai, hoặc null nếu dữ liệu hợp lệ.
    /// </summary>
    public static class KiemTraHelper
    {
        public static string KiemTraTenDangNhap(string ten)
        {
            if (string.IsNullOrEmpty(ten))
                return "Vui lòng nhập tên đăng nhập.";
            if (ten.Length < 3 || ten.Length > 30)
                return "Tên đăng nhập phải từ 3 đến 30 ký tự.";
            if (!Regex.IsMatch(ten, @"^[A-Za-z0-9._]+$"))
                return "Tên đăng nhập chỉ gồm chữ không dấu, số, dấu chấm hoặc gạch dưới (không có khoảng trắng).";
            return null;
        }

        public static string KiemTraHoTen(string hoTen)
        {
            if (string.IsNullOrEmpty(hoTen))
                return "Vui lòng nhập họ tên.";
            if (hoTen.Length < 2)
                return "Họ tên quá ngắn.";
            return null;
        }

        public static string KiemTraMatKhau(string matKhau)
        {
            if (string.IsNullOrEmpty(matKhau))
                return "Vui lòng nhập mật khẩu.";
            if (matKhau.Length < 6)
                return "Mật khẩu cần ít nhất 6 ký tự.";
            if (matKhau.Trim().Length == 0)
                return "Mật khẩu không được chỉ gồm khoảng trắng.";
            return null;
        }

        /// <summary>Email không bắt buộc: để trống là hợp lệ.</summary>
        public static string KiemTraEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;
            if (email.Length > 100)
                return "Email quá dài (tối đa 100 ký tự).";
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Email không đúng định dạng (ví dụ: ten@gmail.com).";
            return null;
        }

        /// <summary>Số điện thoại không bắt buộc: để trống là hợp lệ.</summary>
        public static string KiemTraSdt(string sdt)
        {
            if (string.IsNullOrEmpty(sdt)) return null;
            if (!Regex.IsMatch(sdt, @"^\d{9,11}$"))
                return "Số điện thoại chỉ gồm 9 đến 11 chữ số.";
            return null;
        }

        /// <summary>Kiểm tra số giấy tờ theo loại: CCCD, CMND, HoChieu.</summary>
        public static string KiemTraSoGiayTo(string loai, string so)
        {
            if (string.IsNullOrEmpty(so))
                return "Vui lòng nhập số giấy tờ.";
            switch (loai)
            {
                case "CCCD":
                    if (!Regex.IsMatch(so, @"^\d{12}$"))
                        return "Số CCCD phải gồm đúng 12 chữ số.";
                    break;
                case "CMND":
                    if (!Regex.IsMatch(so, @"^(\d{9}|\d{12})$"))
                        return "Số CMND phải gồm 9 hoặc 12 chữ số.";
                    break;
                case "HoChieu":
                    if (!Regex.IsMatch(so, @"^[A-Za-z0-9]{6,15}$"))
                        return "Số hộ chiếu gồm 6 đến 15 ký tự chữ hoặc số, không dấu.";
                    break;
            }
            return null;
        }
    }
}
