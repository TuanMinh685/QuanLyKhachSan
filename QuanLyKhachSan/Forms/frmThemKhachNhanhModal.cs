using System;
using System.Drawing;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public class frmThemKhachNhanhModal : Form
    {
        private KhachHangBLL bll = new KhachHangBLL();

        public int MaKH { get; private set; }
        public string HoTen { get; private set; }
        public string SoDienThoai { get; private set; }
        public string MaKHHienThi { get; private set; }

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlDivider;

        private TextBox txtHoTen;
        private TextBox txtSDT;
        private TextBox txtCCCD;
        private ComboBox cboGioiTinh;
        private TextBox txtQuocTich;
        private TextBox txtDiaChi;

        private Panel pnlFooter;
        private Button btnLuu;
        private Button btnHuy;

        public frmThemKhachNhanhModal()
        {
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);
        }

        private void InitializeComponentCustom()
        {
            Text = "Thêm khách hàng mới";
            Size = new Size(540, 520);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = UITheme.MainFont;

            // Header
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Color.White,
                Padding = new Padding(24, 14, 24, 8)
            };

            lblTitle = new Label
            {
                Text = "➕ KHAI BÁO HỒ SƠ KHÁCH HÀNG MỚI",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Thông tin sẽ được lưu vào cơ sở dữ liệu và hiển thị ngay bên Hồ sơ khách hàng",
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

            // Footer
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
                Text = "💾 Lưu & Chọn khách này",
                Size = new Size(190, 40),
                Location = new Point(pnlFooter.Width - 310, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += (_, __) => LuuKhachHang();

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

            // Body
            Panel pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(28, 16, 28, 16),
                BackColor = Color.White
            };

            int y = 14;
            int gap = 58;

            // Họ tên
            TaoNhan(pnlBody, "Họ và tên khách hàng *", 28, y);
            var pnlHoTen = TaoBox(pnlBody, 28, y + 20, 468, 36);
            txtHoTen = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Ví dụ: Trần Văn Bình" };
            pnlHoTen.Controls.Add(txtHoTen);

            // Số điện thoại & CCCD
            y += gap;
            TaoNhan(pnlBody, "Số điện thoại *", 28, y);
            var pnlSDT = TaoBox(pnlBody, 28, y + 20, 220, 36);
            txtSDT = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "09xxxxxxxx" };
            pnlSDT.Controls.Add(txtSDT);

            TaoNhan(pnlBody, "Số CCCD / CMND / Hộ chiếu *", 262, y);
            var pnlCCCD = TaoBox(pnlBody, 262, y + 20, 234, 36);
            txtCCCD = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "12 số CCCD" };
            pnlCCCD.Controls.Add(txtCCCD);

            // Giới tính & Quốc tịch
            y += gap;
            TaoNhan(pnlBody, "Giới tính", 28, y);
            var pnlGioiTinh = TaoBox(pnlBody, 28, y + 20, 220, 36);
            cboGioiTinh = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F) };
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;
            pnlGioiTinh.Controls.Add(cboGioiTinh);

            TaoNhan(pnlBody, "Quốc tịch", 262, y);
            var pnlQuocTich = TaoBox(pnlBody, 262, y + 20, 234, 36);
            txtQuocTich = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, Text = "Việt Nam" };
            pnlQuocTich.Controls.Add(txtQuocTich);

            // Địa chỉ
            y += gap;
            TaoNhan(pnlBody, "Địa chỉ thường trú", 28, y);
            var pnlDiaChi = TaoBox(pnlBody, 28, y + 20, 468, 36);
            txtDiaChi = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Số nhà, tên đường, tỉnh/thành..." };
            pnlDiaChi.Controls.Add(txtDiaChi);

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

        private void LuuKhachHang()
        {
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string cccd = txtCCCD.Text.Trim();

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            KhachHang kh = new KhachHang
            {
                HoTen = hoTen,
                DienThoai = sdt,
                SoGiayTo = string.IsNullOrWhiteSpace(cccd) ? null : cccd,
                GioiTinh = cboGioiTinh.Text,
                QuocTich = string.IsNullOrWhiteSpace(txtQuocTich.Text) ? "Việt Nam" : txtQuocTich.Text.Trim(),
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                DongYNhanUuDai = true
            };

            var kq = bll.Them(kh);
            if (!kq.ThanhCong)
            {
                string msg = kq.ThongBao;
                if (kq.LoiTheoTruong.Count > 0)
                {
                    msg += "\n" + string.Join("\n", kq.LoiTheoTruong.Values);
                }
                MessageBox.Show(msg, "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy lại hồ sơ vừa tạo để có mã hiển thị KH000xxx
            var khMoi = bll.LayTheoMa(kq.MaKhachHang);
            MaKH = kq.MaKhachHang;
            HoTen = khMoi?.HoTen ?? kh.HoTen;
            SoDienThoai = khMoi?.DienThoai ?? kh.DienThoai;
            MaKHHienThi = khMoi?.MaKH ?? ("KH" + kq.MaKhachHang.ToString("D6"));

            MessageBox.Show($"Đã tạo mới khách hàng [{HoTen}] ({MaKHHienThi}) thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
