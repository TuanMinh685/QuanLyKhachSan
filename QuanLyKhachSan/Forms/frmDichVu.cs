using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDichVu : Form
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");

        private readonly DichVuDAL dichVuDAL = new DichVuDAL();
        private readonly ChiTietHoaDonDAL chiTietDAL = new ChiTietHoaDonDAL();

        private List<DichVu> danhSachGoc = new List<DichVu>();
        private string maDVDangChon = null;
        private string danhMucDangLoc = "Tất cả";

        // Controls Cột Trái (cardLeft)
        private Panel pnlHeaderLeft;
        private Label lblTitleLeft;
        private Label lblSubtitleLeft;
        private Label lblNgayHienTai;

        private Panel pnlFilterBar;
        private TextBox txtTim;
        private ComboBox cboLocDanhMuc;
        private ComboBox cboLocTrangThai;
        private Button btnThemMoiDV;

        private FlowLayoutPanel pnlCategoryPills;
        private Button btnPillTatCa;
        private Button btnPillAnUong;
        private Button btnPillGiatUi;
        private Button btnPillDuaDon;
        private Button btnPillKhac;

        private Panel pnlBottomBarLeft;
        private Label lblDaChonDV;
        private Button btnSuaDV;
        private Button btnDoiTrangThaiDV;

        // Controls Cột Phải (cardRight: Ghi nhận cho phòng)
        private Panel pnlHeaderRight;
        private Label lblTitleRight;
        private Label lblPillDangSuDung;
        private Panel pnlDividerRight;

        private ComboBox cboPhongDangO;
        private ComboBox cboDichVuGhiNhan;
        private NumericUpDown nudSoLuong;
        private TextBox txtDonGiaHienThi;
        private TextBox txtThoiDiem;
        private Button btnThemVaoPhieu;

        private Label lblDichVuTrongKy;
        private Label lblBadgeSoMuc;
        private DataGridView dgvChiTietPhong;

        private Panel pnlTongTienBox;
        private Label lblTongTienTieuDe;
        private Label lblTongTienGiaTri;
        private Label lblTongTienSubtext;
        private Button btnThanhToanNgay;
        private Button btnThuTienDichVuRieng;

        private DataTable dtPhongDangO;
        private DataTable dtChiTietPhong;

        public frmDichVu()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
            KhoiTaoGiaoDienFigma();
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvDichVu);
            TaiDanhSachDichVu();
            TaiDanhSachPhongDangO();
        }

        private void KhoiTaoGiaoDienFigma()
        {
            // Thiết lập tỷ lệ 2 cột: 62% bảng danh mục trái, 38% ghi nhận cho phòng phải
            tblMainLayout.ColumnStyles[0].SizeType = SizeType.Percent;
            tblMainLayout.ColumnStyles[0].Width = 62F;
            tblMainLayout.ColumnStyles[1].SizeType = SizeType.Percent;
            tblMainLayout.ColumnStyles[1].Width = 38F;

            cardLeft.Margin = new Padding(0, 0, 14, 0);
            cardLeft.Padding = new Padding(20, 16, 20, 14);
            cardRight.Visible = true;
            cardRight.Margin = new Padding(0);
            cardRight.Padding = new Padding(20, 16, 20, 16);
            cardRight.Controls.Clear(); // Dọn sạch các control cũ của cardRight

            // =========================================================================
            // 1. CỘT TRÁI (CARD LEFT): BẢNG DANH MỤC DỊCH VỤ
            // =========================================================================

            // Header Cột Trái
            pnlHeaderLeft = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.Transparent
            };

            lblTitleLeft = new Label
            {
                Text = "Quản lý dịch vụ",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };

            lblSubtitleLeft = new Label
            {
                Text = "Danh mục dịch vụ và ghi nhận phát sinh cho phòng đang sử dụng",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = UITheme.TextMuted,
                Location = new Point(0, 26),
                AutoSize = true
            };

            lblNgayHienTai = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = DateTime.Today.ToString("dddd, dd/MM/yyyy", Vi),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = UITheme.TextMuted,
                Location = new Point(cardLeft.ClientSize.Width - 240, 6),
                Size = new Size(230, 22),
                TextAlign = ContentAlignment.MiddleRight
            };

            pnlHeaderLeft.Controls.Add(lblTitleLeft);
            pnlHeaderLeft.Controls.Add(lblSubtitleLeft);
            pnlHeaderLeft.Controls.Add(lblNgayHienTai);
            cardLeft.Controls.Add(pnlHeaderLeft);

            // Thanh công cụ tìm kiếm và bộ lọc trên cột trái
            pnlFilterBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 4, 0, 4)
            };

            txtTim = new TextBox
            {
                Location = new Point(0, 6),
                Size = new Size(240, 29),
                Font = new Font("Segoe UI", 9.5F),
                PlaceholderText = "🔍 Tìm tên hoặc mã dịch vụ... (Ctrl+F)"
            };
            txtTim.TextChanged += (_, __) => ApDungBoLoc();

            cboLocDanhMuc = new ComboBox
            {
                Location = new Point(248, 6),
                Size = new Size(130, 29),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            cboLocDanhMuc.Items.AddRange(new object[] { "Tất cả danh mục", "Ăn uống", "Giặt ủi", "Đưa đón", "Khác" });
            cboLocDanhMuc.SelectedIndex = 0;
            cboLocDanhMuc.SelectedIndexChanged += (_, __) => ApDungBoLoc();

            cboLocTrangThai = new ComboBox
            {
                Location = new Point(386, 6),
                Size = new Size(125, 29),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            cboLocTrangThai.Items.AddRange(new object[] { "Tất cả trạng thái", "Đang phục vụ", "Tạm ngưng" });
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndexChanged += (_, __) => ApDungBoLoc();

            btnThemMoiDV = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "＋ Thêm dịch vụ",
                Size = new Size(135, 34),
                Location = new Point(cardLeft.ClientSize.Width - 140, 4),
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThemMoiDV.FlatAppearance.BorderSize = 0;
            btnThemMoiDV.Click += (_, __) => MoModalThem();

            pnlFilterBar.Controls.AddRange(new Control[] { txtTim, cboLocDanhMuc, cboLocTrangThai, btnThemMoiDV });
            cardLeft.Controls.Add(pnlFilterBar);

            // Hàng Category Filter Pills (Theo đúng Figma: Ăn uống • 18, Giặt ủi • 8...)
            pnlCategoryPills = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 38,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 4, 0, 4)
            };

            btnPillTatCa = TaoButtonPill("Tất cả • 0", "Tất cả");
            btnPillAnUong = TaoButtonPill("🍽️ Ăn uống • 0", "Ăn uống");
            btnPillGiatUi = TaoButtonPill("👔 Giặt ủi • 0", "Giặt ủi");
            btnPillDuaDon = TaoButtonPill("🚗 Đưa đón • 0", "Đưa đón");
            btnPillKhac = TaoButtonPill("📦 Khác • 0", "Khác");

            pnlCategoryPills.Controls.AddRange(new Control[] { btnPillTatCa, btnPillAnUong, btnPillGiatUi, btnPillDuaDon, btnPillKhac });
            cardLeft.Controls.Add(pnlCategoryPills);

            // Bảng danh mục dgvDichVu nằm giữa
            pnlGridWrapper.Controls.Clear();
            pnlGridWrapper.Controls.Add(dgvDichVu);
            pnlGridWrapper.Dock = DockStyle.Fill;
            pnlGridWrapper.Padding = new Padding(0, 6, 0, 8);
            cardLeft.Controls.Add(pnlGridWrapper);

            // Thanh thao tác dưới cùng bên trái: Đã chọn DV... [Sửa] [Ngừng phục vụ] [Xóa]
            pnlBottomBarLeft = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 44,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(12, 6, 12, 6)
            };

            lblDaChonDV = new Label
            {
                Text = "Chưa chọn dịch vụ nào",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                Location = new Point(10, 11),
                AutoSize = true
            };

            var flowBottomActions = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlBottomBarLeft.Width - 360, 5),
                Size = new Size(350, 34),
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            btnDoiTrangThaiDV = new Button
            {
                Text = "🚫 Ngừng phục vụ",
                Width = 140,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(220, 38, 38),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDoiTrangThaiDV.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            btnDoiTrangThaiDV.Click += (_, __) => DoiTrangThaiDichVu();

            btnSuaDV = new Button
            {
                Text = "✏️ Sửa",
                Width = 80,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UITheme.DarkNavy,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSuaDV.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnSuaDV.Click += (_, __) => MoModalSua();

            flowBottomActions.Controls.AddRange(new Control[] { btnDoiTrangThaiDV, btnSuaDV });
            pnlBottomBarLeft.Controls.Add(lblDaChonDV);
            pnlBottomBarLeft.Controls.Add(flowBottomActions);
            cardLeft.Controls.Add(pnlBottomBarLeft);

            // Thứ tự hiển thị control trên cardLeft
            pnlBottomBarLeft.BringToFront();
            pnlCategoryPills.BringToFront();
            pnlFilterBar.BringToFront();
            pnlHeaderLeft.BringToFront();

            cardLeft.Resize += (_, __) =>
            {
                lblNgayHienTai.Location = new Point(Math.Max(300, cardLeft.ClientSize.Width - 240), 6);
                btnThemMoiDV.Location = new Point(Math.Max(400, cardLeft.ClientSize.Width - 140), 4);
                flowBottomActions.Location = new Point(Math.Max(150, pnlBottomBarLeft.Width - 360), 5);
            };

            // Sự kiện bảng dịch vụ
            dgvDichVu.CellDoubleClick += (_, __) => MoModalSua();
            dgvDichVu.SelectionChanged += DgvDichVu_SelectionChanged;
            dgvDichVu.CellFormatting += DgvDichVu_CellFormatting;

            // =========================================================================
            // 2. CỘT PHẢI (CARD RIGHT): GHI NHẬN CHO PHÒNG ĐANG SỬ DỤNG (CHUẨN FIGMA)
            // =========================================================================

            // Header Cột Phải
            pnlHeaderRight = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Color.Transparent
            };

            lblTitleRight = new Label
            {
                Text = "Ghi nhận cho phòng",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 2),
                AutoSize = true
            };

            lblPillDangSuDung = new Label
            {
                Text = "🔴 Đang sử dụng",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),
                BackColor = Color.FromArgb(254, 226, 226),
                Padding = new Padding(8, 3, 8, 3),
                Location = new Point(160, 2),
                AutoSize = true
            };

            pnlDividerRight = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = UITheme.BorderColor
            };

            pnlHeaderRight.Controls.Add(lblTitleRight);
            pnlHeaderRight.Controls.Add(lblPillDangSuDung);
            pnlHeaderRight.Controls.Add(pnlDividerRight);

            // Thẻ Box Tổng tiền dịch vụ dưới đáy Cột Phải
            pnlTongTienBox = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 110,
                BackColor = Color.FromArgb(255, 247, 237),
                Padding = new Padding(14, 10, 14, 10)
            };
            pnlTongTienBox.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle r = new Rectangle(0, 0, pnlTongTienBox.Width - 1, pnlTongTienBox.Height - 1);
                using var path = UITheme.GetRoundedRectPath(r, 8);
                using var pen = new Pen(Color.FromArgb(254, 215, 170), 1.2F);
                e.Graphics.DrawPath(pen, path);
            };

            lblTongTienTieuDe = new Label
            {
                Text = "Tổng tiền dịch vụ:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(14, 12),
                AutoSize = true
            };

            lblTongTienGiaTri = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "0 đ",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange,
                Location = new Point(pnlTongTienBox.Width - 210, 8),
                Size = new Size(195, 28),
                TextAlign = ContentAlignment.MiddleRight
            };

            lblTongTienSubtext = new Label
            {
                Text = "Cộng vào hóa đơn khi trả phòng hoặc thanh toán ngay",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                Location = new Point(14, 38),
                AutoSize = true
            };

            btnThanhToanNgay = new Button
            {
                Text = "💳 Thanh toán trả phòng",
                Size = new Size(190, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlTongTienBox.Width - 204, 62),
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThanhToanNgay.FlatAppearance.BorderSize = 0;
            btnThanhToanNgay.Click += (_, __) => MoThanhToanPhongDangChon();

            btnThuTienDichVuRieng = new Button
            {
                Text = "⚡ Thu riêng dịch vụ",
                Size = new Size(160, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlTongTienBox.Width - 374, 62),
                BackColor = UITheme.DarkNavy,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThuTienDichVuRieng.FlatAppearance.BorderSize = 0;
            btnThuTienDichVuRieng.Click += (_, __) => ThuTienDichVuRiengNgay();

            pnlTongTienBox.Controls.Add(lblTongTienTieuDe);
            pnlTongTienBox.Controls.Add(lblTongTienGiaTri);
            pnlTongTienBox.Controls.Add(lblTongTienSubtext);
            pnlTongTienBox.Controls.Add(btnThanhToanNgay);
            pnlTongTienBox.Controls.Add(btnThuTienDichVuRieng);

            // Container Form Nhập liệu trên cardRight
            Panel pnlInputsRight = new Panel
            {
                Dock = DockStyle.Top,
                Height = 270,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 8, 0, 0)
            };

            // 1. Chọn phòng đang lưu trú
            var lblPhong = new Label { Text = "Phòng đang sử dụng *", Font = new Font("Segoe UI", 8.8F, FontStyle.Bold), ForeColor = UITheme.TextMain, Location = new Point(0, 4), AutoSize = true };
            cboPhongDangO = new ComboBox
            {
                Location = new Point(0, 24),
                Size = new Size(cardRight.ClientSize.Width - 40, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboPhongDangO.SelectedIndexChanged += (_, __) => TaiChiTietDichVuPhongDangChon();

            // 2. Chọn dịch vụ
            var lblDV = new Label { Text = "Dịch vụ phát sinh *", Font = new Font("Segoe UI", 8.8F, FontStyle.Bold), ForeColor = UITheme.TextMain, Location = new Point(0, 62), AutoSize = true };
            cboDichVuGhiNhan = new ComboBox
            {
                Location = new Point(0, 82),
                Size = new Size(cardRight.ClientSize.Width - 40, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboDichVuGhiNhan.SelectedIndexChanged += (_, __) => CapNhatDonGiaTheoDichVuChon();

            // 3. Số lượng & Đơn giá
            var lblSL = new Label { Text = "Số lượng *", Font = new Font("Segoe UI", 8.8F, FontStyle.Bold), ForeColor = UITheme.TextMain, Location = new Point(0, 120), AutoSize = true };
            nudSoLuong = new NumericUpDown
            {
                Location = new Point(0, 140),
                Size = new Size(130, 28),
                DecimalPlaces = 1,
                Minimum = 0.5M,
                Maximum = 999M,
                Value = 1M,
                Font = new Font("Segoe UI", 9.5F)
            };

            var lblDG = new Label { Text = "Đơn giá", Font = new Font("Segoe UI", 8.8F, FontStyle.Bold), ForeColor = UITheme.TextMain, Location = new Point(145, 120), AutoSize = true };
            txtDonGiaHienThi = new TextBox
            {
                Location = new Point(145, 140),
                Size = new Size(cardRight.ClientSize.Width - 185, 28),
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 250, 252),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange
            };

            // 4. Thời điểm
            var lblTD = new Label { Text = "Thời điểm", Font = new Font("Segoe UI", 8.8F, FontStyle.Bold), ForeColor = UITheme.TextMain, Location = new Point(0, 178), AutoSize = true };
            txtThoiDiem = new TextBox
            {
                Location = new Point(0, 198),
                Size = new Size(cardRight.ClientSize.Width - 40, 28),
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 250, 252),
                Font = new Font("Segoe UI", 9F),
                Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            };

            // 5. Nút "+ Thêm vào phiếu"
            btnThemVaoPhieu = new Button
            {
                Text = "＋ Thêm vào phiếu",
                Location = new Point(0, 234),
                Size = new Size(cardRight.ClientSize.Width - 40, 36),
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThemVaoPhieu.FlatAppearance.BorderSize = 0;
            btnThemVaoPhieu.Click += (_, __) => GhiNhanDichVu();

            pnlInputsRight.Controls.AddRange(new Control[] {
                lblPhong, cboPhongDangO,
                lblDV, cboDichVuGhiNhan,
                lblSL, nudSoLuong,
                lblDG, txtDonGiaHienThi,
                lblTD, txtThoiDiem,
                btnThemVaoPhieu
            });
            cardRight.Controls.Add(pnlInputsRight);

            // Khung danh sách "Dịch vụ trong kỳ lưu trú"
            Panel pnlListWrap = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 8, 0, 8)
            };

            var pnlListHeader = new Panel { Dock = DockStyle.Top, Height = 26 };
            lblDichVuTrongKy = new Label
            {
                Text = "Dịch vụ trong kỳ lưu trú",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 2),
                AutoSize = true
            };
            lblBadgeSoMuc = new Label
            {
                Text = "0 mục",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryBlue,
                BackColor = Color.FromArgb(239, 246, 255),
                Location = new Point(155, 2),
                AutoSize = true,
                Padding = new Padding(6, 2, 6, 2)
            };
            pnlListHeader.Controls.Add(lblDichVuTrongKy);
            pnlListHeader.Controls.Add(lblBadgeSoMuc);
            pnlListWrap.Controls.Add(pnlListHeader);

            dgvChiTietPhong = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.None,
                ColumnHeadersVisible = false,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvChiTietPhong.RowTemplate.Height = 36;
            dgvChiTietPhong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            dgvChiTietPhong.DefaultCellStyle.SelectionForeColor = UITheme.TextMain;
            dgvChiTietPhong.CellContentClick += DgvChiTietPhong_CellContentClick;
            pnlListWrap.Controls.Add(dgvChiTietPhong);

            // Dọn dẹp và gắn control vào cardRight theo đúng thứ tự phân lớp WinForms chuẩn
            cardRight.Controls.Clear();
            cardRight.Controls.Add(pnlListWrap);
            cardRight.Controls.Add(pnlInputsRight);
            cardRight.Controls.Add(pnlTongTienBox);
            cardRight.Controls.Add(pnlHeaderRight);

            // Cố định Z-Index để đảm bảo phân lớp không bao giờ bị đè lấn
            cardRight.Controls.SetChildIndex(pnlHeaderRight, 3); // Dock Top trên cùng
            cardRight.Controls.SetChildIndex(pnlTongTienBox, 2); // Dock Bottom dưới cùng
            cardRight.Controls.SetChildIndex(pnlInputsRight, 1); // Dock Top dưới Header
            cardRight.Controls.SetChildIndex(pnlListWrap, 0);    // Dock Fill ở giữa hiển thị danh sách món đã gọi

            cardRight.Resize += (_, __) =>
            {
                int w = cardRight.ClientSize.Width - 40;
                cboPhongDangO.Width = w;
                cboDichVuGhiNhan.Width = w;
                txtDonGiaHienThi.Width = Math.Max(100, w - 145);
                txtThoiDiem.Width = w;
                btnThemVaoPhieu.Width = w;
                lblTongTienGiaTri.Location = new Point(pnlTongTienBox.Width - 210, 8);
                btnThanhToanNgay.Location = new Point(pnlTongTienBox.Width - 204, 62);
                btnThuTienDichVuRieng.Location = new Point(Math.Max(14, pnlTongTienBox.Width - 374), 62);
            };
        }

        private Button TaoButtonPill(string text, string tag)
        {
            var btn = new Button
            {
                Text = text,
                Tag = tag,
                AutoSize = true,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = (tag == "Tất cả") ? UITheme.DarkNavy : Color.FromArgb(241, 245, 249),
                ForeColor = (tag == "Tất cả") ? Color.White : UITheme.TextMain,
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 8, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (_, __) => ChonPillCategory(tag);
            return btn;
        }

        private void ChonPillCategory(string category)
        {
            danhMucDangLoc = category;
            foreach (Control c in pnlCategoryPills.Controls)
            {
                if (c is Button b)
                {
                    bool active = (b.Tag?.ToString() == category);
                    b.BackColor = active ? UITheme.DarkNavy : Color.FromArgb(241, 245, 249);
                    b.ForeColor = active ? Color.White : UITheme.TextMain;
                }
            }
            ApDungBoLoc();
        }

        private void TaiDanhSachDichVu()
        {
            try
            {
                danhSachGoc = dichVuDAL.GetAll();
                CapNhatPillCounts();
                ApDungBoLoc();
                NapComboDichVuGhiNhan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatPillCounts()
        {
            int total = danhSachGoc.Count;
            int anUong = danhSachGoc.Count(d => d.LoaiDichVu.IndexOf("ăn", StringComparison.OrdinalIgnoreCase) >= 0 || d.LoaiDichVu.IndexOf("uống", StringComparison.OrdinalIgnoreCase) >= 0);
            int giatUi = danhSachGoc.Count(d => d.LoaiDichVu.IndexOf("giặt", StringComparison.OrdinalIgnoreCase) >= 0);
            int duaDon = danhSachGoc.Count(d => d.LoaiDichVu.IndexOf("đón", StringComparison.OrdinalIgnoreCase) >= 0 || d.LoaiDichVu.IndexOf("xe", StringComparison.OrdinalIgnoreCase) >= 0);
            int khac = Math.Max(0, total - (anUong + giatUi + duaDon));

            btnPillTatCa.Text = $"Tất cả • {total}";
            btnPillAnUong.Text = $"🍽️ Ăn uống • {anUong}";
            btnPillGiatUi.Text = $"👔 Giặt ủi • {giatUi}";
            btnPillDuaDon.Text = $"🚗 Đưa đón • {duaDon}";
            btnPillKhac.Text = $"📦 Khác • {khac}";
        }

        private void ApDungBoLoc()
        {
            var query = danhSachGoc.AsEnumerable();

            // 1. Lọc theo danh mục Pills
            if (danhMucDangLoc != "Tất cả")
            {
                query = query.Where(d => d.LoaiDichVu.IndexOf(danhMucDangLoc, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // 2. Lọc theo Dropdown danh mục
            if (cboLocDanhMuc.SelectedIndex > 0)
            {
                string dm = cboLocDanhMuc.SelectedItem.ToString();
                query = query.Where(d => d.LoaiDichVu.IndexOf(dm, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // 3. Lọc theo Dropdown trạng thái
            if (cboLocTrangThai.SelectedIndex == 1) // Đang phục vụ
            {
                query = query.Where(d => d.TrangThai);
            }
            else if (cboLocTrangThai.SelectedIndex == 2) // Tạm ngưng
            {
                query = query.Where(d => !d.TrangThai);
            }

            // 4. Lọc theo từ khóa tìm kiếm
            string kw = txtTim.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(d =>
                    (d.MaDichVu != null && d.MaDichVu.ToLower().Contains(kw)) ||
                    (d.TenDichVu != null && d.TenDichVu.ToLower().Contains(kw)) ||
                    (d.LoaiDichVu != null && d.LoaiDichVu.ToLower().Contains(kw)));
            }

            HienThiLenBang(query.ToList());
        }

        private void HienThiLenBang(List<DichVu> danhSach)
        {
            dgvDichVu.DataSource = null;
            dgvDichVu.DataSource = danhSach;

            if (dgvDichVu.Columns["MaDichVu"] != null)
            {
                dgvDichVu.Columns["MaDichVu"].HeaderText = "MÃ";
                dgvDichVu.Columns["MaDichVu"].FillWeight = 65;
                dgvDichVu.Columns["MaDichVu"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                dgvDichVu.Columns["MaDichVu"].DefaultCellStyle.ForeColor = UITheme.DarkNavy;
                dgvDichVu.Columns["MaDichVu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvDichVu.Columns["TenDichVu"] != null)
            {
                dgvDichVu.Columns["TenDichVu"].HeaderText = "TÊN DỊCH VỤ";
                dgvDichVu.Columns["TenDichVu"].FillWeight = 160;
                dgvDichVu.Columns["TenDichVu"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
            if (dgvDichVu.Columns["LoaiDichVu"] != null)
            {
                dgvDichVu.Columns["LoaiDichVu"].HeaderText = "DANH MỤC";
                dgvDichVu.Columns["LoaiDichVu"].FillWeight = 90;
            }
            if (dgvDichVu.Columns["DonGia"] != null)
            {
                dgvDichVu.Columns["DonGia"].HeaderText = "ĐƠN GIÁ";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDichVu.Columns["DonGia"].FillWeight = 95;
            }
            if (dgvDichVu.Columns["DonViTinh"] != null)
            {
                dgvDichVu.Columns["DonViTinh"].HeaderText = "ĐVT";
                dgvDichVu.Columns["DonViTinh"].FillWeight = 55;
                dgvDichVu.Columns["DonViTinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvDichVu.Columns["TrangThai"] != null)
                dgvDichVu.Columns["TrangThai"].Visible = false;

            if (dgvDichVu.Columns["TrangThaiHienThi"] != null)
            {
                dgvDichVu.Columns["TrangThaiHienThi"].HeaderText = "TRẠNG THÁI";
                dgvDichVu.Columns["TrangThaiHienThi"].FillWeight = 100;
                dgvDichVu.Columns["TrangThaiHienThi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void DgvDichVu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = dgvDichVu.Columns[e.ColumnIndex];

            if (col.Name == "TrangThaiHienThi" && e.Value != null)
            {
                string text = e.Value.ToString();
                bool active = text.Contains("phục vụ");
                e.CellStyle.ForeColor = active ? Color.FromArgb(22, 163, 74) : Color.FromArgb(148, 163, 184);
                e.CellStyle.Font = new Font("Segoe UI", 8.8F, active ? FontStyle.Bold : FontStyle.Regular);
            }
            else if (col.Name == "DonGia" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal gia))
                {
                    e.Value = gia.ToString("N0") + " đ";
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    e.CellStyle.ForeColor = UITheme.PrimaryOrange;
                    e.FormattingApplied = true;
                }
            }
        }

        private void DgvDichVu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDichVu.CurrentRow == null || !(dgvDichVu.CurrentRow.DataBoundItem is DichVu dv)) return;

            maDVDangChon = dv.MaDichVu;
            lblDaChonDV.Text = $"Đã chọn: {dv.MaDichVu} - {dv.TenDichVu}";

            // Tự động đồng bộ dịch vụ đang chọn sang dropdown bên cột phải
            if (cboDichVuGhiNhan.DataSource != null)
            {
                for (int i = 0; i < cboDichVuGhiNhan.Items.Count; i++)
                {
                    if (cboDichVuGhiNhan.Items[i] is DichVu item && item.MaDichVu == dv.MaDichVu)
                    {
                        cboDichVuGhiNhan.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void NapComboDichVuGhiNhan()
        {
            var dsHoatDong = danhSachGoc.Where(d => d.TrangThai).ToList();
            cboDichVuGhiNhan.DataSource = dsHoatDong;
            cboDichVuGhiNhan.DisplayMember = "TenDichVu";
            cboDichVuGhiNhan.ValueMember = "MaDichVu";
            if (dsHoatDong.Count > 0)
            {
                cboDichVuGhiNhan.SelectedIndex = 0;
                CapNhatDonGiaTheoDichVuChon();
            }
        }

        private void CapNhatDonGiaTheoDichVuChon()
        {
            if (cboDichVuGhiNhan.SelectedItem is DichVu dv)
            {
                txtDonGiaHienThi.Text = $"{dv.DonGia:N0} đ / {dv.DonViTinh}";
            }
        }

        private void TaiDanhSachPhongDangO()
        {
            try
            {
                dtPhongDangO = chiTietDAL.LayDanhSachPhongDangO();

                DataTable dtCombo = new DataTable();
                dtCombo.Columns.Add("MaDatPhong", typeof(string));
                dtCombo.Columns.Add("MaPhong", typeof(int));
                dtCombo.Columns.Add("HienThi", typeof(string));
                dtCombo.Columns.Add("TongTienPhong", typeof(decimal));
                dtCombo.Columns.Add("TienCoc", typeof(decimal));

                if (dtPhongDangO.Rows.Count > 0)
                {
                    foreach (DataRow r in dtPhongDangO.Rows)
                    {
                        string maDat = r["MaDat"]?.ToString() ?? r["MaDatPhong"].ToString();
                        string soPhong = r["SoPhong"].ToString();
                        string loai = r["TenLoai"].ToString();
                        string khach = r["HoTen"].ToString();
                        decimal tienPhong = Convert.ToDecimal(r["TongTienPhong"]);
                        decimal tienCoc = Convert.ToDecimal(r["TienCoc"]);

                        dtCombo.Rows.Add(maDat, Convert.ToInt32(r["MaPhong"]), $"Phòng {soPhong} • {loai} | {khach}", tienPhong, tienCoc);
                    }
                }
                else
                {
                    dtCombo.Rows.Add("", 0, "-- Chưa có phòng nào đang lưu trú --", 0M, 0M);
                }

                cboPhongDangO.DataSource = dtCombo;
                cboPhongDangO.DisplayMember = "HienThi";
                cboPhongDangO.ValueMember = "MaDatPhong";

                if (dtCombo.Rows.Count > 0)
                {
                    cboPhongDangO.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void TaiChiTietDichVuPhongDangChon()
        {
            if (cboPhongDangO.SelectedValue == null) return;
            string maDat = cboPhongDangO.SelectedValue.ToString();
            if (string.IsNullOrEmpty(maDat))
            {
                dgvChiTietPhong.DataSource = null;
                lblBadgeSoMuc.Text = "0 mục";
                lblTongTienGiaTri.Text = "0 đ";
                return;
            }

            try
            {
                dtChiTietPhong = chiTietDAL.LayChiTietTheoDatPhong(maDat);

                // Định dạng hiển thị lưới
                dgvChiTietPhong.Columns.Clear();
                dgvChiTietPhong.AutoGenerateColumns = false;

                dgvChiTietPhong.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TenDichVu",
                    HeaderText = "Dịch vụ",
                    FillWeight = 130
                });

                dgvChiTietPhong.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ColMoTaSL",
                    HeaderText = "SL x Giá",
                    FillWeight = 110,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                dgvChiTietPhong.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ThanhTien",
                    HeaderText = "Thành tiền",
                    FillWeight = 95,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight,
                        Format = "#,##0 đ",
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = UITheme.PrimaryOrange
                    }
                });

                var colBtnXoa = new DataGridViewButtonColumn
                {
                    Name = "ColXoa",
                    HeaderText = "Xóa",
                    Text = "✕",
                    UseColumnTextForButtonValue = true,
                    FillWeight = 45,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        ForeColor = Color.Red,
                        Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
                    }
                };
                dgvChiTietPhong.Columns.Add(colBtnXoa);

                dgvChiTietPhong.DataSource = dtChiTietPhong;

                decimal tongTien = 0;
                int soMuc = dtChiTietPhong.Rows.Count;

                for (int i = 0; i < dtChiTietPhong.Rows.Count; i++)
                {
                    DataRow r = dtChiTietPhong.Rows[i];
                    decimal sl = Convert.ToDecimal(r["SoLuong"]);
                    decimal dg = Convert.ToDecimal(r["DonGia"]);
                    decimal tt = Convert.ToDecimal(r["ThanhTien"]);
                    string dvt = r["DonViTinh"].ToString();

                    tongTien += tt;

                    if (i < dgvChiTietPhong.Rows.Count)
                    {
                        dgvChiTietPhong.Rows[i].Cells["ColMoTaSL"].Value = $"{sl:0.#} x {dg:N0}";
                    }
                }

                lblBadgeSoMuc.Text = $"{soMuc} mục";
                lblTongTienGiaTri.Text = $"{tongTien:N0} đ";

                string soPhong = "";
                if (cboPhongDangO.SelectedItem is DataRowView drv)
                {
                    string hienThi = drv["HienThi"]?.ToString() ?? "";
                    var m = System.Text.RegularExpressions.Regex.Match(hienThi, @"Phòng\s*(\w+)");
                    if (m.Success) soPhong = m.Groups[1].Value;
                }
                lblTongTienSubtext.Text = string.IsNullOrEmpty(soPhong) ? "Cộng vào hóa đơn phòng khi trả phòng" : $"Cộng vào hóa đơn phòng {soPhong}";

                if (btnThuTienDichVuRieng != null) btnThuTienDichVuRieng.Enabled = (tongTien > 0);
            }
            catch
            {
                lblBadgeSoMuc.Text = "0 mục";
                lblTongTienGiaTri.Text = "0 đ";
                if (btnThuTienDichVuRieng != null) btnThuTienDichVuRieng.Enabled = false;
            }
        }

        private void GhiNhanDichVu()
        {
            if (cboPhongDangO.SelectedValue == null || string.IsNullOrEmpty(cboPhongDangO.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn một phòng đang lưu trú để ghi nhận dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(cboDichVuGhiNhan.SelectedItem is DichVu dv))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal soLuong = nudSoLuong.Value;
            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng dịch vụ phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDat = cboPhongDangO.SelectedValue.ToString();
            int maPhong = 0;
            decimal tienPhong = 0;
            decimal tienCoc = 0;

            if (cboPhongDangO.SelectedItem is DataRowView drv)
            {
                maPhong = Convert.ToInt32(drv["MaPhong"]);
                tienPhong = Convert.ToDecimal(drv["TongTienPhong"]);
                tienCoc = Convert.ToDecimal(drv["TienCoc"]);
            }

            bool ok = chiTietDAL.GhiNhanDichVuChoPhong(maDat, maPhong, dv.MaDichVu, soLuong, dv.DonGia, tienPhong, tienCoc);
            if (ok)
            {
                TaiChiTietDichVuPhongDangChon();
                nudSoLuong.Value = 1;
                txtThoiDiem.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                MessageBox.Show($"Đã thêm dịch vụ [{dv.TenDichVu} x {soLuong}] vào phiếu phòng thành công!", "Ghi nhận thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DgvChiTietPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvChiTietPhong.Columns["ColXoa"].Index) return;

            if (dtChiTietPhong == null || e.RowIndex >= dtChiTietPhong.Rows.Count) return;

            int maCTHD = Convert.ToInt32(dtChiTietPhong.Rows[e.RowIndex]["MaCTHD"]);
            string tenDV = dtChiTietPhong.Rows[e.RowIndex]["TenDichVu"].ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ [{tenDV}] khỏi phiếu phòng này không?",
                "Xác nhận xóa món", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (chiTietDAL.XoaChiTiet(maCTHD))
                {
                    TaiChiTietDichVuPhongDangChon();
                }
            }
        }

        private void MoThanhToanPhongDangChon()
        {
            if (cboPhongDangO.SelectedValue == null || string.IsNullOrEmpty(cboPhongDangO.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn phòng đang lưu trú cần thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDat = cboPhongDangO.SelectedValue.ToString();
            using var modal = new frmThanhToanModal(maDat);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                TaiDanhSachPhongDangO();
                TaiDanhSachDichVu();
            }
        }

        private void ThuTienDichVuRiengNgay()
        {
            if (cboPhongDangO.SelectedValue == null || string.IsNullOrEmpty(cboPhongDangO.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn phòng cần thanh toán dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDat = cboPhongDangO.SelectedValue.ToString();
            if (dtChiTietPhong == null || dtChiTietPhong.Rows.Count == 0)
            {
                MessageBox.Show("Phòng này chưa có dịch vụ nào cần thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal tongTien = 0;
            foreach (DataRow r in dtChiTietPhong.Rows)
            {
                tongTien += Convert.ToDecimal(r["ThanhTien"]);
            }

            if (tongTien <= 0)
            {
                MessageBox.Show("Tổng tiền dịch vụ bằng 0 đ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tenPhong = cboPhongDangO.Text;
            string msg = $"Khách tại [{tenPhong}] muốn thanh toán riêng dịch vụ phát sinh:\n\n" +
                         $"• Số lượng dịch vụ: {dtChiTietPhong.Rows.Count} mục\n" +
                         $"• Tổng tiền cần thu: {tongTien:N0} đ\n\n" +
                         $"Bạn có muốn tiến hành thu tiền ngay cho các dịch vụ này không?";

            if (MessageBox.Show(msg, "Xác nhận thu tiền dịch vụ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            DialogResult ptttChoice = MessageBox.Show(
                "Chọn phương thức thanh toán:\n\n[Yes] Chuyển khoản VietQR\n[No] Tiền mặt (TM)",
                "Hình thức thanh toán",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (ptttChoice == DialogResult.Cancel) return;

            string maHD = "HDDV" + DateTime.Now.ToString("yyMMddHHmmss");

            if (ptttChoice == DialogResult.Yes)
            {
                using var qr = new frmVNPAYQR(maHD, tongTien, $"DICH VU PHONG {tenPhong.Split('•')[0].Trim()}");
                if (qr.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
            }

            bool ok = chiTietDAL.ThanhToanDichVuRieng(maHD, maDat, tongTien, ptttChoice == DialogResult.Yes ? "CK" : "TM");
            if (ok)
            {
                MessageBox.Show($"Đã thu tiền dịch vụ thành công cho [{tenPhong}]!\nSố tiền đã thu: {tongTien:N0} đ",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiChiTietDichVuPhongDangChon();
            }
        }

        private void MoModalThem()
        {
            using var modal = new frmDichVuModal(null);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                TaiDanhSachDichVu();
            }
        }

        private void MoModalSua()
        {
            if (dgvDichVu.CurrentRow == null || !(dgvDichVu.CurrentRow.DataBoundItem is DichVu dv))
            {
                MessageBox.Show("Vui lòng chọn 1 dịch vụ trong danh mục để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var modal = new frmDichVuModal(dv);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                TaiDanhSachDichVu();
            }
        }

        private void DoiTrangThaiDichVu()
        {
            if (dgvDichVu.CurrentRow == null || !(dgvDichVu.CurrentRow.DataBoundItem is DichVu dv))
            {
                MessageBox.Show("Vui lòng chọn 1 dịch vụ để đổi trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dv.TrangThai = !dv.TrangThai;
            if (dichVuDAL.Update(dv))
            {
                TaiDanhSachDichVu();
            }
        }

        private void XoaDichVuDangChon()
        {
            if (dgvDichVu.CurrentRow == null || !(dgvDichVu.CurrentRow.DataBoundItem is DichVu dv))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ [{dv.TenDichVu}] khỏi hệ thống?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (dichVuDAL.Delete(dv.MaDichVu))
                {
                    TaiDanhSachDichVu();
                }
            }
        }

        // --- CÁC HÀM TƯƠNG THÍCH DESIGNER ---
        private void DrawFigmaCard(object sender, PaintEventArgs e) => UITheme.DrawCard(sender, e);
        private void DrawFigmaInputBox(object sender, PaintEventArgs e) => UITheme.DrawInputBox(sender, e);
        private void DrawPillBorder(object sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Region = new Region(UITheme.GetRoundedRectPath(new Rectangle(0, 0, btn.Width, btn.Height), 6));
            }
        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e) => ApDungBoLoc();
        private void dgvDichVu_SelectionChanged(object sender, EventArgs e) => DgvDichVu_SelectionChanged(sender, e);
        private void btnLuu_Click(object sender, EventArgs e) => MoModalThem();
        private void btnLamMoi_Click(object sender, EventArgs e) => TaiDanhSachDichVu();
        private void btnXoa_Click(object sender, EventArgs e) => XoaDichVuDangChon();
    }
}
