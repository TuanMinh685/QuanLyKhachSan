using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmHoaDon : Form
    {
        private HoaDonBLL bll = new HoaDonBLL();
        private VoucherDAL voucherDal = new VoucherDAL();
        private Voucher currentVoucher = null;

        private decimal tienPhong = 2900000m;
        private decimal tienCoc = 1000000m;
        private List<ChiTietHoaDon> listDV = new List<ChiTietHoaDon>();

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvChiTietDichVu);
            cboDatPhong.SelectedIndex = 0;
            cboPTTT.SelectedIndex = 0;
            LoadMockData();
            CapNhatChiPhi();
        }

        private void cboDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDatPhong.SelectedIndex == 0)
            {
                tienPhong = 2900000m;
                tienCoc = 1000000m;
            }
            else if (cboDatPhong.SelectedIndex == 1)
            {
                tienPhong = 1800000m;
                tienCoc = 500000m;
            }
            else
            {
                tienPhong = 950000m;
                tienCoc = 300000m;
            }
            CapNhatChiPhi();
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            listDV.Add(new ChiTietHoaDon
            {
                TenDichVu = "Nước suối / Khăn lạnh",
                SoLuong = 1,
                DonGia = 15000
            });
            RefreshGrid();
            CapNhatChiPhi();
        }

        private void btnXoaDongDichVu_Click(object sender, EventArgs e)
        {
            if (dgvChiTietDichVu.CurrentRow != null)
            {
                int index = dgvChiTietDichVu.CurrentRow.Index;
                if (index >= 0 && index < listDV.Count)
                {
                    listDV.RemoveAt(index);
                    RefreshGrid();
                    CapNhatChiPhi();
                }
            }
        }

        private void btnApDungVoucher_Click(object sender, EventArgs e)
        {
            string code = txtVoucher.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                currentVoucher = null;
                CapNhatChiPhi();
                return;
            }

            currentVoucher = voucherDal.GetByCode(code);
            if (currentVoucher == null)
            {
                currentVoucher = new Voucher
                {
                    Code = code,
                    LoaiGiam = "PhanTram",
                    GiaTri = 10,
                    TrangThai = true,
                    NgayHetHan = DateTime.Now.AddDays(10)
                };
            }

            MessageBox.Show("Áp dụng mã ưu đãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CapNhatChiPhi();
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            string pttt = cboPTTT.SelectedItem?.ToString() ?? "Tiền mặt";
            DialogResult dr = MessageBox.Show($"Xác nhận thanh toán {lblTongTien.Text} qua [{pttt}] và trả phòng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Thanh toán thành công! Đang tiến hành in hóa đơn...", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void LoadMockData()
        {
            listDV = new List<ChiTietHoaDon>
            {
                new ChiTietHoaDon { TenDichVu = "Nước ngọt lon", SoLuong = 3, DonGia = 20000 },
                new ChiTietHoaDon { TenDichVu = "Giặt ủi quần áo", SoLuong = 2, DonGia = 45000 },
                new ChiTietHoaDon { TenDichVu = "Bữa sáng Buffet", SoLuong = 2, DonGia = 120000 }
            };
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgvChiTietDichVu.DataSource = null;
            dgvChiTietDichVu.DataSource = listDV;
            if (dgvChiTietDichVu.Columns["MaHoaDon"] != null) dgvChiTietDichVu.Columns["MaHoaDon"].Visible = false;
            if (dgvChiTietDichVu.Columns["MaDichVu"] != null) dgvChiTietDichVu.Columns["MaDichVu"].Visible = false;
        }

        private void CapNhatChiPhi()
        {
            decimal tienDV = bll.TinhTongTienDichVu(listDV);
            decimal tienGiam = bll.TinhGiamGiaVoucher(currentVoucher, tienPhong + tienDV);
            decimal tong = bll.TinhTongThanhToan(tienPhong, tienDV, tienGiam, tienCoc);

            lblTienPhong.Text = $"{tienPhong:N0} đ";
            lblTienDichVu.Text = $"{tienDV:N0} đ";
            lblCoc.Text = $"- {tienCoc:N0} đ";
            lblTienGiamGia.Text = $"- {tienGiam:N0} đ";
            lblTongTien.Text = $"{tong:N0} đ";
        }
    }
}