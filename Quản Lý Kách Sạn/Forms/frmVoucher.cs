using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmVoucher : Form
    {
        private int? maVoucherDangChon = null;
        private VoucherDAL voucherDAL = new VoucherDAL();
        private List<Voucher> danhSachGoc = new List<Voucher>();

        public frmVoucher()
        {
            InitializeComponent();
        }

        private void frmVoucher_Load(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void TaiDanhSach()
        {
            try
            {
                danhSachGoc = voucherDAL.GetAll();
                HienThiLenBang(danhSachGoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiLenBang(List<Voucher> danhSach)
        {
            dgvVoucher.DataSource = null;
            dgvVoucher.DataSource = danhSach;

            // Tùy biến tiêu đề và ẩn các cột không cần hiển thị
            if (dgvVoucher.Columns["MaVoucher"] != null) dgvVoucher.Columns["MaVoucher"].HeaderText = "ID";
            if (dgvVoucher.Columns["Code"] != null) dgvVoucher.Columns["Code"].HeaderText = "Mã Code";
            if (dgvVoucher.Columns["LoaiGiam"] != null) dgvVoucher.Columns["LoaiGiam"].HeaderText = "Loại giảm";
            if (dgvVoucher.Columns["GiaTri"] != null)
            {
                dgvVoucher.Columns["GiaTri"].HeaderText = "Mức giảm";
                dgvVoucher.Columns["GiaTri"].DefaultCellStyle.Format = "N0";
            }
            if (dgvVoucher.Columns["DonToiThieu"] != null)
            {
                dgvVoucher.Columns["DonToiThieu"].HeaderText = "Đơn tối thiểu";
                dgvVoucher.Columns["DonToiThieu"].DefaultCellStyle.Format = "N0";
            }
            if (dgvVoucher.Columns["SoLuong"] != null) dgvVoucher.Columns["SoLuong"].HeaderText = "Số lượng";
            if (dgvVoucher.Columns["NgayBatDau"] != null)
            {
                dgvVoucher.Columns["NgayBatDau"].HeaderText = "Bắt đầu";
                dgvVoucher.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvVoucher.Columns["NgayHetHan"] != null)
            {
                dgvVoucher.Columns["NgayHetHan"].HeaderText = "Hết hạn";
                dgvVoucher.Columns["NgayHetHan"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvVoucher.Columns["TrangThai"] != null) dgvVoucher.Columns["TrangThai"].HeaderText = "Kích hoạt";
            if (dgvVoucher.Columns["GiamToiDa"] != null) dgvVoucher.Columns["GiamToiDa"].Visible = false;
            if (dgvVoucher.Columns["PhanTramGiam"] != null) dgvVoucher.Columns["PhanTramGiam"].Visible = false;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                HienThiLenBang(danhSachGoc);
            }
            else
            {
                var loc = danhSachGoc.Where(v => v.Code != null && v.Code.ToUpper().Contains(tuKhoa)).ToList();
                HienThiLenBang(loc);
            }
        }

        private void dgvVoucher_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVoucher.CurrentRow == null) return;
            var v = dgvVoucher.CurrentRow.DataBoundItem as Voucher;
            if (v == null) return;

            maVoucherDangChon = v.MaVoucher;
            txtCode.Text = v.Code;
            rdoPhanTram.Checked = v.LoaiGiam == "PhanTram";
            rdoSoTien.Checked = v.LoaiGiam == "SoTien";
            txtGiaTri.Text = v.GiaTri.ToString("N0");
            txtDonToiThieu.Text = v.DonToiThieu.ToString("N0");
            txtSoLuong.Text = v.SoLuong?.ToString() ?? "";
            dtpNgayBatDau.Value = v.NgayBatDau;
            dtpNgayHetHan.Value = v.NgayHetHan;
            chkTrangThai.Checked = v.TrangThai;
        }

        private bool KiemTraHopLe(out decimal giaTri, out decimal donToiThieu, out int? soLuong)
        {
            giaTri = 0;
            donToiThieu = 0;
            soLuong = null;

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã Voucher (Code).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }
            if (!decimal.TryParse(txtGiaTri.Text.Replace(",", "").Replace(".", ""), out giaTri) || giaTri <= 0)
            {
                MessageBox.Show("Giá trị giảm phải là số lớn hơn 0.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaTri.Focus();
                return false;
            }
            if (rdoPhanTram.Checked && giaTri > 100)
            {
                MessageBox.Show("Giảm theo phần trăm (%) không được vượt quá 100%.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaTri.Focus();
                return false;
            }
            if (!decimal.TryParse(txtDonToiThieu.Text.Replace(",", "").Replace(".", ""), out donToiThieu))
                donToiThieu = 0;

            if (!string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                if (!int.TryParse(txtSoLuong.Text.Trim(), out int sl) || sl < 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên không âm (hoặc để trống).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoLuong.Focus();
                    return false;
                }
                soLuong = sl;
            }
            if (dtpNgayBatDau.Value.Date > dtpNgayHetHan.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu phải trước ngày hết hạn.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe(out decimal giaTri, out decimal donToiThieu, out int? soLuong)) return;
            try
            {
                var v = new Voucher
                {
                    Code = txtCode.Text.Trim().ToUpper(),
                    LoaiGiam = rdoPhanTram.Checked ? "PhanTram" : "SoTien",
                    GiaTri = giaTri,
                    DonToiThieu = donToiThieu,
                    SoLuong = soLuong,
                    NgayBatDau = dtpNgayBatDau.Value.Date,
                    NgayHetHan = dtpNgayHetHan.Value.Date,
                    TrangThai = chkTrangThai.Checked
                };
                voucherDAL.Insert(v);
                MessageBox.Show("Thêm voucher thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSach();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm (mã Code có thể đã tồn tại): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maVoucherDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn voucher cần chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraHopLe(out decimal giaTri, out decimal donToiThieu, out int? soLuong)) return;

            try
            {
                var v = new Voucher
                {
                    MaVoucher = maVoucherDangChon.Value,
                    Code = txtCode.Text.Trim().ToUpper(),
                    LoaiGiam = rdoPhanTram.Checked ? "PhanTram" : "SoTien",
                    GiaTri = giaTri,
                    DonToiThieu = donToiThieu,
                    SoLuong = soLuong,
                    NgayBatDau = dtpNgayBatDau.Value.Date,
                    NgayHetHan = dtpNgayHetHan.Value.Date,
                    TrangThai = chkTrangThai.Checked
                };
                voucherDAL.Update(v);
                MessageBox.Show("Cập nhật thông tin voucher thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSach();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maVoucherDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn voucher cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mã voucher này không?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                voucherDAL.Delete(maVoucherDangChon.Value);
                MessageBox.Show("Đã xóa voucher khỏi hệ thống.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSach();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa (voucher có thể đã liên kết với hóa đơn): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoiForm();

        private void LamMoiForm()
        {
            maVoucherDangChon = null;
            txtCode.Clear();
            txtGiaTri.Clear();
            txtDonToiThieu.Clear();
            txtSoLuong.Clear();
            rdoPhanTram.Checked = true;
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayHetHan.Value = DateTime.Now.AddMonths(1);
            chkTrangThai.Checked = true;
            txtTimKiem.Clear();
            dgvVoucher.ClearSelection();
        }
    }
}