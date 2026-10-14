using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDangNhap : Form
    {
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private Button btnHienMatKhau;
        private CheckBox chkGhiNho;
        private Button btnDangNhap;
        private LinkLabel llDangKy, llQuenMatKhau;
        private Label lblLoi;

        public frmDangNhap()
        {
            InitializeComponent();
            KhoiTaoGiaoDienThieu();
            NapTenDangNhapDaLuu();
        }

        private void KhoiTaoGiaoDienThieu()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.Text = "Đăng nhập - Khách sạn An Nhiên";
            this.ClientSize = new Size(980, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9.5f);

            // Khung trái
            var panelTrai = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(420, 620),
                BackColor = UITheme.DarkNavy
            };
            this.Controls.Add(panelTrai);

            try
            {
                string[] candidates = new[] {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Không gian khách sạn.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Không gian khách sạn.png")
                };

                string imgPath = candidates.FirstOrDefault(p => File.Exists(p));
                if (!string.IsNullOrEmpty(imgPath))
                {
                    var pic = new PictureBox
                    {
                        Image = Image.FromFile(imgPath),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Location = new Point(0, 0),
                        Size = panelTrai.Size
                    };
                    panelTrai.Controls.Add(pic);
                }
                else
                {
                    panelTrai.Controls.Add(new Label { Text = "KHÁCH SẠN AN NHIÊN", ForeColor = Color.White, Font = new Font("Segoe UI", 16F, FontStyle.Bold), Location = new Point(32, 32), AutoSize = true });
                }
            }
            catch
            {
                panelTrai.Controls.Add(new Label { Text = "KHÁCH SẠN AN NHIÊN", ForeColor = Color.White, Font = new Font("Segoe UI", 16F, FontStyle.Bold), Location = new Point(32, 32), AutoSize = true });
            }

            // Khung phải
            var panelPhai = new Panel
            {
                Location = new Point(420, 0),
                Size = new Size(560, 620),
                BackColor = ColorTranslator.FromHtml("#F8FAFC")
            };
            this.Controls.Add(panelPhai);

            var the = new Panel { Size = new Size(400, 470), Location = new Point(80, 75), BackColor = Color.White };
            the.Paint += (s, e) => UITheme.DrawCard(s, e);
            panelPhai.Controls.Add(the);

            the.Controls.Add(new Label { Text = "AN NHIÊN HOTEL", ForeColor = UITheme.DarkNavy, Font = new Font("Segoe UI", 14F, FontStyle.Bold), Location = new Point(0, 24), Size = new Size(400, 26), TextAlign = ContentAlignment.MiddleCenter });
            the.Controls.Add(new Label { Text = "Hệ thống quản lý khách sạn tập trung", ForeColor = UITheme.TextMuted, Font = new Font("Segoe UI", 9.5F), Location = new Point(0, 52), Size = new Size(400, 20), TextAlign = ContentAlignment.MiddleCenter });
            the.Controls.Add(new Label { Text = "Đăng nhập", ForeColor = UITheme.TextMain, Font = new Font("Segoe UI", 13F, FontStyle.Bold), Location = new Point(32, 92), AutoSize = true });
            the.Controls.Add(new Label { Text = "Nhập thông tin tài khoản để tiếp tục ca làm việc.", ForeColor = UITheme.TextMuted, Font = new Font("Segoe UI", 9F), Location = new Point(32, 120), AutoSize = true });

            lblLoi = new Label { ForeColor = UITheme.DangerRed, Location = new Point(32, 144), Size = new Size(336, 36), Font = new Font("Segoe UI", 8.5F, FontStyle.Bold) };
            the.Controls.Add(lblLoi);

            the.Controls.Add(new Label { Text = "Tên đăng nhập *", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = UITheme.TextMain, Location = new Point(32, 184), AutoSize = true });
            txtTenDangNhap = new TextBox { Location = new Point(32, 206), Size = new Size(336, 28), Font = new Font("Segoe UI", 9.5F), MaxLength = 50 };
            the.Controls.Add(txtTenDangNhap);

            the.Controls.Add(new Label { Text = "Mật khẩu *", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = UITheme.TextMain, Location = new Point(32, 242), AutoSize = true });
            txtMatKhau = new TextBox { Location = new Point(32, 264), Size = new Size(298, 28), Font = new Font("Segoe UI", 9.5F), PasswordChar = '*' };
            the.Controls.Add(txtMatKhau);

            btnHienMatKhau = new Button { Text = "👁", Location = new Point(334, 264), Size = new Size(34, 28), FlatStyle = FlatStyle.Flat, BackColor = Color.White, Cursor = Cursors.Hand };
            btnHienMatKhau.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnHienMatKhau.Click += (s, e) => txtMatKhau.PasswordChar = txtMatKhau.PasswordChar == '*' ? '\0' : '*';
            the.Controls.Add(btnHienMatKhau);

            chkGhiNho = new CheckBox { Text = "Ghi nhớ đăng nhập", Location = new Point(32, 306), AutoSize = true, Font = new Font("Segoe UI", 9F), ForeColor = UITheme.TextMain };
            the.Controls.Add(chkGhiNho);

            llQuenMatKhau = new LinkLabel { Text = "Quên mật khẩu?", Location = new Point(255, 306), AutoSize = true, Font = new Font("Segoe UI", 9F), LinkColor = UITheme.PrimaryOrange };
            llQuenMatKhau.Click += (s, e) => MessageBox.Show("Vui lòng liên hệ Admin để cấp lại mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            the.Controls.Add(llQuenMatKhau);

            btnDangNhap = new Button { Text = "Đăng nhập", Location = new Point(32, 340), Size = new Size(336, 40), BackColor = UITheme.PrimaryOrange, ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.Region = new Region(UITheme.GetRoundedRectPath(new Rectangle(0, 0, btnDangNhap.Width, btnDangNhap.Height), 6));
            btnDangNhap.Click += BtnDangNhap_Click;
            the.Controls.Add(btnDangNhap);

            llDangKy = new LinkLabel { Text = "Chưa có tài khoản? Đăng ký nhân viên mới", Location = new Point(70, 395), AutoSize = true, Font = new Font("Segoe UI", 9F), LinkColor = UITheme.DarkNavy };
            llDangKy.Click += (s, e) => MessageBox.Show("Chức năng đang được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            the.Controls.Add(llDangKy);

            the.Controls.Add(new Label { Text = "© 2026 An Nhiên Hotel System", ForeColor = UITheme.TextMuted, Font = new Font("Segoe UI", 8F), Location = new Point(0, 435), Size = new Size(400, 18), TextAlign = ContentAlignment.MiddleCenter });

            this.AcceptButton = btnDangNhap;
        }

        private static string DuongDanFileGhiNho =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuanLyKhachSan", "ghi_nho_dang_nhap.txt");

        private void NapTenDangNhapDaLuu()
        {
            try
            {
                if (File.Exists(DuongDanFileGhiNho))
                {
                    txtTenDangNhap.Text = File.ReadAllText(DuongDanFileGhiNho).Trim();
                    chkGhiNho.Checked = !string.IsNullOrEmpty(txtTenDangNhap.Text);
                }
            }
            catch { }
        }

        private void LuuHoacXoaTenDangNhap(string tenDangNhap)
        {
            try
            {
                string thuMuc = Path.GetDirectoryName(DuongDanFileGhiNho);
                if (chkGhiNho.Checked)
                {
                    Directory.CreateDirectory(thuMuc);
                    File.WriteAllText(DuongDanFileGhiNho, tenDangNhap);
                }
                else if (File.Exists(DuongDanFileGhiNho))
                {
                    File.Delete(DuongDanFileGhiNho);
                }
            }
            catch { }
        }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                lblLoi.Text = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
                return;
            }

            try
            {
                string sql = "SELECT COUNT(*) FROM NhanVien WHERE TenDangNhap = @tdn AND MatKhau = @mk";
                SqlParameter[] pars = {
                    new SqlParameter("@tdn", tenDangNhap),
                    new SqlParameter("@mk", matKhau)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, pars);
                int count = (dt != null && dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0][0]) : 0;

                if (count > 0)
                {
                    LuuHoacXoaTenDangNhap(tenDangNhap);
                    this.Hide();
                    frmTrangChu mainForm = new frmTrangChu();
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    lblLoi.Text = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                }
            }
            catch (Exception ex)
            {
                lblLoi.Text = "Lỗi CSDL: " + ex.Message;
            }
        }
    }
}