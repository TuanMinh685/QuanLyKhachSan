using System;
using System.Data;
using Microsoft.Data.SqlClient;    // Nếu DatabaseHelper.cs của nhóm dùng System.Data.SqlClient thì đổi dòng này cho giống

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
WHERE p.TinhTrang = N'Hoạt động'
  AND lp.SoNguoiToiDa >= @TongNguoi
  AND (@MaLoai IS NULL OR lp.MaLoai = @MaLoai)
  AND NOT EXISTS (
        SELECT 1 FROM DatPhong d
        WHERE d.MaPhong = p.MaPhong
          AND d.TrangThai IN (N'Đã đặt', N'Đang ở')
          AND ((d.NgayNhan < @Tra AND d.NgayTra > @Nhan)
               OR (d.TrangThai = N'Đang ở' AND @Nhan <= CAST(GETDATE() AS date))))
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
WHERE p.MaPhong = @MaPhong AND p.TinhTrang = N'Hoạt động'";

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
            decimal tongTien, decimal tienCoc, string phuongThuc, int maNhanVien)
        {
            using (var conn = TaoKetNoi())
            {
                conn.Open();
                // Serializable: hai người cùng đặt một phòng không thể cùng vượt qua bước kiểm tra trùng
                using (var tran = conn.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        // Cùng khóa với nhận/hủy phòng để tuần tự hóa thay đổi đặt phòng.
                        KhoaDatPhong(conn, tran);
                        using (var check = new SqlCommand(@"
SELECT lp.GiaMotDem, lp.SoNguoiToiDa FROM Phong p WITH (UPDLOCK, HOLDLOCK)
JOIN LoaiPhong lp WITH (HOLDLOCK) ON lp.MaLoai = p.MaLoai
WHERE p.MaPhong = @Phong AND p.TinhTrang = N'Hoạt động'", conn, tran))
                        {
                            check.Parameters.Add("@Phong", SqlDbType.Int).Value = maPhong;
                            using var reader = check.ExecuteReader();
                            if (!reader.Read())
                                throw new BLL.NghiepVuException("Phòng đã ngừng hoạt động. Hãy tìm lại phòng.");
                            if (Convert.ToDecimal(reader["GiaMotDem"]) != giaMotDem)
                                throw new BLL.NghiepVuException("Giá phòng vừa thay đổi. Hãy tìm lại và xác nhận số tiền mới.");
                            if ((long)nguoiLon + treEm > Convert.ToInt32(reader["SoNguoiToiDa"]))
                                throw new BLL.NghiepVuException("Số khách vượt quá sức chứa hiện tại của phòng.");
                        }
                        // 1) Kiểm tra trùng lịch NGAY TRONG transaction
                        int soDonTrung;
                        using (var cmd = new SqlCommand(@"
SELECT COUNT(*) FROM DatPhong
WHERE MaPhong = @MaPhong AND TrangThai IN (N'Đã đặt', N'Đang ở')
  AND ((NgayNhan < @Tra AND NgayTra > @Nhan)
       OR (TrangThai = N'Đang ở' AND @Nhan <= CAST(GETDATE() AS date)))", conn, tran))
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

                        // Dùng ngày của SQL Server và số lớn nhất để không tái sử dụng mã khi xóa đơn.
                        string prefix;
                        using (var clock = new SqlCommand("SELECT 'DP-' + CONVERT(char(6), GETDATE(), 12) + '-'", conn, tran))
                            prefix = Convert.ToString(clock.ExecuteScalar());
                        int stt;
                        using (var cmd = new SqlCommand(@"
SELECT ISNULL(MAX(TRY_CONVERT(int, SUBSTRING(MaDat, 11, 10))), 0) + 1
FROM DatPhong WHERE MaDat LIKE @Prefix + '%'", conn, tran))
                        {
                            cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 10).Value = prefix;
                            stt = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        string maDat = prefix + stt.ToString("000");

                        // 3) Ghi đơn
                        int maDatPhong;
                        using (var cmd = new SqlCommand(@"
INSERT INTO DatPhong (MaDat, MaKH, MaPhong, NgayNhan, NgayTra, SoNguoiLon, SoTreEm,
                      GiaMotDem, PhuThu, ThueVaPhi, TongTien, TienCoc, TrangThai, MaNhanVien)
OUTPUT INSERTED.MaDatPhong
VALUES (@MaDat, @MaKH, @MaPhong, @Nhan, @Tra, @NguoiLon, @TreEm,
        @Gia, @PhuThu, @Thue, @Tong, @Coc, N'Đã đặt', @MaNV)", conn, tran))
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
                            cmd.Parameters.Add("@Coc", SqlDbType.Decimal).Value = tienCoc;
                            cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = maNhanVien;
                            foreach (SqlParameter parameter in cmd.Parameters)
                                if (parameter.SqlDbType == SqlDbType.Decimal) { parameter.Precision = 18; parameter.Scale = 2; }
                            maDatPhong = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Đơn và phiếu thu cùng thành công hoặc cùng rollback.
                        PhieuDatCocDAL.GhiPhieu(conn, tran, maDatPhong, "Thu", tienCoc, phuongThuc, maNhanVien);

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

        internal static void KhoaDatPhong(SqlConnection conn, SqlTransaction tran)
        {
            using var cmd = new SqlCommand(@"
DECLARE @Result int;
EXEC @Result = sys.sp_getapplock @Resource=N'Hotel:DatPhong',
     @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout=10000;
SELECT @Result;", conn, tran);
            if (Convert.ToInt32(cmd.ExecuteScalar()) < 0)
                throw new BLL.NghiepVuException("Có thao tác đặt phòng khác đang xử lý. Vui lòng thử lại.");
        }
    }
}
