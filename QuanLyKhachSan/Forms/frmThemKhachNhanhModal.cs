using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmThemKhachNhanhModal : Form
    {
        private readonly KhachHangBLL bll = new KhachHangBLL();

        public int MaKH { get; private set; }
        public string HoTen { get; private set; }
        public string SoDienThoai { get; private set; }
        public string MaKHHienThi { get; private set; }

        public frmThemKhachNhanhModal()
        {
            InitializeComponent();
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);
        }

        private void InitializeComponentCustom()
        {
            Text = "Khai báo thông tin khách hàng mới";
            Size = new Size(620, 680);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = UITheme.MainFont;

            // 1. HEADER PANEL
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 74,
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
                Text = "Lưu hồ sơ khách vào hệ thống và tự động chọn cho phòng đang đặt",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, 42),
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

            // 2. FOOTER PANEL
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
                Size = new Size(205, 40),
                Location = new Point(pnlFooter.Width - 325, 12),
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

            // 3. BODY PANEL (Chứa đầy đủ các trường thông tin chuẩn)
            pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(28, 16, 28, 20),
                BackColor = Color.White
            };

            int y = 14;
            int gap = 58;
            int fullW = 546;
            int halfW = 265;
            int col2X = 309;

            // Dòng 1: Họ và tên khách hàng *
            TaoNhan(pnlBody, "Họ và tên khách hàng *", 28, y);
            var pnlHoTen = TaoBox(pnlBody, 28, y + 20, fullW, 36);
            txtHoTen = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.8F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Dock = DockStyle.Fill,
                PlaceholderText = "Ví dụ: Nguyễn Văn Hùng"
            };
            pnlHoTen.Controls.Add(txtHoTen);

            // Dòng 2: Số điện thoại * & Số CCCD / Hộ chiếu *
            y += gap;
            TaoNhan(pnlBody, "Số điện thoại *", 28, y);
            var pnlSDT = TaoBox(pnlBody, 28, y + 20, halfW, 36);
            txtSDT = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                PlaceholderText = "09xxxxxxxx"
            };
            pnlSDT.Controls.Add(txtSDT);

            TaoNhan(pnlBody, "Số CCCD / CMND / Hộ chiếu *", col2X, y);
            var pnlCCCD = TaoBox(pnlBody, col2X, y + 20, halfW, 36);
            txtCCCD = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                PlaceholderText = "12 số CCCD / Hộ chiếu"
            };
            pnlCCCD.Controls.Add(txtCCCD);

            // Dòng 3: Giới tính & Ngày sinh
            y += gap;
            TaoNhan(pnlBody, "Giới tính", 28, y);
            var pnlGioiTinh = TaoBox(pnlBody, 28, y + 20, halfW, 36);
            cboGioiTinh = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;
            pnlGioiTinh.Controls.Add(cboGioiTinh);

            TaoNhan(pnlBody, "Ngày sinh (tùy chọn)", col2X, y);
            var pnlNgaySinh = TaoBox(pnlBody, col2X, y + 20, halfW, 36);
            dtpNgaySinh = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                ShowCheckBox = true,
                Checked = false,
                Font = new Font("Segoe UI", 9.5F)
            };
            pnlNgaySinh.Controls.Add(dtpNgaySinh);

            // Dòng 4: Quốc tịch & Email
            y += gap;
            TaoNhan(pnlBody, "Quốc tịch", 28, y);
            var pnlQuocTich = TaoBox(pnlBody, 28, y + 20, halfW, 36);
            txtQuocTich = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                Text = "Việt Nam"
            };
            pnlQuocTich.Controls.Add(txtQuocTich);

            TaoNhan(pnlBody, "Email liên hệ", col2X, y);
            var pnlEmail = TaoBox(pnlBody, col2X, y + 20, halfW, 36);
            txtEmail = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                PlaceholderText = "khachhang@gmail.com"
            };
            pnlEmail.Controls.Add(txtEmail);

            // Dòng 5: Địa chỉ thường trú
            y += gap;
            TaoNhan(pnlBody, "Địa chỉ thường trú", 28, y);
            var pnlDiaChi = TaoBox(pnlBody, 28, y + 20, fullW, 36);
            txtDiaChi = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                PlaceholderText = "Số nhà, tên đường, phường/xã, quận/huyện, tỉnh/TP..."
            };
            pnlDiaChi.Controls.Add(txtDiaChi);

            // Dòng 6: Ghi chú sở thích phòng
            y += gap;
            TaoNhan(pnlBody, "Ghi chú & Yêu cầu lưu trú", 28, y);
            var pnlGhiChu = TaoBox(pnlBody, 28, y + 20, fullW, 36);
            txtGhiChu = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                PlaceholderText = "Ví dụ: Khách thích tầng cao, yên tĩnh, không hút thuốc..."
            };
            pnlGhiChu.Controls.Add(txtGhiChu);

            // Dòng 7: Checkbox đồng ý
            y += gap + 8;
            chkDongY = new CheckBox
            {
                Text = "Đồng ý nhận thông tin ưu đãi và chăm sóc thành viên VIP của khách sạn",
                Location = new Point(28, y),
                Size = new Size(fullW, 26),
                Font = new Font("Segoe UI", 9F),
                ForeColor = UITheme.TextMain,
                Checked = true
            };
            pnlBody.Controls.Add(chkDongY);

            // THỨ TỰ ADD CONTROL CHUẨN ĐỂ KHÔNG BỊ HEADER CHE KHUẤT
            Controls.Add(pnlBody);
            Controls.Add(pnlHeader);
            Controls.Add(pnlFooter);

            pnlHeader.SendToBack();
            pnlFooter.SendToBack();
            pnlBody.BringToFront();

            Shown += (_, __) => txtHoTen.Focus();
        }

        private static Label TaoNhan(Control parent, string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                ForeColor = UITheme.TextMain,
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
                Padding = new Padding(10, 8, 10, 6)
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
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                NgaySinh = dtpNgaySinh.Checked ? dtpNgaySinh.Value.Date : (DateTime?)null,
                QuocTich = string.IsNullOrWhiteSpace(txtQuocTich.Text) ? "Việt Nam" : txtQuocTich.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
                DongYNhanUuDai = chkDongY.Checked,
                NhomKhach = "Mới"
            };

            var kq = bll.Them(kh);
            if (kq.ThanhCong)
            {
                MaKH = kq.MaKhachHang;
                HoTen = kh.HoTen;
                SoDienThoai = kh.DienThoai;
                MaKHHienThi = $"KH{kq.MaKhachHang:D6}";

                MessageBox.Show($"Đã tạo hồ sơ khách hàng [{kh.HoTen}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                string msg = string.IsNullOrWhiteSpace(kq.ThongBao) ? "Không thể lưu thông tin khách hàng." : kq.ThongBao;
                if (kq.LoiTheoTruong.Count > 0)
                {
                    msg += "\n• " + string.Join("\n• ", kq.LoiTheoTruong.Values);
                }
                MessageBox.Show(msg, "Lỗi kiểm tra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
