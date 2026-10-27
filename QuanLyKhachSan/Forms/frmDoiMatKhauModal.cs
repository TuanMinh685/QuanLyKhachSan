using System;
using System.Drawing;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDoiMatKhauModal : Form
    {
        public frmDoiMatKhauModal()
        {
            InitializeComponent();
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);
        }

        private void InitializeComponentCustom()
        {
            Text = "Đổi mật khẩu tài khoản";
            Size = new Size(480, 430);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = UITheme.MainFont;

            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.White,
                Padding = new Padding(24, 14, 24, 8)
            };
            var lblTitle = new Label
            {
                Text = "🔑 THAY ĐỔI MẬT KHẨU BẢO MẬT",
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(20, 12),
                AutoSize = true
            };
            var lblSub = new Label
            {
                Text = $"Tài khoản đang đăng nhập: {Session.TenDangNhap} ({Session.VaiTro})",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(20, 36),
                AutoSize = true
            };
            var divider = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = Color.FromArgb(226, 232, 240) };
            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblSub, divider });
            Controls.Add(pnlHeader);

            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(28, 16, 28, 16),
                BackColor = Color.White
            };

            int y = 16;
            // 1. Mật khẩu hiện tại
            TaoNhan(pnlBody, "Mật khẩu hiện tại *", 24, y);
            y += 24;
            txtMatKhauCu = new TextBox { Location = new Point(24, y), Size = new Size(412, 30), Font = new Font("Segoe UI", 10.5F), UseSystemPasswordChar = true };
            pnlBody.Controls.Add(txtMatKhauCu);
            y += 42;

            // 2. Mật khẩu mới
            TaoNhan(pnlBody, "Mật khẩu mới (tối thiểu 8 ký tự, có hoa, thường, số) *", 24, y);
            y += 24;
            txtMatKhauMoi = new TextBox { Location = new Point(24, y), Size = new Size(412, 30), Font = new Font("Segoe UI", 10.5F), UseSystemPasswordChar = true };
            txtMatKhauMoi.TextChanged += TxtMatKhauMoi_TextChanged;
            pnlBody.Controls.Add(txtMatKhauMoi);
            y += 34;

            // Thanh đo độ mạnh mật khẩu
            prgDoManh = new ProgressBar { Location = new Point(24, y), Size = new Size(200, 8), Minimum = 0, Maximum = 5, Value = 0 };
            lblDoManh = new Label { Location = new Point(234, y - 4), Size = new Size(202, 20), Font = new Font("Segoe UI", 8F), ForeColor = UITheme.TextMuted, Text = "Độ mạnh: Chưa nhập" };
            pnlBody.Controls.Add(prgDoManh);
            pnlBody.Controls.Add(lblDoManh);
            y += 24;

            // 3. Nhập lại mật khẩu mới
            TaoNhan(pnlBody, "Xác nhận lại mật khẩu mới *", 24, y);
            y += 24;
            txtXacNhanMoi = new TextBox { Location = new Point(24, y), Size = new Size(412, 30), Font = new Font("Segoe UI", 10.5F), UseSystemPasswordChar = true };
            pnlBody.Controls.Add(txtXacNhanMoi);
            y += 44;

            // Nút bấm
            btnLuu = new Button
            {
                Text = "💾 Cập nhật mật khẩu",
                Location = new Point(226, y),
                Size = new Size(210, 38),
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;

            btnHuy = new Button
            {
                Text = "Đóng",
                Location = new Point(130, y),
                Size = new Size(86, 38),
                BackColor = Color.White,
                ForeColor = UITheme.TextMuted,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnHuy.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnHuy.Click += (_, __) => Close();

            pnlBody.Controls.Add(btnLuu);
            pnlBody.Controls.Add(btnHuy);
            Controls.Add(pnlBody);

            pnlHeader.SendToBack();
            pnlBody.BringToFront();
        }

        private void TxtMatKhauMoi_TextChanged(object sender, EventArgs e)
        {
            var res = BaoMatHelper.KiemTraDoManhMatKhau(txtMatKhauMoi.Text);
            prgDoManh.Value = Math.Min(5, res.DiemDanhGia);

            if (res.DiemDanhGia <= 1)
            {
                lblDoManh.Text = "Độ mạnh: Yếu";
                lblDoManh.ForeColor = Color.FromArgb(220, 38, 38);
            }
            else if (res.DiemDanhGia <= 3)
            {
                lblDoManh.Text = "Độ mạnh: Trung bình";
                lblDoManh.ForeColor = Color.FromArgb(217, 119, 6);
            }
            else
            {
                lblDoManh.Text = "Độ mạnh: Rất mạnh (An toàn)";
                lblDoManh.ForeColor = Color.FromArgb(22, 163, 74);
            }
        }

        private static void TaoNhan(Control parent, string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(x, y),
                AutoSize = true
            };
            parent.Controls.Add(lbl);
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string passCu = txtMatKhauCu.Text;
            string passMoi = txtMatKhauMoi.Text;
            string xacNhan = txtXacNhanMoi.Text;

            if (string.IsNullOrEmpty(passCu))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại của bạn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauCu.Focus();
                return;
            }

            var kiemTra = BaoMatHelper.KiemTraDoManhMatKhau(passMoi);
            if (!kiemTra.HopLe)
            {
                MessageBox.Show(kiemTra.ThongBao, "Mật khẩu mới không an toàn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (passCu == passMoi)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu hiện tại.", "Mật khẩu trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (passMoi != xacNhan)
            {
                MessageBox.Show("Xác nhận mật khẩu mới không khớp. Vui lòng nhập lại chính xác.", "Lỗi xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMoi.Focus();
                return;
            }

            try
            {
                using var conn = DatabaseHelper.GetConnection();
                conn.Open();

                // Lấy mật khẩu lưu trữ trong database
                string sqlGet = "SELECT MatKhau FROM TaiKhoan WHERE TenDangNhap = @user";
                using var cmdGet = new SqlCommand(sqlGet, conn);
                cmdGet.Parameters.AddWithValue("@user", Session.TenDangNhap);

                object res = cmdGet.ExecuteScalar();
                if (res == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin tài khoản hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string stored = res.ToString();
                if (!BaoMatHelper.KiemTraVaNangCap(passCu, stored, Session.TenDangNhap, out _))
                {
                    MessageBox.Show("Mật khẩu hiện tại không chính xác. Vui lòng kiểm tra lại.", "Sai mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauCu.Focus();
                    return;
                }

                // Băm mật khẩu mới bằng PBKDF2-SHA256
                string hashMoi = BaoMatHelper.BamMatKhau(passMoi);

                string sqlUpdate = "UPDATE TaiKhoan SET MatKhau = @hash WHERE TenDangNhap = @user";
                using var cmdUp = new SqlCommand(sqlUpdate, conn);
                cmdUp.Parameters.AddWithValue("@hash", hashMoi);
                cmdUp.Parameters.AddWithValue("@user", Session.TenDangNhap);
                cmdUp.ExecuteNonQuery();

                BaoMatHelper.GhiNhatKy(Session.TenDangNhap, "DOI_MAT_KHAU", "Người dùng tự cập nhật mật khẩu mới qua hệ thống bảo mật");

                MessageBox.Show("Đã đổi mật khẩu thành công!\nMật khẩu mới đã được mã hóa an toàn theo tiêu chuẩn PBKDF2-SHA256.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
