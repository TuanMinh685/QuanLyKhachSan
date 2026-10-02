using Quản_Lý_Khách_Sạn.DAL;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;
using System.Collections.Generic;

namespace QuanLyKhachSan.BLL
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
    }
}