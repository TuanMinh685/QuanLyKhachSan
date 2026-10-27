using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
// using Microsoft.Data.SqlClient; // migrated to Supabase Npgsql
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.BLL
{
    /// <summary>Kết quả của thao tác Thêm / Cập nhật: giao diện chỉ cần đọc object này để biết phải hiển thị gì.</summary>
    public class KetQuaLuu
    {
        public bool ThanhCong { get; set; }
        public int MaKhachHang { get; set; }
        public string ThongBao { get; set; } = string.Empty;
        /// <summary>Lỗi theo từng trường: key = tên thuộc tính (HoTen, DienThoai, SoGiayTo ...), value = nội dung lỗi.</summary>
        public Dictionary<string, string> LoiTheoTruong { get; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// Tầng nghiệp vụ (BLL) của Khách hàng: chuẩn hóa dữ liệu, kiểm tra quy tắc, rồi mới gọi DAL.
    /// Form KHÔNG gọi thẳng DAL; DAL KHÔNG chứa quy tắc nghiệp vụ.
    /// </summary>
    public class KhachHangBLL
    {
        // ====== Hằng số quy tắc nghiệp vụ (đổi 1 chỗ, áp dụng toàn hệ thống) ======
        public const int SO_DONG_TOI_DA = 50;            // số hồ sơ nạp lên lưới mỗi lần
        public const int TUOI_TOI_THIEU = 18;            // khách đứng tên đặt phòng phải đủ tuổi
        private const string MSG_TRUNG_GIAY_TO = "Số giấy tờ đã tồn tại trong hồ sơ khách hàng khác.";

        private static readonly Regex RxEmail     = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", RegexOptions.Compiled);
        private static readonly Regex RxSdtVN     = new Regex(@"^(0\d{9}|\+84\d{9})$", RegexOptions.Compiled);
        private static readonly Regex RxSdtQuocTe = new Regex(@"^\+?\d{8,15}$", RegexOptions.Compiled);
        private static readonly Regex RxGiayTo    = new Regex(@"^[A-Z0-9]{6,20}$", RegexOptions.Compiled);
        private static readonly Regex RxNhieuTrang = new Regex(@"\s+", RegexOptions.Compiled);

        private readonly KhachHangDAL dal = new KhachHangDAL();

        // ==================================================================
        // TRUY VẤN (chỉ chuyển tiếp, kèm chuẩn hóa tham số)
        // ==================================================================
        public List<KhachHang> TimKiem(string tuKhoa, string nhom)
        {
            return dal.TimKiem(tuKhoa, nhom, SO_DONG_TOI_DA);
        }

        public int DemHoSo() => dal.DemHoSo();

        public KhachHang LayTheoMa(int ma) => dal.LayTheoMa(ma);

        public List<LichSuLuuTru> LayLichSu(int maKhachHang) => dal.LayLichSuLuuTru(maKhachHang);
        public System.Data.DataTable LayGiaoDichCoc(int maKhachHang) => PhieuDatCocDAL.LayTheoKhach(maKhachHang);

        /// <summary>Dùng cho kiểm tra "tức thời" khi người dùng rời ô Số giấy tờ.</summary>
        public bool KiemTraTrungGiayTo(string soGiayTo, int? maHienTai)
        {
            string chuan = ChuanHoaGiayTo(soGiayTo);
            return chuan != null && dal.TonTaiSoGiayTo(chuan, maHienTai);
        }

        // ==================================================================
        // THÊM MỚI
        // ==================================================================
        public KetQuaLuu Them(KhachHang kh)
        {
            var kq = new KetQuaLuu();

            ChuanHoa(kh);                                   // bước 1: làm sạch dữ liệu
            KiemTraHopLe(kh, kq.LoiTheoTruong);             // bước 2: kiểm tra quy tắc (không cần chạm DB)
            KiemTraTrungGiayToTruocKhiLuu(kh, kq);          // bước 3: kiểm tra quy tắc cần DB

            if (kq.LoiTheoTruong.Count > 0)
            {
                kq.ThongBao = "Vui lòng kiểm tra lại các trường được đánh dấu.";
                return kq;
            }

            try
            {
                kq.MaKhachHang = dal.Them(kh);              // bước 4: ghi xuống DB
                kq.ThanhCong = true;
                kq.ThongBao = "Đã thêm khách hàng \"" + kh.HoTen + "\".";
            }
            catch (Exception ex) when (ex is PostgresException pe && pe.SqlState == "23505" || ex.Message.Contains("23505") || ex.Message.Contains("duplicate"))
            {
                // Hai lễ tân cùng lưu 1 giấy tờ trong cùng 1 giây: bước 3 chưa kịp thấy trùng,
                // nhưng unique index của CSDL là "chốt chặn cuối" và ném lỗi trùng.
                kq.LoiTheoTruong["SoGiayTo"] = MSG_TRUNG_GIAY_TO;
                kq.ThongBao = "Vui lòng kiểm tra lại các trường được đánh dấu.";
            }
            catch (Exception ex)
            {
                kq.ThongBao = "Lỗi cơ sở dữ liệu: " + ex.Message;
            }
            return kq;
        }

        // ==================================================================
        // CẬP NHẬT
        // ==================================================================
        public KetQuaLuu CapNhat(KhachHang kh)
        {
            var kq = new KetQuaLuu { MaKhachHang = kh.MaKhachHang };

            if (kh.MaKhachHang <= 0)
            {
                kq.ThongBao = "Chưa chọn khách hàng cần cập nhật.";
                return kq;
            }

            ChuanHoa(kh);
            KiemTraHopLe(kh, kq.LoiTheoTruong);
            KiemTraTrungGiayToTruocKhiLuu(kh, kq);

            if (kq.LoiTheoTruong.Count > 0)
            {
                kq.ThongBao = "Vui lòng kiểm tra lại các trường được đánh dấu.";
                return kq;
            }

            try
            {
                if (dal.CapNhat(kh))
                {
                    kq.ThanhCong = true;
                    kq.ThongBao = "Đã cập nhật hồ sơ \"" + kh.HoTen + "\".";
                }
                else
                {
                    kq.ThongBao = "Không tìm thấy hồ sơ (có thể đã bị người khác xóa). Hãy tải lại danh sách.";
                }
            }
            catch (Exception ex) when (ex is PostgresException pe && pe.SqlState == "23505" || ex.Message.Contains("23505") || ex.Message.Contains("duplicate"))
            {
                kq.LoiTheoTruong["SoGiayTo"] = MSG_TRUNG_GIAY_TO;
                kq.ThongBao = "Vui lòng kiểm tra lại các trường được đánh dấu.";
            }
            catch (Exception ex)
            {
                kq.ThongBao = "Lỗi cơ sở dữ liệu: " + ex.Message;
            }
            return kq;
        }

        // ==================================================================
        // CHUẨN HÓA – "làm sạch" dữ liệu trước khi kiểm tra / lưu
        // ==================================================================
        private static void ChuanHoa(KhachHang kh)
        {
            kh.HoTen = RxNhieuTrang.Replace((kh.HoTen ?? "").Trim(), " ");               // "  Nguyễn   Văn  A " -> "Nguyễn Văn A"
            kh.DienThoai = Regex.Replace(kh.DienThoai ?? "", @"[\s.\-()]", "");           // bỏ khoảng trắng . - ( )
            kh.QuocTich = string.IsNullOrWhiteSpace(kh.QuocTich) ? "Việt Nam" : RxNhieuTrang.Replace(kh.QuocTich.Trim(), " ");
            kh.Email = RongThanhNull(kh.Email)?.ToLowerInvariant();
            kh.DiaChi = RongThanhNull(kh.DiaChi);
            kh.GhiChu = RongThanhNull(kh.GhiChu);
            kh.GioiTinh = RongThanhNull(kh.GioiTinh);
            kh.SoGiayTo = ChuanHoaGiayTo(kh.SoGiayTo);
        }

        /// <summary>Giấy tờ: bỏ khoảng trắng, IN HOA ("b12345678" == "B12345678"); rỗng -> null.</summary>
        private static string ChuanHoaGiayTo(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            return Regex.Replace(s, @"\s+", "").ToUpperInvariant();
        }

        private static string RongThanhNull(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? null : s.Trim();
        }

        private static bool LaVietNam(string quocTich)
        {
            string q = (quocTich ?? "").Trim();
            return q.Equals("Việt Nam", StringComparison.OrdinalIgnoreCase)
                || q.Equals("Viet Nam", StringComparison.OrdinalIgnoreCase)
                || q.Equals("Vietnam", StringComparison.OrdinalIgnoreCase);
        }

        // ==================================================================
        // KIỂM TRA QUY TẮC (không cần truy cập DB)
        // ==================================================================
        private static void KiemTraHopLe(KhachHang kh, Dictionary<string, string> loi)
        {
            // --- Họ tên (bắt buộc) ---
            if (kh.HoTen.Length == 0) loi["HoTen"] = "Vui lòng nhập họ và tên.";
            else if (kh.HoTen.Length < 2) loi["HoTen"] = "Họ và tên quá ngắn.";
            else if (kh.HoTen.Length > 100) loi["HoTen"] = "Họ và tên tối đa 100 ký tự.";

            // --- Điện thoại (bắt buộc; luật khác nhau cho khách Việt Nam / nước ngoài) ---
            if (kh.DienThoai.Length == 0)
                loi["DienThoai"] = "Vui lòng nhập số điện thoại.";
            else if (LaVietNam(kh.QuocTich) && !RxSdtVN.IsMatch(kh.DienThoai))
                loi["DienThoai"] = "SĐT Việt Nam gồm 10 chữ số, bắt đầu bằng 0 (hoặc +84...).";
            else if (!LaVietNam(kh.QuocTich) && !RxSdtQuocTe.IsMatch(kh.DienThoai))
                loi["DienThoai"] = "SĐT không hợp lệ (8–15 chữ số, có thể bắt đầu bằng +).";

            // --- Ngày sinh (tùy chọn; nếu nhập phải hợp lý) ---
            if (kh.NgaySinh.HasValue)
            {
                DateTime ns = kh.NgaySinh.Value.Date;
                DateTime homNay = DateTime.Today;
                if (ns > homNay) loi["NgaySinh"] = "Ngày sinh không được ở tương lai.";
                else if (ns < new DateTime(1900, 1, 1)) loi["NgaySinh"] = "Ngày sinh không hợp lệ.";
                else if (ns.AddYears(TUOI_TOI_THIEU) > homNay) loi["NgaySinh"] = $"Khách đứng tên đặt phòng phải từ {TUOI_TOI_THIEU} tuổi.";
            }

            // --- Email (tùy chọn) ---
            if (kh.Email != null)
            {
                if (kh.Email.Length > 100) loi["Email"] = "Email tối đa 100 ký tự.";
                else if (!RxEmail.IsMatch(kh.Email)) loi["Email"] = "Email không đúng định dạng.";
            }

            // --- Số giấy tờ (tùy chọn ở bước hồ sơ; bắt buộc khi nhận phòng – chức năng 8) ---
            if (kh.SoGiayTo != null && !RxGiayTo.IsMatch(kh.SoGiayTo))
                loi["SoGiayTo"] = "Số giấy tờ chỉ gồm chữ và số, dài 6–20 ký tự.";

            // --- Giới tính / độ dài các trường văn bản ---
            if (kh.GioiTinh != null && kh.GioiTinh != "Nam" && kh.GioiTinh != "Nữ" && kh.GioiTinh != "Khác")
                loi["GioiTinh"] = "Giới tính không hợp lệ.";
            if (kh.QuocTich.Length > 50) loi["QuocTich"] = "Quốc tịch tối đa 50 ký tự.";
            if (kh.DiaChi != null && kh.DiaChi.Length > 255) loi["DiaChi"] = "Địa chỉ tối đa 255 ký tự.";
            if (kh.GhiChu != null && kh.GhiChu.Length > 500) loi["GhiChu"] = "Ghi chú tối đa 500 ký tự.";
        }

        /// <summary>Quy tắc cần hỏi DB: số giấy tờ không được trùng hồ sơ khác.</summary>
        private void KiemTraTrungGiayToTruocKhiLuu(KhachHang kh, KetQuaLuu kq)
        {
            if (kh.SoGiayTo == null || kq.LoiTheoTruong.ContainsKey("SoGiayTo")) return;

            int? maTru = kh.MaKhachHang > 0 ? kh.MaKhachHang : (int?)null;   // thêm mới: không loại trừ ai
            if (dal.TonTaiSoGiayTo(kh.SoGiayTo, maTru))
                kq.LoiTheoTruong["SoGiayTo"] = MSG_TRUNG_GIAY_TO;
        }
    }
}
