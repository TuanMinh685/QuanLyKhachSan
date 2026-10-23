using System;
using System.Drawing;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDangNhap : Form
    {
        private readonly TextBox txtTenDangNhap = new();
        private readonly TextBox txtMatKhau = new();
        private readonly Button btnHienMatKhau = new();
        private readonly Button btnDangNhap = new();
        private readonly Label lblLoi = new();
        private readonly System.Windows.Forms.Timer tmrDemNguocKhoa = new();
        private int _giayConLaiKhoa = 0;

        public frmDangNhap()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Text = "Đăng nhập hệ thống - An Nhiên Hotel";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(450, 480);
            BackColor = Color.FromArgb(248, 250, 252);
            Font = new Font("Segoe UI", 10F);

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(32, 28, 32, 28),
                BackColor = Color.White
            };
            Controls.Add(panel);

            var title = new Label
            {
                Text = "AN NHIÊN HOTEL",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(232, 119, 26),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Location = new Point(32, 20),
                Size = new Size(386, 38)
            };
            var subtitle = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN NỘI BỘ",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Location = new Point(32, 58),
                Size = new Size(386, 24)
            };

            var lblUser = new Label
            {
                Text = "Tên đăng nhập",
                Font = new Font("Segoe UI", 9.2F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 37, 55),
                AutoSize = false,
                Location = new Point(32, 102),
                Size = new Size(386, 26),
                TextAlign = ContentAlignment.MiddleLeft
            };
            txtTenDangNhap.Location = new Point(32, 130);
            txtTenDangNhap.Size = new Size(386, 32);
            txtTenDangNhap.Font = new Font("Segoe UI", 11F);
            txtTenDangNhap.PlaceholderText = "Nhập tài khoản (vd: admin, letan01)";
            txtTenDangNhap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            var lblPass = new Label
            {
                Text = "Mật khẩu",
                Font = new Font("Segoe UI", 9.2F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 37, 55),
                AutoSize = false,
                Location = new Point(32, 176),
                Size = new Size(386, 26),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Password container with eye toggle
            var pnlPassWrap = new Panel
            {
                Location = new Point(32, 204),
                Size = new Size(386, 34),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            txtMatKhau.Location = new Point(0, 0);
            txtMatKhau.Size = new Size(346, 32);
            txtMatKhau.Font = new Font("Segoe UI", 11F);
            txtMatKhau.UseSystemPasswordChar = true;
            txtMatKhau.PlaceholderText = "Nhập mật khẩu";
            txtMatKhau.Dock = DockStyle.Fill;

            btnHienMatKhau.Text = "👁";
            btnHienMatKhau.Dock = DockStyle.Right;
            btnHienMatKhau.Width = 38;
            btnHienMatKhau.FlatStyle = FlatStyle.Flat;
            btnHienMatKhau.FlatAppearance.BorderSize = 0;
            btnHienMatKhau.BackColor = Color.FromArgb(241, 245, 249);
            btnHienMatKhau.Cursor = Cursors.Hand;
            btnHienMatKhau.Font = new Font("Segoe UI", 11F);
            btnHienMatKhau.Click += (_, __) =>
            {
                txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;
                btnHienMatKhau.Text = txtMatKhau.UseSystemPasswordChar ? "👁" : "🔒";
            };

            pnlPassWrap.Controls.Add(txtMatKhau);
            pnlPassWrap.Controls.Add(btnHienMatKhau);

            btnDangNhap.Text = "ĐĂNG NHẬP AN TOÀN";
            btnDangNhap.Location = new Point(32, 262);
            btnDangNhap.Size = new Size(386, 44);
            btnDangNhap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.BackColor = Color.FromArgb(232, 119, 26);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnDangNhap.Cursor = Cursors.Hand;
            btnDangNhap.Click += BtnDangNhap_Click;

            lblLoi.Location = new Point(32, 318);
            lblLoi.Size = new Size(386, 52);
            lblLoi.ForeColor = Color.FromArgb(220, 38, 38);
            lblLoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLoi.TextAlign = ContentAlignment.MiddleCenter;
            lblLoi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            var lblSecurityBadge = new Label
            {
                Text = "🛡️ Bảo mật PBKDF2-SHA256 • Chống dò quét Brute-Force",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(148, 163, 184),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(32, 380),
                Size = new Size(386, 26),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            panel.Controls.Add(title);
            panel.Controls.Add(subtitle);
            panel.Controls.Add(lblUser);
            panel.Controls.Add(txtTenDangNhap);
            panel.Controls.Add(lblPass);
            panel.Controls.Add(pnlPassWrap);
            panel.Controls.Add(btnDangNhap);
            panel.Controls.Add(lblLoi);
            panel.Controls.Add(lblSecurityBadge);

            AcceptButton = btnDangNhap;

            tmrDemNguocKhoa.Interval = 1000;
            tmrDemNguocKhoa.Tick += TmrDemNguocKhoa_Tick;
        }

        private void TmrDemNguocKhoa_Tick(object sender, EventArgs e)
        {
            _giayConLaiKhoa--;
            if (_giayConLaiKhoa <= 0)
            {
                tmrDemNguocKhoa.Stop();
                btnDangNhap.Enabled = true;
                btnDangNhap.Text = "ĐĂNG NHẬP AN TOÀN";
                lblLoi.Text = "Đã hết thời gian khóa tạm thời. Bạn có thể thử lại.";
                lblLoi.ForeColor = Color.FromArgb(22, 163, 74);
            }
            else
            {
                btnDangNhap.Text = $"TẠM KHÓA ({_giayConLaiKhoa}s)";
                lblLoi.Text = $"Nhập sai quá nhiều lần! Vui lòng chờ {_giayConLaiKhoa} giây để tiếp tục.";
            }
        }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            lblLoi.ForeColor = Color.FromArgb(220, 38, 38);
            lblLoi.Text = "";

            string user = txtTenDangNhap.Text.Trim();
            string pass = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(pass))
            {
                lblLoi.Text = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
                return;
            }

            // 1. Kiểm tra trạng thái khóa Brute-force
            var trangThaiKhoa = BaoMatHelper.KiemTraKhoaDangNhap(user);
            if (trangThaiKhoa.BiKhoa)
            {
                KhoaNutDangNhap(trangThaiKhoa.GiayConLai);
                return;
            }

            try
            {
                using var conn = DatabaseHelper.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand("SELECT MatKhau, VaiTro FROM TaiKhoan WHERE TenDangNhap = @user", conn);
                cmd.Parameters.AddWithValue("@user", user);

                string storedPassword = null;
                string role = null;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        storedPassword = Convert.ToString(reader["MatKhau"]);
                        role = Convert.ToString(reader["VaiTro"]);
                    }
                }

                if (storedPassword == null)
                {
                    XuLyDangNhapThatBai(user);
                    return;
                }

                // 2. Xác thực mật khẩu qua BaoMatHelper (hỗ trợ hash mạnh + auto upgrade plaintext cũ)
                bool hopLe = BaoMatHelper.KiemTraVaNangCap(pass, storedPassword, user, out bool daNangCap);
                if (!hopLe)
                {
                    XuLyDangNhapThatBai(user);
                    return;
                }

                // 3. Đăng nhập thành công -> Reset vết thất bại
                BaoMatHelper.XoaVepKhoaDangNhap(user);
                BaoMatHelper.GhiNhatKy(user, "DANG_NHAP_THANH_CONG", $"Đăng nhập thành công với vai trò: {role}" + (daNangCap ? " (Đã tự động nâng cấp PBKDF2-SHA256)" : ""));

                Session.TenDangNhap = user;
                Session.VaiTro = role ?? "LeTan";
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu xác thực:\n" + ex.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuLyDangNhapThatBai(string user)
        {
            BaoMatHelper.GhiNhanDangNhapThatBai(user, out int soLanConLai);

            if (soLanConLai == 0)
            {
                KhoaNutDangNhap(BaoMatHelper.ThoiGianKhoaGiay);
            }
            else
            {
                lblLoi.Text = $"Tên đăng nhập hoặc mật khẩu không đúng.\n(Còn {soLanConLai} lần thử trước khi bị tạm khóa)";
            }
            txtMatKhau.Clear();
            txtMatKhau.Focus();
        }

        private void KhoaNutDangNhap(int giay)
        {
            _giayConLaiKhoa = giay;
            btnDangNhap.Enabled = false;
            btnDangNhap.Text = $"TẠM KHÓA ({_giayConLaiKhoa}s)";
            lblLoi.Text = $"Đăng nhập sai quá {BaoMatHelper.SoLanSaiToiDa} lần! Vui lòng chờ {_giayConLaiKhoa} giây để thử lại.";
            tmrDemNguocKhoa.Start();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
        }
    }
}
