using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;
// using Microsoft.Data.SqlClient; // migrated to Supabase Npgsql
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDangNhap : Form
    {
        private readonly TextBox txtTenDangNhap = new();
        private readonly TextBox txtMatKhau = new();
        private readonly Button btnDangNhap = new();
        private readonly Label lblLoi = new();

        public frmDangNhap()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Text = "Đăng nhập - An Nhiên Hotel";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(440, 440);
            MinimumSize = new Size(456, 479);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F);

            
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(28),
                BackColor = Color.White
            };
            Controls.Add(panel);

            var title = new Label
            {
                Text = "AN NHIÊN HOTEL",
                Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                ForeColor = Color.FromArgb(232, 119, 26),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Location = new Point(28, 22),
                Size = new Size(384, 42)
            };
            var subtitle = new Label
            {
                Text = "ĐĂNG NHẬP HỆ THỐNG",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 37, 55),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Location = new Point(28, 64),
                Size = new Size(384, 30)
            };

            var lblUser = new Label
            {
                Text = "Tên đăng nhập",
                AutoSize = false,
                Location = new Point(28, 112),
                Size = new Size(384, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            txtTenDangNhap.Location = new Point(28, 148);
            txtTenDangNhap.Size = new Size(384, 32);
            txtTenDangNhap.Font = new Font("Segoe UI", 11F);
            txtTenDangNhap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            var lblPass = new Label
            {
                Text = "Mật khẩu",
                AutoSize = false,
                Location = new Point(28, 198),
                Size = new Size(384, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            txtMatKhau.Location = new Point(28, 234);
            txtMatKhau.Size = new Size(384, 32);
            txtMatKhau.Font = new Font("Segoe UI", 11F);
            txtMatKhau.UseSystemPasswordChar = true;
            txtMatKhau.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.Location = new Point(28, 286);
            btnDangNhap.Size = new Size(384, 42);
            btnDangNhap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.BackColor = Color.FromArgb(232, 119, 26);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDangNhap.Click += BtnDangNhap_Click;

            lblLoi.Location = new Point(28, 337);
            lblLoi.Size = new Size(384, 42);
            lblLoi.ForeColor = Color.Firebrick;
            lblLoi.TextAlign = ContentAlignment.MiddleLeft;
            lblLoi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            panel.Controls.Add(title);
            panel.Controls.Add(subtitle);
            panel.Controls.Add(lblUser);
            panel.Controls.Add(txtTenDangNhap);
            panel.Controls.Add(lblPass);
            panel.Controls.Add(txtMatKhau);
            panel.Controls.Add(btnDangNhap);
            panel.Controls.Add(lblLoi);
            AcceptButton = btnDangNhap;
        }

        private void frmDangNhap_Load(object sender, EventArgs e) { }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            lblLoi.Text = "";
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            { lblLoi.Text = "Vui lòng nhập tên đăng nhập và mật khẩu."; return; }

            try
            {
                using var conn = DatabaseHelper.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand("SELECT MatKhau, VaiTro FROM TaiKhoan WHERE TenDangNhap=@user", conn);
                cmd.Parameters.AddWithValue("@user", txtTenDangNhap.Text.Trim());
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
                if (storedPassword == null || !KiemTraMatKhau(txtMatKhau.Text, storedPassword))
                { lblLoi.Text = "Tên đăng nhập hoặc mật khẩu không đúng."; return; }
                Session.TenDangNhap = txtTenDangNhap.Text.Trim();
                Session.VaiTro = role ?? "LeTan";
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đăng nhập. Hãy kiểm tra kết nối SQL Server và cơ sở dữ liệu.\n" + ex.Message,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool KiemTraMatKhau(string password, string stored)
        {
            // Định dạng mới: PBKDF2-SHA256$210000$salt$hash
            var parts = stored.Split('$');
            if (parts.Length == 4 && parts[0] == "PBKDF2-SHA256" && int.TryParse(parts[1], out int iterations))
            {
                try
                {
                    byte[] salt = Convert.FromBase64String(parts[2]);
                    byte[] expected = Convert.FromBase64String(parts[3]);
                    byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expected.Length);
                    return CryptographicOperations.FixedTimeEquals(actual, expected);
                }
                catch { return false; }
            }
            // Hỗ trợ dữ liệu cũ nếu database đang lưu mật khẩu dạng chữ thường.
            return string.Equals(password, stored, StringComparison.Ordinal);
        }
    }
}
