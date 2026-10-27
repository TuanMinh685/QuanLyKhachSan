using System;
using System.Drawing;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmPhuongThucTTModal : Form
    {
        private PhuongThucThanhToanDAL dal = new PhuongThucThanhToanDAL();
        private PhuongThucThanhToanModel modelHienTai = null;

        public frmPhuongThucTTModal(PhuongThucThanhToanModel model = null)
        {
            modelHienTai = model;
            InitializeComponent();
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);

            if (modelHienTai != null)
            {
                lblTitle.Text = "✏️ CHỈNH SỬA PHƯƠNG THỨC THANH TOÁN";
                lblSubtitle.Text = "Cập nhật tên cổng thu ngân hoặc cấu hình trạng thái hoạt động";
                btnLuu.Text = "💾 Cập nhật";
                btnLuu.BackColor = UITheme.PrimaryBlue;
                DoDuLieu(modelHienTai);
            }
            else
            {
                lblTitle.Text = "➕ THÊM MỚI PHƯƠNG THỨC THANH TOÁN";
                lblSubtitle.Text = "Thiết lập kênh thu ngân mới (Tiền mặt, VietQR, Thẻ POS ngân hàng...)";
                btnLuu.Text = "💾 Lưu phương thức";
                btnLuu.BackColor = UITheme.PrimaryOrange;
            }
        }

        private void InitializeComponentCustom()
        {
            Text = "Phương thức thanh toán";
            Size = new Size(520, 420);
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
                Text = "➕ THÊM MỚI PHƯƠNG THỨC THANH TOÁN",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Thiết lập kênh thu ngân mới cho khách sạn",
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
                Size = new Size(160, 40),
                Location = new Point(pnlFooter.Width - 280, 12),
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

            TaoNhan(pnlBody, "Tên phương thức thanh toán *", 28, y);
            var pnlTen = TaoBox(pnlBody, 28, y + 20, 448, 36);
            txtTen = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Ví dụ: Chuyển khoản VietQR, Tiền mặt..." };
            pnlTen.Controls.Add(txtTen);

            y += gap;
            TaoNhan(pnlBody, "Mô tả / Hướng dẫn thanh toán", 28, y);
            var pnlMoTa = TaoBox(pnlBody, 28, y + 20, 448, 36);
            txtMoTa = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Tài khoản ngân hàng, máy POS..." };
            pnlMoTa.Controls.Add(txtMoTa);

            y += gap + 8;
            chkKichHoat = new CheckBox
            {
                Text = "🟢 Kích hoạt kênh thanh toán (Cho phép chọn lúc lập hóa đơn)",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(28, y),
                AutoSize = true,
                Checked = true
            };
            pnlBody.Controls.Add(chkKichHoat);

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

        private void DoDuLieu(PhuongThucThanhToanModel m)
        {
            txtTen.Text = m.TenPhuongThuc;
            txtMoTa.Text = m.MoTa;
            chkKichHoat.Checked = m.KichHoat;
        }

        private void LuuDuLieu()
        {
            string ten = txtTen.Text.Trim();
            if (string.IsNullOrWhiteSpace(ten))
            {
                MessageBox.Show("Vui lòng nhập tên phương thức thanh toán.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            string moTa = txtMoTa.Text.Trim();
            bool kichHoat = chkKichHoat.Checked;

            if (modelHienTai == null)
            {
                if (dal.Insert(ten, moTa, kichHoat))
                {
                    MessageBox.Show($"Thêm mới phương thức [{ten}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể thêm phương thức thanh toán.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (dal.Update(modelHienTai.MaPhuongThuc, ten, moTa, kichHoat))
                {
                    MessageBox.Show($"Cập nhật phương thức [{ten}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật phương thức thanh toán.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
