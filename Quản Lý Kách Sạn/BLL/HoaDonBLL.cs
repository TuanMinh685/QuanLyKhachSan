using System;
using System.Collections.Generic;
using Quản_Lý_Khách_Sạn.Models;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.BLL
{
    public class HoaDonBLL
    {
        public int LapHoaDon(string maDatPhong, decimal tienPhong, List<ChiTietHoaDon> danhSachDichVu, string maVoucher, string maPhuongThuc, decimal tienCoc, out string loi)
        {
            loi = "";
            try
            {
                // Logic xử lý lưu hóa đơn
                return 1;
            }
            catch (Exception ex)
            {
                loi = ex.Message;
                return -1;
            }
        }
    }
}