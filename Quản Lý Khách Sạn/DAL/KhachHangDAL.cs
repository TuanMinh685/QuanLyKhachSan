using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.DAL
{
    /// <summary>
    /// Tầng truy cập dữ liệu (DAL) của Khách hàng.
    /// Quy tắc: CHỈ làm việc với SQL, không kiểm tra nghiệp vụ, không đụng tới giao diện.
    /// Mọi giá trị đưa vào câu lệnh đều đi qua SqlParameter -> chống SQL Injection.
    /// </summary>
    public class KhachHangDAL
    {
        // ------------------------------------------------------------------
        // 1. TÌM KIẾM + LỌC NHÓM
        // ------------------------------------------------------------------
        /// <param name="tuKhoa">Họ tên / SĐT / số giấy tờ / mã KH. Rỗng = không lọc theo từ khóa.</param>
        /// <param name="nhom">"VIP" | "Thân thiết" | "Mới" | "" (tất cả)</param>
        /// <param name="top">Số dòng tối đa trả về (tránh nạp hàng nghìn hồ sơ lên lưới).</param>
        public List<KhachHang> TimKiem(string tuKhoa, string nhom, int top)
        {
            string kw = (tuKhoa ?? "").Trim();
            string kwSo = kw.Replace(" ", "").Replace(".", "").Replace("-", "");   // "0903 456 782" -> "0903456782"

            // COLLATE Vietnamese_CI_AI: không phân biệt hoa/thường và dấu => gõ "hung" vẫn ra "Hùng"
            const string sql = @"
                SELECT TOP (@Top) *
                FROM dbo.vw_KhachHangTongQuan
                WHERE ( @TuKhoa = N''
                        OR HoTen COLLATE Vietnamese_CI_AI LIKE @Like ESCAPE '\'
                        OR DienThoai LIKE @LikeSo ESCAPE '\'
                        OR SoGiayTo  LIKE @LikeSo ESCAPE '\'
                        OR MaKH      LIKE @LikeSo ESCAPE '\' )
                  AND ( @Nhom = N'' OR NhomKhach = @Nhom )
                ORDER BY ISNULL(CAST(LanLuuTruGanNhat AS DATETIME), NgayTao) DESC, MaKhachHang DESC";

            SqlParameter[] p =
            {
                new SqlParameter("@Top", SqlDbType.Int) { Value = top },
                new SqlParameter("@TuKhoa", SqlDbType.NVarChar, 100) { Value = kw },
                new SqlParameter("@Like", SqlDbType.NVarChar, 120) { Value = "%" + EscapeLike(kw) + "%" },
                new SqlParameter("@LikeSo", SqlDbType.VarChar, 120) { Value = "%" + EscapeLike(kwSo) + "%" },
                new SqlParameter("@Nhom", SqlDbType.NVarChar, 20) { Value = nhom ?? "" }
            };

            var list = new List<KhachHang>();
            using (DataTable dt = DatabaseHelper.ExecuteQuery(sql, p))
            {
                foreach (DataRow r in dt.Rows) list.Add(MapRow(r));
            }
            return list;
        }

        // ------------------------------------------------------------------
        // 2. ĐẾM TỔNG HỒ SƠ (hiển thị "4.206 hồ sơ")
        // ------------------------------------------------------------------
        public int DemHoSo()
        {
            using (DataTable dt = DatabaseHelper.ExecuteQuery("SELECT COUNT(*) FROM dbo.KhachHang"))
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
        }

        // ------------------------------------------------------------------
        // 3. LẤY 1 KHÁCH THEO MÃ
        // ------------------------------------------------------------------
        public KhachHang LayTheoMa(int maKhachHang)
        {
            const string sql = "SELECT * FROM dbo.vw_KhachHangTongQuan WHERE MaKhachHang = @Ma";
            using (DataTable dt = DatabaseHelper.ExecuteQuery(sql, new[] { new SqlParameter("@Ma", SqlDbType.Int) { Value = maKhachHang } }))
            {
                return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
            }
        }

        // ------------------------------------------------------------------
        // 4. KIỂM TRA TRÙNG SỐ GIẤY TỜ
        //    maLoaiTru: khi SỬA hồ sơ, bỏ qua chính hồ sơ đó (không tự báo trùng với mình).
        // ------------------------------------------------------------------
        public bool TonTaiSoGiayTo(string soGiayTo, int? maLoaiTru)
        {
            if (string.IsNullOrEmpty(soGiayTo)) return false;

            const string sql = @"SELECT COUNT(1) FROM dbo.KhachHang
                                 WHERE SoGiayTo = @So AND (@MaTru IS NULL OR MaKhachHang <> @MaTru)";
            SqlParameter[] p =
            {
                new SqlParameter("@So", SqlDbType.VarChar, 30) { Value = soGiayTo },
                new SqlParameter("@MaTru", SqlDbType.Int) { Value = (object)maLoaiTru ?? DBNull.Value }
            };
            using (DataTable dt = DatabaseHelper.ExecuteQuery(sql, p))
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
        }

        // ------------------------------------------------------------------
        // 5. THÊM MỚI – trả về MaKhachHang vừa sinh
        //    OUTPUT INSERTED.MaKhachHang: lấy khóa tự tăng ngay trong 1 câu lệnh,
        //    an toàn hơn SELECT MAX(...) (không bị lẫn khi 2 lễ tân thêm cùng lúc).
        // ------------------------------------------------------------------
        public int Them(KhachHang kh)
        {
            const string sql = @"
                INSERT INTO dbo.KhachHang
                    (HoTen, DienThoai, GioiTinh, NgaySinh, QuocTich, Email, DiaChi, SoGiayTo, GhiChu, DongYNhanUuDai)
                OUTPUT INSERTED.MaKhachHang
                VALUES
                    (@HoTen, @DienThoai, @GioiTinh, @NgaySinh, @QuocTich, @Email, @DiaChi, @SoGiayTo, @GhiChu, @DongY)";

            using (DataTable dt = DatabaseHelper.ExecuteQuery(sql, ThamSoGhi(kh)))
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
        }

        // ------------------------------------------------------------------
        // 6. CẬP NHẬT
        // ------------------------------------------------------------------
        public bool CapNhat(KhachHang kh)
        {
            const string sql = @"
                UPDATE dbo.KhachHang
                SET HoTen = @HoTen, DienThoai = @DienThoai, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh,
                    QuocTich = @QuocTich, Email = @Email, DiaChi = @DiaChi, SoGiayTo = @SoGiayTo,
                    GhiChu = @GhiChu, DongYNhanUuDai = @DongY, NgayCapNhat = GETDATE()
                WHERE MaKhachHang = @Ma";

            var p = new List<SqlParameter>(ThamSoGhi(kh))
            {
                new SqlParameter("@Ma", SqlDbType.Int) { Value = kh.MaKhachHang }
            };
            return DatabaseHelper.ExecuteNonQuery(sql, p.ToArray()) > 0;
        }

        // ------------------------------------------------------------------
        // 7. LỊCH SỬ LƯU TRÚ của 1 khách
        //    Chỉ lấy lượt ĐÃ nhận / ĐÃ trả (khớp với SoLanLuuTru trong view).
        // ------------------------------------------------------------------
        public List<LichSuLuuTru> LayLichSuLuuTru(int maKhachHang)
        {
            const string sql = @"
                SELECT  dp.MaDatPhong, p.SoPhong, lp.TenLoaiPhong,
                        dp.NgayNhanDuKien, dp.NgayTraDuKien,
                        DATEDIFF(DAY, dp.NgayNhanDuKien, dp.NgayTraDuKien) AS SoDem,
                        dp.TrangThai,
                        dp.GiaPhongDem * DATEDIFF(DAY, dp.NgayNhanDuKien, dp.NgayTraDuKien) AS TongTien
                FROM dbo.DatPhong dp
                JOIN dbo.Phong p      ON p.MaPhong = dp.MaPhong
                JOIN dbo.LoaiPhong lp ON lp.MaLoaiPhong = p.MaLoaiPhong
                WHERE dp.MaKhachHang = @Ma
                  AND dp.TrangThai IN (N'Đã nhận phòng', N'Đã trả phòng')
                ORDER BY dp.NgayNhanDuKien DESC";

            var list = new List<LichSuLuuTru>();
            using (DataTable dt = DatabaseHelper.ExecuteQuery(sql, new[] { new SqlParameter("@Ma", SqlDbType.Int) { Value = maKhachHang } }))
            {
                foreach (DataRow r in dt.Rows)
                {
                    list.Add(new LichSuLuuTru
                    {
                        MaDatPhong = Convert.ToInt32(r["MaDatPhong"]),
                        SoPhong = Convert.ToString(r["SoPhong"]),
                        TenLoaiPhong = Convert.ToString(r["TenLoaiPhong"]),
                        NgayNhan = Convert.ToDateTime(r["NgayNhanDuKien"]),
                        NgayTra = Convert.ToDateTime(r["NgayTraDuKien"]),
                        SoDem = Convert.ToInt32(r["SoDem"]),
                        TrangThai = Convert.ToString(r["TrangThai"]),
                        TongTien = Convert.ToDecimal(r["TongTien"])
                    });
                }
            }
            return list;
        }

        // ==================================================================
        // HÀM PHỤ
        // ==================================================================

        /// <summary>Tham số dùng chung cho INSERT và UPDATE (tránh viết trùng 2 lần).</summary>
        private static SqlParameter[] ThamSoGhi(KhachHang kh)
        {
            return new[]
            {
                new SqlParameter("@HoTen",     SqlDbType.NVarChar, 100) { Value = kh.HoTen },
                new SqlParameter("@DienThoai", SqlDbType.VarChar,  20)  { Value = kh.DienThoai },
                new SqlParameter("@GioiTinh",  SqlDbType.NVarChar, 10)  { Value = (object)kh.GioiTinh ?? DBNull.Value },
                new SqlParameter("@NgaySinh",  SqlDbType.Date)          { Value = (object)kh.NgaySinh ?? DBNull.Value },
                new SqlParameter("@QuocTich",  SqlDbType.NVarChar, 50)  { Value = kh.QuocTich },
                new SqlParameter("@Email",     SqlDbType.VarChar,  100) { Value = (object)kh.Email ?? DBNull.Value },
                new SqlParameter("@DiaChi",    SqlDbType.NVarChar, 255) { Value = (object)kh.DiaChi ?? DBNull.Value },
                new SqlParameter("@SoGiayTo",  SqlDbType.VarChar,  30)  { Value = (object)kh.SoGiayTo ?? DBNull.Value },
                new SqlParameter("@GhiChu",    SqlDbType.NVarChar, 500) { Value = (object)kh.GhiChu ?? DBNull.Value },
                new SqlParameter("@DongY",     SqlDbType.Bit)           { Value = kh.DongYNhanUuDai }
            };
        }

        /// <summary>Chuyển 1 dòng DataTable thành đối tượng KhachHang (xử lý DBNull).</summary>
        private static KhachHang MapRow(DataRow r)
        {
            return new KhachHang
            {
                MaKhachHang = Convert.ToInt32(r["MaKhachHang"]),
                MaKH = Convert.ToString(r["MaKH"]),
                HoTen = Convert.ToString(r["HoTen"]),
                DienThoai = Convert.ToString(r["DienThoai"]),
                GioiTinh = r["GioiTinh"] as string,
                NgaySinh = r["NgaySinh"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["NgaySinh"]),
                QuocTich = Convert.ToString(r["QuocTich"]),
                Email = r["Email"] as string,
                DiaChi = r["DiaChi"] as string,
                SoGiayTo = r["SoGiayTo"] as string,
                GhiChu = r["GhiChu"] as string,
                DongYNhanUuDai = Convert.ToBoolean(r["DongYNhanUuDai"]),
                NgayTao = Convert.ToDateTime(r["NgayTao"]),
                NgayCapNhat = r["NgayCapNhat"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["NgayCapNhat"]),
                SoLanLuuTru = Convert.ToInt32(r["SoLanLuuTru"]),
                LanLuuTruGanNhat = r["LanLuuTruGanNhat"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["LanLuuTruGanNhat"]),
                NhomKhach = Convert.ToString(r["NhomKhach"])
            };
        }

        /// <summary>Vô hiệu hóa ký tự đặc biệt của LIKE (% _ [) để người dùng gõ gì tìm đúng cái đó.</summary>
        private static string EscapeLike(string s)
        {
            return s.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[");
        }
    }
}
