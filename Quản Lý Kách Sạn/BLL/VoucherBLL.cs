using System;
using System.Linq;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.BLL
{
    public class VoucherBLL
    {
        private VoucherDAL voucherDAL = new VoucherDAL();

        public Voucher KiemTraVoucher(string maVoucher)
        {
            if (string.IsNullOrWhiteSpace(maVoucher)) return null;
            return voucherDAL.GetAll().FirstOrDefault(v => v.MaVoucher.ToString().Trim().Equals(maVoucher.Trim(), StringComparison.OrdinalIgnoreCase) && v.TrangThai && v.NgayHetHan >= DateTime.Now);
        }

        public decimal TinhTienGiam(string maVoucher, decimal tongTien)
        {
            var voucher = KiemTraVoucher(maVoucher);
            if (voucher == null) return 0;

            decimal tienGiam = tongTien * (voucher.PhanTramGiam / 100m);
            if (voucher.GiamToiDa > 0 && tienGiam > voucher.GiamToiDa)
            {
                tienGiam = voucher.GiamToiDa;
            }
            return tienGiam;
        }
    }
}