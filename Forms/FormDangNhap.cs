using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;
using System.IO;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormDangNhap : Form
    {
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private Button btnHienMatKhau;
        private CheckBox chkGhiNho;
        private Button btnDangNhap;
        private LinkLabel llDangKy, llQuenMatKhau;
        private Label lblLoi;

        public FormDangNhap()
        {
            KhoiTaoGiaoDien();
            NapTenDangNhapDaLuu();
        }

        private void KhoiTaoGiaoDien()
        {
            AutoScaleMode = AutoScaleMode.None; // Tat tu dong phong to theo DPI, tranh giao dien bi lech/de nhau

            Text = "Đăng nhập - Khách sạn An Nhiên";
            ClientSize = new Size(980, 620); // Dung ClientSize (vung noi dung thuc), khong dung Size, de tinh toa do chinh xac
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.White;
            Font = GiaoDien.FontChuan;

            // ===== Khung trái: gioi thieu thuong hieu (nen navy dam) =====
            var panelTrai = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(420, 620),
                BackColor = GiaoDien.MauChinhDam
            };
            Controls.Add(panelTrai);

            // Replace textual left-panel branding with an image
            try
            {
                // Try several relative locations so the image can be found in dev or published output
                string[] candidates = new[] {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Không gian khách sạn.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Không gian khách sạn.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Không gian khách sạn\\Không gian khách sạn.png"),
                };

                string imgPath = candidates.FirstOrDefault(p => File.Exists(p));
                if (!string.IsNullOrEmpty(imgPath))
                {
                    var pic = new PictureBox
                    {
                        Image = Image.FromFile(imgPath),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Location = new Point(0, 0),
                        Size = panelTrai.Size,
                        Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
                    };
                    panelTrai.Controls.Add(pic);
                }
                else
                {
                    // If image not found, keep a minimal accessible fallback text
                    var lblFallback = new Label
                    {
                        Text = "KHÁCH SẠN AN NHIÊN",
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                        Location = new Point(32, 32),
                        AutoSize = true
                    };
                    panelTrai.Controls.Add(lblFallback);
                }
            }
            catch
            {
                // If loading the image fails for any reason, do not crash the form; silently fall back
                var lblFallback = new Label
                {
                    Text = "KHÁCH SẠN AN NHIÊN",
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                    Location = new Point(32, 32),
                    AutoSize = true
                };
                panelTrai.Controls.Add(lblFallback);
            }

            // ===== Khung phải: form dang nhap (the trang giua man hinh) =====
            var panelPhai = new Panel
            {
                Location = new Point(420, 0),
                Size = new Size(560, 620),
                BackColor = ColorTranslator.FromHtml("#f4f6fa")
            };
            Controls.Add(panelPhai);

            var the = new Panel
            {
                Size = new Size(400, 460),
                Location = new Point(80, 80), // Canh giua thu cong: (560-400)/2 = 80
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelPhai.Controls.Add(the);

            var lblTenHt = new Label
            {
                Text = "KHÁCH SẠN AN NHIÊN",
                ForeColor = GiaoDien.MauChinhDam,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(0, 24),
                Size = new Size(400, 26),
                TextAlign = ContentAlignment.MiddleCenter
            };
            the.Controls.Add(lblTenHt);

            var lblHeThong = new Label
            {
                Text = "Hệ thống quản lý khách sạn",
                ForeColor = GiaoDien.MauChuMo,
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(0, 52),
                Size = new Size(400, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            the.Controls.Add(lblHeThong);

            var lblDangNhap = new Label
            {
                Text = "Đăng nhập",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                Location = new Point(32, 92),
                AutoSize = true
            };
            the.Controls.Add(lblDangNhap);

            var lblPhu = new Label
            {
                Text = "Nhập thông tin tài khoản để tiếp tục ca làm việc.",
                ForeColor = GiaoDien.MauChuMo,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(32, 120),
                AutoSize = true
            };
            the.Controls.Add(lblPhu);

            lblLoi = new Label
            {
                ForeColor = GiaoDien.MauNguyHiem,
                Location = new Point(32, 144),
                Size = new Size(336, 36),
                Font = new Font("Segoe UI", 9F)
            };
            the.Controls.Add(lblLoi);

            var lblTen = new Label { Text = "Tên đăng nhập *", Font = GiaoDien.FontNhan, Location = new Point(32, 184), AutoSize = true };
            the.Controls.Add(lblTen);
            txtTenDangNhap = new TextBox { Location = new Point(32, 206), Size = new Size(336, 28), Font = GiaoDien.FontChuan, MaxLength = 50 };
            the.Controls.Add(txtTenDangNhap);

            var lblMk = new Label { Text = "Mật khẩu *", Font = GiaoDien.FontNhan, Location = new Point(32, 242), AutoSize = true };
            the.Controls.Add(lblMk);
            txtMatKhau = new TextBox { Location = new Point(32, 264), Size = new Size(298, 28), Font = GiaoDien.FontChuan, PasswordChar = '*' };
            the.Controls.Add(txtMatKhau);

            btnHienMatKhau = new Button
            {
                Text = "👁",
                Location = new Point(334, 264),
                Size = new Size(34, 28),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnHienMatKhau.FlatAppearance.BorderColor = GiaoDien.MauVien;
            btnHienMatKhau.Click += (s, e) =>
                txtMatKhau.PasswordChar = txtMatKhau.PasswordChar == '*' ? '\0' : '*';
            the.Controls.Add(btnHienMatKhau);

            chkGhiNho = new CheckBox
            {
                Text = "Ghi nhớ đăng nhập",
                Location = new Point(32, 306),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F)
            };
            the.Controls.Add(chkGhiNho);

            llQuenMatKhau = new LinkLabel
            {
                Text = "Quên mật khẩu?",
                Location = new Point(255, 306),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F)
            };
            llQuenMatKhau.Click += (s, e) => MessageBox.Show(
                "Vui lòng liên hệ Admin để được đặt lại mật khẩu.",
                "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            the.Controls.Add(llQuenMatKhau);

            btnDangNhap = new Button { Text = "Đăng nhập", Location = new Point(32, 340), Size = new Size(336, 38) };
            GiaoDien.ApDungNutChinh(btnDangNhap);
            btnDangNhap.Click += BtnDangNhap_Click;
            the.Controls.Add(btnDangNhap);

            llDangKy = new LinkLabel
            {
                Text = "Chưa có tài khoản? Đăng ký tài khoản",
                Location = new Point(80, 392),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F)
            };
            llDangKy.Click += (s, e) =>
            {
                using var f = new FormDangKy();
                f.ShowDialog();
            };
            the.Controls.Add(llDangKy);

            var lblFooter = new Label
            {
                Text = "© 2026 An Nhiên Hotel",
                ForeColor = GiaoDien.MauChuMo,
                Font = new Font("Segoe UI", 8F),
                Location = new Point(0, 428),
                Size = new Size(400, 18),
                TextAlign = ContentAlignment.MiddleCenter
            };
            the.Controls.Add(lblFooter);

            AcceptButton = btnDangNhap;
        }

        // Ghi nho ten dang nhap gan nhat vao 1 file nho trong may (khong luu mat khau)
        private static string DuongDanFileGhiNho =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "QuanLyKhachSan_Phan1", "ghi_nho_dang_nhap.txt");

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
            catch { /* Khong sao neu khong doc duoc, chi la tien ich nho */ }
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
            catch { /* Bo qua loi ghi file, khong anh huong dang nhap */ }
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

            int id = 0;
            string tenLuu = "", hoTen = "", vaiTro = "", matKhauDaLuu = "", trangThai = "";

            try
            {
                using (var conn = DatabaseHelper.LayKetNoi())
                using (var cmd = new SqlCommand(
                    @"SELECT id, ten_dang_nhap, mat_khau, ho_ten, vai_tro, trang_thai
                      FROM tai_khoan WHERE ten_dang_nhap = @tdn", conn))
                {
                    cmd.Parameters.AddWithValue("@tdn", tenDangNhap);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            lblLoi.Text = "Tên đăng nhập hoặc mật khẩu không đúng.";
                            return;
                        }

                        id = Convert.ToInt32(reader["id"]);
                        tenLuu = (string)reader["ten_dang_nhap"];
                        matKhauDaLuu = (string)reader["mat_khau"];
                        hoTen = (string)reader["ho_ten"];
                        vaiTro = (string)reader["vai_tro"];
                        trangThai = (string)reader["trang_thai"];
                    }
                }
            }
            catch (SqlException ex)
            {
                lblLoi.Text = "Lỗi kết nối CSDL: " + ex.Message;
                return;
            }

            if (!MatKhauHelper.KiemTra(matKhau, matKhauDaLuu))
            {
                lblLoi.Text = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return;
            }

            if (trangThai == "ChoDuyet")
            {
                lblLoi.Text = "Tài khoản đang chờ Admin phê duyệt. Vui lòng quay lại sau.";
                return;
            }
            if (trangThai == "KhoaTaiKhoan")
            {
                lblLoi.Text = "Tài khoản của bạn đã bị khóa. Liên hệ Admin để được hỗ trợ.";
                return;
            }

            LuuHoacXoaTenDangNhap(tenLuu);

            Session.Id = id;
            Session.TenDangNhap = tenLuu;
            Session.HoTen = hoTen;
            Session.VaiTro = vaiTro;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}