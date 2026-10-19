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

        public bool ThanhToanVaTraPhong(HoaDon hd, string maPhong)
        {
            // Kiểm tra logic nghiệp vụ cơ bản trước khi đẩy xuống CSDL
            if (hd == null || hd.TongTien < 0 || string.IsNullOrEmpty(maPhong))
            {
                return false;
            }
            return dal.LuuHoaDonVaCheckOut(hd, maPhong);
        }
    }
}