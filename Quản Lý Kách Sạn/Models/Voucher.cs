using System;

namespace Quản_Lý_Khách_Sạn.Models
{
    public class Voucher
    {
        private int _maVoucher = 0;

        // Khóa chính kiểu int? nhưng tự ép kiểu không gây lỗi
        public int? MaVoucher
        {
            get => _maVoucher;
            set => _maVoucher = value.HasValue ? value.Value : 0;
        }

        public int Id
        {
            get => _maVoucher;
            set => _maVoucher = value;
        }

        public string Code
        {
            get => _maVoucher.ToString();
            set
            {
                if (int.TryParse(value, out int v))
                    _maVoucher = v;
            }
        }

        public string TenChuongTrinh { get; set; } = string.Empty;
        public decimal GiaTri { get; set; }
        public decimal GiaTriGiam
        {
            get => GiaTri;
            set => GiaTri = value;
        }

        public string LoaiGiam { get; set; } = "PhanTram";
        public string LoaiGiamGia
        {
            get => LoaiGiam;
            set => LoaiGiam = value;
        }

        public decimal DonToiThieu { get; set; }
        public decimal DonGiaToiThieu
        {
            get => DonToiThieu;
            set => DonToiThieu = value;
        }

        public decimal GiamToiDa { get; set; } = 0;
        public DateTime NgayBatDau { get; set; } = DateTime.Now.AddDays(-1);
        public DateTime NgayHetHan { get; set; } = DateTime.Now.AddMonths(1);
        public DateTime NgayKetThuc
        {
            get => NgayHetHan;
            set => NgayHetHan = value;
        }

        public int SoLuong { get; set; } = 100;
        public bool TrangThai { get; set; } = true;
        public bool ConHieuLuc
        {
            get => TrangThai;
            set => TrangThai = value;
        }
    }
}