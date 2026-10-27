using NpgsqlTypes;
using System;
using System.Data;

namespace Quản_Lý_Khách_Sạn.DAL
{
    /// <summary>
    /// Toàn bộ câu SQL của chức năng tìm phòng trống + đặt phòng.
    /// Giả định CSDL theo script đã đưa: LoaiPhong, Phong, KhachHang, DatPhong.
    /// Nếu bảng/cột của nhóm khác, CHỈ sửa trong file này.
    /// </summary>
    public static class LeTanDatPhongDAL
    {

        internal static NpgsqlConnection TaoKetNoi()
        {
            return DatabaseHelper.GetConnection();
        }

        // Chạy SELECT, trả về DataTable (Fill tự mở và đóng kết nối)
        internal static DataTable LayBang(string sql, params NpgsqlParameter[] ps)
        {
            using (var conn = TaoKetNoi())
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
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
          AND ((d.NgayNhan < @Tra AND d.NgayTra > @Nhan)
               OR (d.TrangThai = 'Đang ở' AND @Nhan <= CAST(CURRENT_TIMESTAMP AS date))))
ORDER BY p.SoPhong";

            var pNhan = new NpgsqlParameter("@Nhan", NpgsqlDbType.Date) { Value = nhan.Date };
            var pTra = new NpgsqlParameter("@Tra", NpgsqlDbType.Date) { Value = tra.Date };
            var pNguoi = new NpgsqlParameter("@TongNguoi", NpgsqlDbType.Integer) { Value = tongNguoi };
            var pLoai = new NpgsqlParameter("@MaLoai", NpgsqlDbType.Integer) { Value = (object)maLoai ?? DBNull.Value };

            return LayBang(sql, pNhan, pTra, pNguoi, pLoai);
        }

        public static DataTable TimKhach(string tuKhoa)
        {
            const string sql = @"
SELECT MaKH, MaKHHienThi, HoTen, SoDienThoai
FROM KhachHang
WHERE HoTen LIKE @k OR SoDienThoai LIKE @k OR MaKHHienThi LIKE @k
ORDER BY HoTen";
            return LayBang(sql, new NpgsqlParameter("@k", "%" + tuKhoa + "%"));
        }

        // Giá/đêm của phòng; null nếu phòng không tồn tại hoặc đang bảo trì
        public static decimal? LayGiaPhong(int maPhong)
        {
            const string sql = @"
SELECT lp.GiaMotDem
FROM Phong p JOIN LoaiPhong lp ON lp.MaLoai = p.MaLoai
WHERE p.MaPhong = @MaPhong AND p.TinhTrang = 'Hoạt động'";

            using (var conn = TaoKetNoi())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@MaPhong", NpgsqlDbType.Integer).Value = maPhong;
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
                // ReadCommitted đọc được đơn vừa commit sau khi chờ khóa giao dịch dùng chung.
                using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        // Cùng khóa với nhận/hủy phòng để tuần tự hóa thay đổi đặt phòng.
                        KhoaDatPhong(conn, tran);
                        using (var check = new NpgsqlCommand(@"
SELECT lp.GiaMotDem, lp.SoNguoiToiDa FROM Phong p
JOIN LoaiPhong lp ON lp.MaLoai = p.MaLoai
WHERE p.MaPhong = @Phong AND p.TinhTrang = 'Hoạt động' FOR SHARE OF p, lp", conn, tran))
                        {
                            check.Parameters.Add("@Phong", NpgsqlDbType.Integer).Value = maPhong;
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
                        using (var cmd = new NpgsqlCommand(@"
SELECT COUNT(*) FROM DatPhong
WHERE MaPhong = @MaPhong AND TrangThai IN ('Đã đặt', 'Đang ở')
  AND ((NgayNhan < @Tra AND NgayTra > @Nhan)
       OR (TrangThai = 'Đang ở' AND @Nhan <= CAST(CURRENT_TIMESTAMP AS date)))", conn, tran))
                        {
                            cmd.Parameters.Add("@MaPhong", NpgsqlDbType.Integer).Value = maPhong;
                            cmd.Parameters.Add("@Nhan", NpgsqlDbType.Date).Value = nhan.Date;
                            cmd.Parameters.Add("@Tra", NpgsqlDbType.Date).Value = tra.Date;
                            soDonTrung = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (soDonTrung > 0)
                        {
                            tran.Rollback();
                            return null;   // báo cho BLL biết là trùng lịch
                        }

                        // Dùng ngày của khách sạn và số lớn nhất để không tái sử dụng mã khi xóa đơn.
                        string prefix;
                        using (var clock = new NpgsqlCommand("SELECT 'DP-' || to_char(CURRENT_DATE, 'YYMMDD') || '-'", conn, tran))
                            prefix = Convert.ToString(clock.ExecuteScalar());
                        int stt;
                        using (var cmd = new NpgsqlCommand(@"
SELECT COALESCE(MAX(CASE WHEN substring(MaDat from 11) ~ '^[0-9]{1,9}$' THEN substring(MaDat from 11)::integer END), 0) + 1
FROM DatPhong WHERE MaDat LIKE @Prefix || '%'", conn, tran))
                        {
                            cmd.Parameters.Add("@Prefix", NpgsqlDbType.Varchar, 10).Value = prefix;
                            stt = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        string maDat = prefix + stt.ToString("000");

                        // 3) Ghi đơn
                        int maDatPhong;
                        using (var cmd = new NpgsqlCommand(@"
INSERT INTO DatPhong (MaDat, MaKH, MaPhong, NgayNhan, NgayTra, SoNguoiLon, SoTreEm,
                      GiaMotDem, PhuThu, ThueVaPhi, TongTien, TienCoc, TrangThai, MaNhanVien)
VALUES (@MaDat, @MaKH, @MaPhong, @Nhan, @Tra, @NguoiLon, @TreEm,
        @Gia, @PhuThu, @Thue, @Tong, @Coc, 'Đã đặt', @MaNV) RETURNING MaDatPhong", conn, tran))
                        {
                            cmd.Parameters.Add("@MaDat", NpgsqlDbType.Varchar, 20).Value = maDat;
                            cmd.Parameters.Add("@MaKH", NpgsqlDbType.Integer).Value = maKH;
                            cmd.Parameters.Add("@MaPhong", NpgsqlDbType.Integer).Value = maPhong;
                            cmd.Parameters.Add("@Nhan", NpgsqlDbType.Date).Value = nhan.Date;
                            cmd.Parameters.Add("@Tra", NpgsqlDbType.Date).Value = tra.Date;
                            cmd.Parameters.Add("@NguoiLon", NpgsqlDbType.Integer).Value = nguoiLon;
                            cmd.Parameters.Add("@TreEm", NpgsqlDbType.Integer).Value = treEm;
                            cmd.Parameters.Add("@Gia", NpgsqlDbType.Numeric).Value = giaMotDem;
                            cmd.Parameters.Add("@PhuThu", NpgsqlDbType.Numeric).Value = phuThu;
                            cmd.Parameters.Add("@Thue", NpgsqlDbType.Numeric).Value = thueVaPhi;
                            cmd.Parameters.Add("@Tong", NpgsqlDbType.Numeric).Value = tongTien;
                            cmd.Parameters.Add("@Coc", NpgsqlDbType.Numeric).Value = tienCoc;
                            cmd.Parameters.Add("@MaNV", NpgsqlDbType.Integer).Value = maNhanVien;
                            foreach (NpgsqlParameter parameter in cmd.Parameters)
                                if (parameter.NpgsqlDbType == NpgsqlDbType.Numeric) { parameter.Precision = 18; parameter.Scale = 2; }
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

        internal static void KhoaDatPhong(NpgsqlConnection conn, NpgsqlTransaction tran)
        {
            using var cmd = new NpgsqlCommand(@"
SET LOCAL lock_timeout = '10s';
SELECT pg_advisory_xact_lock(20261023, 1);", conn, tran);
            try { cmd.ExecuteNonQuery(); }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.LockNotAvailable)
            { throw new BLL.NghiepVuException("Có thao tác đặt phòng khác đang xử lý. Vui lòng thử lại."); }
        }
    }
}
