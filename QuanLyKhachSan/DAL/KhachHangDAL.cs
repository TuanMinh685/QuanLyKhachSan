using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.DAL
{
    /// <summary>
    /// Tầng truy cập dữ liệu (DAL) của Khách hàng.
    /// Quy tắc: CHỈ làm việc với SQL, không kiểm tra nghiệp vụ, không đụng tới giao diện.
    /// Mọi giá trị đưa vào câu lệnh đều đi qua NpgsqlParameter -> chống SQL Injection.
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
                SELECT *
                FROM vw_LeTanKhachHangTongQuan
                WHERE ( @TuKhoa = ''
                        OR HoTen ILIKE @Like ESCAPE '\'
                        OR DienThoai LIKE @LikeSo ESCAPE '\'
                        OR SoGiayTo  LIKE @LikeSo ESCAPE '\'
                        OR MaKH      LIKE @LikeSo ESCAPE '\' )
                  AND ( @Nhom = '' OR NhomKhach = @Nhom )
                ORDER BY COALESCE(LanLuuTruGanNhat, NgayTao) DESC, MaKhachHang DESC
                LIMIT @Top";

            NpgsqlParameter[] p =
            {
                new NpgsqlParameter("@Top", NpgsqlDbType.Integer) { Value = top },
                new NpgsqlParameter("@TuKhoa", NpgsqlDbType.Varchar, 100) { Value = kw },
                new NpgsqlParameter("@Like", NpgsqlDbType.Varchar, 120) { Value = "%" + EscapeLike(kw) + "%" },
                new NpgsqlParameter("@LikeSo", NpgsqlDbType.Varchar, 120) { Value = "%" + EscapeLike(kwSo) + "%" },
                new NpgsqlParameter("@Nhom", NpgsqlDbType.Varchar, 20) { Value = nhom ?? "" }
            };

            var list = new List<KhachHang>();
            using (DataTable dt = LeTanDatPhongDAL.LayBang(sql, p))
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
            using (DataTable dt = LeTanDatPhongDAL.LayBang("SELECT COUNT(*) FROM KhachHang"))
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
        }

        // ------------------------------------------------------------------
        // 3. LẤY 1 KHÁCH THEO MÃ
        // ------------------------------------------------------------------
        public KhachHang LayTheoMa(int maKhachHang)
        {
            const string sql = "SELECT * FROM vw_LeTanKhachHangTongQuan WHERE MaKhachHang = @Ma";
            using (DataTable dt = LeTanDatPhongDAL.LayBang(sql, new[] { new NpgsqlParameter("@Ma", NpgsqlDbType.Integer) { Value = maKhachHang } }))
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

            const string sql = @"SELECT COUNT(1) FROM KhachHang
                                 WHERE CCCD = @So AND (@MaTru IS NULL OR MaKH <> @MaTru)";
            NpgsqlParameter[] p =
            {
                new NpgsqlParameter("@So", NpgsqlDbType.Varchar, 30) { Value = soGiayTo },
                new NpgsqlParameter("@MaTru", NpgsqlDbType.Integer) { Value = (object)maLoaiTru ?? DBNull.Value }
            };
            using (DataTable dt = LeTanDatPhongDAL.LayBang(sql, p))
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
        }

        // ------------------------------------------------------------------
        // 5. THÊM MỚI – trả về MaKhachHang vừa sinh
        // ------------------------------------------------------------------
        public int Them(KhachHang kh)
        {
            const string sql = @"
                INSERT INTO KhachHang
                    (HoTen, SoDienThoai, GioiTinh, NgaySinh, QuocTich, Email, DiaChi, CCCD, GhiChu, DongYNhanUuDai)
                VALUES
                    (@HoTen, @DienThoai, @GioiTinh, @NgaySinh, @QuocTich, @Email, @DiaChi, @SoGiayTo, @GhiChu, @DongY)
                RETURNING MaKH";

            using (DataTable dt = LeTanDatPhongDAL.LayBang(sql, ThamSoGhi(kh)))
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
                UPDATE KhachHang
                SET HoTen = @HoTen, SoDienThoai = @DienThoai, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh,
                    QuocTich = @QuocTich, Email = @Email, DiaChi = @DiaChi, CCCD = @SoGiayTo,
                    GhiChu = @GhiChu, DongYNhanUuDai = @DongY, NgayCapNhat = CURRENT_TIMESTAMP
                WHERE MaKH = @Ma";

            var p = new List<NpgsqlParameter>(ThamSoGhi(kh))
            {
                new NpgsqlParameter("@Ma", NpgsqlDbType.Integer) { Value = kh.MaKhachHang }
            };
            return ChayCapNhat(sql, p.ToArray()) > 0;
        }

        // ------------------------------------------------------------------
        // 7. LỊCH SỬ LƯU TRÚ của 1 khách
        //    Chỉ lấy lượt ĐÃ nhận / ĐÃ trả (khớp với SoLanLuuTru trong view).
        // ------------------------------------------------------------------
        public List<LichSuLuuTru> LayLichSuLuuTru(int maKhachHang)
        {
            const string sql = @"
                SELECT dp.MaDatPhong, p.SoPhong, lp.TenLoai AS TenLoaiPhong,
                       dp.NgayNhan AS NgayNhanDuKien, dp.NgayTra AS NgayTraDuKien,
                       dp.SoDem, dp.GioNhanThucTe, dp.GioTraThucTe,
                       CASE WHEN dp.TrangThai = 'Đang ở' THEN 'Đang lưu trú'
                            ELSE 'Đã trả phòng' END AS TrangThai,
                       dp.TongTien
                FROM DatPhong dp
                JOIN Phong p ON p.MaPhong = dp.MaPhong
                JOIN LoaiPhong lp ON lp.MaLoai = p.MaLoai
                WHERE dp.MaKH = @Ma AND dp.TrangThai IN ('Đang ở', 'Đã trả')
                ORDER BY dp.NgayNhan DESC, dp.MaDatPhong DESC";

            var list = new List<LichSuLuuTru>();
            using (DataTable dt = LeTanDatPhongDAL.LayBang(sql, new[] { new NpgsqlParameter("@Ma", NpgsqlDbType.Integer) { Value = maKhachHang } }))
            {
                foreach (DataRow r in dt.Rows)
                {
                    list.Add(new LichSuLuuTru
                    {
                        MaDatPhong = Convert.ToInt32(r["MaDatPhong"]),
                        SoPhong = Convert.ToString(r["SoPhong"]),
                        TenLoaiPhong = Convert.ToString(r["TenLoaiPhong"]),
                        NgayNhan = ToDateTime(r["NgayNhanDuKien"]),
                        NgayTra = ToDateTime(r["NgayTraDuKien"]),
                        GioNhanThucTe = r["GioNhanThucTe"] == DBNull.Value ? null : ToDateTime(r["GioNhanThucTe"]),
                        GioTraThucTe = r["GioTraThucTe"] == DBNull.Value ? null : ToDateTime(r["GioTraThucTe"]),
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
        private static NpgsqlParameter[] ThamSoGhi(KhachHang kh)
        {
            return new[]
            {
                new NpgsqlParameter("@HoTen",     NpgsqlDbType.Varchar, 100) { Value = kh.HoTen },
                new NpgsqlParameter("@DienThoai", NpgsqlDbType.Varchar,  20)  { Value = kh.DienThoai },
                new NpgsqlParameter("@GioiTinh",  NpgsqlDbType.Varchar, 10)  { Value = (object)kh.GioiTinh ?? DBNull.Value },
                new NpgsqlParameter("@NgaySinh",  NpgsqlDbType.Date)          { Value = (object)kh.NgaySinh ?? DBNull.Value },
                new NpgsqlParameter("@QuocTich",  NpgsqlDbType.Varchar, 50)  { Value = kh.QuocTich },
                new NpgsqlParameter("@Email",     NpgsqlDbType.Varchar,  100) { Value = (object)kh.Email ?? DBNull.Value },
                new NpgsqlParameter("@DiaChi",    NpgsqlDbType.Varchar, 255) { Value = (object)kh.DiaChi ?? DBNull.Value },
                new NpgsqlParameter("@SoGiayTo",  NpgsqlDbType.Varchar,  30)  { Value = (object)kh.SoGiayTo ?? DBNull.Value },
                new NpgsqlParameter("@GhiChu",    NpgsqlDbType.Varchar, 500) { Value = (object)kh.GhiChu ?? DBNull.Value },
                new NpgsqlParameter("@DongY",     NpgsqlDbType.Boolean)           { Value = kh.DongYNhanUuDai }
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
                NgaySinh = ToDateTimeNullable(r["NgaySinh"]),
                QuocTich = Convert.ToString(r["QuocTich"]),
                Email = r["Email"] as string,
                DiaChi = r["DiaChi"] as string,
                SoGiayTo = r["SoGiayTo"] as string,
                GhiChu = r["GhiChu"] as string,
                DongYNhanUuDai = Convert.ToBoolean(r["DongYNhanUuDai"]),
                NgayTao = ToDateTime(r["NgayTao"]),
                NgayCapNhat = ToDateTimeNullable(r["NgayCapNhat"]),
                SoLanLuuTru = Convert.ToInt32(r["SoLanLuuTru"]),
                LanLuuTruGanNhat = ToDateTimeNullable(r["LanLuuTruGanNhat"]),
                NhomKhach = Convert.ToString(r["NhomKhach"])
            };
        }

        /// <summary>Vô hiệu hóa ký tự đặc biệt của LIKE (% _ [) để người dùng gõ gì tìm đúng cái đó.</summary>
        private static int ChayCapNhat(string sql, NpgsqlParameter[] parameters)
        {
            using (var conn = LeTanDatPhongDAL.TaoKetNoi())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        private static string EscapeLike(string s)
        {
            return s.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[");
        }
    }
}
