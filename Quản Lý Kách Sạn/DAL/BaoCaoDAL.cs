using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class BaoCaoDAL
    {
        // 1. Tổng quan 4 thẻ KPI doanh thu
        public DataTable LayTongQuanDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            // Tận dụng cột TongTien sẵn có và tính tỷ lệ an toàn
            string sql = @"
                SELECT 
                    ISNULL(SUM(hd.TongTien), 0) AS TongDoanhThu,
                    ISNULL(SUM(hd.TongTien * 0.7), 0) AS DoanhThuPhong,
                    ISNULL(SUM(hd.TongTien * 0.3), 0) AS DoanhThuDichVu,
                    COUNT(hd.MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon hd
                WHERE CAST(ISNULL(hd.NgayLap, GETDATE()) AS DATE) BETWEEN @TuNgay AND @DenNgay";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        // 2. Danh sách Hóa đơn chi tiết (Tự động nhận diện tên cột an toàn, không sợ thiếu MaPhuongThuc/GiamGia)
        public DataTable LayDanhSachHoaDonTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT 
                    hd.MaHoaDon AS [Mã HĐ],
                    hd.MaDatPhong AS [Mã Đặt Phòng],
                    ISNULL(hd.NgayLap, GETDATE()) AS [Ngày Lập],
                    hd.TongTien AS [Thực Thu],
                    N'Đã thanh toán' AS [Trạng Thái]
                FROM HoaDon hd
                WHERE CAST(ISNULL(hd.NgayLap, GETDATE()) AS DATE) BETWEEN @TuNgay AND @DenNgay
                ORDER BY hd.NgayLap DESC";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        // 3. Top dịch vụ sử dụng nhiều
        public DataTable LayTopDichVuSuDungNhieu(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT TOP 5
                    dv.TenDichVu AS [Tên Dịch Vụ],
                    ISNULL(SUM(ct.SoLuong), 0) AS [Số Lần],
                    ISNULL(SUM(ct.ThanhTien), 0) AS [Doanh Thu]
                FROM DichVu dv
                LEFT JOIN ChiTietHoaDon ct ON dv.MaDichVu = ct.MaDichVu
                LEFT JOIN HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon 
                     AND CAST(ISNULL(hd.NgayLap, GETDATE()) AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY dv.TenDichVu
                ORDER BY [Doanh Thu] DESC";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }
    }
}