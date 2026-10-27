using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDichVuModal : Form
    {
        private DichVuDAL dal = new DichVuDAL();
        private DichVu dichVuHienTai = null;

        public frmDichVuModal(DichVu dv = null)
        {
            dichVuHienTai = dv;
            InitializeComponent();
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);

            if (dichVuHienTai != null)
            {
                lblTitle.Text = "✏️ CHỈNH SỬA DỊCH VỤ PHÒNG";
                lblSubtitle.Text = "Cập nhật bảng giá, đơn vị tính hoặc trạng thái phục vụ";
                btnLuu.Text = "💾 Cập nhật";
                btnLuu.BackColor = UITheme.PrimaryBlue;
                DoDuLieu(dichVuHienTai);
            }
            else
            {
                lblTitle.Text = "➕ THÊM MỚI DỊCH VỤ PHÒNG";
                lblSubtitle.Text = "Khai báo dịch vụ ăn uống, giặt ủi, đưa đón mới vào hệ thống";
                btnLuu.Text = "💾 Lưu dịch vụ";
                btnLuu.BackColor = UITheme.PrimaryOrange;
                txtMaDV.Text = "DV" + DateTime.Now.ToString("MMddHHmm");
            }
        }

        private void InitializeComponentCustom()
        {
            Text = "Thông tin Dịch vụ phòng";
            Size = new Size(540, 500);
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
                Text = "➕ THÊM MỚI DỊCH VỤ PHÒNG",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Khai báo dịch vụ ăn uống, giặt ủi, đưa đón mới vào hệ thống",
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

            // Mã & Tên DV
            TaoNhan(pnlBody, "Mã dịch vụ", 28, y);
            var pnlMa = TaoBox(pnlBody, 28, y + 20, 140, 36);
            txtMaDV = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), Dock = DockStyle.Fill, ReadOnly = true, BackColor = Color.White, ForeColor = UITheme.DarkNavy };
            pnlMa.Controls.Add(txtMaDV);

            TaoNhan(pnlBody, "Tên dịch vụ *", 182, y);
            var pnlTen = TaoBox(pnlBody, 182, y + 20, 314, 36);
            txtTenDV = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Ví dụ: Nước suối Lavie, Giặt ủi sơ mi..." };
            pnlTen.Controls.Add(txtTenDV);

            // Phân loại dịch vụ
            y += gap;
            TaoNhan(pnlBody, "Nhóm / Loại dịch vụ *", 28, y);
            var pnlLoai = TaoBox(pnlBody, 28, y + 20, 468, 36);
            cboLoaiDV = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F) };
            cboLoaiDV.Items.AddRange(new object[] { "Ăn uống", "Giặt ủi", "Đưa đón", "Giải trí & Spa", "Tiện ích khác" });
            cboLoaiDV.SelectedIndex = 0;
            pnlLoai.Controls.Add(cboLoaiDV);

            // Đơn vị tính & Đơn giá
            y += gap;
            TaoNhan(pnlBody, "Đơn vị tính *", 28, y);
            var pnlDvt = TaoBox(pnlBody, 28, y + 20, 210, 36);
            cboDonViTinh = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F) };
            cboDonViTinh.Items.AddRange(new object[] { "Lon", "Chai", "Phần", "Kg", "Chuyến", "Lần", "Giờ", "Vé" });
            cboDonViTinh.SelectedIndex = 0;
            pnlDvt.Controls.Add(cboDonViTinh);

            TaoNhan(pnlBody, "Đơn giá (VNĐ) *", 252, y);
            var pnlGia = TaoBox(pnlBody, 252, y + 20, 244, 36);
            txtDonGia = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = UITheme.PrimaryOrange, Dock = DockStyle.Fill, TextAlign = HorizontalAlignment.Right, PlaceholderText = "0" };
            txtDonGia.TextChanged += (_, __) =>
            {
                if (txtDonGia.Focused)
                {
                    string raw = Regex.Replace(txtDonGia.Text, @"[^\d]", "");
                    if (decimal.TryParse(raw, out decimal val))
                    {
                        txtDonGia.Text = val.ToString("N0");
                        txtDonGia.SelectionStart = txtDonGia.Text.Length;
                    }
                }
            };
            pnlGia.Controls.Add(txtDonGia);

            // Trạng thái phục vụ
            y += gap + 10;
            chkTrangThai = new CheckBox
            {
                Text = "🟢 Đang phục vụ khách hàng (Hiển thị lúc thanh toán)",
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

        private void DoDuLieu(DichVu dv)
        {
            txtMaDV.Text = dv.MaDichVu;
            txtTenDV.Text = dv.TenDichVu;

            int idxLoai = cboLoaiDV.FindStringExact(dv.LoaiDichVu);
            cboLoaiDV.SelectedIndex = idxLoai >= 0 ? idxLoai : 0;

            int idxDvt = cboDonViTinh.FindStringExact(dv.DonViTinh);
            cboDonViTinh.SelectedIndex = idxDvt >= 0 ? idxDvt : 0;

            txtDonGia.Text = dv.DonGia.ToString("N0");
            chkTrangThai.Checked = dv.TrangThai;
        }

        private void LuuDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên dịch vụ.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDV.Focus();
                return;
            }

            string rawGia = Regex.Replace(txtDonGia.Text, @"[^\d]", "");
            if (!decimal.TryParse(rawGia, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            var dv = new DichVu
            {
                MaDichVu = txtMaDV.Text.Trim(),
                TenDichVu = txtTenDV.Text.Trim(),
                LoaiDichVu = cboLoaiDV.Text,
                DonViTinh = cboDonViTinh.Text,
                DonGia = donGia,
                TrangThai = chkTrangThai.Checked
            };

            if (dichVuHienTai == null)
            {
                if (dal.Insert(dv))
                {
                    MessageBox.Show($"Thêm mới dịch vụ [{dv.TenDichVu}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể thêm dịch vụ. Mã dịch vụ có thể đã tồn tại.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (dal.Update(dv))
                {
                    MessageBox.Show($"Cập nhật dịch vụ [{dv.TenDichVu}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật dịch vụ.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
