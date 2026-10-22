using System.Collections.Generic;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.BLL
{
    public class VoucherBLL
    {
        private VoucherDAL dal = new VoucherDAL();

        public List<Voucher> LayDanhSachVoucher()
        {
            return dal.GetAll();
        }

        public Voucher TimVoucherTheoMa(int id)
        {
            return dal.GetById(id);
        }

        public Voucher TimVoucherTheoMa(int? id)
        {
            if (!id.HasValue) return null;
            return dal.GetById(id.Value);
        }

        public Voucher TimVoucherTheoCode(string code)
        {
            return dal.GetByCode(code);
        }

        public bool ThemVoucher(Voucher v)
        {
            return dal.Insert(v);
        }

        public bool CapNhatVoucher(Voucher v)
        {
            return dal.Update(v);
        }

        public bool XoaVoucher(int id)
        {
            return dal.Delete(id);
        }

        public bool XoaVoucher(int? id)
        {
            if (!id.HasValue) return false;
            return dal.Delete(id.Value);
        }

        public bool GiamSoLuongVoucher(int id)
        {
            return dal.GiamSoLuong(id);
        }
    }
}