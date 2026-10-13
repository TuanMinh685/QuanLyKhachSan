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

            DateTime nhan = Convert.ToDateTime(r["NgayNhan"]).Date;
            DateTime tra = Convert.ToDateTime(r["NgayTra"]).Date;

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
        public static void HuyPhong(int maDatPhong, string lyDo)
        {
            lyDo = (lyDo ?? "").Trim();
            if (lyDo.Length == 0)
                throw new NghiepVuException("Vui lòng nhập lý do hủy phòng.");
            if (lyDo.Length > 200)
                throw new NghiepVuException("Lý do hủy quá dài (tối đa 200 ký tự).");

            LayDonDungTrangThai(maDatPhong, DaDat, "hủy phòng");

            if (!LeTanLuuTruDAL.HuyPhong(maDatPhong, lyDo))
                throw new NghiepVuException("Đơn vừa được người khác thay đổi. Vui lòng mở lại danh sách.");
            // Chính sách hoàn / giữ tiền đặt cọc khi hủy sẽ làm ở chức năng 9
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