using System;
using System.Collections.Generic;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class HoaDonDAL
    {
        public List<HoaDon> GetAll()
        {
            var list = new List<HoaDon>();
            string sql = "SELECT * FROM HoaDon ORDER BY NgayThanhToan DESC";
            try
            {
                using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    conn.Open();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var hd = new HoaDon();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string col = reader.GetName(i).ToLower();
                                object val = reader.GetValue(i);
                                if (val == DBNull.Value) continue;

                                if (col == "mahoadon") hd.MaHoaDon = val.ToString() ?? "";
                                else if (col == "madatphong") hd.MaDatPhong = val.ToString() ?? "";
                                else if (col == "ngaylap") hd.NgayLap = val is DateOnly dl ? dl.ToDateTime(TimeOnly.MinValue) : ToDateTime(val);
                                else if (col == "ngaythanhtoan") hd.NgayThanhToan = val is DateOnly dt ? dt.ToDateTime(TimeOnly.MinValue) : ToDateTime(val);
                                else if (col == "tienphong") hd.TienPhong = Convert.ToDecimal(val);
                                else if (col == "tiendichvu") hd.TienDichVu = Convert.ToDecimal(val);
                                else if (col == "tiengiamgia") hd.TienGiamGia = Convert.ToDecimal(val);
                                else if (col == "tiendatcoc") hd.TienDatCoc = Convert.ToDecimal(val);
                                else if (col == "tongtien") hd.TongTien = Convert.ToDecimal(val);
                                else if (col == "mapttt") hd.MaPTTT = val.ToString() ?? "TM";
                                else if (col == "mavoucher") hd.MaVoucher = Convert.ToInt32(val);
                                else if (col == "trangthai") hd.TrangThai = val.ToString() ?? "Đã thanh toán";
                            }
                            list.Add(hd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi lấy danh sách Hóa Đơn: " + ex.Message);
            }
            return list;
        }

        public bool Insert(HoaDon hd)
        {
            string sql = @"INSERT INTO HoaDon (MaHoaDon, MaDatPhong, NgayLap, NgayThanhToan, TienPhong, TienDichVu, TienGiamGia, TienDatCoc, TongTien, MaPTTT, MaVoucher, TrangThai)
                           VALUES (@MaHD, @MaDP, @NgayLap, @NgayTT, @TienPhong, @TienDV, @GiamGia, @DatCoc, @TongTien, @PTTT, @MaVoucher, @TrangThai)";
            try
            {
                using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHD", hd.MaHoaDon);
                    cmd.Parameters.AddWithValue("@MaDP", hd.MaDatPhong ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                    cmd.Parameters.AddWithValue("@NgayTT", hd.NgayThanhToan);
                    cmd.Parameters.AddWithValue("@TienPhong", hd.TienPhong);
                    cmd.Parameters.AddWithValue("@TienDV", hd.TienDichVu);
                    cmd.Parameters.AddWithValue("@GiamGia", hd.TienGiamGia);
                    cmd.Parameters.AddWithValue("@DatCoc", hd.TienDatCoc);
                    cmd.Parameters.AddWithValue("@TongTien", hd.TongTien);
                    cmd.Parameters.AddWithValue("@PTTT", hd.MaPTTT ?? "TM");
                    cmd.Parameters.AddWithValue("@MaVoucher", hd.MaVoucher.HasValue ? (object)hd.MaVoucher.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", hd.TrangThai ?? "Đã thanh toán");

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi lưu Hóa Đơn: " + ex.Message, "SQL Error");
                return false;
            }
        }

        // --- HÀM NÂNG CẤP ĐÃ ĐƯỢC THÊM VÀO ĐÂY ---
        public bool LuuHoaDonVaCheckOut(HoaDon hd, string maDatPhong, IReadOnlyList<ChiTietHoaDon> chiTiet, int? maNhanVien)
        {
            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using NpgsqlTransaction trans = conn.BeginTransaction();
                try
                {
                    LeTanDatPhongDAL.KhoaDatPhong(conn, trans);
                    int id;
                    string maDat;
                    using (var don = new NpgsqlCommand(@"
SELECT MaDatPhong, MaDat, TienCoc, TongTien, TrangThai FROM DatPhong
WHERE MaDat = @Ma OR CAST(MaDatPhong AS text) = @Ma FOR UPDATE", conn, trans))
                    {
                        don.Parameters.AddWithValue("@Ma", maDatPhong);
                        using var reader = don.ExecuteReader();
                        if (!reader.Read()) throw new BLL.NghiepVuException("Không tìm thấy đơn đặt phòng cần thanh toán.");
                        id = Convert.ToInt32(reader["MaDatPhong"]);
                        maDat = Convert.ToString(reader["MaDat"]);
                        string trangThai = Convert.ToString(reader["TrangThai"]);
                        if (trangThai != "Đang ở" && trangThai != "Đã trả")
                            throw new BLL.NghiepVuException("Chỉ thanh toán đơn đã nhận phòng hoặc đã trả phòng.");
                        if (hd.TienDatCoc != Convert.ToDecimal(reader["TienCoc"]) || hd.TienPhong != Convert.ToDecimal(reader["TongTien"]))
                            throw new BLL.NghiepVuException("Tiền phòng/cọc không khớp đơn đã lưu. Hãy chọn lại đơn để tải số tiền mới nhất.");
                    }
                    using (var duplicate = new NpgsqlCommand(@"SELECT COUNT(*) FROM HoaDon
WHERE MaDatPhong IN (@Id, @Ma) AND TrangThai = 'Đã thanh toán'", conn, trans))
                    {
                        duplicate.Parameters.AddWithValue("@Id", id.ToString());
                        duplicate.Parameters.AddWithValue("@Ma", maDat);
                        if (Convert.ToInt32(duplicate.ExecuteScalar()) > 0)
                            throw new BLL.NghiepVuException("Đơn này đã được thanh toán. Vui lòng làm mới danh sách.");
                    }
                    hd.MaDatPhong = maDat;
                    // Module dịch vụ của nhóm tạo hóa đơn tạm. Thay bằng hóa đơn chốt
                    // trong cùng transaction để lỗi lưu chi tiết sẽ khôi phục cả dữ liệu tạm.
                    using (var clean = new NpgsqlCommand(@"
DELETE FROM ChiTietHoaDon WHERE MaHoaDon IN
    (SELECT MaHoaDon FROM HoaDon WHERE MaDatPhong IN (@Id, @Ma) AND TrangThai = 'Chưa thanh toán');
DELETE FROM HoaDon WHERE MaDatPhong IN (@Id, @Ma) AND TrangThai = 'Chưa thanh toán';", conn, trans))
                    {
                        clean.Parameters.AddWithValue("@Id", id.ToString());
                        clean.Parameters.AddWithValue("@Ma", maDat);
                        clean.ExecuteNonQuery();
                    }
                    // 1. Thêm mới Hóa Đơn
                    string sqlHoaDon = @"INSERT INTO HoaDon (MaHoaDon, MaDatPhong, NgayLap, NgayThanhToan, TienPhong, TienDichVu, TienGiamGia, TienDatCoc, TongTien, MaPTTT, MaVoucher, TrangThai)
                                         VALUES (@MaHD, @MaDP, @NgayLap, @NgayTT, @TienPhong, @TienDV, @GiamGia, @DatCoc, @TongTien, @PTTT, @MaVoucher, @TrangThai)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlHoaDon, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaHD", hd.MaHoaDon);
                        cmd.Parameters.AddWithValue("@MaDP", hd.MaDatPhong ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                        cmd.Parameters.AddWithValue("@NgayTT", hd.NgayThanhToan);
                        cmd.Parameters.AddWithValue("@TienPhong", hd.TienPhong);
                        cmd.Parameters.AddWithValue("@TienDV", hd.TienDichVu);
                        cmd.Parameters.AddWithValue("@GiamGia", hd.TienGiamGia);
                        cmd.Parameters.AddWithValue("@DatCoc", hd.TienDatCoc);
                        cmd.Parameters.AddWithValue("@TongTien", hd.TongTien);
                        cmd.Parameters.AddWithValue("@PTTT", hd.MaPTTT ?? "TM");
                        cmd.Parameters.AddWithValue("@MaVoucher", hd.MaVoucher.HasValue ? (object)hd.MaVoucher.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", hd.TrangThai ?? "Đã thanh toán");
                        cmd.ExecuteNonQuery();
                    }

                    // Chi tiết dịch vụ cùng transaction với hóa đơn và trả phòng.
                    foreach (var ct in chiTiet)
                    {
                        using var detail = new NpgsqlCommand(@"
INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, SoLuong, DonGia, ThanhTien)
VALUES (@HD, @DV, @SL, @Gia, @Tien)", conn, trans);
                        detail.Parameters.AddWithValue("@HD", hd.MaHoaDon);
                        detail.Parameters.AddWithValue("@DV", ct.MaDichVu);
                        detail.Parameters.AddWithValue("@SL", ct.SoLuong);
                        detail.Parameters.AddWithValue("@Gia", ct.DonGia);
                        detail.Parameters.AddWithValue("@Tien", ct.ThanhTien);
                        detail.ExecuteNonQuery();
                    }

                    // Cập nhật chính xác đơn đã khóa và kiểm tra ở trên.
                    if (!string.IsNullOrWhiteSpace(maDatPhong))
                    {
                        string sqlDatPhong = @"UPDATE DatPhong
                                               SET TrangThai = 'Đã trả',
                                                   GioTraThucTe = COALESCE(GioTraThucTe, CURRENT_TIMESTAMP)
                                               WHERE MaDatPhong = @MaDP AND TrangThai IN ('Đang ở', 'Đã trả')";
                        using (NpgsqlCommand cmdDP = new NpgsqlCommand(sqlDatPhong, conn, trans))
                        {
                            cmdDP.Parameters.AddWithValue("@MaDP", id);
                            if (cmdDP.ExecuteNonQuery() != 1)
                                throw new BLL.NghiepVuException("Không thể cập nhật trạng thái trả phòng.");
                        }
                    }

                    decimal hoanDu = hd.TienDatCoc - (hd.TienPhong + hd.TienDichVu - hd.TienGiamGia);
                    if (hoanDu > 0)
                        PhieuDatCocDAL.GhiPhieu(conn, trans, id, "Hoan", hoanDu, BLL.LeTanDatPhongBLL.PhuongThucGhiSoCoc(hd.MaPTTT),
                            maNhanVien.Value, "Hoàn cọc dư khi thanh toán " + hd.MaHoaDon + " qua " + hd.MaPTTT);
                    trans.Commit();
                    return true;
                }
                catch
                {
                    try { trans.Rollback(); } catch { }
                    throw;
                }
            }
        }
    }
}
