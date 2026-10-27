using NpgsqlTypes;
using System.Data;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public static class PhieuDatCocDAL
    {
        internal static void GhiPhieu(NpgsqlConnection conn, NpgsqlTransaction tran,
            int maDatPhong, string loai, decimal soTien, string phuongThuc,
            int maNhanVien, string ghiChu = null)
        {
            using var cmd = new NpgsqlCommand(@"
INSERT INTO PhieuDatCoc
    (MaDatPhong, LoaiGiaoDich, SoTien, PhuongThuc, MaNhanVien, GhiChu)
VALUES (@Don, @Loai, @Tien, @PT, @NV, @GhiChu)", conn, tran);
            cmd.Parameters.Add("@Don", NpgsqlDbType.Integer).Value = maDatPhong;
            cmd.Parameters.Add("@Loai", NpgsqlDbType.Varchar, 10).Value = loai;
            var tien = cmd.Parameters.Add("@Tien", NpgsqlDbType.Numeric);
            tien.Precision = 18;
            tien.Scale = 2;
            tien.Value = soTien;
            cmd.Parameters.Add("@PT", NpgsqlDbType.Varchar, 10).Value = phuongThuc;
            cmd.Parameters.Add("@NV", NpgsqlDbType.Integer).Value = maNhanVien;
            cmd.Parameters.Add("@GhiChu", NpgsqlDbType.Varchar, 200).Value = (object)ghiChu ?? DBNull.Value;
            cmd.ExecuteNonQuery();
        }

        public static DataTable LayTheoKhach(int maKH)
        {
            return DatabaseHelper.ExecuteQuery(@"
SELECT d.MaDat AS ""Mã đặt"", p.SoPhong AS ""Phòng"", d.TrangThai AS ""Trạng thái đơn"",
       CASE WHEN f.LoaiGiaoDich = 'Thu' THEN 'Thu cọc' WHEN f.SoTien = 0 THEN 'Giữ toàn bộ cọc' ELSE 'Hoàn cọc' END AS ""Giao dịch"",
       f.SoTien AS ""Số tiền"",
       CASE f.PhuongThuc WHEN 'TM' THEN 'Tiền mặt' WHEN 'CK' THEN 'Chuyển khoản' ELSE 'Thẻ' END AS ""Phương thức"",
       f.NgayGiaoDich AS ""Thời điểm"", nv.HoTen AS ""Nhân viên"", f.GhiChu AS ""Ghi chú""
FROM PhieuDatCoc f
JOIN DatPhong d ON d.MaDatPhong = f.MaDatPhong
JOIN Phong p ON p.MaPhong = d.MaPhong
JOIN NhanVien nv ON nv.MaNhanVien = f.MaNhanVien
WHERE d.MaKH = @KH ORDER BY f.NgayGiaoDich DESC, f.MaPhieu DESC",
                new[] { new NpgsqlParameter("@KH", NpgsqlDbType.Integer) { Value = maKH } });
        }
    }
}
