using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmHoaDon : Form
    {
        private BindingList<ChiTietHoaDon> danhSachDichVuChon = new BindingList<ChiTietHoaDon>();
        private ThongTinDatPhongThanhToan thongTinDangChon = null;
        private Voucher voucherDaApDung = null;

        // Khởi tạo các đối tượng BLL & DAL
        private DichVuDAL dichVuDAL = new DichVuDAL();
        private PhuongThucThanhToanDAL phuongThucDAL = new PhuongThucThanhToanDAL();
        private ThanhToanDAL thanhToanDAL = new ThanhToanDAL();
        private VoucherBLL voucherBLL = new VoucherBLL();
        private HoaDonBLL hoaDonBLL = new HoaDonBLL();

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            TaiDanhSachDatPhong();
            TaiDanhSachDichVu();
            TaiPhuongThucThanhToan();
            dgvDichVuDaChon.DataSource = danhSachDichVuChon;
            CapNhatTongTien();
        }

        private void TaiDanhSachDatPhong()
        {
            try
            {
                cboDatPhong.DataSource = ThanhToanDAL.GetDanhSachChuaThanhToan();
                cboDatPhong.DisplayMember = "SoPhong";
                cboDatPhong.ValueMember = "MaDatPhong";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đặt phòng: " + ex.Message);
            }
        }

        private void TaiDanhSachDichVu()
        {
            try
            {
                cboDichVu.DataSource = dichVuDAL.GetAll();
                cboDichVu.DisplayMember = "TenDichVu";
                cboDichVu.ValueMember = "MaDichVu";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách dịch vụ: " + ex.Message);
            }
        }

        private void TaiPhuongThucThanhToan()
        {
            try
            {
                cboPhuongThuc.DataSource = phuongThucDAL.GetAll();
                cboPhuongThuc.DisplayMember = "TenPhuongThuc";
                cboPhuongThuc.ValueMember = "MaPhuongThuc";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phương thức thanh toán: " + ex.Message);
            }
        }

        private void cboDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            thongTinDangChon = cboDatPhong.SelectedItem as ThongTinDatPhongThanhToan;
            danhSachDichVuChon.Clear();
            voucherDaApDung = null;
            txtVoucher.Clear();
            lblKetQuaVoucher.Text = "";
            CapNhatTongTien();
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            if (thongTinDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn lượt đặt phòng trước.");
                return;
            }
            var dv = cboDichVu.SelectedItem as DichVu;
            if (dv == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ.");
                return;
            }

            danhSachDichVuChon.Add(new ChiTietHoaDon
            {
                MaDichVu = dv.MaDichVu,
                NoiDung = dv.TenDichVu,
                SoLuong = (int)numSoLuong.Value,
                DonGia = dv.DonGia,
                ThanhTien = dv.DonGia * numSoLuong.Value
            });

            CapNhatTongTien();
        }

        private void btnXoaDongDichVu_Click(object sender, EventArgs e)
        {
            if (dgvDichVuDaChon.CurrentRow == null) return;
            var ct = dgvDichVuDaChon.CurrentRow.DataBoundItem as ChiTietHoaDon;
            if (ct != null) danhSachDichVuChon.Remove(ct);
            CapNhatTongTien();
        }

        private void btnApDungVoucher_Click(object sender, EventArgs e)
        {
            if (thongTinDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn lượt đặt phòng trước.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtVoucher.Text))
            {
                voucherDaApDung = null;
                lblKetQuaVoucher.Text = "";
                CapNhatTongTien();
                return;
            }

            decimal tongTruocGiam = thongTinDangChon.TienPhong + TinhTienDichVu();
            string maVoucher = txtVoucher.Text.Trim().ToUpper();

            // Kiểm tra áp dụng voucher qua BLL
            var v = voucherBLL.KiemTraVoucher(maVoucher);

            if (v == null)
            {
                voucherDaApDung = null;
                lblKetQuaVoucher.ForeColor = System.Drawing.Color.Red;
                lblKetQuaVoucher.Text = "Mã voucher không hợp lệ hoặc đã hết hạn!";
            }
            else
            {
                voucherDaApDung = v;
                lblKetQuaVoucher.ForeColor = System.Drawing.Color.Green;
                lblKetQuaVoucher.Text = "Áp dụng voucher thành công!";
            }
            CapNhatTongTien();
        }

        private decimal TinhTienDichVu()
        {
            return danhSachDichVuChon.Sum(ct => ct.SoLuong * ct.DonGia);
        }

        private void CapNhatTongTien()
        {
            decimal tienPhong = thongTinDangChon != null ? thongTinDangChon.TienPhong : 0;
            decimal tienCoc = thongTinDangChon != null ? thongTinDangChon.TienCoc : 0;
            decimal tienDichVu = TinhTienDichVu();
            decimal tongTruocGiam = tienPhong + tienDichVu;

            decimal tienGiam = 0;
            if (voucherDaApDung != null)
            {
                // Sửa lỗi: Truyền Code dạng chuỗi hoặc ToString() MaVoucher
                string maVC = !string.IsNullOrEmpty(voucherDaApDung.Code)
                    ? voucherDaApDung.Code
                    : voucherDaApDung.MaVoucher.ToString();
                tienGiam = voucherBLL.TinhTienGiam(maVC, tongTruocGiam);
            }

            decimal tongTien = tongTruocGiam - tienGiam - tienCoc;
            if (tongTien < 0) tongTien = 0;

            lblGiaTriTienPhong.Text = tienPhong.ToString("N0") + " đ";
            lblGiaTriTienDichVu.Text = tienDichVu.ToString("N0") + " đ";
            lblGiaTriTienCoc.Text = tienCoc.ToString("N0") + " đ";
            lblGiaTriGiamGia.Text = tienGiam.ToString("N0") + " đ";
            lblGiaTriTongTien.Text = tongTien.ToString("N0") + " đ";
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (thongTinDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn lượt đặt phòng cần thanh toán.");
                return;
            }
            if (cboPhuongThuc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán.");
                return;
            }

            var xacNhan = MessageBox.Show(
                $"Xác nhận lập hóa đơn với tổng tiền {lblGiaTriTongTien.Text}?",
                "Xác nhận", MessageBoxButtons.YesNo);
            if (xacNhan != DialogResult.Yes) return;

            var phuongThuc = cboPhuongThuc.SelectedItem as Quản_Lý_Khách_Sạn.Models.PhuongThucThanhToan;

            try
            {
                string loi = "";
                string maDatPhongStr = thongTinDangChon.MaDatPhong != null ? thongTinDangChon.MaDatPhong.ToString() : "";
                string maVoucherStr = voucherDaApDung != null
                    ? (!string.IsNullOrEmpty(voucherDaApDung.Code) ? voucherDaApDung.Code : voucherDaApDung.MaVoucher.ToString())
                    : null;
                string maPhuongThucStr = phuongThuc != null ? phuongThuc.MaPhuongThuc : null;

                // Gọi hàm lập hóa đơn qua BLL
                int maHoaDon = hoaDonBLL.LapHoaDon(
                    maDatPhongStr,
                    thongTinDangChon.TienPhong,
                    danhSachDichVuChon.ToList(),
                    maVoucherStr,
                    maPhuongThucStr,
                    thongTinDangChon.TienCoc,
                    out loi);

                if (maHoaDon == -1)
                {
                    MessageBox.Show("Không lập được hóa đơn: " + loi);
                    return;
                }

                MessageBox.Show($"Lập hóa đơn #{maHoaDon} thành công!");

                // Làm mới form
                danhSachDichVuChon.Clear();
                voucherDaApDung = null;
                txtVoucher.Clear();
                lblKetQuaVoucher.Text = "";
                TaiDanhSachDatPhong();
                CapNhatTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lập hóa đơn: " + ex.Message);
            }
        }
    }
}