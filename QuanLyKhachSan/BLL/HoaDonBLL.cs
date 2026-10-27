using System;
using System.Collections.Generic;
using Quản_Lý_Khách_Sạn.Models;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.BLL
{
    public class HoaDonBLL
    {
        private HoaDonDAL dal = new HoaDonDAL();

        // Các hàm tính toán giữ nguyên của bạn
        public decimal TinhTongTienDichVu(List<ChiTietHoaDon> cthdList)
        {
            if (cthdList == null) return 0;
            decimal sum = 0;
            foreach (var item in cthdList)
            {
                sum += item.ThanhTien;
            }
            return sum;
        }

        public decimal TinhGiamGiaVoucher(Voucher voucher, decimal tongTienTruocGiam)
        {
            if (voucher == null || !voucher.ConHieuLuc) return 0;
            if (DateTime.Now < voucher.NgayBatDau || DateTime.Now > voucher.NgayKetThuc) return 0;
            if (tongTienTruocGiam < voucher.DonGiaToiThieu) return 0;

            decimal discount = 0;
            if (voucher.LoaiGiamGia != null && voucher.LoaiGiamGia.Equals("PhanTram", StringComparison.OrdinalIgnoreCase))
            {
                discount = tongTienTruocGiam * (voucher.GiaTriGiam / 100m);
                if (voucher.GiamToiDa > 0 && discount > voucher.GiamToiDa)
                {
                    discount = voucher.GiamToiDa;
                }
            }
            else
            {
                discount = voucher.GiaTriGiam;
            }

            return discount > tongTienTruocGiam ? tongTienTruocGiam : discount;
        }

        public decimal TinhTongThanhToan(decimal tienPhong, decimal tienDichVu, decimal tienGiamGia, decimal tienDatCoc)
        {
            decimal total = (tienPhong + tienDichVu) - tienGiamGia - tienDatCoc;
            return total > 0 ? total : 0;
        }

        // --- HÀM NÂNG CẤP GIAO TIẾP VỚI DAL ---
        public List<HoaDon> LayDanhSachHoaDon()
        {
            return dal.GetAll();
        }

        public bool ThemHoaDon(HoaDon hd)
        {
            return dal.Insert(hd);
        }

        public bool ThanhToanVaTraPhong(HoaDon hd, string maPhong, IReadOnlyList<ChiTietHoaDon> chiTiet, int? maNhanVien)
        {
            // Kiểm tra logic nghiệp vụ cơ bản trước khi đẩy xuống CSDL
            if (hd == null || string.IsNullOrWhiteSpace(hd.MaHoaDon) || string.IsNullOrWhiteSpace(maPhong) || chiTiet == null)
                throw new NghiepVuException("Vui lòng chọn đơn đặt phòng và nhập mã hóa đơn.");
            decimal tongDV = 0;
            foreach (var ct in chiTiet)
            {
                if (string.IsNullOrWhiteSpace(ct.MaDichVu) || ct.SoLuong <= 0 || ct.DonGia < 0)
                    throw new NghiepVuException("Chi tiết dịch vụ có số lượng hoặc đơn giá không hợp lệ.");
                tongDV += ct.ThanhTien;
            }
            if (hd.TienPhong < 0 || hd.TienDatCoc < 0 || hd.TienGiamGia < 0 || hd.TienGiamGia > hd.TienPhong + tongDV)
                throw new NghiepVuException("Tiền phòng, tiền cọc hoặc giảm giá không hợp lệ.");
            hd.TienDichVu = tongDV;
            hd.TongTien = TinhTongThanhToan(hd.TienPhong, tongDV, hd.TienGiamGia, hd.TienDatCoc);
            if (hd.TienDatCoc > hd.TienPhong + tongDV - hd.TienGiamGia)
            {
                if (!maNhanVien.HasValue || maNhanVien <= 0)
                    throw new NghiepVuException("Vui lòng đăng nhập lại để ghi nhận nhân viên hoàn cọc dư.");
                LeTanDatPhongBLL.PhuongThucGhiSoCoc(hd.MaPTTT);
            }
            return dal.LuuHoaDonVaCheckOut(hd, maPhong, chiTiet, maNhanVien);
        }
    }
}
