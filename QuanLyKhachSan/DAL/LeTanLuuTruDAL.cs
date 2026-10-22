using System;
using System.Data;
// using Microsoft.Data.SqlClient; // migrated to Supabase Npgsql    // Giống LeTanDatPhongDAL: nếu nhóm dùng System.Data.SqlClient thì đổi cho giống

namespace Quản_Lý_Khách_Sạn.DAL
{
    /// <summary>
    /// SQL của Nhận phòng / Trả phòng / Hủy phòng (chức năng 4, phần B).
    /// Dùng chung kết nối với LeTanDatPhongDAL.
    /// Vòng đời một đơn:  Đã đặt --nhận--> Đang ở --trả--> Đã trả
    ///                    Đã đặt --hủy---> Đã hủy
    /// </summary>
    public static class LeTanLuuTruDAL
    {
        // Danh sách đơn theo trạng thái; từ khóa tìm theo mã đơn / tên khách / SĐT / số phòng
        public static DataTable TimDatPhong(string trangThai, string tuKhoa)
        {
            const string sql = @"
SELECT d.MaDatPhong, d.MaDat, kh.HoTen, kh.SoDienThoai, p.SoPhong, lp.TenLoai,
       d.NgayNhan, d.NgayTra, d.SoDem, d.TongTien, d.TrangThai
FROM DatPhong d
JOIN KhachHang kh ON kh.MaKH = d.MaKH
JOIN Phong p ON p.MaPhong = d.MaPhong
JOIN LoaiPhong lp ON lp.MaLoai = p.MaLoai
WHERE d.TrangThai = @TrangThai
  AND (d.MaDat LIKE @k OR kh.HoTen LIKE @k OR kh.SoDienThoai LIKE @k OR p.SoPhong LIKE @k)
ORDER BY d.NgayNhan, d.MaDat";

            return LeTanDatPhongDAL.LayBang(sql,
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 20) { Value = trangThai },
                new SqlParameter("@k", SqlDbType.NVarChar, 120) { Value = "%" + tuKhoa + "%" });
        }

        // Một đơn (để BLL kiểm tra quy tắc); null nếu không tồn tại
        public static DataRow LayDatPhong(int maDatPhong)
        {
            const string sql = @"
SELECT d.MaDatPhong, d.MaDat, d.MaPhong, d.NgayNhan, d.NgayTra, d.TrangThai, p.SoPhong
FROM DatPhong d
JOIN Phong p ON p.MaPhong = d.MaPhong
WHERE d.MaDatPhong = @Id";

            DataTable dt = LeTanDatPhongDAL.LayBang(sql,
                new SqlParameter("@Id", SqlDbType.Int) { Value = maDatPhong });
            return dt.Rows.Count == 0 ? null : dt.Rows[0];
        }

        // Phòng này có đơn KHÁC đang ở (khách chưa trả) không?
        public static bool PhongDangCoKhach(int maPhong, int boQuaMaDatPhong)
        {
            const string sql = @"
SELECT COUNT(*) FROM DatPhong
WHERE MaPhong = @MaPhong AND TrangThai = 'Đang ở' AND MaDatPhong <> @Id";

            using (var conn = LeTanDatPhongDAL.TaoKetNoi())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = maPhong;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = boQuaMaDatPhong;
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // ---------------------------------------------------------------------
        // 3 lệnh đổi trạng thái. Mỗi lệnh là MỘT câu UPDATE có điều kiện trạng thái cũ
        // (WHERE ... AND TrangThai = <trạng thái cũ>). Trả về true nếu đổi được đúng 1 đơn.
        // Nếu giữa lúc mở danh sách và lúc bấm xác nhận có người khác đã đổi đơn này,
        // điều kiện không còn đúng -> 0 dòng bị đổi -> trả về false, không ghi đè nhầm.
        // Một câu UPDATE tự nó là nguyên tử (atomic), nên không cần BeginTransaction.
        // ---------------------------------------------------------------------

        public static bool NhanPhong(int maDatPhong)
        {
            const string sql = @"
UPDATE DatPhong
SET TrangThai = 'Đang ở', GioNhanThucTe = CURRENT_TIMESTAMP
WHERE MaDatPhong = @Id
  AND TrangThai = 'Đã đặt'
  AND NOT EXISTS (SELECT 1 FROM DatPhong x
                  WHERE x.MaPhong = DatPhong.MaPhong
                    AND x.TrangThai = 'Đang ở'
                    AND x.MaDatPhong <> @Id)";
            return ChayCapNhat(sql, new SqlParameter("@Id", SqlDbType.Int) { Value = maDatPhong });
        }

        public static bool TraPhong(int maDatPhong)
        {
            const string sql = @"
UPDATE DatPhong
SET TrangThai = 'Đã trả', GioTraThucTe = CURRENT_TIMESTAMP
WHERE MaDatPhong = @Id AND TrangThai = 'Đang ở'";
            return ChayCapNhat(sql, new SqlParameter("@Id", SqlDbType.Int) { Value = maDatPhong });
        }

        public static bool HuyPhong(int maDatPhong, string lyDo)
        {
            const string sql = @"
UPDATE DatPhong
SET TrangThai = 'Đã hủy', NgayHuy = CURRENT_TIMESTAMP, LyDoHuy = @LyDo
WHERE MaDatPhong = @Id AND TrangThai = 'Đã đặt'";
            return ChayCapNhat(sql,
                new SqlParameter("@Id", SqlDbType.Int) { Value = maDatPhong },
                new SqlParameter("@LyDo", SqlDbType.NVarChar, 200) { Value = lyDo });
        }

        private static bool ChayCapNhat(string sql, params SqlParameter[] ps)
        {
            using (var conn = LeTanDatPhongDAL.TaoKetNoi())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteNonQuery() == 1;     // số dòng bị ảnh hưởng
            }
        }
    }
}