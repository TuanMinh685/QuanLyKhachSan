using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class BaoCaoDAL
    {
        // 1. KPI Doanh Thu (Tính chính xác từ ChiTietHoaDon)
        public DataTable LayTongQuanDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT 
                    ISNULL(SUM(hd.TongTien), 0) AS TongDoanhThu,
                    ISNULL(SUM(ISNULL(ct.TienDV, 0)), 0) AS DoanhThuDichVu,
                    ISNULL(SUM(hd.TongTien) - SUM(ISNULL(ct.TienDV, 0)), 0) AS DoanhThuPhong,
                    COUNT(DISTINCT hd.MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon hd
                LEFT JOIN (
                    SELECT MaHoaDon, SUM(ThanhTien) AS TienDV 
                    FROM ChiTietHoaDon 
                    GROUP BY MaHoaDon
                ) ct ON hd.MaHoaDon = ct.MaHoaDon
                WHERE CAST(ISNULL(hd.NgayLap, GETDATE()) AS DATE) BETWEEN @TuNgay AND @DenNgay";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        // 2. Danh sách Hóa đơn chi tiết
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
                INNER JOIN ChiTietHoaDon ct ON dv.MaDichVu = ct.MaDichVu
                INNER JOIN HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon
                WHERE CAST(ISNULL(hd.NgayLap, GETDATE()) AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY dv.TenDichVu
                ORDER BY [Doanh Thu] DESC";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        // 4. Báo cáo Công suất sử dụng phòng (%)
        public DataTable LayCongSuatPhong()
        {
            string sql = @"
                SELECT 
                    lp.TenLoai AS [Loại Phòng],
                    COUNT(p.MaPhong) AS [Tổng Số Phòng],
                    SUM(CASE WHEN EXISTS (SELECT 1 FROM DatPhong d
                                          WHERE d.MaPhong = p.MaPhong AND d.TrangThai = N'Đang ở') THEN 1 ELSE 0 END) AS [Đang Ở],
                    CAST(ROUND(CAST(SUM(CASE WHEN EXISTS (SELECT 1 FROM DatPhong d
                                          WHERE d.MaPhong = p.MaPhong AND d.TrangThai = N'Đang ở') THEN 1 ELSE 0 END) AS FLOAT) * 100.0 / NULLIF(COUNT(p.MaPhong), 0), 1) AS NVARCHAR(20)) + ' %' AS [Công Suất]
                FROM Phong p
                INNER JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
                GROUP BY lp.TenLoai";

            return DatabaseHelper.ExecuteQuery(sql);
        }

        // 5. Thống kê Lượt đặt phòng theo khoảng thời gian
        public DataTable LayLuotDatPhong(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT 
                    CONVERT(VARCHAR(10), dp.NgayTao, 103) AS [Ngày Đặt],
                    COUNT(dp.MaDatPhong) AS [Lượt Đặt],
                    SUM(CASE WHEN dp.TrangThai = N'Đã hủy' THEN 1 ELSE 0 END) AS [Lượt Hủy]
                FROM DatPhong dp
                WHERE CAST(dp.NgayTao AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY CONVERT(VARCHAR(10), dp.NgayTao, 103), CAST(dp.NgayTao AS DATE)
                ORDER BY CAST(dp.NgayTao AS DATE) DESC";

            SqlParameter[] pars = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };
            return DatabaseHelper.ExecuteQuery(sql, pars);
        }

        // 6. Top Khách hàng chi tiêu nhiều
        public DataTable LayThongKeKhachHang()
        {
            string sql = @"
                SELECT TOP 5
                    kh.MaKHHienThi AS [Mã KH],
                    kh.HoTen AS [Tên Khách Hàng],
                    kh.SoDienThoai AS [Điện Thoại],
                    COUNT(DISTINCT dp.MaDatPhong) AS [Số Lần Thuê],
                    ISNULL(SUM(hd.TongTien), 0) AS [Tổng Chi Tiêu]
                FROM KhachHang kh
                LEFT JOIN DatPhong dp ON kh.MaKH = dp.MaKH
                LEFT JOIN HoaDon hd ON hd.MaDatPhong IN (CAST(dp.MaDatPhong AS NVARCHAR(30)), dp.MaDat)
                GROUP BY kh.MaKH, kh.MaKHHienThi, kh.HoTen, kh.SoDienThoai
                ORDER BY [Tổng Chi Tiêu] DESC";

            return DatabaseHelper.ExecuteQuery(sql);
        }
    }
}