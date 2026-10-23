using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmVoucherModal : Form
    {
        private VoucherBLL bll = new VoucherBLL();
        private Voucher voucherHienTai = null;

        public frmVoucherModal(Voucher v = null)
        {
            voucherHienTai = v;
            InitializeComponent();
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);

            if (voucherHienTai != null)
            {
                lblTitle.Text = "✏️ CHỈNH SỬA CHƯƠNG TRÌNH VOUCHER";
                lblSubtitle.Text = "Cập nhật mã giảm giá, mức chiết khấu, số lượng hoặc thời hạn";
                btnLuu.Text = "💾 Cập nhật";
                btnLuu.BackColor = UITheme.PrimaryBlue;
                DoDuLieu(voucherHienTai);
            }
            else
            {
                lblTitle.Text = "➕ TẠO MỚI CHƯƠNG TRÌNH VOUCHER";
                lblSubtitle.Text = "Thiết lập mã khuyến mãi chiết khấu phòng cho khách hàng";
                btnLuu.Text = "💾 Lưu Voucher";
                btnLuu.BackColor = UITheme.PrimaryOrange;
                txtCode.Text = "VC" + DateTime.Now.ToString("MMdd");
            }
        }

        private void InitializeComponentCustom()
        {
            Text = "Thông tin Voucher khuyến mãi";
            Size = new Size(580, 580);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = UITheme.MainFont;

            // 1. Header
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Color.White,
                Padding = new Padding(24, 14, 24, 8)
            };

            lblTitle = new Label
            {
                Text = "➕ TẠO MỚI CHƯƠNG TRÌNH VOUCHER",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Thiết lập mã khuyến mãi chiết khấu phòng cho khách hàng",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, 40),
                AutoSize = true
            };

            pnlDivider = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = UITheme.BorderColor
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(pnlDivider);
            Controls.Add(pnlHeader);

            // 2. Footer
            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(24, 12, 24, 12)
            };
            pnlFooter.Paint += (s, e) =>
            {
                using var p = new Pen(UITheme.BorderColor, 1F);
                e.Graphics.DrawLine(p, 0, 0, pnlFooter.Width, 0);
            };

            btnLuu = new Button
            {
                Size = new Size(150, 40),
                Location = new Point(pnlFooter.Width - 270, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += (_, __) => LuuDuLieu();

            btnHuy = new Button
            {
                Text = "✕ Đóng",
                Size = new Size(95, 40),
                Location = new Point(pnlFooter.Width - 110, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UITheme.TextMuted,
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand
            };
            btnHuy.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnHuy.Click += (_, __) => Close();

            pnlFooter.Controls.Add(btnLuu);
            pnlFooter.Controls.Add(btnHuy);
            Controls.Add(pnlFooter);

            // 3. Body Content
            Panel pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(28, 16, 28, 16),
                BackColor = Color.White
            };

            int y = 14;
            int gap = 58;

            // Mã Code & Tên Chương Trình
            TaoNhan(pnlBody, "Mã Voucher (Code) *", 28, y);
            var pnlCode = TaoBox(pnlBody, 28, y + 20, 160, 36);
            txtCode = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), CharacterCasing = CharacterCasing.Upper, Dock = DockStyle.Fill, ForeColor = UITheme.DarkNavy, PlaceholderText = "GIAM10" };
            pnlCode.Controls.Add(txtCode);

            TaoNhan(pnlBody, "Tên chương trình ưu đãi *", 204, y);
            var pnlTen = TaoBox(pnlBody, 204, y + 20, 332, 36);
            txtTen = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Ưu đãi mùa hè, Tri ân khách hàng..." };
            pnlTen.Controls.Add(txtTen);

            // Loại giảm & Mức giảm
            y += gap;
            TaoNhan(pnlBody, "Loại giảm giá *", 28, y);
            var pnlLoai = TaoBox(pnlBody, 28, y + 20, 240, 36);
            cboLoaiGiam = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F) };
            cboLoaiGiam.Items.AddRange(new object[] { "Phần trăm (%)", "Số tiền cố định (VNĐ)" });
            cboLoaiGiam.SelectedIndex = 0;
            pnlLoai.Controls.Add(cboLoaiGiam);

            TaoNhan(pnlBody, "Giá trị giảm *", 284, y);
            var pnlGiaTri = TaoBox(pnlBody, 284, y + 20, 252, 36);
            txtGiaTri = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = UITheme.PrimaryOrange, Dock = DockStyle.Fill, TextAlign = HorizontalAlignment.Right, PlaceholderText = "10 hoặc 100,000" };
            pnlGiaTri.Controls.Add(txtGiaTri);

            // Đơn tối thiểu & Số lượng
            y += gap;
            TaoNhan(pnlBody, "Đơn hàng tối thiểu (VNĐ)", 28, y);
            var pnlMin = TaoBox(pnlBody, 28, y + 20, 240, 36);
            txtDonToiThieu = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, TextAlign = HorizontalAlignment.Right, Text = "0" };
            pnlMin.Controls.Add(txtDonToiThieu);

            TaoNhan(pnlBody, "Số lượng phát hành *", 284, y);
            nudSoLuong = new NumericUpDown
            {
                Location = new Point(284, y + 20),
                Size = new Size(252, 36),
                Minimum = 1,
                Maximum = 10000,
                Value = 100,
                Font = new Font("Segoe UI", 10F),
                TextAlign = HorizontalAlignment.Center
            };
            pnlBody.Controls.Add(nudSoLuong);

            // Ngày bắt đầu & Ngày hết hạn
            y += gap;
            TaoNhan(pnlBody, "Ngày bắt đầu áp dụng", 28, y);
            dtpBatDau = new DateTimePicker { Location = new Point(28, y + 20), Size = new Size(240, 36), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            pnlBody.Controls.Add(dtpBatDau);

            TaoNhan(pnlBody, "Ngày hết hạn", 284, y);
            dtpHetHan = new DateTimePicker { Location = new Point(284, y + 20), Size = new Size(252, 36), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F), Value = DateTime.Now.AddMonths(1) };
            pnlBody.Controls.Add(dtpHetHan);

            // Trạng thái kích hoạt
            y += gap + 8;
            chkTrangThai = new CheckBox
            {
                Text = "🟢 Kích hoạt mã voucher (Cho phép áp dụng khi thu ngân tính tiền)",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(28, y),
                AutoSize = true,
                Checked = true
            };
            pnlBody.Controls.Add(chkTrangThai);

            Controls.Add(pnlBody);
        }

        private static Label TaoNhan(Control parent, string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(x, y),
                AutoSize = true
            };
            parent.Controls.Add(lbl);
            return lbl;
        }

        private static Panel TaoBox(Control parent, int x, int y, int w, int h)
        {
            var pnl = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = Color.White,
                Padding = new Padding(8, 7, 8, 4)
            };
            pnl.Paint += (s, e) => UITheme.DrawInputBox(s, e, 6);
            parent.Controls.Add(pnl);
            return pnl;
        }

        private void DoDuLieu(Voucher v)
        {
            txtCode.Text = v.Code;
            txtTen.Text = v.TenChuongTrinh;
            cboLoaiGiam.SelectedIndex = v.LoaiGiam == "PhanTram" ? 0 : 1;
            txtGiaTri.Text = v.GiaTri.ToString("N0");
            txtDonToiThieu.Text = v.DonToiThieu.ToString("N0");
            nudSoLuong.Value = Math.Max(1, Math.Min(10000, v.SoLuong));
            dtpBatDau.Value = v.NgayBatDau;
            dtpHetHan.Value = v.NgayHetHan;
            chkTrangThai.Checked = v.TrangThai;
        }

        private void LuuDuLieu()
        {
            string code = txtCode.Text.Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập mã Voucher.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chương trình.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            string rawGiaTri = Regex.Replace(txtGiaTri.Text, @"[^\d]", "");
            if (!decimal.TryParse(rawGiaTri, out decimal giaTri) || giaTri <= 0)
            {
                MessageBox.Show("Mức giảm không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaTri.Focus();
                return;
            }

            string rawMin = Regex.Replace(txtDonToiThieu.Text, @"[^\d]", "");
            decimal.TryParse(rawMin, out decimal donMin);

            if (dtpHetHan.Value.Date < dtpBatDau.Value.Date)
            {
                MessageBox.Show("Ngày hết hạn không được trước ngày bắt đầu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string loai = cboLoaiGiam.SelectedIndex == 0 ? "PhanTram" : "SoTien";
            if (loai == "PhanTram" && giaTri > 100)
            {
                MessageBox.Show("Chiết khấu phần trăm không được vượt quá 100%.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var v = new Voucher
            {
                Code = code,
                TenChuongTrinh = txtTen.Text.Trim(),
                LoaiGiam = loai,
                GiaTri = giaTri,
                DonToiThieu = donMin,
                NgayBatDau = dtpBatDau.Value.Date,
                NgayHetHan = dtpHetHan.Value.Date,
                SoLuong = (int)nudSoLuong.Value,
                TrangThai = chkTrangThai.Checked
            };

            if (voucherHienTai == null)
            {
                if (bll.ThemVoucher(v))
                {
                    MessageBox.Show($"Tạo mới mã Voucher [{v.Code}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể tạo voucher. Mã Code có thể đã tồn tại.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                v.MaVoucher = voucherHienTai.MaVoucher;
                if (bll.CapNhatVoucher(v))
                {
                    MessageBox.Show($"Cập nhật mã Voucher [{v.Code}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật voucher.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
