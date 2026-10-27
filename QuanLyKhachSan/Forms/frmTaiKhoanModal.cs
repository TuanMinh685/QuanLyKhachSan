using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public class frmTaiKhoanModal : Form
    {
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlDivider;

        private TextBox txtHoTen;
        private ComboBox cboChucVu;
        private TextBox txtSDT;
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private Label lblHintMatKhau;
        private ComboBox cboVaiTro;

        private Panel pnlFooter;
        private Button btnLuu;
        private Button btnHuy;

        private int maTaiKhoan = 0;
        private int maNhanVien = 0;

        public frmTaiKhoanModal(int idTK = 0, int idNV = 0)
        {
            maTaiKhoan = idTK;
            maNhanVien = idNV;

            InitializeComponentCustom();
            UITheme.ApplyTheme(this);

            if (maTaiKhoan > 0)
            {
                lblTitle.Text = "✏️ CHỈNH SỬA THÔNG TIN NHÂN SỰ";
                lblSubtitle.Text = "Cập nhật hồ sơ nhân viên, mật khẩu hoặc vai trò quyền hạn";
                btnLuu.Text = "💾 Cập nhật";
                btnLuu.BackColor = UITheme.PrimaryBlue;
                TaiThongTin(maTaiKhoan);
            }
            else
            {
                lblTitle.Text = "➕ THÊM MỚI NHÂN SỰ & TÀI KHOẢN";
                lblSubtitle.Text = "Khai báo nhân viên mới và cấp tài khoản đăng nhập hệ thống";
                btnLuu.Text = "💾 Tạo nhân sự mới";
                btnLuu.BackColor = UITheme.PrimaryOrange;
            }
        }

        private void InitializeComponentCustom()
        {
            Text = "Thông tin nhân sự & Phân quyền";
            Size = new Size(580, 560);
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
                Text = "➕ THÊM MỚI NHÂN SỰ & TÀI KHOẢN",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Khai báo nhân viên mới và cấp tài khoản đăng nhập hệ thống",
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

            // Họ tên
            TaoNhan(pnlBody, "Họ và tên nhân viên *", 28, y);
            var pnlHoTen = TaoBox(pnlBody, 28, y + 20, 508, 36);
            txtHoTen = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Nguyễn Văn A" };
            pnlHoTen.Controls.Add(txtHoTen);

            // Chức vụ & Số điện thoại
            y += gap;
            TaoNhan(pnlBody, "Chức vụ *", 28, y);
            var pnlChucVu = TaoBox(pnlBody, 28, y + 20, 245, 36);
            cboChucVu = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F) };
            cboChucVu.Items.AddRange(new object[] { "Quản lý", "Lễ tân", "Kế toán", "Buồng phòng", "Bảo vệ" });
            cboChucVu.SelectedIndex = 1;
            pnlChucVu.Controls.Add(cboChucVu);

            TaoNhan(pnlBody, "Số điện thoại", 291, y);
            var pnlSDT = TaoBox(pnlBody, 291, y + 20, 245, 36);
            txtSDT = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "09xxxxxxxx" };
            pnlSDT.Controls.Add(txtSDT);

            // Tên đăng nhập
            y += gap;
            TaoNhan(pnlBody, "Tên đăng nhập hệ thống *", 28, y);
            var pnlUser = TaoBox(pnlBody, 28, y + 20, 508, 36);
            txtTenDangNhap = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Ít nhất 4 ký tự" };
            pnlUser.Controls.Add(txtTenDangNhap);

            // Mật khẩu
            y += gap;
            TaoNhan(pnlBody, "Mật khẩu đăng nhập *", 28, y);
            var pnlPass = TaoBox(pnlBody, 28, y + 20, 508, 36);
            txtMatKhau = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, UseSystemPasswordChar = true, PlaceholderText = "Ít nhất 8 ký tự (để trống nếu không đổi)" };
            pnlPass.Controls.Add(txtMatKhau);

            lblHintMatKhau = new Label
            {
                Text = "💡 Để trống ô mật khẩu nếu muốn giữ nguyên mật khẩu cũ của nhân viên khi cập nhật.",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                Location = new Point(28, y + 58),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblHintMatKhau);

            // Vai trò
            y += gap + 18;
            TaoNhan(pnlBody, "Vai trò & Phân quyền truy cập *", 28, y);
            var pnlRole = TaoBox(pnlBody, 28, y + 20, 508, 36);
            cboVaiTro = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F) };
            cboVaiTro.Items.AddRange(new object[] { "Admin - Quản trị viên (Toàn quyền hệ thống)", "LeTan - Lễ tân thu ngân (Nghiệp vụ phòng & hóa đơn)" });
            cboVaiTro.SelectedIndex = 1;
            pnlRole.Controls.Add(cboVaiTro);

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

        private void TaiThongTin(int id)
        {
            try
            {
                string sql = @"
                    SELECT t.TenDangNhap, t.VaiTro, n.HoTen, n.ChucVu, n.SoDienThoai
                    FROM TaiKhoan t
                    JOIN NhanVien n ON n.MaNhanVien = t.MaNhanVien
                    WHERE t.MaTaiKhoan = @id";

                using var conn = DatabaseHelper.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                using var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    txtTenDangNhap.Text = r["TenDangNhap"].ToString();
                    txtHoTen.Text = r["HoTen"].ToString();
                    txtSDT.Text = r["SoDienThoai"] == DBNull.Value ? "" : r["SoDienThoai"].ToString();

                    string chucVu = r["ChucVu"].ToString();
                    int idxCv = cboChucVu.FindStringExact(chucVu);
                    cboChucVu.SelectedIndex = idxCv >= 0 ? idxCv : 1;

                    string vaiTro = r["VaiTro"].ToString();
                    cboVaiTro.SelectedIndex = vaiTro.Equals("Admin", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
                }
            }
            catch { }
        }

        private static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 210000, HashAlgorithmName.SHA256, 32);
            return "PBKDF2-SHA256$210000$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(hash);
        }

        private void LuuDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên nhân viên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || txtTenDangNhap.Text.Trim().Length < 4)
            {
                MessageBox.Show("Tên đăng nhập bắt buộc và phải có ít nhất 4 ký tự.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            string vaiTro = cboVaiTro.SelectedIndex == 0 ? "Admin" : "LeTan";
            string chucVu = cboChucVu.Text;
            string hoTen = txtHoTen.Text.Trim();
            string sdt = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim();
            string user = txtTenDangNhap.Text.Trim();

            if (maTaiKhoan == 0)
            {
                // TẠO MỚI
                if (string.IsNullOrWhiteSpace(txtMatKhau.Text) || txtMatKhau.Text.Length < 8)
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu ban đầu (ít nhất 8 ký tự).", "Mật khẩu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return;
                }

                try
                {
                    using var conn = DatabaseHelper.GetConnection();
                    conn.Open();
                    using var tx = conn.BeginTransaction();

                    int newMaNV;
                    string sqlNV = "INSERT INTO NhanVien (HoTen, ChucVu, SoDienThoai) VALUES (@ten, @cv, @sdt) RETURNING MaNhanVien";
                    using (var cmd = new SqlCommand(sqlNV, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@ten", hoTen);
                        cmd.Parameters.AddWithValue("@cv", chucVu);
                        cmd.Parameters.AddWithValue("@sdt", (object)sdt ?? DBNull.Value);
                        newMaNV = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    string sqlTK = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNhanVien) VALUES (@user, @pass, @role, @nv)";
                    using (var cmd = new SqlCommand(sqlTK, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", HashPassword(txtMatKhau.Text));
                        cmd.Parameters.AddWithValue("@role", vaiTro);
                        cmd.Parameters.AddWithValue("@nv", newMaNV);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    MessageBox.Show($"Tạo mới nhân viên & tài khoản [{user}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tạo tài khoản. Tên đăng nhập có thể đã tồn tại trong hệ thống.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // CẬP NHẬT
                try
                {
                    using var conn = DatabaseHelper.GetConnection();
                    conn.Open();
                    using var tx = conn.BeginTransaction();

                    string sqlNV = "UPDATE NhanVien SET HoTen = @ten, ChucVu = @cv, SoDienThoai = @sdt WHERE MaNhanVien = @nv";
                    using (var cmd = new SqlCommand(sqlNV, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@ten", hoTen);
                        cmd.Parameters.AddWithValue("@cv", chucVu);
                        cmd.Parameters.AddWithValue("@sdt", (object)sdt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@nv", maNhanVien);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlTK;
                    if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                    {
                        if (txtMatKhau.Text.Length < 8) throw new Exception("Mật khẩu mới phải có ít nhất 8 ký tự.");
                        sqlTK = "UPDATE TaiKhoan SET TenDangNhap = @user, VaiTro = @role, MatKhau = @pass WHERE MaTaiKhoan = @id";
                    }
                    else
                    {
                        sqlTK = "UPDATE TaiKhoan SET TenDangNhap = @user, VaiTro = @role WHERE MaTaiKhoan = @id";
                    }

                    using (var cmd = new SqlCommand(sqlTK, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@role", vaiTro);
                        cmd.Parameters.AddWithValue("@id", maTaiKhoan);
                        if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                        {
                            cmd.Parameters.AddWithValue("@pass", HashPassword(txtMatKhau.Text));
                        }
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    MessageBox.Show($"Cập nhật nhân viên [{hoTen}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
