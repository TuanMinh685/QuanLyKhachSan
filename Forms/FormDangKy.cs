using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormDangKy : Form
    {
        private TextBox txtTenDangNhap, txtHoTen, txtEmail, txtSdt, txtMatKhau, txtNhapLai;
        private Label lblLoi;
        private Button btnDangKy;

        public FormDangKy()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Đăng ký tài khoản";
            Size = new Size(460, 560);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = GiaoDien.FontChuan;
            AutoScroll = true;

            var lblTieuDe = new Label
            {
                Text = "Tạo tài khoản mới",
                Font = GiaoDien.FontTieuDe,
                ForeColor = GiaoDien.MauChinhDam,
                Location = new Point(24, 20),
                AutoSize = true
            };
            Controls.Add(lblTieuDe);

            var lblPhu = new Label
            {
                Text = "Tài khoản tạo mới mặc định có vai trò Nhân viên",
                ForeColor = GiaoDien.MauChuMo,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(24, 50),
                AutoSize = true
            };
            Controls.Add(lblPhu);

            lblLoi = new Label
            {
                ForeColor = GiaoDien.MauNguyHiem,
                Location = new Point(24, 76),
                Size = new Size(400, 40),
                Font = new Font("Segoe UI", 9F)
            };
            Controls.Add(lblLoi);

            int y = 120;
            txtTenDangNhap = ThemO("Tên đăng nhập * (3-30 ký tự, không dấu)", ref y, maxLength: 30);
            txtHoTen = ThemO("Họ và tên *", ref y, maxLength: 100);
            txtEmail = ThemO("Email", ref y, maxLength: 100);
            txtSdt = ThemO("Số điện thoại (9-11 chữ số)", ref y, maxLength: 11);
            txtMatKhau = ThemO("Mật khẩu * (ít nhất 6 ký tự)", ref y, matKhau: true, maxLength: 100);
            txtNhapLai = ThemO("Nhập lại mật khẩu *", ref y, matKhau: true, maxLength: 100);

            btnDangKy = new Button { Text = "Đăng ký", Location = new Point(24, y + 10), Size = new Size(400, 38) };
            GiaoDien.ApDungNutChinh(btnDangKy);
            btnDangKy.Click += BtnDangKy_Click;
            Controls.Add(btnDangKy);

            AcceptButton = btnDangKy;
            ClientSize = new Size(448, y + 70);
        }

        private TextBox ThemO(string nhan, ref int y, bool matKhau = false, int maxLength = 0)
        {
            var lbl = new Label { Text = nhan, Font = GiaoDien.FontNhan, Location = new Point(24, y), AutoSize = true };
            Controls.Add(lbl);
            var txt = new TextBox
            {
                Location = new Point(24, y + 22),
                Size = new Size(400, 28),
                Font = GiaoDien.FontChuan,
                PasswordChar = matKhau ? '*' : '\0'
            };
            if (maxLength > 0) txt.MaxLength = maxLength;
            Controls.Add(txt);
            y += 62;
            return txt;
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sdt = txtSdt.Text.Trim();
            string matKhau = txtMatKhau.Text;
            string nhapLai = txtNhapLai.Text;

            string loi = KiemTraHelper.KiemTraTenDangNhap(tenDangNhap)
                      ?? KiemTraHelper.KiemTraHoTen(hoTen)
                      ?? KiemTraHelper.KiemTraEmail(email)
                      ?? KiemTraHelper.KiemTraSdt(sdt)
                      ?? KiemTraHelper.KiemTraMatKhau(matKhau);
            if (loi == null && matKhau != nhapLai)
                loi = "Mật khẩu nhập lại không khớp.";

            if (loi != null)
            {
                lblLoi.Text = loi;
                return;
            }

            try
            {
                using var conn = DatabaseHelper.LayKetNoi();

                using (var cmdKiemTra = new SqlCommand("SELECT id FROM tai_khoan WHERE ten_dang_nhap = @tdn", conn))
                {
                    cmdKiemTra.Parameters.AddWithValue("@tdn", tenDangNhap);
                    if (cmdKiemTra.ExecuteScalar() != null)
                    {
                        lblLoi.Text = "Tên đăng nhập này đã tồn tại.";
                        return;
                    }
                }

                string hash = MatKhauHelper.Bam(matKhau);
                using var cmd = new SqlCommand(
                    @"INSERT INTO tai_khoan (ten_dang_nhap, mat_khau, ho_ten, email, sdt, vai_tro)
                      VALUES (@tdn, @mk, @ht, @email, @sdt, 'NhanVien')", conn);
                cmd.Parameters.AddWithValue("@tdn", tenDangNhap);
                cmd.Parameters.AddWithValue("@mk", hash);
                cmd.Parameters.AddWithValue("@ht", hoTen);
                cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? DBNull.Value : email);
                cmd.Parameters.AddWithValue("@sdt", string.IsNullOrEmpty(sdt) ? DBNull.Value : sdt);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (SqlException ex)
            {
                lblLoi.Text = "Lỗi kết nối CSDL: " + ex.Message;
            }
        }
    }
}
