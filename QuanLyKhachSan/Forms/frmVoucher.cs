using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmVoucher : Form
    {
        private int? maVoucherDangChon = null;
        private VoucherBLL voucherBLL = new VoucherBLL();
        private List<Voucher> danhSachGoc = new List<Voucher>();

        // Controls giao diện nâng cấp hiện đại
        private Panel pnlHeaderLeft;
        private Label lblTitleLeft;
        private Label lblSubtitleLeft;
        private FlowLayoutPanel pnlStatsPills;
        private Label lblPillTotal;
        private Label lblPillActive;
        private Label lblPillExpired;

        public frmVoucher()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
            KhoiTaoGiaoDienNangCao();
        }

        private void KhoiTaoGiaoDienNangCao()
        {
            // 1. Tạo Header và Thẻ thống kê (Stats Pills) ở bên trái
            pnlHeaderLeft = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = Color.Transparent
            };

            lblTitleLeft = new Label
            {
                Text = "🏷️ CHƯƠNG TRÌNH KHUYẾN MÃI & VOUCHER",
                Font = new Font("Segoe UI", 12.5f, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };

            lblSubtitleLeft = new Label
            {
                Text = "Cấu hình mã chiết khấu phòng, tỷ lệ % hoặc tiền mặt và thời hạn áp dụng",
                Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                ForeColor = UITheme.TextMuted,
                Location = new Point(0, 26),
                AutoSize = true
            };

            pnlStatsPills = new FlowLayoutPanel
            {
                Location = new Point(0, 52),
                Height = 30,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            lblPillTotal = TaoTheThongKe("Tổng: 0 mã", Color.FromArgb(241, 245, 249), UITheme.DarkNavy);
            lblPillActive = TaoTheThongKe("🟢 Đang áp dụng: 0", Color.FromArgb(220, 252, 231), Color.FromArgb(22, 163, 74));
            lblPillExpired = TaoTheThongKe("⏳ Hết hạn / Khóa: 0", Color.FromArgb(254, 226, 226), Color.FromArgb(220, 38, 38));

            pnlStatsPills.Controls.Add(lblPillTotal);
            pnlStatsPills.Controls.Add(lblPillActive);
            pnlStatsPills.Controls.Add(lblPillExpired);

            pnlHeaderLeft.Controls.Add(lblTitleLeft);
            pnlHeaderLeft.Controls.Add(lblSubtitleLeft);
            pnlHeaderLeft.Controls.Add(pnlStatsPills);

            cardLeft.Controls.Add(pnlHeaderLeft);

            // Cân đối vị trí tìm kiếm và grid
            pnlSearch.Location = new Point(20, 108);
            pnlSearch.Height = 40;

            pnlGridWrapper.Location = new Point(20, 160);
            pnlGridWrapper.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            cardLeft.Resize += (s, e) =>
            {
                pnlGridWrapper.Size = new Size(cardLeft.ClientSize.Width - 40, cardLeft.ClientSize.Height - 180);
            };

            // 2. Nâng cấp nút bấm bên phải với icon và màu chuẩn SaaS
            btnLuu.Text = "💾 Lưu Voucher";
            btnLamMoi.Text = "🔄 Làm mới";
            btnXoa.Text = "🗑 Xóa Voucher";

            btnLuu.BackColor = UITheme.PrimaryOrange;
            btnLuu.ForeColor = Color.White;

            btnLamMoi.BackColor = Color.FromArgb(248, 250, 252);
            btnLamMoi.ForeColor = UITheme.TextMain;

            btnXoa.BackColor = Color.FromArgb(254, 242, 242);
            btnXoa.ForeColor = Color.FromArgb(220, 38, 38);

            // 3. Sự kiện định dạng tiền / số trực tiếp khi gõ
            txtGiaTri.TextChanged += (s, e) => DinhDangOChuSo(txtGiaTri);
            txtDonToiThieu.TextChanged += (s, e) => DinhDangOChuSo(txtDonToiThieu);

            // 4. Sự kiện định dạng hiển thị bảng
            dgvVoucher.CellFormatting += DgvVoucher_CellFormatting;
        }

        private Label TaoTheThongKe(string text, Color bg, Color fg)
        {
            var lbl = new Label
            {
                Text = text,
                BackColor = bg,
                ForeColor = fg,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(10, 4, 10, 4),
                Margin = new Padding(0, 0, 8, 0),
                Cursor = Cursors.Default
            };
            lbl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, lbl.Width - 1, lbl.Height - 1);
                using (GraphicsPath path = UITheme.GetRoundedRectPath(rect, 6))
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 1f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };
            return lbl;
        }

        private void DinhDangOChuSo(TextBox tb)
        {
            if (tb.Focused)
            {
                string raw = Regex.Replace(tb.Text, @"[^\d]", "");
                if (decimal.TryParse(raw, out decimal val))
                {
                    tb.Text = val.ToString("N0");
                    tb.SelectionStart = tb.Text.Length;
                }
            }
        }

        private void DgvVoucher_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var column = dgvVoucher.Columns[e.ColumnIndex];
            string col = column.Name;

            if (col == "TrangThai" && !(column is DataGridViewCheckBoxColumn) && e.Value != null)
            {
                bool active = Convert.ToBoolean(e.Value);
                e.Value = active ? "● Đang kích hoạt" : "● Tạm ngưng";
                e.CellStyle.ForeColor = active ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);
                e.CellStyle.Font = new Font("Segoe UI", 9.25f, active ? FontStyle.Bold : FontStyle.Regular);
                e.FormattingApplied = true;
            }
            else if (col == "LoaiGiam" && e.Value != null)
            {
                string loai = e.Value.ToString();
                e.Value = (loai == "PhanTram") ? "Giảm theo %" : "Tiền mặt (VNĐ)";
                e.FormattingApplied = true;
            }
            else if (col == "GiaTri" && e.Value != null)
            {
                var row = dgvVoucher.Rows[e.RowIndex].DataBoundItem as Voucher;
                if (row != null && decimal.TryParse(e.Value.ToString(), out decimal val))
                {
                    e.Value = (row.LoaiGiam == "PhanTram") ? $"{val:N0} %" : $"{val:N0} đ";
                    e.CellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    e.CellStyle.ForeColor = UITheme.PrimaryOrange;
                    e.FormattingApplied = true;
                }
            }
            else if (col == "DonToiThieu" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal val))
                {
                    e.Value = $"{val:N0} đ";
                    e.FormattingApplied = true;
                }
            }
        }

        private void frmVoucher_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvVoucher);
            TaiDanhSach();
            LamMoiForm();
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
                danhSachGoc = voucherBLL.LayDanhSachVoucher();
                HienThiLenBang(danhSachGoc);
                CapNhatThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatThongKe()
        {
            int total = danhSachGoc.Count;
            int active = danhSachGoc.Count(v => v.TrangThai && v.NgayHetHan >= DateTime.Now.Date);
            int expired = total - active;

            lblPillTotal.Text = $"Tổng số: {total} mã";
            lblPillActive.Text = $"🟢 Đang áp dụng: {active}";
            lblPillExpired.Text = $"⏳ Hết hạn / Khóa: {expired}";
        }

        private void HienThiLenBang(List<Voucher> danhSach)
        {
            dgvVoucher.DataSource = null;
            dgvVoucher.DataSource = danhSach;

            // Ẩn tất cả các cột phụ dư thừa
            foreach (DataGridViewColumn col in dgvVoucher.Columns)
            {
                col.Visible = false;
            }

            if (dgvVoucher.Columns["Code"] != null)
            {
                dgvVoucher.Columns["Code"].Visible = true;
                dgvVoucher.Columns["Code"].HeaderText = "MÃ VOUCHER";
                dgvVoucher.Columns["Code"].FillWeight = 95;
                dgvVoucher.Columns["Code"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvVoucher.Columns["Code"].DefaultCellStyle.ForeColor = UITheme.DarkNavy;
            }
            if (dgvVoucher.Columns["LoaiGiam"] != null)
            {
                dgvVoucher.Columns["LoaiGiam"].Visible = true;
                dgvVoucher.Columns["LoaiGiam"].HeaderText = "HÌNH THỨC";
                dgvVoucher.Columns["LoaiGiam"].FillWeight = 80;
                dgvVoucher.Columns["LoaiGiam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvVoucher.Columns["GiaTri"] != null)
            {
                dgvVoucher.Columns["GiaTri"].Visible = true;
                dgvVoucher.Columns["GiaTri"].HeaderText = "MỨC GIẢM";
                dgvVoucher.Columns["GiaTri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvVoucher.Columns["GiaTri"].FillWeight = 85;
            }
            if (dgvVoucher.Columns["DonToiThieu"] != null)
            {
                dgvVoucher.Columns["DonToiThieu"].Visible = true;
                dgvVoucher.Columns["DonToiThieu"].HeaderText = "ĐƠN TỐI THIỂU";
                dgvVoucher.Columns["DonToiThieu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvVoucher.Columns["DonToiThieu"].FillWeight = 90;
            }
            if (dgvVoucher.Columns["SoLuong"] != null)
            {
                dgvVoucher.Columns["SoLuong"].Visible = true;
                dgvVoucher.Columns["SoLuong"].HeaderText = "SL";
                dgvVoucher.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVoucher.Columns["SoLuong"].FillWeight = 40;
            }
            if (dgvVoucher.Columns["NgayHetHan"] != null)
            {
                dgvVoucher.Columns["NgayHetHan"].Visible = true;
                dgvVoucher.Columns["NgayHetHan"].HeaderText = "HẾT HẠN";
                dgvVoucher.Columns["NgayHetHan"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvVoucher.Columns["NgayHetHan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVoucher.Columns["NgayHetHan"].FillWeight = 75;
            }
            if (dgvVoucher.Columns["TrangThai"] is DataGridViewCheckBoxColumn)
            {
                int idx = dgvVoucher.Columns["TrangThai"].Index;
                dgvVoucher.Columns.Remove("TrangThai");
                var col = new DataGridViewTextBoxColumn
                {
                    Name = "TrangThai",
                    DataPropertyName = "TrangThai",
                    HeaderText = "TRẠNG THÁI",
                    FillWeight = 80,
                    Visible = true
                };
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVoucher.Columns.Insert(idx, col);
            }
            else if (dgvVoucher.Columns["TrangThai"] != null)
            {
                dgvVoucher.Columns["TrangThai"].Visible = true;
                dgvVoucher.Columns["TrangThai"].HeaderText = "TRẠNG THÁI";
                dgvVoucher.Columns["TrangThai"].FillWeight = 80;
                dgvVoucher.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

            lblCardTitle.Text = "✏️ Cập nhật Voucher: " + v.Code;
            lblCardSubtitle.Text = "Đang chỉnh sửa chương trình khuyến mãi đã chọn";
            btnLuu.Text = "💾 Cập nhật";
            btnXoa.Enabled = true;
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

            string rawGiaTri = Regex.Replace(txtGiaTri.Text, @"[^\d]", "");
            if (!decimal.TryParse(rawGiaTri, out giaTri) || giaTri <= 0)
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

            string rawDonToiThieu = Regex.Replace(txtDonToiThieu.Text, @"[^\d]", "");
            if (!decimal.TryParse(rawDonToiThieu, out donToiThieu))
                donToiThieu = 0;

            if (!string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                if (!int.TryParse(txtSoLuong.Text.Trim(), out int sl) || sl < 0)
                {
                    MessageBox.Show("Số lượng phát hành phải là số nguyên không âm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            string code = txtCode.Text.Trim().ToUpper();

            var v = new Voucher
            {
                Code = code,
                TenChuongTrinh = "Ưu đãi phòng khách sạn",
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
                if (voucherBLL.CapNhatVoucher(v))
                {
                    MessageBox.Show($"Cập nhật Voucher [{v.Code}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoiForm();
                }
            }
            else
            {
                // Kiểm tra trùng code
                if (danhSachGoc.Any(x => string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show($"Mã Voucher [{code}] đã tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Focus();
                    return;
                }

                if (voucherBLL.ThemVoucher(v))
                {
                    MessageBox.Show($"Tạo mới mã Voucher [{v.Code}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoiForm();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maVoucherDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn voucher cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa mã voucher [{txtCode.Text}]?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (voucherBLL.XoaVoucher(maVoucherDangChon.Value))
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
            txtCode.Text = GợiÝMãVoucher();
            txtGiaTri.Clear();
            txtDonToiThieu.Clear();
            txtSoLuong.Text = "50";
            rdoPhanTram.Checked = true;
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayHetHan.Value = DateTime.Now.AddMonths(1);
            chkTrangThai.Checked = true;

            lblCardTitle.Text = "✨ Thêm mới Voucher";
            lblCardSubtitle.Text = "Thiết lập chương trình chiết khấu mới cho khách lưu trú";
            btnLuu.Text = "💾 Lưu Voucher";
            btnXoa.Enabled = false;

            txtTimKiem.Clear();
            dgvVoucher.ClearSelection();
        }

        private string GợiÝMãVoucher()
        {
            string namThang = DateTime.Now.ToString("yyMM");
            return $"VIP{namThang}";
        }
    }
}
