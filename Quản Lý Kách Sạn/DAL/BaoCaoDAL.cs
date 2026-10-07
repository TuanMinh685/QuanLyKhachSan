using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class BaoCaoDAL
    {
        public DataTable LayTongQuanDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT 
                    ISNULL(SUM(TongTien), 0) AS TongDoanhThu,
                    ISNULL(SUM(TienPhong), 0) AS DoanhThuPhong,
                    ISNULL(SUM(TienDichVu), 0) AS DoanhThuDichVu,
                    ISNULL(SUM(TienGiamGia), 0) AS TongTienGiam,
                    COUNT(MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon
                WHERE CAST(NgayThanhToan AS DATE) BETWEEN @TuNgay AND @DenNgay";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        public DataTable LayDanhSachHoaDonTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT 
                    hd.MaHoaDon AS [Mã HĐ],
                    hd.MaDatPhong AS [Mã Đặt Phòng],
                    hd.NgayThanhToan AS [Ngày Thanh Toán],
                    hd.TienPhong AS [Tiền Phòng],
                    hd.TienDichVu AS [Tiền Dịch Vụ],
                    hd.TienGiamGia AS [Giảm Giá],
                    hd.TongTien AS [Thực Thu],
                    ISNULL(pt.TenPTTT, hd.MaPTTT) AS [Hình Thức]
                FROM HoaDon hd
                LEFT JOIN PhuongThucThanhToan pt ON hd.MaPTTT = pt.MaPTTT
                WHERE CAST(hd.NgayThanhToan AS DATE) BETWEEN @TuNgay AND @DenNgay
                ORDER BY hd.NgayThanhToan DESC";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        public DataTable LayTopDichVuSuDungNhieu(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT TOP 5
                    dv.TenDichVu AS [Tên Dịch Vụ],
                    dv.LoaiDichVu AS [Loại],
                    ISNULL(SUM(ct.SoLuong), 0) AS [Số Lượng Dùng],
                    ISNULL(SUM(ct.ThanhTien), 0) AS [Doanh Thu]
                FROM DichVu dv
                LEFT JOIN ChiTietHoaDon ct ON dv.MaDichVu = ct.MaDichVu
                LEFT JOIN HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon
                WHERE hd.NgayThanhToan IS NULL OR (CAST(hd.NgayThanhToan AS DATE) BETWEEN @TuNgay AND @DenNgay)
                GROUP BY dv.TenDichVu, dv.LoaiDichVu
                ORDER BY [Doanh Thu] DESC";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }
    }
}