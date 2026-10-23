using System.Data;
using Microsoft.Data.SqlClient;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public static class PhieuDatCocDAL
    {
        internal static void GhiPhieu(SqlConnection conn, SqlTransaction tran,
            int maDatPhong, string loai, decimal soTien, string phuongThuc,
            int maNhanVien, string ghiChu = null)
        {
            using var cmd = new SqlCommand(@"
INSERT INTO dbo.PhieuDatCoc
    (MaDatPhong, LoaiGiaoDich, SoTien, PhuongThuc, MaNhanVien, GhiChu)
VALUES (@Don, @Loai, @Tien, @PT, @NV, @GhiChu)", conn, tran);
            cmd.Parameters.Add("@Don", SqlDbType.Int).Value = maDatPhong;
            cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 10).Value = loai;
            var tien = cmd.Parameters.Add("@Tien", SqlDbType.Decimal);
            tien.Precision = 18;
            tien.Scale = 2;
            tien.Value = soTien;
            cmd.Parameters.Add("@PT", SqlDbType.NVarChar, 10).Value = phuongThuc;
            cmd.Parameters.Add("@NV", SqlDbType.Int).Value = maNhanVien;
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 200).Value = (object)ghiChu ?? DBNull.Value;
            cmd.ExecuteNonQuery();
        }

        public static DataTable LayTheoKhach(int maKH)
        {
            return DatabaseHelper.ExecuteQuery(@"
SELECT d.MaDat AS [Mã đặt], p.SoPhong AS [Phòng], d.TrangThai AS [Trạng thái đơn],
       CASE WHEN f.LoaiGiaoDich = N'Thu' THEN N'Thu cọc' WHEN f.SoTien = 0 THEN N'Giữ toàn bộ cọc' ELSE N'Hoàn cọc' END AS [Giao dịch],
       f.SoTien AS [Số tiền],
       CASE f.PhuongThuc WHEN N'TM' THEN N'Tiền mặt' WHEN N'CK' THEN N'Chuyển khoản' ELSE N'Thẻ' END AS [Phương thức],
       f.NgayGiaoDich AS [Thời điểm], nv.HoTen AS [Nhân viên], f.GhiChu AS [Ghi chú]
FROM dbo.PhieuDatCoc f
JOIN dbo.DatPhong d ON d.MaDatPhong = f.MaDatPhong
JOIN dbo.Phong p ON p.MaPhong = d.MaPhong
JOIN dbo.NhanVien nv ON nv.MaNhanVien = f.MaNhanVien
WHERE d.MaKH = @KH ORDER BY f.NgayGiaoDich DESC, f.MaPhieu DESC",
                new[] { new SqlParameter("@KH", SqlDbType.Int) { Value = maKH } });
        }
    }
}
