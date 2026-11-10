using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDangNhap : Form
    {
        private readonly TextBox txtTenDangNhap = new();
        private readonly TextBox txtMatKhau = new();
        private readonly NutHienMatKhau btnHienMatKhau = new();
        private readonly Button btnDangNhap = new();
        private readonly Nhan lblLoi = new();
        private readonly System.Windows.Forms.Timer tmrDemNguocKhoa = new();
        private int _giayConLaiKhoa = 0;

        public frmDangNhap()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        // ============================================================
        //  GIAO DIỆN ĐĂNG NHẬP: bố cục 2 cột (ảnh khách sạn | biểu mẫu trắng).
        //  Chỉ phần dựng giao diện thay đổi; logic đăng nhập ở các hàm bên dưới giữ nguyên.
        //  Muốn dùng ảnh khách sạn thật: chép đè file Resources\login_hero.jpg (nên ~1000x1200 px).
        // ============================================================
        private void DungGiaoDien()
        {
            // Quy mọi kích thước về 96 DPI rồi tự nhân theo mức phóng to của Windows (100%/125%/150%).
            // Trước đây chữ (tính bằng point) to theo DPI còn khung (tính bằng pixel) thì không -> chữ đè nhau, bị cắt.
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;

            Text = "Đăng nhập hệ thống - An Nhiên Hotel";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(900, 560);
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            // Bảng màu giữ nguyên như bản cũ: tiêu đề cam, chữ xanh đậm, nền trắng, nút cam
            var cam = Color.FromArgb(232, 119, 26);
            var xanhDam = Color.FromArgb(15, 37, 55);
            var xam = Color.FromArgb(100, 116, 139);
            const int rongND = 340;   // bề rộng khối nội dung bên phải

            // ---- Cột phải: biểu mẫu (nền trắng) ----
            var pnlPhai = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            var noiDung = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                Margin = Padding.Empty
            };
            pnlPhai.Controls.Add(noiDung);

            var lblThuongHieu = new Nhan
            {
                Text = "AN NHIÊN HOTEL",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = cam,
                AutoSize = false,
                Size = new Size(rongND, 44),
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false,
                Margin = new Padding(0)
            };
            var lblTieuDe = new Nhan
            {
                Text = "ĐĂNG NHẬP HỆ THỐNG",
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                ForeColor = xanhDam,
                AutoSize = false,
                Size = new Size(rongND, 28),
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false,
                Margin = new Padding(0)
            };
            var lblChao = new Nhan
            {
                Text = "Chào mừng trở lại! Vui lòng đăng nhập để tiếp tục làm việc.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = xam,
                AutoSize = false,
                Size = new Size(rongND, 44),
                TextAlign = ContentAlignment.TopLeft,
                NhieuDong = true,
                UseMnemonic = false,
                Margin = new Padding(0, 4, 0, 14)
            };

            var lblUser = new Nhan
            {
                Text = "Tên đăng nhập",
                Font = new Font("Segoe UI", 9.2F, FontStyle.Bold),
                ForeColor = xanhDam,
                AutoSize = false,
                Size = new Size(rongND, 24),
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false,
                Margin = new Padding(0, 0, 0, 4)
            };
            var oUser = new OInput { Size = new Size(rongND, 44), Margin = new Padding(0, 0, 0, 16) };
            txtTenDangNhap.Font = new Font("Segoe UI", 11F);
            txtTenDangNhap.PlaceholderText = "Nhập tài khoản (vd: admin, letan01)";
            oUser.Gan(txtTenDangNhap);

            var lblPass = new Nhan
            {
                Text = "Mật khẩu",
                Font = new Font("Segoe UI", 9.2F, FontStyle.Bold),
                ForeColor = xanhDam,
                AutoSize = false,
                Size = new Size(rongND, 24),
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false,
                Margin = new Padding(0, 0, 0, 4)
            };
            var oPass = new OInput { Size = new Size(rongND, 44), Margin = new Padding(0) };
            txtMatKhau.Font = new Font("Segoe UI", 11F);
            txtMatKhau.UseSystemPasswordChar = true;
            txtMatKhau.PlaceholderText = "Nhập mật khẩu";

            btnHienMatKhau.Width = 44;
            btnHienMatKhau.Click += (_, __) =>
            {
                txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;
                btnHienMatKhau.DangHien = !txtMatKhau.UseSystemPasswordChar;
                txtMatKhau.Focus();
            };
            components ??= new System.ComponentModel.Container();
            new ToolTip(components).SetToolTip(btnHienMatKhau, "Hiện / ẩn mật khẩu");
            oPass.Gan(txtMatKhau, btnHienMatKhau);

            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.Size = new Size(rongND, 48);
            btnDangNhap.Margin = new Padding(0, 22, 0, 0);
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatAppearance.MouseOverBackColor = Color.FromArgb(214, 104, 14);
            btnDangNhap.FlatAppearance.MouseDownBackColor = Color.FromArgb(190, 90, 10);
            btnDangNhap.BackColor = cam;
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnDangNhap.Cursor = Cursors.Hand;
            btnDangNhap.UseMnemonic = false;
            btnDangNhap.Click += BtnDangNhap_Click;

            lblLoi.AutoSize = false;
            lblLoi.Size = new Size(rongND, 50);
            lblLoi.Margin = new Padding(0, 8, 0, 0);
            lblLoi.ForeColor = Color.FromArgb(220, 38, 38);
            lblLoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLoi.TextAlign = ContentAlignment.MiddleCenter;
            lblLoi.NhieuDong = true;
            lblLoi.UseMnemonic = false;

            var lblBaoMat = new Nhan
            {
                Text = "Bảo mật PBKDF2-SHA256  •  Chống dò quét Brute-Force",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(148, 163, 184),
                AutoSize = false,
                Size = new Size(rongND, 22),
                TextAlign = ContentAlignment.MiddleCenter,
                UseMnemonic = false,
                Margin = new Padding(0, 4, 0, 0)
            };

            noiDung.Controls.Add(lblThuongHieu);
            noiDung.Controls.Add(lblTieuDe);
            noiDung.Controls.Add(lblChao);
            noiDung.Controls.Add(lblUser);
            noiDung.Controls.Add(oUser);
            noiDung.Controls.Add(lblPass);
            noiDung.Controls.Add(oPass);
            noiDung.Controls.Add(btnDangNhap);
            noiDung.Controls.Add(lblLoi);
            noiDung.Controls.Add(lblBaoMat);

            // Luôn đặt khối nội dung chính giữa cột phải (kể cả khi Windows phóng to chữ)
            void CanGiua()
            {
                noiDung.Location = new Point(
                    Math.Max(0, (pnlPhai.ClientSize.Width - noiDung.Width) / 2),
                    Math.Max(0, (pnlPhai.ClientSize.Height - noiDung.Height) / 2));
            }
            pnlPhai.Resize += (_, __) => CanGiua();
            noiDung.SizeChanged += (_, __) => CanGiua();

            // ---- Cột trái: ảnh khách sạn ----
            var pnlAnh = new AnhNenPanel(TaiAnhNen()) { Dock = DockStyle.Left, Width = 440 };

            // QUAN TRỌNG: control Fill phải nằm ở vị trí 0 của Controls (thêm TRƯỚC) để được xếp bố cục SAU
            // panel Left; nếu thêm ngược lại nó sẽ chiếm cả form và bị panel ảnh che mất một phần.
            Controls.Add(pnlPhai);
            Controls.Add(pnlAnh);

            AcceptButton = btnDangNhap;
            ActiveControl = txtTenDangNhap;

            tmrDemNguocKhoa.Interval = 1000;
            tmrDemNguocKhoa.Tick += TmrDemNguocKhoa_Tick;
        }

        private static Image TaiAnhNen()
        {
            try
            {
                using var luong = typeof(frmDangNhap).Assembly.GetManifestResourceStream("login_hero.jpg");
                if (luong == null) return null;
                using var goc = Image.FromStream(luong);
                return new Bitmap(goc);   // sao chép ra Bitmap để không phụ thuộc luồng đã đóng
            }
            catch
            {
                return null;              // không có ảnh thì panel tự vẽ nền chuyển sắc thay thế
            }
        }

        /// <summary>
        /// Nhãn chữ canh lề chính xác. Label mặc định của WinForms tự chèn đệm ~1/6 cỡ chữ ở hai bên
        /// (chữ càng to càng thụt vào nhiều) nên các dòng chữ không thẳng hàng; lớp này tắt đệm đó.
        /// </summary>
        private sealed class Nhan : Label
        {
            [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public bool NhieuDong { get; set; }

            public Nhan()
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                         | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
                AutoSize = false;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var co = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix;
                co |= NhieuDong ? TextFormatFlags.WordBreak : TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis;
                switch (TextAlign)
                {
                    case ContentAlignment.MiddleLeft: co |= TextFormatFlags.Left | TextFormatFlags.VerticalCenter; break;
                    case ContentAlignment.MiddleCenter: co |= TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter; break;
                    case ContentAlignment.TopCenter: co |= TextFormatFlags.HorizontalCenter | TextFormatFlags.Top; break;
                    default: co |= TextFormatFlags.Left | TextFormatFlags.Top; break;
                }
                TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, co);
            }
        }

        /// <summary>Nút hiện/ẩn mật khẩu: biểu tượng con mắt vẽ bằng nét vector, đổi màu cam khi rê chuột.</summary>
        private sealed class NutHienMatKhau : Control
        {
            private bool _hover;
            private bool _dangHien;

            [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public bool DangHien
            {
                get => _dangHien;
                set { _dangHien = value; Invalidate(); }
            }

            public NutHienMatKhau()
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                         | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
                SetStyle(ControlStyles.Selectable, false);   // không chiếm phím Tab
                TabStop = false;
                Cursor = Cursors.Hand;
                BackColor = Color.White;
            }

            protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
            protected override void OnMouseLeave(EventArgs e) { _hover = false; Invalidate(); base.OnMouseLeave(e); }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                float k = DeviceDpi / 96f;
                float cx = Width / 2f, cy = Height / 2f;
                float rong = 11f * k, cao = 7f * k;
                var mau = (_hover || _dangHien) ? Color.FromArgb(232, 119, 26) : Color.FromArgb(148, 163, 184);

                using var but = new Pen(mau, 1.8f * k) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round };
                using var mat = new GraphicsPath();
                mat.AddBezier(cx - rong, cy, cx - rong / 2, cy - cao * 1.9f, cx + rong / 2, cy - cao * 1.9f, cx + rong, cy);
                mat.AddBezier(cx + rong, cy, cx + rong / 2, cy + cao * 1.9f, cx - rong / 2, cy + cao * 1.9f, cx - rong, cy);
                g.DrawPath(but, mat);

                using var longDen = new SolidBrush(mau);
                float r = 2.8f * k;
                g.FillEllipse(longDen, cx - r, cy - r, 2 * r, 2 * r);

                // Đang hiện mật khẩu -> gạch chéo con mắt (bấm để ẩn lại)
                if (_dangHien)
                    g.DrawLine(but, cx - rong * 0.8f, cy + cao * 1.3f, cx + rong * 0.8f, cy - cao * 1.3f);
            }
        }

        /// <summary>Ô nhập có viền bo góc; viền đổi sang màu cam khi đang gõ.</summary>
        private sealed class OInput : Panel
        {
            private TextBox _campo;
            private Control _phai;
            private bool _dangFocus;

            public OInput()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
                         | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
                BackColor = Color.White;
                Padding = new Padding(14, 2, 6, 2);
            }

            public void Gan(TextBox campo, Control phai = null)
            {
                _campo = campo;
                _phai = phai;
                campo.BorderStyle = BorderStyle.None;
                campo.BackColor = Color.White;
                if (phai != null)
                {
                    phai.Dock = DockStyle.Right;
                    Controls.Add(phai);
                }
                Controls.Add(campo);
                campo.Enter += (_, __) => { _dangFocus = true; Invalidate(); };
                campo.Leave += (_, __) => { _dangFocus = false; Invalidate(); };
                Click += (_, __) => campo.Focus();
                XepLai();
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                XepLai();
            }

            private void XepLai()
            {
                if (_campo == null) return;
                int rongPhai = _phai?.Width ?? 0;
                _campo.Width = Math.Max(20, Width - Padding.Left - Padding.Right - rongPhai);
                _campo.Left = Padding.Left;
                _campo.Top = Math.Max(0, (Height - _campo.Height) / 2);   // canh giữa theo chiều dọc
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var vien = new Rectangle(0, 0, Width - 1, Height - 1);
                using var duongDan = TaoHinhBoGoc(vien, 8);
                using var nen = new SolidBrush(Color.White);
                g.FillPath(nen, duongDan);
                using var but = new Pen(_dangFocus ? Color.FromArgb(232, 119, 26) : Color.FromArgb(203, 213, 225),
                                        _dangFocus ? 1.8F : 1F);
                g.DrawPath(but, duongDan);
            }

            private static GraphicsPath TaoHinhBoGoc(Rectangle r, int banKinh)
            {
                int d = banKinh * 2;
                var p = new GraphicsPath();
                p.AddArc(r.X, r.Y, d, d, 180, 90);
                p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                p.CloseFigure();
                return p;
            }
        }

        /// <summary>Panel bên trái: vẽ ảnh khách sạn (cắt cho đầy khung) cùng lớp phủ và dòng chữ giới thiệu.</summary>
        private sealed class AnhNenPanel : Panel
        {
            private readonly Image _anh;

            public AnhNenPanel(Image anh)
            {
                _anh = anh;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
                         | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing) _anh?.Dispose();
                base.Dispose(disposing);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                var rc = ClientRectangle;
                if (rc.Width <= 0 || rc.Height <= 0) return;

                if (_anh != null)
                {
                    // "cover": phóng ảnh phủ kín khung rồi cắt phần thừa ở giữa
                    float tiLe = Math.Max((float)rc.Width / _anh.Width, (float)rc.Height / _anh.Height);
                    float w = _anh.Width * tiLe, h = _anh.Height * tiLe;
                    g.DrawImage(_anh, (rc.Width - w) / 2f, (rc.Height - h) / 2f, w, h);
                }
                else
                {
                    using var nen = new LinearGradientBrush(rc, Color.FromArgb(15, 37, 55), Color.FromArgb(232, 119, 26), 70F);
                    g.FillRectangle(nen, rc);
                }

                // Lớp phủ tối ở dưới để chữ trắng luôn dễ đọc, và nhẹ ở trên cho logo
                var duoi = new Rectangle(0, rc.Height / 2, rc.Width, rc.Height - rc.Height / 2);
                using (var phu = new LinearGradientBrush(duoi, Color.FromArgb(0, 8, 18, 36), Color.FromArgb(220, 8, 18, 36), 90F))
                    g.FillRectangle(phu, duoi);
                var tren = new Rectangle(0, 0, rc.Width, (int)(120 * DeviceDpi / 96f));
                using (var phuT = new LinearGradientBrush(tren, Color.FromArgb(150, 8, 18, 36), Color.FromArgb(0, 8, 18, 36), 90F))
                    g.FillRectangle(phuT, tren);

                float k = DeviceDpi / 96f;     // hệ số phóng to của Windows
                float le = 32 * k;             // lề trái/phải
                float rongChu = rc.Width - 2 * le;

                // Logo chữ ở góc trên trái
                using var cam = new SolidBrush(Color.FromArgb(255, 150, 50));
                using var fLogo = new Font("Segoe UI", 15F, FontStyle.Bold);
                using var fLogoNho = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                float hLogo = fLogo.GetHeight(g), hNho = fLogoNho.GetHeight(g);
                float yLogo = 28 * k;
                g.FillRectangle(cam, le, yLogo, 4 * k, hLogo + hNho - 4 * k);
                g.DrawString("AN NHIÊN", fLogo, Brushes.White, le + 12 * k, yLogo);
                g.DrawString("H O T E L", fLogoNho, cam, le + 13 * k, yLogo + hLogo - 4 * k);

                // Khẩu hiệu: xếp từ DƯỚI LÊN theo chiều cao đo thật nên không bao giờ đè hay bị cắt
                using var fTieuDe = new Font("Segoe UI", 20F, FontStyle.Bold);
                using var fMoTa = new Font("Segoe UI", 9.5F);
                using var fChan = new Font("Segoe UI", 8F);
                using var moTa = new SolidBrush(Color.FromArgb(215, 224, 238));
                using var chan = new SolidBrush(Color.FromArgb(160, 175, 200));

                float yChan = rc.Height - 26 * k - fChan.GetHeight(g);
                g.DrawString("© 2026 An Nhiên Hotel", fChan, chan, le, yChan);

                float yMoTa = yChan - 12 * k - fMoTa.GetHeight(g);
                g.DrawString("Hệ thống quản lý khách sạn nội bộ", fMoTa, moTa, le, yMoTa);

                const string khauHieu = "Nghỉ dưỡng tinh tế,\nphục vụ tận tâm.";
                float hKhauHieu = g.MeasureString(khauHieu, fTieuDe, (int)rongChu).Height;
                float yKhauHieu = yMoTa - 18 * k - hKhauHieu;
                g.DrawString(khauHieu, fTieuDe, Brushes.White, new RectangleF(le, yKhauHieu, rongChu, hKhauHieu + 6 * k));

                g.FillRectangle(cam, le, yKhauHieu - 16 * k, 48 * k, 4 * k);
            }
        }

        private void TmrDemNguocKhoa_Tick(object sender, EventArgs e)
        {
            _giayConLaiKhoa--;
            if (_giayConLaiKhoa <= 0)
            {
                tmrDemNguocKhoa.Stop();
                btnDangNhap.Enabled = true;
                btnDangNhap.Text = "ĐĂNG NHẬP";
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
                using var cmd = new SqlCommand("SELECT MatKhau, VaiTro, MaNhanVien FROM TaiKhoan WHERE TenDangNhap=@user", conn);
                cmd.Parameters.AddWithValue("@user", txtTenDangNhap.Text.Trim());
                string storedPassword = null;
                string role = null;
                int? maNhanVien = null;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        storedPassword = Convert.ToString(reader["MatKhau"]);
                        role = Convert.ToString(reader["VaiTro"]);
                        if (reader["MaNhanVien"] != DBNull.Value)
                            maNhanVien = Convert.ToInt32(reader["MaNhanVien"]);
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
                Session.MaNhanVien = maNhanVien;
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
