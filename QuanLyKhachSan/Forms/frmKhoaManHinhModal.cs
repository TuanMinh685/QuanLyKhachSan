using System;
using System.Drawing;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmKhoaManHinhModal : Form
    {
        private int giayKhoaConLai = 0;
        private readonly DateTime thoiDiemKhoa = DateTime.Now;

        public frmKhoaManHinhModal()
        {
            InitializeComponent();
            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;
            Bounds = Screen.PrimaryScreen.Bounds;
            TopMost = true;
            BackColor = Color.FromArgb(10, 25, 41);
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            KeyPreview = true;
            KeyDown += frmKhoaManHinhModal_KeyDown;

            // Timer cho rate-limiting
            tmrKhoa = new System.Windows.Forms.Timer { Interval = 1000 };
            tmrKhoa.Tick += TmrKhoa_Tick;

            // Card trung tâm
            pnlCard = new Panel
            {
                Size = new Size(460, 540),
                BackColor = Color.FromArgb(17, 34, 54),
                Padding = new Padding(32, 28, 32, 28)
            };
            pnlCard.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(30, 58, 88), 1.5f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
            };
            Controls.Add(pnlCard);
            CenterCard();
            Resize += (s, e) => CenterCard();

            // 1. Icon & Header
            lblLogo = new Label
            {
                Text = "🔒",
                Font = new Font("Segoe UI Emoji", 32F),
                ForeColor = Color.FromArgb(232, 119, 26),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(64, 60),
                Location = new Point((pnlCard.Width - 64) / 2, 20)
            };
            pnlCard.Controls.Add(lblLogo);

            lblTieuDe = new Label
            {
                Text = "BÀN LÀM VIỆC ĐANG KHÓA",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(pnlCard.Width - 40, 30),
                Location = new Point(20, 84)
            };
            pnlCard.Controls.Add(lblTieuDe);

            lblPhuDe = new Label
            {
                Text = "Hệ thống PMS tạm khóa bảo vệ an toàn khi bạn rời quầy",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(148, 163, 184),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(pnlCard.Width - 40, 20),
                Location = new Point(20, 114)
            };
            pnlCard.Controls.Add(lblPhuDe);

            // 2. User info box
            pnlUserInfo = new Panel
            {
                Location = new Point(32, 144),
                Size = new Size(pnlCard.Width - 64, 86),
                BackColor = Color.FromArgb(24, 46, 70)
            };
            pnlUserInfo.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(45, 75, 110), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlUserInfo.Width - 1, pnlUserInfo.Height - 1);
            };
            pnlCard.Controls.Add(pnlUserInfo);

            lblAvatar = new Label
            {
                Text = "👤",
                Font = new Font("Segoe UI Emoji", 20F),
                ForeColor = Color.FromArgb(232, 119, 26),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(46, 46),
                Location = new Point(14, 20)
            };
            pnlUserInfo.Controls.Add(lblAvatar);

            lblTenNguoiDung = new Label
            {
                Text = string.IsNullOrEmpty(Session.TenDangNhap) ? "Nhân viên quầy" : Session.TenDangNhap,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(66, 14),
                Size = new Size(300, 24)
            };
            pnlUserInfo.Controls.Add(lblTenNguoiDung);

            lblVaiTro = new Label
            {
                Text = Session.LaAdmin ? "🛡️ Quản trị viên (Admin)" : "🛎️ Nhân viên lễ tân",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Session.LaAdmin ? Color.FromArgb(245, 158, 11) : Color.FromArgb(56, 189, 248),
                Location = new Point(66, 38),
                Size = new Size(300, 20)
            };
            pnlUserInfo.Controls.Add(lblVaiTro);

            lblThoiGianKhoa = new Label
            {
                Text = $"Thời điểm khóa: {thoiDiemKhoa:HH:mm:ss dd/MM/yyyy}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(66, 58),
                Size = new Size(300, 18)
            };
            pnlUserInfo.Controls.Add(lblThoiGianKhoa);

            // 3. Password input
            lblMatKhau = new Label
            {
                Text = "MẬT KHẨU MỞ KHÓA:",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(203, 213, 225),
                Location = new Point(32, 246),
                AutoSize = true
            };
            pnlCard.Controls.Add(lblMatKhau);

            pnlInputPass = new Panel
            {
                Location = new Point(32, 270),
                Size = new Size(pnlCard.Width - 64, 42),
                BackColor = Color.FromArgb(10, 25, 41)
            };
            pnlInputPass.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(51, 85, 122), 1.2f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlInputPass.Width - 1, pnlInputPass.Height - 1);
            };
            pnlCard.Controls.Add(pnlInputPass);

            txtMatKhau = new TextBox
            {
                Location = new Point(12, 10),
                Size = new Size(pnlInputPass.Width - 52, 24),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(10, 25, 41),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F),
                UseSystemPasswordChar = true
            };
            pnlInputPass.Controls.Add(txtMatKhau);

            btnHienPass = new Button
            {
                Text = "👁",
                Font = new Font("Segoe UI Emoji", 9F),
                Location = new Point(pnlInputPass.Width - 38, 6),
                Size = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(148, 163, 184),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            btnHienPass.FlatAppearance.BorderSize = 0;
            btnHienPass.Click += (s, e) =>
            {
                txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;
                btnHienPass.ForeColor = txtMatKhau.UseSystemPasswordChar ? Color.FromArgb(148, 163, 184) : Color.FromArgb(232, 119, 26);
            };
            pnlInputPass.Controls.Add(btnHienPass);

            // 4. Thông báo lỗi
            lblLoi = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(248, 113, 113),
                Location = new Point(32, 318),
                Size = new Size(pnlCard.Width - 64, 38),
                TextAlign = ContentAlignment.TopCenter
            };
            pnlCard.Controls.Add(lblLoi);

            // 5. Nút mở khóa
            btnMoKhoa = new Button
            {
                Text = "🔓 MỞ KHÓA BÀN LÀM VIỆC",
                Location = new Point(32, 362),
                Size = new Size(pnlCard.Width - 64, 46),
                BackColor = Color.FromArgb(232, 119, 26),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnMoKhoa.FlatAppearance.BorderSize = 0;
            btnMoKhoa.Click += (s, e) => XuLyMoKhoa();
            pnlCard.Controls.Add(btnMoKhoa);

            // 6. Nút đổi tài khoản / đăng xuất
            btnDoiTaiKhoan = new Button
            {
                Text = "🚪 Đổi tài khoản khác / Bàn giao ca",
                Location = new Point(32, 422),
                Size = new Size(pnlCard.Width - 64, 36),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(148, 163, 184),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };
            btnDoiTaiKhoan.FlatAppearance.BorderSize = 0;
            btnDoiTaiKhoan.MouseEnter += (s, e) => btnDoiTaiKhoan.ForeColor = Color.FromArgb(248, 113, 113);
            btnDoiTaiKhoan.MouseLeave += (s, e) => btnDoiTaiKhoan.ForeColor = Color.FromArgb(148, 163, 184);
            btnDoiTaiKhoan.Click += (s, e) => XuLyDoiTaiKhoan();
            pnlCard.Controls.Add(btnDoiTaiKhoan);

            AcceptButton = btnMoKhoa;

            // Kiểm tra trạng thái brute-force ngay khi hiển thị
            KiemTraTrangThaiKhoa();
        }

        private void CenterCard()
        {
            pnlCard.Location = new Point(
                (ClientSize.Width - pnlCard.Width) / 2,
                (ClientSize.Height - pnlCard.Height) / 2);
        }

        private void frmKhoaManHinhModal_KeyDown(object sender, KeyEventArgs e)
        {
            // Chặn ESC hoặc Alt+F4 thoát mà không nhập mật khẩu
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Chặn Alt + F4 để không bypass khóa màn hình
            if (keyData == (Keys.Alt | Keys.F4))
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void KiemTraTrangThaiKhoa()
        {
            var khoa = BaoMatHelper.KiemTraKhoaDangNhap(Session.TenDangNhap);
            if (khoa.BiKhoa)
            {
                giayKhoaConLai = khoa.GiayConLai;
                btnMoKhoa.Enabled = false;
                txtMatKhau.Enabled = false;
                lblLoi.Text = $"Tài khoản tạm bị khóa do nhập sai nhiều lần.\nVui lòng đợi {giayKhoaConLai}s để mở khóa.";
                tmrKhoa.Start();
            }
        }

        private void TmrKhoa_Tick(object sender, EventArgs e)
        {
            giayKhoaConLai--;
            if (giayKhoaConLai <= 0)
            {
                tmrKhoa.Stop();
                btnMoKhoa.Enabled = true;
                txtMatKhau.Enabled = true;
                lblLoi.Text = "";
                txtMatKhau.Focus();
            }
            else
            {
                lblLoi.Text = $"Tài khoản tạm bị khóa do nhập sai nhiều lần.\nVui lòng đợi {giayKhoaConLai}s để mở khóa.";
            }
        }

        private void XuLyMoKhoa()
        {
            string pass = txtMatKhau.Text;
            if (string.IsNullOrEmpty(pass))
            {
                lblLoi.Text = "Vui lòng nhập mật khẩu để mở khóa.";
                txtMatKhau.Focus();
                return;
            }

            var checkKhoa = BaoMatHelper.KiemTraKhoaDangNhap(Session.TenDangNhap);
            if (checkKhoa.BiKhoa)
            {
                giayKhoaConLai = checkKhoa.GiayConLai;
                btnMoKhoa.Enabled = false;
                txtMatKhau.Enabled = false;
                lblLoi.Text = $"Tài khoản tạm khóa do nhập sai nhiều lần ({giayKhoaConLai}s).";
                tmrKhoa.Start();
                return;
            }

            try
            {
                string storedPass = null;
                string sql = "SELECT MatKhau FROM TaiKhoan WHERE TenDangNhap = @user";
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user", Session.TenDangNhap);
                    var val = cmd.ExecuteScalar();
                    if (val != null && val != DBNull.Value)
                    {
                        storedPass = val.ToString();
                    }
                }

                if (storedPass == null)
                {
                    lblLoi.Text = "Không tìm thấy thông tin tài khoản hiện tại.";
                    return;
                }

                bool hopLe = BaoMatHelper.KiemTraVaNangCap(pass, storedPass, Session.TenDangNhap, out _);
                if (hopLe)
                {
                    BaoMatHelper.XoaVepKhoaDangNhap(Session.TenDangNhap);
                    BaoMatHelper.GhiNhatKy(Session.TenDangNhap, "MO_KHOA_MAN_HINH", "Mở khóa màn hình lễ tân thành công");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    BaoMatHelper.GhiNhanDangNhapThatBai(Session.TenDangNhap, out int soLanConLai);
                    if (soLanConLai <= 0)
                    {
                        giayKhoaConLai = BaoMatHelper.ThoiGianKhoaGiay;
                        btnMoKhoa.Enabled = false;
                        txtMatKhau.Enabled = false;
                        lblLoi.Text = $"Đã nhập sai {BaoMatHelper.SoLanSaiToiDa} lần. Bị tạm khóa trong {giayKhoaConLai}s.";
                        tmrKhoa.Start();
                    }
                    else
                    {
                        lblLoi.Text = $"Mật khẩu không chính xác! Bạn còn {soLanConLai} lần thử.";
                        txtMatKhau.SelectAll();
                        txtMatKhau.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lblLoi.Text = "Lỗi xác thực: " + ex.Message;
            }
        }

        private void XuLyDoiTaiKhoan()
        {
            var hoi = MessageBox.Show(this,
                "Bạn có chắc chắn muốn đăng xuất tài khoản hiện tại để giao ca hoặc đăng nhập tài khoản khác?",
                "Xác nhận bàn giao ca",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (hoi == DialogResult.Yes)
            {
                BaoMatHelper.GhiNhatKy(Session.TenDangNhap, "DANG_XUAT_TU_MAN_HINH_KHOA", "Bàn giao ca / đăng xuất từ màn hình khóa");
                Session.DangXuat();
                DialogResult = DialogResult.Abort;
                Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Ngăn chặn đóng nếu không phải qua Unlock hoặc Đăng xuất
            if (DialogResult != DialogResult.OK && DialogResult != DialogResult.Abort)
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
    }
}
