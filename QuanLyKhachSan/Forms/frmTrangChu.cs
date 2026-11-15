using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmTrangChu : Form
    {
        private Control manHinhHienTai;
        private ucDatPhong manHinhDatPhong;
        private frmKhachHang manHinhKhachHang;
        private ucBanLamViecLeTan manHinhBanLamViec;
        private frmDanhSachPhong manHinhPhong;
        private frmDichVu manHinhDichVu;
        private frmHoaDon manHinhHoaDon;
        private frmVoucher manHinhVoucher;
        private frmBaoCao manHinhBaoCao;
        private frmTaiKhoan manHinhTaiKhoan;
        private frmLoaiPhong manHinhLoaiPhong;
        private frmPhuongThucThanhToan manHinhPhuongThucTT;

        private Button nutDangChon = null;
        private Button btnBanLamViec;
        private Button btnTaiKhoan;
        private Button btnPhuongThucTT;
        private Button btnLoaiPhong;
        private Button btnKhoaManHinh;
        private Button btnDoiMK;
        private Label lblBreadcrumb;
        private Label lblClock;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Timer _timerTabAnim;
        private float _headerSweepProgress = 1.0f;
        private float _activePillScale = 1.0f;
        private bool _daChayIntro = false;

        public frmTrangChu()
        {
            InitializeComponent();
            UITheme.SetDoubleBuffered(this);
            UITheme.DisableMnemonics(this);
            lblTitleSection.UseMnemonic = false; // hiển thị đúng ký tự '&' trong tiêu đề

            // 1. NÂNG CẤP LOGO THƯƠNG HIỆU TRÊN SIDEBAR (PRO MAX BRAND HEADER)
            lblHotelName.Location = new Point(74, 19);
            lblHotelName.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblHotelName.ForeColor = Color.White;
            lblHotelSub.Location = new Point(75, 48);
            lblHotelSub.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblHotelSub.ForeColor = Color.FromArgb(251, 146, 60);
            lblHotelSub.Text = "HOSPITALITY PMS PRO";
            pnlLogo.Paint += PnlLogo_Paint;

            // 2. KHỞI TẠO CÁC NÚT ĐIỀU HƯỚNG BỔ SUNG
            btnBanLamViec = new Button
            {
                Name = "btnBanLamViec",
                Text = "⊞  Bàn làm việc lễ tân",
                Dock = DockStyle.Top,
                Height = 52,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(203, 213, 225),
                BackColor = Color.FromArgb(15, 37, 55),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(26, 0, 0, 0),
                UseMnemonic = false
            };
            btnBanLamViec.FlatAppearance.BorderSize = 0;
            btnBanLamViec.Click += (_, __) => MoBanLamViec();
            pnlSidebar.Controls.Add(btnBanLamViec);
            pnlSidebar.Controls.SetChildIndex(btnBanLamViec, pnlSidebar.Controls.IndexOf(btnPhong));

            btnTaiKhoan = new Button
            {
                Name = "btnTaiKhoan",
                Text = "🛡️  Tài khoản & Phân quyền",
                Dock = DockStyle.Top,
                Height = 52,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(203, 213, 225),
                BackColor = Color.FromArgb(15, 37, 55),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(26, 0, 0, 0),
                UseMnemonic = false
            };
            btnTaiKhoan.FlatAppearance.BorderSize = 0;
            btnTaiKhoan.Click += (_, __) => MoTaiKhoan();
            pnlSidebar.Controls.Add(btnTaiKhoan);
            pnlSidebar.Controls.SetChildIndex(btnTaiKhoan, 1);

            btnPhuongThucTT = new Button
            {
                Name = "btnPhuongThucTT",
                Text = "💳  Phương thức TT",
                Dock = DockStyle.Top,
                Height = 52,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(203, 213, 225),
                BackColor = Color.FromArgb(15, 37, 55),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(26, 0, 0, 0),
                UseMnemonic = false,
                Visible = false
            };
            btnPhuongThucTT.FlatAppearance.BorderSize = 0;
            btnPhuongThucTT.Click += (_, __) => MoPhuongThucTT();

            // Menu "Loại phòng & giá" (Admin): đặt ngay dưới "Quản lý phòng"
            btnLoaiPhong = new Button
            {
                Name = "btnLoaiPhong",
                Text = "🏷️  Loại phòng & giá",
                Dock = DockStyle.Top,
                Height = 52,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(203, 213, 225),
                BackColor = Color.FromArgb(15, 37, 55),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(26, 0, 0, 0),
                UseMnemonic = false
            };
            btnLoaiPhong.FlatAppearance.BorderSize = 0;
            btnLoaiPhong.Click += (_, __) => MoLoaiPhong();
            pnlSidebar.Controls.Add(btnLoaiPhong);
            pnlSidebar.Controls.SetChildIndex(btnLoaiPhong, pnlSidebar.Controls.GetChildIndex(btnPhong));

            // 3. ĐỒNG BỘ PRO MAX NAV INDICATOR CHO TOÀN BỘ NÚT MENU SIDEBAR
            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button btn && btn != btnDangXuat)
                {
                    btn.UseMnemonic = false;
                    btn.Height = 52;
                    btn.Padding = new Padding(26, 0, 0, 0);
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, 47, 69);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(26, 58, 84);
                    btn.Paint -= NutMenuSidebar_Paint;
                    btn.Paint += NutMenuSidebar_Paint;
                }
            }

            btnDangXuat.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 41, 59);
            btnDangXuat.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(30, 58, 82), 1f);
                e.Graphics.DrawLine(pen, 20, 0, btnDangXuat.Width - 20, 0);
            };

            // 4. THIẾT KẾ HEADER TRUNG TÂM HIỆN ĐẠI (BREADCRUMB + ĐỒNG HỒ THỜI GIAN THỰC + NÚT BẢO MẬT)
            KeyPreview = true;
            KeyDown += frmTrangChu_KeyDown;

            lblBreadcrumb = new Label
            {
                Name = "lblBreadcrumb",
                Text = "AN NHIÊN HOTEL  •  PHÂN HỆ TÁC NGHIỆP",
                Font = new Font("Segoe UI", 7.8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(38, 13),
                AutoSize = true,
                UseMnemonic = false
            };
            pnlHeader.Controls.Add(lblBreadcrumb);

            lblTitleSection.Location = new Point(38, 30);
            lblTitleSection.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblTitleSection.ForeColor = UITheme.DarkNavy;

            btnKhoaManHinh = new Button
            {
                Name = "btnKhoaManHinh",
                Text = "🔒 Khóa máy (Ctrl+L)",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Size = new Size(158, 36),
                Location = new Point(pnlHeader.Width - 175, 19),
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnKhoaManHinh.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnKhoaManHinh.MouseEnter += (s, e) => btnKhoaManHinh.BackColor = Color.FromArgb(226, 232, 240);
            btnKhoaManHinh.MouseLeave += (s, e) => btnKhoaManHinh.BackColor = Color.FromArgb(248, 250, 252);
            btnKhoaManHinh.Click += (s, e) => MoKhoaManHinh();

            btnDoiMK = new Button
            {
                Name = "btnDoiMK",
                Text = "🔑 Đổi MK",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Size = new Size(98, 36),
                Location = new Point(pnlHeader.Width - 282, 19),
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDoiMK.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnDoiMK.MouseEnter += (s, e) => btnDoiMK.BackColor = Color.FromArgb(226, 232, 240);
            btnDoiMK.MouseLeave += (s, e) => btnDoiMK.BackColor = Color.FromArgb(248, 250, 252);
            btnDoiMK.Click += (s, e) => MoDoiMatKhau();

            lblClock = new Label
            {
                Name = "lblClock",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlHeader.Width - 690, 14),
                Size = new Size(395, 20),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange,
                TextAlign = ContentAlignment.MiddleRight,
                Text = "🕒 " + DateTime.Now.ToString("HH:mm:ss  •  dd/MM/yyyy")
            };

            lblUserGreeting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserGreeting.Location = new Point(pnlHeader.Width - 690, 35);
            lblUserGreeting.Size = new Size(395, 24);
            lblUserGreeting.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            lblUserGreeting.ForeColor = UITheme.DarkNavy;

            pnlHeader.Controls.Add(lblClock);
            pnlHeader.Controls.Add(btnKhoaManHinh);
            pnlHeader.Controls.Add(btnDoiMK);

            timerClock = new System.Windows.Forms.Timer { Interval = 1000 };
            timerClock.Tick += (_, __) =>
            {
                if (!IsDisposed && lblClock != null)
                    lblClock.Text = "🕒 " + DateTime.Now.ToString("HH:mm:ss  •  dd/MM/yyyy");
            };
            timerClock.Start();

            ApDungPhanQuyen();

            // 5. HIỆU ỨNG ENTRANCE ANIMATION KHI ĐĂNG NHẬP VÀO TRANG CHỦ (60FPS HARDWARE + STAGGERED CASCADE)
            Opacity = 0.02;
            Shown += frmTrangChu_Shown;
        }

        private void frmTrangChu_Shown(object sender, EventArgs e)
        {
            var navButtons = pnlSidebar.Controls
                .OfType<Button>()
                .Where(b => b.Visible && b != btnDangXuat)
                .OrderBy(b => b.Top)
                .ToList();

            Color bgDark = Color.FromArgb(15, 37, 55);
            Color fgNormal = Color.FromArgb(203, 213, 225);

            // Đặt trạng thái khởi đầu của hiệu ứng Staggered Cascade
            foreach (var btn in navButtons)
            {
                btn.Padding = new Padding(58, 0, 0, 0);
                btn.ForeColor = bgDark;
            }
            lblBreadcrumb.Location = new Point(76, 13);
            lblTitleSection.Location = new Point(76, 30);
            _headerSweepProgress = 0.0f;
            _activePillScale = 0.1f;
            manHinhBanLamViec?.CapNhatKhungHinhIntro(0, 24);

            int step = 0;
            const int totalSteps = 24; // ~360ms ở 60FPS
            var tmrIntro = new System.Windows.Forms.Timer { Interval = 15 };
            tmrIntro.Tick += (s, ev) =>
            {
                if (IsDisposed || !IsHandleCreated)
                {
                    tmrIntro.Stop();
                    tmrIntro.Dispose();
                    return;
                }

                step++;
                double t = Math.Min(1.0, (double)step / totalSteps);
                double easeOut = 1.0 - Math.Pow(1.0 - t, 3); // Cubic Ease-Out

                // 1) Fade-In toàn cửa sổ (GPU DWM Compositor)
                Opacity = Math.Max(0.02, easeOut);

                // 2) Trượt tiêu đề Header & Dải sáng Shimmer
                int headerX = 38 + (int)Math.Round(38 * (1.0 - easeOut));
                lblBreadcrumb.Location = new Point(headerX, 13);
                lblTitleSection.Location = new Point(headerX, 30);
                _headerSweepProgress = (float)easeOut;
                _activePillScale = (float)(0.1 + 0.9 * easeOut);
                pnlHeader.Invalidate();

                // 3) Staggered Cascade cho từng nút Menu trên Sidebar từ trên xuống
                for (int i = 0; i < navButtons.Count; i++)
                {
                    var btn = navButtons[i];
                    double startStep = i * 1.5;
                    double duration = Math.Max(1.0, totalSteps - 8.0);
                    double tBtn = Math.Clamp((step - startStep) / duration, 0.0, 1.0);
                    double easeBtn = 1.0 - Math.Pow(1.0 - tBtn, 3);

                    int padLeft = 26 + (int)Math.Round(32 * (1.0 - easeBtn));
                    btn.Padding = new Padding(padLeft, 0, 0, 0);

                    Color targetFg = ReferenceEquals(btn, nutDangChon) ? Color.White : fgNormal;
                    btn.ForeColor = PhaMau(bgDark, targetFg, easeBtn);
                    btn.Invalidate();
                }

                // 4) Hiệu ứng nổi tuần tự 4 thẻ Quick Actions trên Bàn làm việc Lễ tân
                manHinhBanLamViec?.CapNhatKhungHinhIntro(step, totalSteps);

                if (step >= totalSteps)
                {
                    Opacity = 1.0;
                    _headerSweepProgress = 1.0f;
                    _activePillScale = 1.0f;
                    lblBreadcrumb.Location = new Point(38, 13);
                    lblTitleSection.Location = new Point(38, 30);
                    foreach (var btn in navButtons)
                    {
                        btn.Padding = new Padding(26, 0, 0, 0);
                        btn.ForeColor = ReferenceEquals(btn, nutDangChon) ? Color.White : fgNormal;
                        btn.Invalidate();
                    }
                    pnlHeader.Invalidate();
                    _daChayIntro = true;
                    tmrIntro.Stop();
                    tmrIntro.Dispose();
                }
            };
            tmrIntro.Start();
        }

        private static Color PhaMau(Color c1, Color c2, double ratio)
        {
            ratio = Math.Clamp(ratio, 0.0, 1.0);
            int r = (int)Math.Round(c1.R + (c2.R - c1.R) * ratio);
            int g = (int)Math.Round(c1.G + (c2.G - c1.G) * ratio);
            int b = (int)Math.Round(c1.B + (c2.B - c1.B) * ratio);
            return Color.FromArgb(r, g, b);
        }

        private void ChayHieuUngChuyenTab()
        {
            if (!_daChayIntro || IsDisposed || !IsHandleCreated) return;

            _timerTabAnim?.Stop();
            _timerTabAnim?.Dispose();

            int step = 0;
            const int totalSteps = 10; // ~150ms phản hồi tức thì
            _headerSweepProgress = 0.0f;
            _activePillScale = 0.2f;
            lblTitleSection.Location = new Point(58, 30);

            _timerTabAnim = new System.Windows.Forms.Timer { Interval = 15 };
            _timerTabAnim.Tick += (s, e) =>
            {
                if (IsDisposed || !IsHandleCreated)
                {
                    _timerTabAnim?.Stop();
                    return;
                }

                step++;
                double t = Math.Min(1.0, (double)step / totalSteps);
                double easeOut = 1.0 - Math.Pow(1.0 - t, 3);

                lblTitleSection.Location = new Point(38 + (int)Math.Round(20 * (1.0 - easeOut)), 30);
                _headerSweepProgress = (float)easeOut;
                _activePillScale = (float)(0.2 + 0.8 * easeOut);

                pnlHeader.Invalidate();
                nutDangChon?.Invalidate();

                if (step >= totalSteps)
                {
                    lblTitleSection.Location = new Point(38, 30);
                    _headerSweepProgress = 1.0f;
                    _activePillScale = 1.0f;
                    pnlHeader.Invalidate();
                    nutDangChon?.Invalidate();
                    _timerTabAnim.Stop();
                    _timerTabAnim.Dispose();
                    _timerTabAnim = null;
                }
            };
            _timerTabAnim.Start();
        }

        private void frmTrangChu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.L)
            {
                e.SuppressKeyPress = true;
                MoKhoaManHinh();
            }
        }

        private void MoKhoaManHinh()
        {
            using var f = new frmKhoaManHinhModal();
            if (f.ShowDialog(this) == DialogResult.Abort)
            {
                Close();
            }
        }

        private void MoDoiMatKhau()
        {
            using var f = new frmDoiMatKhauModal();
            f.ShowDialog(this);
        }

        private void ApDungPhanQuyen()
        {
            bool admin = Quản_Lý_Khách_Sạn.Session.LaAdmin;
            btnTaiKhoan.Visible = admin;
            btnPhuongThucTT.Visible = false; // Tinh gọn menu: Phương thức TT tích hợp sẵn trong màn hình Hóa đơn
            btnLoaiPhong.Visible = admin;
            btnVoucher.Visible = admin;
            btnBaoCao.Visible = admin;
            lblUserGreeting.Text = (admin ? "🛡️ Quản trị viên (Admin)" : "🛎️ Ca trực Lễ tân") +
                "  •  " + Quản_Lý_Khách_Sạn.Session.TenDangNhap;
        }

        private bool YeuCauAdmin()
        {
            if (Quản_Lý_Khách_Sạn.Session.LaAdmin) return true;
            MessageBox.Show(this, "Bạn không có quyền thực hiện chức năng này.",
                "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            MoBanLamViec();
        }

        private void MoBanLamViec()
        {
            if (manHinhBanLamViec == null || manHinhBanLamViec.IsDisposed)
            {
                manHinhBanLamViec = new ucBanLamViecLeTan();
                manHinhBanLamViec.YeuCauDatPhong += (_, __) => MoDatPhong();
                manHinhBanLamViec.YeuCauNhanPhong += (_, __) => ThucHienNhanPhong();
                manHinhBanLamViec.YeuCauTraPhong += (_, __) => ThucHienTraPhong();
                manHinhBanLamViec.YeuCauTraCuuKhach += (_, __) => MoKhachHang();
                manHinhBanLamViec.YeuCauQuanLyPhong += (_, __) => MoDanhSachPhong();
            }
            else
            {
                manHinhBanLamViec.TaiDuLieuHoatDong();
            }

            HienThiManHinh(manHinhBanLamViec, btnBanLamViec, "BÀN LÀM VIỆC LỄ TÂN");
        }

        private void MoDatPhong()
        {
            if (manHinhDatPhong == null || manHinhDatPhong.IsDisposed)
                manHinhDatPhong = new ucDatPhong();

            HienThiManHinh(manHinhDatPhong, btnDatPhong, "ĐẶT PHÒNG & LƯU TRÚ");
        }

        private void MoKhachHang()
        {
            bool daMo = manHinhKhachHang != null && !manHinhKhachHang.IsDisposed;
            if (!daMo)
                manHinhKhachHang = new frmKhachHang();

            HienThiManHinh(manHinhKhachHang, btnKhachHang, "HỒ SƠ KHÁCH HÀNG");
            if (daMo) manHinhKhachHang.LamMoiLuuTru();
        }

        private void MoDanhSachPhong()
        {
            if (manHinhPhong == null || manHinhPhong.IsDisposed)
                manHinhPhong = new frmDanhSachPhong();

            HienThiManHinh(manHinhPhong, btnPhong, "DANH SÁCH & TRẠNG THÁI PHÒNG");
        }

        private void MoDichVu()
        {
            if (manHinhDichVu == null || manHinhDichVu.IsDisposed)
                manHinhDichVu = new frmDichVu();

            HienThiManHinh(manHinhDichVu, btnDichVu, "DANH MỤC DỊCH VỤ PHÒNG");
        }

        private void MoHoaDon()
        {
            if (manHinhHoaDon == null || manHinhHoaDon.IsDisposed)
                manHinhHoaDon = new frmHoaDon();

            HienThiManHinh(manHinhHoaDon, btnHoaDon, "HÓA ĐƠN & THANH TOÁN TRẢ PHÒNG");
        }

        private void MoVoucher()
        {
            if (!YeuCauAdmin()) return;
            if (manHinhVoucher == null || manHinhVoucher.IsDisposed)
                manHinhVoucher = new frmVoucher();

            HienThiManHinh(manHinhVoucher, btnVoucher, "QUẢN LÝ MÃ GIẢM GIÁ (VOUCHER)");
        }

        private void MoBaoCao()
        {
            if (!YeuCauAdmin()) return;
            if (manHinhBaoCao == null || manHinhBaoCao.IsDisposed)
                manHinhBaoCao = new frmBaoCao();

            HienThiManHinh(manHinhBaoCao, btnBaoCao, "BÁO CÁO DOANH THU & THỐNG KÊ");
        }

        private void MoTaiKhoan()
        {
            if (!YeuCauAdmin()) return;
            if (manHinhTaiKhoan == null || manHinhTaiKhoan.IsDisposed)
                manHinhTaiKhoan = new frmTaiKhoan();

            HienThiManHinh(manHinhTaiKhoan, btnTaiKhoan, "QUẢN LÝ TÀI KHOẢN & PHÂN QUYỀN");
        }

        private void MoLoaiPhong()
        {
            if (!YeuCauAdmin()) return;
            if (manHinhLoaiPhong == null || manHinhLoaiPhong.IsDisposed)
                manHinhLoaiPhong = new frmLoaiPhong();

            HienThiManHinh(manHinhLoaiPhong, btnLoaiPhong, "QUẢN LÝ LOẠI PHÒNG & GIÁ");
        }

        private void MoPhuongThucTT()
        {
            if (!YeuCauAdmin()) return;
            if (manHinhPhuongThucTT == null || manHinhPhuongThucTT.IsDisposed)
                manHinhPhuongThucTT = new frmPhuongThucThanhToan();

            HienThiManHinh(manHinhPhuongThucTT, btnPhuongThucTT, "QUẢN LÝ PHƯƠNG THỨC THANH TOÁN");
        }

        private void ThucHienNhanPhong()
        {
            using var f = new frmChonDatPhong(ThaoTacDatPhong.NhanPhong);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                string hoi = $"Xác nhận NHẬN PHÒNG (Check-in)?\n\n• Mã đặt: {f.MaDat}\n• Khách hàng: {f.TenKhach}\n• Phòng: {f.SoPhong}";
                if (MessageBox.Show(hoi, "Xác nhận nhận phòng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LeTanLuuTruBLL.NhanPhong(f.MaDatPhong);
                        MessageBox.Show($"🎉 Nhận phòng thành công!\nKhách hàng {f.TenKhach} đã nhận phòng {f.SoPhong}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        manHinhBanLamViec?.TaiDuLieuHoatDong();
                        manHinhPhong?.TaiLaiDuLieu();
                    }
                    catch (NghiepVuException ex)
                    {
                        MessageBox.Show(ex.Message, "Lưu ý nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi nhận phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ThucHienTraPhong()
        {
            using var f = new frmChonDatPhong(ThaoTacDatPhong.TraPhong);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                // Mở cửa sổ thanh toán & lập hóa đơn trả phòng
                using var fTT = new frmThanhToanModal(f.MaDat);
                if (fTT.ShowDialog(this) == DialogResult.OK)
                {
                    manHinhBanLamViec?.TaiDuLieuHoatDong();
                    manHinhPhong?.TaiLaiDuLieu();
                    manHinhHoaDon?.TaiLaiDuLieu();
                }
                else
                {
                    // Nếu chưa thanh toán ngay qua hóa đơn, hỏi xem có muốn ghi nhận trả phòng trước không
                    var hoi = MessageBox.Show($"Bạn chưa hoàn tất thanh toán hóa đơn.\nBạn có muốn ghi nhận TRẢ PHÒNG (Check-out) cho phòng {f.SoPhong} ({f.TenKhach}) ngay không?", "Xác nhận trả phòng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (hoi == DialogResult.Yes)
                    {
                        try
                        {
                            LeTanLuuTruBLL.TraPhong(f.MaDatPhong);
                            MessageBox.Show($"Đã ghi nhận trả phòng {f.SoPhong}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            manHinhBanLamViec?.TaiDuLieuHoatDong();
                            manHinhPhong?.TaiLaiDuLieu();
                            manHinhHoaDon?.TaiLaiDuLieu();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi trả phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // Các form khác vẫn gọi qua hàm cũ của trang chủ.
        private void MoFormCon(Form formCon, Button nutMenu, string tieuDe)
        {
            HienThiManHinh(formCon, nutMenu, tieuDe);
        }

        private void HienThiManHinh(Control manHinh, Button nutMenu, string tieuDe)
        {
            if (ReferenceEquals(manHinhHienTai, manHinh))
                return;

            Control manHinhCu = manHinhHienTai;
            try
            {
                if (manHinh.Parent != pnlContent)
                {
                    if (manHinh is Form formCon)
                    {
                        formCon.TopLevel = false;
                        formCon.FormBorderStyle = FormBorderStyle.None;
                        formCon.ShowInTaskbar = false;
                        formCon.WindowState = FormWindowState.Normal;
                    }

                    manHinh.Dock = DockStyle.Fill;
                    manHinh.Visible = false;
                    pnlContent.Controls.Add(manHinh);
                }

                UITheme.SetDoubleBuffered(manHinh);
                UITheme.DisableMnemonics(manHinh); // không để '&' bị nuốt thành phím tắt
                manHinh.Show();
                manHinh.BringToFront();

                if (manHinhCu != null && !manHinhCu.IsDisposed)
                {
                    // Tối ưu đa nhiệm Pro Max: Ẩn màn hình cũ, giữ nguyên bộ nhớ & trạng thái làm việc (không Dispose)
                    manHinhCu.Hide();
                }

                manHinhHienTai = manHinh;
                pnlContent.Tag = manHinh;
                lblTitleSection.Text = tieuDe;
                DoiTrangThaiNutMenu(nutMenu);
                ChayHieuUngChuyenTab();
                manHinh.Select();
            }
            catch (Exception ex)
            {
                pnlContent.Controls.Remove(manHinh);
                manHinh.Dispose();

                if (manHinhCu != null && !manHinhCu.IsDisposed)
                {
                    manHinhCu.Show();
                    manHinhCu.BringToFront();
                }

                MessageBox.Show(this,
                    "Không mở được màn hình: " + tieuDe
                    + Environment.NewLine + ex.Message,
                    "Lỗi điều hướng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoiTrangThaiNutMenu(Button btn)
        {
            if (btn == null) return;

            Button nutCu = nutDangChon;
            if (nutCu != null)
            {
                nutCu.BackColor = Color.FromArgb(15, 37, 55);
                nutCu.ForeColor = Color.FromArgb(203, 213, 225);
                nutCu.Invalidate();
            }

            nutDangChon = btn;
            nutDangChon.BackColor = Color.FromArgb(24, 54, 79);
            nutDangChon.ForeColor = Color.White;
            nutDangChon.Invalidate();
        }

        private void PnlLogo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Huy hiệu thương hiệu bo góc 40x40 bên trái
            var badgeRect = new Rectangle(22, 20, 40, 40);
            using (var badgePath = UITheme.GetRoundedRectPath(badgeRect, 10))
            using (var badgeBrush = new LinearGradientBrush(
                badgeRect,
                Color.FromArgb(234, 88, 12),
                Color.FromArgb(249, 115, 22),
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillPath(badgeBrush, badgePath);
            }

            using (var fontBadge = new Font("Segoe UI", 11F, FontStyle.Bold))
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    "AN",
                    fontBadge,
                    badgeRect,
                    Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            // Đường phân cách mảnh ở đáy khối Logo
            using (var pen = new Pen(Color.FromArgb(30, 58, 82), 1f))
            {
                e.Graphics.DrawLine(pen, 20, pnlLogo.Height - 1, pnlLogo.Width - 20, pnlLogo.Height - 1);
            }
        }

        private void NutMenuSidebar_Paint(object sender, PaintEventArgs e)
        {
            if (sender is not Button btn) return;
            bool active = ReferenceEquals(btn, nutDangChon);
            if (!active) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Thanh chỉ báo trạng thái đang chọn (Active Left Pill Bar) co giãn mượt theo _activePillScale
            int fullH = Math.Max(16, btn.Height - 20);
            int curH = Math.Max(6, (int)Math.Round(fullH * _activePillScale));
            int topY = (btn.Height - curH) / 2;
            var pillRect = new Rectangle(6, topY, 4, curH);
            using (var pillPath = UITheme.GetRoundedRectPath(pillRect, 2))
            using (var pillBrush = new SolidBrush(Color.FromArgb(249, 115, 22)))
            {
                e.Graphics.FillPath(pillBrush, pillPath);
            }

            // Dấu mũi tên điều hướng bên phải trượt êm vào vị trí
            int chevronOffset = (int)Math.Round(10 * (1.0f - _activePillScale));
            using (var chevronFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            {
                var chevronRect = new Rectangle(btn.Width - 28 - chevronOffset, 0, 20, btn.Height);
                TextRenderer.DrawText(
                    e.Graphics,
                    "›",
                    chevronFont,
                    chevronRect,
                    Color.FromArgb(251, 146, 60),
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Dải sáng Shimmer chạy ngang đỉnh Header khi khởi chạy hoặc chuyển Tab
            if (_headerSweepProgress > 0.0f && _headerSweepProgress < 1.0f && pnlHeader.Width > 10)
            {
                int sweepWidth = Math.Max(4, (int)Math.Round(pnlHeader.Width * _headerSweepProgress));
                var sweepRect = new Rectangle(0, 0, sweepWidth, 3);
                using (var sweepBrush = new LinearGradientBrush(
                    sweepRect,
                    UITheme.PrimaryOrange,
                    Color.FromArgb(251, 191, 36),
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(sweepBrush, sweepRect);
                }
            }

            // Thanh nhấn dọc màu cam bên trái tiêu đề trang co giãn mượt
            int fullAccentH = Math.Max(20, pnlHeader.Height - 32);
            int curAccentH = Math.Max(8, (int)Math.Round(fullAccentH * _activePillScale));
            int accentTop = (pnlHeader.Height - curAccentH) / 2;
            var accentRect = new Rectangle(22, accentTop, 4, curAccentH);
            using (var accentPath = UITheme.GetRoundedRectPath(accentRect, 2))
            using (var accentBrush = new SolidBrush(UITheme.PrimaryOrange))
            {
                e.Graphics.FillPath(accentBrush, accentPath);
            }

            // Đường viền đáy Header
            using (var pen = new Pen(Color.FromArgb(226, 232, 240), 1f))
            {
                e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            }
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            MoDanhSachPhong();
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            MoDatPhong();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            MoKhachHang();
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            MoDichVu();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            MoHoaDon();
        }

        private void btnVoucher_Click(object sender, EventArgs e)
        {
            MoVoucher();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            MoBaoCao();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất khỏi hệ thống?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Quản_Lý_Khách_Sạn.Session.DangXuat();
                this.Close(); // Program sẽ mở lại màn hình đăng nhập sau khi trang chủ đóng.
            }
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
