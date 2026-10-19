using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDichVu : Form
    {
        private string maDVDangChon = null;
        private DichVuDAL dichVuDAL = new DichVuDAL();
        private List<DichVu> danhSachGoc = new List<DichVu>();

        public frmDichVu()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvDichVu);
            if (cboLoaiDV.Items.Count > 0) cboLoaiDV.SelectedIndex = 0;
            if (cboDonViTinh.Items.Count > 0) cboDonViTinh.SelectedIndex = 0;
            TaiDanhSach();
        }

        private void DrawFigmaCard(object sender, PaintEventArgs e) => UITheme.DrawCard(sender, e);
        private void DrawFigmaInputBox(object sender, PaintEventArgs e) => UITheme.DrawInputBox(sender, e);

        private void DrawPillBorder(object sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Region = new Region(UITheme.GetRoundedRectPath(new Rectangle(0, 0, btn.Width, btn.Height), 6));
            }
        }

        private void TaiDanhSach()
        {
            try
            {
                danhSachGoc = dichVuDAL.GetAll();
                HienThiLenBang(danhSachGoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiLenBang(List<DichVu> danhSach)
        {
            dgvDichVu.DataSource = null;
            dgvDichVu.DataSource = danhSach;

            if (dgvDichVu.Columns["MaDichVu"] != null)
            {
                dgvDichVu.Columns["MaDichVu"].HeaderText = "MÃ DỊCH VỤ";
                dgvDichVu.Columns["MaDichVu"].FillWeight = 85;
                dgvDichVu.Columns["MaDichVu"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvDichVu.Columns["MaDichVu"].DefaultCellStyle.ForeColor = UITheme.DarkNavy;
            }
            if (dgvDichVu.Columns["TenDichVu"] != null)
            {
                dgvDichVu.Columns["TenDichVu"].HeaderText = "TÊN DỊCH VỤ";
                dgvDichVu.Columns["TenDichVu"].FillWeight = 160;
            }
            if (dgvDichVu.Columns["LoaiDichVu"] != null)
            {
                dgvDichVu.Columns["LoaiDichVu"].HeaderText = "LOẠI";
                dgvDichVu.Columns["LoaiDichVu"].FillWeight = 95;
            }
            if (dgvDichVu.Columns["DonGia"] != null)
            {
                dgvDichVu.Columns["DonGia"].HeaderText = "ĐƠN GIÁ";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.ForeColor = UITheme.PrimaryOrange;
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDichVu.Columns["DonGia"].FillWeight = 95;
            }
            if (dgvDichVu.Columns["DonViTinh"] != null)
            {
                dgvDichVu.Columns["DonViTinh"].HeaderText = "ĐVT";
                dgvDichVu.Columns["DonViTinh"].FillWeight = 60;
                dgvDichVu.Columns["DonViTinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvDichVu.Columns["TrangThai"] != null)
            {
                dgvDichVu.Columns["TrangThai"].HeaderText = "SỬ DỤNG";
                dgvDichVu.Columns["TrangThai"].FillWeight = 70;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string k = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(k))
            {
                HienThiLenBang(danhSachGoc);
            }
            else
            {
                var loc = danhSachGoc.Where(dv =>
                    (dv.MaDichVu != null && dv.MaDichVu.ToLower().Contains(k)) ||
                    (dv.TenDichVu != null && dv.TenDichVu.ToLower().Contains(k)) ||
                    (dv.LoaiDichVu != null && dv.LoaiDichVu.ToLower().Contains(k))).ToList();
                HienThiLenBang(loc);
            }
        }

        private void dgvDichVu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDichVu.CurrentRow == null) return;
            var dv = dgvDichVu.CurrentRow.DataBoundItem as DichVu;
            if (dv == null) return;

            maDVDangChon = dv.MaDichVu;
            txtMaDV.Text = dv.MaDichVu;
            txtMaDV.ReadOnly = true;
            txtTenDV.Text = dv.TenDichVu;
            cboLoaiDV.Text = dv.LoaiDichVu;
            txtDonGia.Text = dv.DonGia.ToString("N0");
            cboDonViTinh.Text = dv.DonViTinh;
            chkTrangThai.Checked = dv.TrangThai;

            btnLuu.Text = "Cập nhật";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text) || string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mã và tên dịch vụ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text.Replace(",", "").Replace(".", ""), out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            var dv = new DichVu
            {
                MaDichVu = txtMaDV.Text.Trim().ToUpper(),
                TenDichVu = txtTenDV.Text.Trim(),
                LoaiDichVu = cboLoaiDV.Text,
                DonGia = donGia,
                DonViTinh = cboDonViTinh.Text,
                TrangThai = chkTrangThai.Checked
            };

            if (!string.IsNullOrEmpty(maDVDangChon))
            {
                if (dichVuDAL.Update(dv))
                {
                    MessageBox.Show("Cập nhật thông tin dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoiForm();
                }
            }
            else
            {
                if (dichVuDAL.Insert(dv))
                {
                    MessageBox.Show("Thêm mới dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoiForm();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDVDangChon))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ {maDVDangChon}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (dichVuDAL.Delete(maDVDangChon))
            {
                MessageBox.Show("Đã xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSach();
                LamMoiForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoiForm();

        private void LamMoiForm()
        {
            maDVDangChon = null;
            txtMaDV.Clear();
            txtMaDV.ReadOnly = false;
            txtTenDV.Clear();
            txtDonGia.Clear();
            if (cboLoaiDV.Items.Count > 0) cboLoaiDV.SelectedIndex = 0;
            if (cboDonViTinh.Items.Count > 0) cboDonViTinh.SelectedIndex = 0;
            chkTrangThai.Checked = true;
            btnLuu.Text = "Lưu dịch vụ";
            txtTimKiem.Clear();
            dgvDichVu.ClearSelection();
        }
    }
}