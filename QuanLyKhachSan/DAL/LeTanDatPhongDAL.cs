using System;
using System.Data;
// using Microsoft.Data.SqlClient; // migrated to Supabase Npgsql    // Nếu DatabaseHelper.cs của nhóm dùng System.Data.SqlClient thì đổi dòng này cho giống

namespace Quản_Lý_Khách_Sạn.DAL
{
    /// <summary>
    /// Toàn bộ câu SQL của chức năng tìm phòng trống + đặt phòng.
    /// Giả định CSDL theo script đã đưa: LoaiPhong, Phong, KhachHang, DatPhong.
    /// Nếu bảng/cột của nhóm khác, CHỈ sửa trong file này.
    /// </summary>
    public static class LeTanDatPhongDAL
    {

        internal static SqlConnection TaoKetNoi()
        {
            return DatabaseHelper.GetConnection();
        }

        // Chạy SELECT, trả về DataTable (Fill tự mở và đóng kết nối)
        internal static DataTable LayBang(string sql, params SqlParameter[] ps)
        {
            using (var conn = TaoKetNoi())
            using (var cmd = new SqlCommand(sql, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable LayLoaiPhong()
        {
            return LayBang("SELECT MaLoai, TenLoai FROM LoaiPhong ORDER BY GiaMotDem");
        }

        // Phòng trống = còn hoạt động + đủ sức chứa + KHÔNG có đơn nào chồng lấn khoảng ngày
        public static DataTable TimPhongTrong(DateTime nhan, DateTime tra, int tongNguoi, int? maLoai)
        {
            const string sql = @"
SELECT p.MaPhong, p.SoPhong, lp.MaLoai, lp.TenLoai, p.MoTaGiuong, p.DienTich, p.Tang,
       lp.GiaMotDem,
       COUNT(*) OVER (PARTITION BY lp.MaLoai) AS SoPhongConLai
FROM Phong p
JOIN LoaiPhong lp ON lp.MaLoai = p.MaLoai
WHERE p.TinhTrang = 'Hoạt động'
  AND lp.SoNguoiToiDa >= @TongNguoi
  AND (@MaLoai IS NULL OR lp.MaLoai = @MaLoai)
  AND NOT EXISTS (
        SELECT 1 FROM DatPhong d
        WHERE d.MaPhong = p.MaPhong
          AND d.TrangThai IN ('Đã đặt', 'Đang ở')
          AND d.NgayNhan < @Tra
          AND d.NgayTra  > @Nhan)
ORDER BY p.SoPhong";

            var pNhan = new SqlParameter("@Nhan", SqlDbType.Date) { Value = nhan.Date };
            var pTra = new SqlParameter("@Tra", SqlDbType.Date) { Value = tra.Date };
            var pNguoi = new SqlParameter("@TongNguoi", SqlDbType.Int) { Value = tongNguoi };
            var pLoai = new SqlParameter("@MaLoai", SqlDbType.Int) { Value = (object)maLoai ?? DBNull.Value };

            return LayBang(sql, pNhan, pTra, pNguoi, pLoai);
        }

        public static DataTable TimKhach(string tuKhoa)
        {
            const string sql = @"
SELECT MaKH, MaKHHienThi, HoTen, SoDienThoai
FROM KhachHang
WHERE HoTen LIKE @k OR SoDienThoai LIKE @k OR MaKHHienThi LIKE @k
ORDER BY HoTen";
            return LayBang(sql, new SqlParameter("@k", "%" + tuKhoa + "%"));
        }

        // Giá/đêm của phòng; null nếu phòng không tồn tại hoặc đang bảo trì
        public static decimal? LayGiaPhong(int maPhong)
        {
            const string sql = @"
SELECT lp.GiaMotDem
FROM Phong p JOIN LoaiPhong lp ON lp.MaLoai = p.MaLoai
WHERE p.MaPhong = @MaPhong AND p.TinhTrang = 'Hoạt động'";

            using (var conn = TaoKetNoi())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = maPhong;
                conn.Open();
                object kq = cmd.ExecuteScalar();
                if (kq == null || kq == DBNull.Value) return null;
                return Convert.ToDecimal(kq);
            }
        }

        /// <summary>
        /// Tạo đơn đặt phòng trong một transaction.
        /// Trả về mã đặt phòng (DP-yyMMdd-STT), hoặc null nếu phòng đã bị đặt trùng lịch.
        /// </summary>
        public static string TaoDatPhong(int maKH, int maPhong, DateTime nhan, DateTime tra,
            int nguoiLon, int treEm, decimal giaMotDem, decimal phuThu, decimal thueVaPhi,
            decimal tongTien, int? maNhanVien)
        {
            using (var conn = TaoKetNoi())
            {
                conn.Open();
                // Serializable: hai người cùng đặt một phòng không thể cùng vượt qua bước kiểm tra trùng
                using (var tran = conn.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        // 1) Kiểm tra trùng lịch NGAY TRONG transaction
                        int soDonTrung;
                        using (var cmd = new SqlCommand(@"
SELECT COUNT(*) FROM DatPhong
WHERE MaPhong = @MaPhong AND TrangThai IN ('Đã đặt', 'Đang ở')
  AND NgayNhan < @Tra AND NgayTra > @Nhan", conn, tran))
                        {
                            cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = maPhong;
                            cmd.Parameters.Add("@Nhan", SqlDbType.Date).Value = nhan.Date;
                            cmd.Parameters.Add("@Tra", SqlDbType.Date).Value = tra.Date;
                            soDonTrung = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (soDonTrung > 0)
                        {
                            tran.Rollback();
                            return null;   // báo cho BLL biết là trùng lịch
                        }

                        // 2) Sinh mã đặt phòng: DP-yyMMdd-STT (STT tăng theo từng ngày)
                        int stt;
                        using (var cmd = new SqlCommand(
                            "SELECT COUNT(*) + 1 FROM DatPhong WHERE CAST(NgayTao AS DATE) = CURRENT_DATE",
                            conn, tran))
                        {
                            stt = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        string maDat = "DP-" + DateTime.Now.ToString("yyMMdd") + "-" + stt.ToString("000");

                        // 3) Ghi đơn
                        using (var cmd = new SqlCommand(@"
INSERT INTO DatPhong (MaDat, MaKH, MaPhong, NgayNhan, NgayTra, SoNguoiLon, SoTreEm,
                      GiaMotDem, PhuThu, ThueVaPhi, TongTien, TrangThai, MaNhanVien)
VALUES (@MaDat, @MaKH, @MaPhong, @Nhan, @Tra, @NguoiLon, @TreEm,
        @Gia, @PhuThu, @Thue, @Tong, 'Đã đặt', @MaNV)", conn, tran))
                        {
                            cmd.Parameters.Add("@MaDat", SqlDbType.NVarChar, 20).Value = maDat;
                            cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = maKH;
                            cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = maPhong;
                            cmd.Parameters.Add("@Nhan", SqlDbType.Date).Value = nhan.Date;
                            cmd.Parameters.Add("@Tra", SqlDbType.Date).Value = tra.Date;
                            cmd.Parameters.Add("@NguoiLon", SqlDbType.Int).Value = nguoiLon;
                            cmd.Parameters.Add("@TreEm", SqlDbType.Int).Value = treEm;
                            cmd.Parameters.Add("@Gia", SqlDbType.Decimal).Value = giaMotDem;
                            cmd.Parameters.Add("@PhuThu", SqlDbType.Decimal).Value = phuThu;
                            cmd.Parameters.Add("@Thue", SqlDbType.Decimal).Value = thueVaPhi;
                            cmd.Parameters.Add("@Tong", SqlDbType.Decimal).Value = tongTien;
                            cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = (object)maNhanVien ?? DBNull.Value;
                            cmd.ExecuteNonQuery();
                        }

                        // >>> CHỨC NĂNG 9 sẽ chèn bước ghi phiếu đặt cọc vào ĐÂY (cùng transaction)

                        tran.Commit();
                        return maDat;
                    }
                    catch
                    {
                        try { tran.Rollback(); } catch { /* transaction đã đóng */ }
                        throw;
                    }
                }
            }
        }
    }
}
