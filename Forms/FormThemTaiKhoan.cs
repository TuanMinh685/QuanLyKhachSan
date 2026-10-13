using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormThemTaiKhoan : Form
    {
        private TextBox txtTenDangNhap, txtHoTen, txtEmail, txtSdt, txtMatKhau, txtNhapLai;
        private ComboBox cboVaiTro;
        private Label lblLoi;

        public FormThemTaiKhoan()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Thêm tài khoản mới";
            Size = new Size(460, 600);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = GiaoDien.FontChuan;
            AutoScroll = true;

            var lblTieuDe = new Label
            {
                Text = "Thêm tài khoản mới",
                Font = GiaoDien.FontTieuDe,
                ForeColor = GiaoDien.MauChinhDam,
                Location = new Point(24, 20),
                AutoSize = true
            };
            Controls.Add(lblTieuDe);

            lblLoi = new Label
            {
                ForeColor = GiaoDien.MauNguyHiem,
                Location = new Point(24, 56),
                Size = new Size(400, 36),
                Font = new Font("Segoe UI", 9F)
            };
            Controls.Add(lblLoi);

            int y = 100;
            txtTenDangNhap = ThemO("Tên đăng nhập * (3-30 ký tự, không dấu)", ref y, maxLength: 30);
            txtHoTen = ThemO("Họ tên *", ref y, maxLength: 100);
            txtEmail = ThemO("Email", ref y, maxLength: 100);
            txtSdt = ThemO("Số điện thoại (9-11 chữ số)", ref y, maxLength: 11);
            txtMatKhau = ThemO("Mật khẩu * (ít nhất 6 ký tự)", ref y, matKhau: true, maxLength: 100);
            txtNhapLai = ThemO("Nhập lại mật khẩu *", ref y, matKhau: true, maxLength: 100);

            var lblVaiTro = new Label { Text = "Vai trò *", Font = GiaoDien.FontNhan, Location = new Point(24, y), AutoSize = true };
            Controls.Add(lblVaiTro);
            cboVaiTro = new ComboBox
            {
                Location = new Point(24, y + 22),
                Size = new Size(400, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = GiaoDien.FontChuan
            };
            // Thứ tự: 0 = Nhân viên (NhanVien), 1 = Admin
            cboVaiTro.Items.AddRange(new object[] { "Nhân viên", "Admin" });
            cboVaiTro.SelectedIndex = 0;
            Controls.Add(cboVaiTro);
            y += 62;

            var btnLuu = new Button { Text = "Lưu tài khoản", Location = new Point(24, y + 10), Size = new Size(190, 36) };
            GiaoDien.ApDungNutChinh(btnLuu);
            btnLuu.Click += BtnLuu_Click;
            Controls.Add(btnLuu);

            var btnHuy = new Button { Text = "Hủy", Location = new Point(228, y + 10), Size = new Size(190, 36) };
            GiaoDien.ApDungNutPhu(btnHuy);
            btnHuy.Click += (s, e) => Close();
            Controls.Add(btnHuy);

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

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sdt = txtSdt.Text.Trim();
            string matKhau = txtMatKhau.Text;
            string nhapLai = txtNhapLai.Text;
            string vaiTro = cboVaiTro.SelectedIndex == 1 ? "Admin" : "NhanVien";

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
                        lblLoi.Text = "Tên đăng nhập đã tồn tại.";
                        return;
                    }
                }

                string hash = MatKhauHelper.Bam(matKhau);
                using var cmd = new SqlCommand(
                    @"INSERT INTO tai_khoan (ten_dang_nhap, mat_khau, ho_ten, email, sdt, vai_tro)
                      VALUES (@tdn, @mk, @ht, @email, @sdt, @vt)", conn);
                cmd.Parameters.AddWithValue("@tdn", tenDangNhap);
                cmd.Parameters.AddWithValue("@mk", hash);
                cmd.Parameters.AddWithValue("@ht", hoTen);
                cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? DBNull.Value : email);
                cmd.Parameters.AddWithValue("@sdt", string.IsNullOrEmpty(sdt) ? DBNull.Value : sdt);
                cmd.Parameters.AddWithValue("@vt", vaiTro);
                cmd.ExecuteNonQuery();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SqlException ex)
            {
                lblLoi.Text = "Lỗi lưu dữ liệu: " + ex.Message;
            }
        }
    }
}
