using System;
using System.Data;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.BLL
{
    /// <summary>Ba thao tác trên một đơn đặt phòng đã có.</summary>
    public enum ThaoTacDatPhong
    {
        NhanPhong,
        TraPhong,
        HuyPhong
    }

    /// <summary>
    /// Nghiệp vụ nhận / trả / hủy phòng: kiểm tra quy tắc rồi mới gọi DAL.
    /// Dùng lại NghiepVuException (khai báo trong LeTanDatPhongBLL.cs).
    /// </summary>
    public static class LeTanLuuTruBLL
    {
        public const string DaDat = "Đã đặt";
        public const string DangO = "Đang ở";

        // Mỗi thao tác chỉ áp dụng cho đơn ở một trạng thái nhất định
        public static string TrangThaiCanCo(ThaoTacDatPhong thaoTac)
        {
            return thaoTac == ThaoTacDatPhong.TraPhong ? DangO : DaDat;
        }

        public static DataTable TimDatPhong(ThaoTacDatPhong thaoTac, string tuKhoa)
        {
            return LeTanLuuTruDAL.TimDatPhong(TrangThaiCanCo(thaoTac), (tuKhoa ?? "").Trim());
        }

        // ---------------- Nhận phòng ----------------
        public static void NhanPhong(int maDatPhong)
        {
            DataRow r = LayDonDungTrangThai(maDatPhong, DaDat, "nhận phòng");
            if (string.IsNullOrWhiteSpace(Convert.ToString(r["CCCD"])))
                throw new NghiepVuException("Khách chưa có CCCD/hộ chiếu. Hãy cập nhật hồ sơ khách hàng trước khi nhận phòng.");
            if (Convert.ToString(r["TinhTrang"]) != "Hoạt động")
                throw new NghiepVuException("Phòng đang ngừng hoạt động/bảo trì, chưa thể nhận phòng.");

            DateTime nhan = ToDateTime(r["NgayNhan"]).Date;
            DateTime tra = ToDateTime(r["NgayTra"]).Date;

            if (nhan > DateTime.Today)
                throw new NghiepVuException(
                    $"Chưa đến ngày nhận phòng ({nhan:dd/MM/yyyy}). Chỉ được nhận phòng từ ngày nhận trở đi.");
            if (tra <= DateTime.Today)
                throw new NghiepVuException(
                    "Đơn này đã quá hạn (ngày trả phòng đã qua hoặc là hôm nay). Vui lòng hủy đơn.");

            int maPhong = Convert.ToInt32(r["MaPhong"]);
            if (LeTanLuuTruDAL.PhongDangCoKhach(maPhong, maDatPhong))
                throw new NghiepVuException(
                    $"Phòng {r["SoPhong"]} đang có khách chưa trả. Hãy trả phòng cho khách đó trước.");

            // UPDATE có điều kiện: nếu đơn vừa bị người khác đổi thì trả về false
            if (!LeTanLuuTruDAL.NhanPhong(maDatPhong))
                throw new NghiepVuException(
                    "Đơn vừa được người khác thay đổi hoặc phòng vừa có khách. Vui lòng mở lại danh sách.");
        }

        // ---------------- Trả phòng ----------------
        public static void TraPhong(int maDatPhong)
        {
            LayDonDungTrangThai(maDatPhong, DangO, "trả phòng");

            if (!LeTanLuuTruDAL.TraPhong(maDatPhong))
                throw new NghiepVuException("Đơn vừa được người khác thay đổi. Vui lòng mở lại danh sách.");
            // Việc lập hóa đơn thanh toán khi trả phòng thuộc luồng Hóa đơn của nhóm
        }

        // ---------------- Hủy phòng ----------------
        public static void KiemTraHoanCoc(decimal tienHoan, decimal tienCoc)
        {
            if (tienHoan < 0 || tienHoan > tienCoc || tienHoan != decimal.Truncate(tienHoan))
                throw new NghiepVuException("Tiền hoàn phải là số nguyên VNĐ, từ 0 đến số cọc đã thu.");
        }

        public static void HuyPhong(int maDatPhong, string lyDo, decimal tienHoan,
            decimal tienCocDaXem, string phuongThuc, int? maNhanVien)
        {
            lyDo = (lyDo ?? "").Trim();
            if (lyDo.Length == 0)
                throw new NghiepVuException("Vui lòng nhập lý do hủy phòng.");
            if (lyDo.Length > 200)
                throw new NghiepVuException("Lý do hủy quá dài (tối đa 200 ký tự).");

            DataRow don = LayDonDungTrangThai(maDatPhong, DaDat, "hủy phòng");
            decimal coc = Convert.ToDecimal(don["TienCoc"]);
            if (coc != tienCocDaXem)
                throw new NghiepVuException("Số cọc của đơn đã thay đổi. Hãy mở lại đơn trước khi hủy.");
            KiemTraHoanCoc(tienHoan, coc);
            LeTanDatPhongBLL.KiemTraPhuongThuc(phuongThuc);
            if (!maNhanVien.HasValue || maNhanVien.Value <= 0)
                throw new NghiepVuException("Vui lòng đăng nhập lại để ghi nhận nhân viên xử lý tiền cọc.");

            if (!LeTanLuuTruDAL.HuyPhong(maDatPhong, lyDo, tienHoan, coc, phuongThuc, maNhanVien.Value))
                throw new NghiepVuException("Đơn vừa được người khác thay đổi. Vui lòng mở lại danh sách.");
        }

        // ---------------- Dùng chung ----------------
        // Lấy đơn và bảo đảm nó đang ở đúng trạng thái cần cho thao tác
        private static DataRow LayDonDungTrangThai(int maDatPhong, string trangThaiCan, string tenViec)
        {
            if (maDatPhong <= 0)
                throw new NghiepVuException("Vui lòng chọn một đơn đặt phòng.");

            DataRow r = LeTanLuuTruDAL.LayDatPhong(maDatPhong);
            if (r == null)
                throw new NghiepVuException("Không tìm thấy đơn đặt phòng.");

            string hienTai = Convert.ToString(r["TrangThai"]);
            if (hienTai != trangThaiCan)
                throw new NghiepVuException(
                    $"Đơn {r["MaDat"]} đang ở trạng thái \"{hienTai}\", không thể {tenViec}.");

            return r;
        }
    }
}
