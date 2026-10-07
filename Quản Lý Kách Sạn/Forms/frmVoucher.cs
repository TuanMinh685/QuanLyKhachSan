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
            ThietKeBangChuanFigma();
            TaiDanhSach();
        }

        #region Figma Modern Styling (Paint Handlers)

        // Bo góc & viền mềm mại cho 2 Khối Card (Card Left & Card Right)
        private void DrawFigmaCard(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 10;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#E2E8F0"), 1.2f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        // Vẽ viền thanh mảnh chuẩn Figma cho từng ô nhập liệu
        private void DrawFigmaInputBox(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 6;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 1f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        // Vẽ bo góc nhẹ cho nút hành động chính (btnLuu)
        private void DrawPillBorder(object sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Region = new Region(GetRoundedRectPath(new Rectangle(0, 0, btn.Width, btn.Height), 6));
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2F;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void ThietKeBangChuanFigma()
        {
            dgvVoucher.EnableHeadersVisualStyles = false;
            dgvVoucher.BorderStyle = BorderStyle.None;
            dgvVoucher.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVoucher.GridColor = ColorTranslator.FromHtml("#F1F5F9");
            dgvVoucher.BackgroundColor = Color.White;
            dgvVoucher.RowHeadersVisible = false;
            dgvVoucher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVoucher.MultiSelect = false;
            dgvVoucher.AllowUserToAddRows = false;
            dgvVoucher.AllowUserToResizeRows = false;
            dgvVoucher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Header xám nhạt tối giản theo Figma
            dgvVoucher.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            dgvVoucher.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#475569");
            dgvVoucher.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.25f, FontStyle.Bold);
            dgvVoucher.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 10, 12, 10);
            dgvVoucher.ColumnHeadersHeight = 44;
            dgvVoucher.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Rows
            dgvVoucher.RowTemplate.Height = 42;
            dgvVoucher.DefaultCellStyle.BackColor = Color.White;
            dgvVoucher.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1E293B");
            dgvVoucher.DefaultCellStyle.Font = new Font("Segoe UI", 9.25f);
            dgvVoucher.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#EFF6FF");
            dgvVoucher.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#1D4ED8");
            dgvVoucher.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
        }

        #endregion

        #region Data & Operations

        private void TaiDanhSach()
        {
            try
            {
                danhSachGoc = voucherDAL.GetAll();
                HienThiLenBang(danhSachGoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiLenBang(List<Voucher> danhSach)
        {
            dgvVoucher.DataSource = null;
            dgvVoucher.DataSource = danhSach;

            // Ẩn tất cả các cột dư thừa
            string[] cotAn = { "Id", "TenChuongTrinh", "GiaTriGiam", "LoaiGiamGia", "DonGiaToiThieu", "GiamToiDa", "PhanTramGiam", "NgayKetThuc", "ConHieuLuc" };
            foreach (var col in cotAn)
            {
                if (dgvVoucher.Columns[col] != null) dgvVoucher.Columns[col].Visible = false;
            }

            // Định dạng cột hiển thị đẹp mắt
            if (dgvVoucher.Columns["MaVoucher"] != null)
            {
                dgvVoucher.Columns["MaVoucher"].HeaderText = "#";
                dgvVoucher.Columns["MaVoucher"].FillWeight = 30;
                dgvVoucher.Columns["MaVoucher"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvVoucher.Columns["Code"] != null)
            {
                dgvVoucher.Columns["Code"].HeaderText = "MÃ VOUCHER";
                dgvVoucher.Columns["Code"].FillWeight = 95;
                dgvVoucher.Columns["Code"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvVoucher.Columns["Code"].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#0F2537");
            }
            if (dgvVoucher.Columns["LoaiGiam"] != null)
            {
                dgvVoucher.Columns["LoaiGiam"].HeaderText = "HÌNH THỨC";
                dgvVoucher.Columns["LoaiGiam"].FillWeight = 75;
                dgvVoucher.Columns["LoaiGiam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvVoucher.Columns["GiaTri"] != null)
            {
                dgvVoucher.Columns["GiaTri"].HeaderText = "MỨC GIẢM";
                dgvVoucher.Columns["GiaTri"].DefaultCellStyle.Format = "N0";
                dgvVoucher.Columns["GiaTri"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvVoucher.Columns["GiaTri"].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#EA580C");
                dgvVoucher.Columns["GiaTri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvVoucher.Columns["GiaTri"].FillWeight = 85;
            }
            if (dgvVoucher.Columns["DonToiThieu"] != null)
            {
                dgvVoucher.Columns["DonToiThieu"].HeaderText = "ĐƠN TỐI THIỂU";
                dgvVoucher.Columns["DonToiThieu"].DefaultCellStyle.Format = "N0";
                dgvVoucher.Columns["DonToiThieu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvVoucher.Columns["DonToiThieu"].FillWeight = 90;
            }
            if (dgvVoucher.Columns["SoLuong"] != null)
            {
                dgvVoucher.Columns["SoLuong"].HeaderText = "SL";
                dgvVoucher.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVoucher.Columns["SoLuong"].FillWeight = 40;
            }
            if (dgvVoucher.Columns["NgayBatDau"] != null)
            {
                dgvVoucher.Columns["NgayBatDau"].HeaderText = "BẮT ĐẦU";
                dgvVoucher.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvVoucher.Columns["NgayBatDau"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVoucher.Columns["NgayBatDau"].FillWeight = 75;
            }
            if (dgvVoucher.Columns["NgayHetHan"] != null)
            {
                dgvVoucher.Columns["NgayHetHan"].HeaderText = "HẾT HẠN";
                dgvVoucher.Columns["NgayHetHan"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvVoucher.Columns["NgayHetHan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVoucher.Columns["NgayHetHan"].FillWeight = 75;
            }
            if (dgvVoucher.Columns["TrangThai"] != null)
            {
                dgvVoucher.Columns["TrangThai"].HeaderText = "KÍCH HOẠT";
                dgvVoucher.Columns["TrangThai"].FillWeight = 60;
            }
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
            rdoPhanTram.Checked = (v.LoaiGiam == "PhanTram");
            rdoSoTien.Checked = (v.LoaiGiam == "SoTien");
            txtGiaTri.Text = v.GiaTri.ToString("N0");
            txtDonToiThieu.Text = v.DonToiThieu.ToString("N0");
            txtSoLuong.Text = v.SoLuong.ToString();
            dtpNgayBatDau.Value = v.NgayBatDau;
            dtpNgayHetHan.Value = v.NgayHetHan;
            chkTrangThai.Checked = v.TrangThai;

            btnLuu.Text = "Cập nhật";
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
                MessageBox.Show("Mức giảm phải là số lớn hơn 0.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Số lượng phải là số nguyên không âm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe(out decimal giaTri, out decimal donToiThieu, out int? soLuong)) return;

            var v = new Voucher
            {
                Code = txtCode.Text.Trim().ToUpper(),
                TenChuongTrinh = "Ưu đãi phòng",
                LoaiGiam = rdoPhanTram.Checked ? "PhanTram" : "SoTien",
                GiaTri = giaTri,
                DonToiThieu = donToiThieu,
                SoLuong = soLuong ?? 100,
                NgayBatDau = dtpNgayBatDau.Value.Date,
                NgayHetHan = dtpNgayHetHan.Value.Date,
                TrangThai = chkTrangThai.Checked
            };

            if (maVoucherDangChon.HasValue)
            {
                v.MaVoucher = maVoucherDangChon.Value;
                if (voucherDAL.Update(v))
                {
                    MessageBox.Show("Cập nhật thông tin voucher thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoiForm();
                }
            }
            else
            {
                if (voucherDAL.Insert(v))
                {
                    MessageBox.Show("Tạo voucher mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoiForm();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maVoucherDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn voucher cần xóa trên bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mã voucher này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (voucherDAL.Delete(maVoucherDangChon.Value))
            {
                MessageBox.Show("Đã xóa voucher thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSach();
                LamMoiForm();
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
            btnLuu.Text = "Lưu Voucher";
            txtTimKiem.Clear();
            dgvVoucher.ClearSelection();
        }

        #endregion
    }
}