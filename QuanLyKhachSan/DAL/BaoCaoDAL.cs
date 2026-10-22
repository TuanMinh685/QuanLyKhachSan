using System;
using System.Data;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class BaoCaoDAL
    {
        // 1. KPI Doanh Thu (Tính từ dữ liệu thực tế đã lưu trong HoaDon trên Supabase)
        public DataTable LayTongQuanDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT 
                    COALESCE(SUM(hd.TongTien), 0) AS TongDoanhThu,
                    COALESCE(SUM(hd.TienDichVu), 0) AS DoanhThuDichVu,
                    COALESCE(SUM(hd.TienPhong), 0) AS DoanhThuPhong,
                    COUNT(DISTINCT hd.MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon hd
                WHERE CAST(COALESCE(hd.NgayThanhToan, hd.NgayLap) AS DATE) BETWEEN @TuNgay AND @DenNgay";

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
                    hd.MaHoaDon AS ""Mã HĐ"",
                    hd.MaDatPhong AS ""Mã Đặt Phòng"",
                    COALESCE(hd.NgayThanhToan, hd.NgayLap) AS ""Ngày Lập"",
                    hd.TongTien AS ""Thực Thu"",
                    hd.TrangThai AS ""Trạng Thái""
                FROM HoaDon hd
                WHERE CAST(COALESCE(hd.NgayThanhToan, hd.NgayLap) AS DATE) BETWEEN @TuNgay AND @DenNgay
                ORDER BY COALESCE(hd.NgayThanhToan, hd.NgayLap) DESC";

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
                SELECT 
                    dv.TenDichVu AS ""Tên Dịch Vụ"",
                    COALESCE(SUM(ct.SoLuong), 0) AS ""Số Lần"",
                    COALESCE(SUM(ct.ThanhTien), 0) AS ""Doanh Thu""
                FROM DichVu dv
                INNER JOIN ChiTietHoaDon ct ON dv.MaDichVu = ct.MaDichVu
                INNER JOIN HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon
                WHERE CAST(COALESCE(hd.NgayThanhToan, hd.NgayLap) AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY dv.TenDichVu
                ORDER BY ""Doanh Thu"" DESC
                LIMIT 5";

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
                    lp.TenLoai AS ""Loại Phòng"",
                    COUNT(DISTINCT p.MaPhong) AS ""Tổng Số Phòng"",
                    COUNT(DISTINCT dp.MaPhong) AS ""Đang Ở"",
                    ROUND((COUNT(DISTINCT dp.MaPhong) * 100.0 / NULLIF(COUNT(DISTINCT p.MaPhong), 0))::NUMERIC, 1)::TEXT || ' %' AS ""Công Suất""
                FROM Phong p
                INNER JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
                LEFT JOIN DatPhong dp ON p.MaPhong = dp.MaPhong AND dp.TrangThai = 'Đang ở'
                GROUP BY lp.TenLoai";

            return DatabaseHelper.ExecuteQuery(sql);
        }

        // 5. Thống kê Lượt đặt phòng theo khoảng thời gian
        public DataTable LayLuotDatPhong(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT 
                    TO_CHAR(dp.NgayTao, 'DD/MM/YYYY') AS ""Ngày Đặt"",
                    COUNT(dp.MaDatPhong) AS ""Lượt Đặt"",
                    SUM(CASE WHEN dp.TrangThai = 'Đã hủy' THEN 1 ELSE 0 END) AS ""Lượt Hủy""
                FROM DatPhong dp
                WHERE CAST(dp.NgayTao AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY TO_CHAR(dp.NgayTao, 'DD/MM/YYYY'), CAST(dp.NgayTao AS DATE)
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
                SELECT 
                    kh.MaKHHienThi AS ""Mã KH"",
                    kh.HoTen AS ""Tên Khách Hàng"",
                    kh.SoDienThoai AS ""Điện Thoại"",
                    COUNT(DISTINCT dp.MaDatPhong) AS ""Số Lần Thuê"",
                    COALESCE(SUM(hd.TongTien), 0) AS ""Tổng Chi Tiêu""
                FROM KhachHang kh
                LEFT JOIN DatPhong dp ON kh.MaKH = dp.MaKH
                LEFT JOIN HoaDon hd ON hd.MaDatPhong IN (CAST(dp.MaDatPhong AS TEXT), dp.MaDat)
                GROUP BY kh.MaKH, kh.MaKHHienThi, kh.HoTen, kh.SoDienThoai
                ORDER BY ""Tổng Chi Tiêu"" DESC
                LIMIT 5";

            return DatabaseHelper.ExecuteQuery(sql);
        }
    }
}
