using System;

namespace Quản_Lý_Khách_Sạn.Models
{
    public class Voucher
    {
        public int MaVoucher { get; set; }
        public string Code { get; set; } = string.Empty;
        public string LoaiGiam { get; set; } = "PhanTram";
        public decimal GiaTri { get; set; }
        public decimal PhanTramGiam
        {
            get => GiaTri;
            set => GiaTri = value;
        }

        public decimal DonToiThieu { get; set; }
        public decimal GiamToiDa { get; set; }
        public int? SoLuong { get; set; }
        public DateTime NgayBatDau { get; set; } = DateTime.Now;
        public DateTime NgayHetHan { get; set; } = DateTime.Now.AddMonths(1);
        public bool TrangThai { get; set; } = true;
    }
}