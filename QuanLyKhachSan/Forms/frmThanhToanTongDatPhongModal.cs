using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmThanhToanTongDatPhongModal : Form
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");

        // Input parameters
        private int _maKH;
        private readonly int _maPhong;
        private readonly string _soPhong;
        private readonly string _tenLoai;
        private readonly decimal _giaMotDem;
        private readonly DateTime _ngayNhan;
        private readonly DateTime _ngayTra;
        private readonly int _nguoiLon;
        private readonly int _treEm;
        private readonly int? _maNhanVien;

        // BLLs
        private readonly KhachHangBLL _khachHangBLL = new KhachHangBLL();
        private readonly VoucherBLL _voucherBLL = new VoucherBLL();

        // State
        private ChiPhiDatPhong _chiPhi;
        private decimal _tienGiamGia = 0;
        private int? _maVoucherDaApDung = null;

        public string MaDatPhongTao { get; private set; }

        public frmThanhToanTongDatPhongModal(
            int maKH,
            int maPhong,
            string soPhong,
            string tenLoai,
            decimal giaMotDem,
            DateTime ngayNhan,
            DateTime ngayTra,
            int nguoiLon,
            int treEm,
            int? maNhanVien = null,
            string tenKhachMacDinh = "",
            string sdtMacDinh = "")
        {
            _maKH = maKH;
            _maPhong = maPhong;
            _soPhong = soPhong;
            _tenLoai = tenLoai;
            _giaMotDem = giaMotDem;
            _ngayNhan = ngayNhan.Date;
            _ngayTra = ngayTra.Date;
            _nguoiLon = nguoiLon;
            _treEm = treEm;
            _maNhanVien = maNhanVien ?? Session.MaNhanVien;

            _chiPhi = LeTanDatPhongBLL.TinhChiPhi(_giaMotDem, _ngayNhan, _ngayTra);

            InitializeComponent();
            InitializeComponentCustom(tenKhachMacDinh, sdtMacDinh);
            // Giảm giá được chốt trong hóa đơn trả phòng; đơn đặt hiện lưu giá và cọc gốc.
            txtVoucherCode.Enabled = false;
            txtVoucherCode.PlaceholderText = "Áp dụng voucher khi trả phòng";
            btnApDungVoucher.Enabled = false;
            NapThongTinKhachNeuCo();
            CapNhatTongChiPhi();
        }

        private void InitializeComponentCustom(string tenKhachMacDinh, string sdtMacDinh)
        {
            Text = "Xác nhận đặt phòng & Thanh toán tổng toàn bộ";
            Size = new Size(1100, 780);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(248, 250, 252);
            Font = UITheme.MainFont;

            // 1. TOP HEADER (Chuẩn Mường Thanh: < Quay lại | THÔNG TIN ĐẶT PHÒNG)
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.White,
                Padding = new Padding(24, 12, 24, 0)
            };

            btnQuayLai = new Button
            {
                Text = "‹  Quay lại",
                Location = new Point(24, 16),
                Size = new Size(100, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UITheme.DarkNavy,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnQuayLai.FlatAppearance.BorderSize = 0;
            btnQuayLai.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };

            lblTitle = new Label
            {
                Text = "THÔNG TIN ĐẶT PHÒNG & THANH TOÁN",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(pnlHeader.Width / 2 - 200, 18),
                AutoSize = true
            };

            pnlHeaderDivider = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = Color.FromArgb(226, 232, 240)
            };

            pnlHeader.Controls.Add(btnQuayLai);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(pnlHeaderDivider);
            pnlHeader.Resize += (_, __) =>
            {
                lblTitle.Location = new Point((pnlHeader.Width - lblTitle.Width) / 2, 18);
            };
            Controls.Add(pnlHeader);

            // 2. MAIN TWO-COLUMN LAYOUT
            tblLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(24, 16, 24, 20),
                BackColor = Color.Transparent
            };
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64F));
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));

            // ==========================================
            // CỘT TRÁI (LEFT PANEL): CUỘN NỘI DUNG FORM
            // ==========================================
            pnlLeftScroll = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 14, 0)
            };

            int y = 0;

            // ---------------- THẺ 1: THÔNG TIN NGƯỜI ĐẶT PHÒNG ----------------
            var cardKhach = TaoCardContainer("Thông tin người đặt phòng", ref y, 350);

            // Dòng 1: Tiêu đề + Họ + Tên
            TaoNhan(cardKhach, "Tiêu đề", 20, 48);
            cboTieuDe = new ComboBox
            {
                Location = new Point(20, 70),
                Size = new Size(110, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboTieuDe.Items.AddRange(new object[] { "Ông", "Bà", "Cô" });
            cboTieuDe.SelectedIndex = 0;
            cardKhach.Controls.Add(cboTieuDe);

            TaoNhan(cardKhach, "Họ và đệm *", 145, 48);
            txtHo = new TextBox
            {
                Location = new Point(145, 70),
                Size = new Size(245, 29),
                Font = new Font("Segoe UI", 9.5F)
            };
            cardKhach.Controls.Add(txtHo);

            TaoNhan(cardKhach, "Tên *", 405, 48);
            txtTen = new TextBox
            {
                Location = new Point(405, 70),
                Size = new Size(215, 29),
                Font = new Font("Segoe UI", 9.5F)
            };
            cardKhach.Controls.Add(txtTen);

            // Dòng 2: Email + Nhập lại Email
            TaoNhan(cardKhach, "Email *", 20, 114);
            txtEmail = new TextBox
            {
                Location = new Point(20, 136),
                Size = new Size(295, 29),
                Font = new Font("Segoe UI", 9.5F),
                PlaceholderText = "Địa chỉ email nhận hóa đơn"
            };
            cardKhach.Controls.Add(txtEmail);

            TaoNhan(cardKhach, "Nhập lại email *", 330, 114);
            txtEmailXacNhan = new TextBox
            {
                Location = new Point(330, 136),
                Size = new Size(290, 29),
                Font = new Font("Segoe UI", 9.5F),
                PlaceholderText = "Xác nhận lại email"
            };
            cardKhach.Controls.Add(txtEmailXacNhan);

            // Dòng 3: Số điện thoại + Quốc gia
            TaoNhan(cardKhach, "Số điện thoại *", 20, 180);
            txtSDT = new TextBox
            {
                Location = new Point(20, 202),
                Size = new Size(295, 29),
                Font = new Font("Segoe UI", 9.5F),
                Text = sdtMacDinh,
                PlaceholderText = "+84 987 654 321"
            };
            cardKhach.Controls.Add(txtSDT);

            TaoNhan(cardKhach, "Quốc gia *", 330, 180);
            cboQuocGia = new ComboBox
            {
                Location = new Point(330, 202),
                Size = new Size(290, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboQuocGia.Items.AddRange(new object[] { "Việt Nam", "Hàn Quốc", "Nhật Bản", "Mỹ", "Trung Quốc", "Úc", "Pháp", "Quốc gia khác" });
            cboQuocGia.SelectedIndex = 0;
            cardKhach.Controls.Add(cboQuocGia);

            // Dòng 4: Yêu cầu thêm
            TaoNhan(cardKhach, "Yêu cầu thêm", 20, 246);
            txtYeuCauThem = new TextBox
            {
                Location = new Point(20, 268),
                Size = new Size(600, 60),
                Multiline = true,
                Font = new Font("Segoe UI", 9.2F),
                PlaceholderText = "Ví dụ: Sở thích về giường đôi/đơn, đón tiễn sân bay, phòng tầng cao, yên tĩnh..."
            };
            cardKhach.Controls.Add(txtYeuCauThem);

            pnlLeftScroll.Controls.Add(cardKhach);

            // Tách tên khách mặc định nếu có
            if (!string.IsNullOrWhiteSpace(tenKhachMacDinh))
            {
                var parts = tenKhachMacDinh.Trim().Split(' ');
                if (parts.Length > 1)
                {
                    txtTen.Text = parts[^1];
                    txtHo.Text = string.Join(" ", parts[..^1]);
                }
                else
                {
                    txtTen.Text = tenKhachMacDinh;
                }
            }

            // ---------------- THẺ 2: CHÍNH SÁCH ĐẶT PHÒNG ----------------
            var cardChinhSach = TaoCardContainer("Chính sách đặt phòng", ref y, 175);

            var lblTenPhongCS = new Label
            {
                Text = $"📄 Phòng {_soPhong} • {_tenLoai}",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(20, 44),
                AutoSize = true
            };

            var lblChinhSachHuy = new Label
            {
                Text = "• Hủy phòng: Nếu hủy hoặc thay đổi trước 24 giờ nhận phòng, không có khoản phí hủy nào. Trong trường hợp không đến, thanh toán theo quy định bảo lưu.",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(20, 70),
                Size = new Size(600, 36)
            };

            var lblChinhSachTT = new Label
            {
                Text = "• Thanh toán: Đặt cọc tối thiểu 30% khi xác nhận hoặc thanh toán toàn bộ khi nhận phòng.",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(20, 110),
                Size = new Size(600, 20)
            };

            var lblBaoGom = new Label
            {
                Text = "• Ưu đãi: Đã bao gồm bữa sáng buffet hàng ngày và sử dụng hồ bơi miễn phí.",
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74),
                Location = new Point(20, 134),
                AutoSize = true
            };

            cardChinhSach.Controls.AddRange(new Control[] { lblTenPhongCS, lblChinhSachHuy, lblChinhSachTT, lblBaoGom });
            pnlLeftScroll.Controls.Add(cardChinhSach);

            // ---------------- THẺ 3: PHƯƠNG THỨC THANH TOÁN & ĐẶT PHÒNG ----------------
            var cardPTTT = TaoCardContainer("Phương thức thanh toán", ref y, 195);

            TaoNhan(cardPTTT, "Chọn hình thức thanh toán *", 20, 44);
            cboPTTT = new ComboBox
            {
                Location = new Point(20, 68),
                Size = new Size(600, 32),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboPTTT.Items.AddRange(new object[] {
                "⚡ Cổng thanh toán Quốc tế & Quốc gia VNPAY-QR (Chuẩn EMVCo)",
                "🟣 Ví điện tử MoMo (Quét mã MoMo QR tức thì)",
                "🍎 Apple Pay / Google Wallet (Chạm máy POS Contactless)",
                "💳 OnePay / Thẻ POS (Thẻ ATM nội địa & Quốc tế Visa/Master)",
                "💵 Tiền mặt tại quầy lễ tân (Thanh toán khi làm thủ tục nhận phòng)"
            });
            cboPTTT.SelectedIndex = 0;
            cardPTTT.Controls.Add(cboPTTT);

            chkDongYDieuKhoan = new CheckBox
            {
                Text = "Tôi đã đọc kỹ và đồng ý với điều khoản đặt phòng & quy định lưu trú của khách sạn.",
                Location = new Point(20, 112),
                Size = new Size(600, 26),
                Font = new Font("Segoe UI", 9F),
                ForeColor = UITheme.TextMain,
                Checked = true
            };
            cardPTTT.Controls.Add(chkDongYDieuKhoan);

            btnThucHienDatPhong = new Button
            {
                Text = "🔒  THỰC HIỆN ĐẶT PHÒNG",
                Location = new Point(20, 144),
                Size = new Size(260, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(234, 179, 8), // Vàng gold chuẩn Mường Thanh
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThucHienDatPhong.FlatAppearance.BorderSize = 0;
            btnThucHienDatPhong.Click += BtnThucHienDatPhong_Click;
            cardPTTT.Controls.Add(btnThucHienDatPhong);

            pnlLeftScroll.Controls.Add(cardPTTT);

            // ==========================================
            // CỘT PHẢI (RIGHT PANEL): SUMMARY CHI PHÍ (CHUẨN MƯỜNG THANH)
            // ==========================================
            cardSummaryRight = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(22, 18, 22, 18),
                Margin = new Padding(10, 0, 0, 0)
            };
            cardSummaryRight.Paint += (s, e) => UITheme.DrawCard(s, e, 10);

            int ry = 18;

            var lblSumTieuDe = new Label
            {
                Text = "Yêu cầu đặt phòng của bạn",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(20, ry),
                AutoSize = true
            };
            cardSummaryRight.Controls.Add(lblSumTieuDe);
            ry += 32;

            var lblSumTenKS = new Label
            {
                Text = "Khách sạn An Nhiên - An Nhiên Hotel",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange,
                Location = new Point(20, ry),
                AutoSize = true
            };
            cardSummaryRight.Controls.Add(lblSumTenKS);
            ry += 24;

            lblSummaryNhanPhong = new Label
            {
                Text = $"Nhận phòng: {_ngayNhan.ToString("dddd, dd/MM/yyyy", Vi)} từ 14:00",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMain,
                Location = new Point(20, ry),
                AutoSize = true
            };
            cardSummaryRight.Controls.Add(lblSummaryNhanPhong);
            ry += 22;

            int soDem = _chiPhi.SoDem;
            lblSummaryTraPhong = new Label
            {
                Text = $"Trả phòng: {_ngayTra.ToString("dddd, dd/MM/yyyy", Vi)} cho đến 12:00 ({soDem + 1} ngày {soDem} đêm)",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMain,
                Location = new Point(20, ry),
                AutoSize = true
            };
            cardSummaryRight.Controls.Add(lblSummaryTraPhong);
            ry += 30;

            cardSummaryRight.Controls.Add(TaoDivider(ry));
            ry += 14;

            var lblTieuDePhong = new Label
            {
                Text = "Thông tin phòng",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(20, ry),
                AutoSize = true
            };
            cardSummaryRight.Controls.Add(lblTieuDePhong);
            ry += 24;

            lblSummaryThongTinPhong = new Label
            {
                Text = $"Phòng {_soPhong}: {_tenLoai}\nMiễn phí hủy phòng trước 24h\nDành cho {_nguoiLon} Người lớn - {_treEm} Trẻ em",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(20, ry),
                Size = new Size(cardSummaryRight.ClientSize.Width - 40, 52)
            };
            cardSummaryRight.Controls.Add(lblSummaryThongTinPhong);
            ry += 54;

            lblSummaryGiaPhong = new Label
            {
                Text = $"Giá mỗi đêm: {_giaMotDem:N0} VNĐ",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(20, ry),
                AutoSize = true
            };
            cardSummaryRight.Controls.Add(lblSummaryGiaPhong);
            ry += 28;

            cardSummaryRight.Controls.Add(TaoDivider(ry));
            ry += 14;

            // Giá phòng tổng
            var lblNhanTienPhong = new Label { Text = "Giá phòng:", Font = new Font("Segoe UI", 9.5F), ForeColor = UITheme.TextMuted, Location = new Point(20, ry), AutoSize = true };
            lblSummaryTienPhong = new Label { Text = $"{_chiPhi.TienPhong:N0} VNĐ", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = UITheme.DarkNavy, Anchor = AnchorStyles.Top | AnchorStyles.Right, AutoSize = true };
            cardSummaryRight.Controls.Add(lblNhanTienPhong);
            cardSummaryRight.Controls.Add(lblSummaryTienPhong);
            ry += 30;

            // Voucher Input Section
            var lblNhanVoucher = new Label { Text = "Mã ưu đãi / Voucher:", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = UITheme.DarkNavy, Location = new Point(20, ry), AutoSize = true };
            cardSummaryRight.Controls.Add(lblNhanVoucher);
            ry += 22;

            txtVoucherCode = new TextBox { Location = new Point(20, ry), Size = new Size(180, 28), Font = new Font("Segoe UI", 9F), PlaceholderText = "Nhập mã..." };
            btnApDungVoucher = new Button { Text = "ÁP DỤNG", Location = new Point(208, ry - 1), Size = new Size(80, 30), BackColor = UITheme.DarkNavy, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), Cursor = Cursors.Hand };
            btnApDungVoucher.FlatAppearance.BorderSize = 0;
            btnApDungVoucher.Click += BtnApDungVoucher_Click;

            cardSummaryRight.Controls.Add(txtVoucherCode);
            cardSummaryRight.Controls.Add(btnApDungVoucher);
            ry += 36;

            lblSummaryGiamGia = new Label { Text = "Giảm giá: 0 VNĐ", Font = new Font("Segoe UI", 8.8F, FontStyle.Bold), ForeColor = Color.FromArgb(22, 163, 74), Location = new Point(20, ry), AutoSize = true, Visible = false };
            cardSummaryRight.Controls.Add(lblSummaryGiamGia);
            ry += 24;

            cardSummaryRight.Controls.Add(TaoDivider(ry));
            ry += 16;

            // Tổng giá to màu vàng/cam chuẩn Mường Thanh
            var lblNhanTongGia = new Label { Text = "Tổng giá:", Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), ForeColor = UITheme.DarkNavy, Location = new Point(20, ry), AutoSize = true };
            lblSummaryTongGia = new Label
            {
                Text = $"{_chiPhi.TongTien:N0} VNĐ",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(234, 179, 8), // Vàng gold Mường Thanh
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true
            };
            cardSummaryRight.Controls.Add(lblNhanTongGia);
            cardSummaryRight.Controls.Add(lblSummaryTongGia);
            ry += 38;

            var lblThuePhi = new Label
            {
                Text = "Bao gồm tất cả các loại thuế và phí dịch vụ\n(Theo quy định của NHNN Việt Nam, thanh toán bằng VNĐ)",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.FromArgb(220, 38, 38),
                Location = new Point(20, ry),
                Size = new Size(cardSummaryRight.ClientSize.Width - 40, 34)
            };
            cardSummaryRight.Controls.Add(lblThuePhi);

            cardSummaryRight.Resize += (_, __) =>
            {
                lblSummaryTienPhong.Location = new Point(cardSummaryRight.Width - lblSummaryTienPhong.Width - 24, 204);
                lblSummaryTongGia.Location = new Point(cardSummaryRight.Width - lblSummaryTongGia.Width - 24, 340);
                txtVoucherCode.Width = Math.Max(100, cardSummaryRight.Width - 140);
                btnApDungVoucher.Location = new Point(cardSummaryRight.Width - 105, 256);
            };

            tblLayout.Controls.Add(pnlLeftScroll, 0, 0);
            tblLayout.Controls.Add(cardSummaryRight, 1, 0);
            Controls.Add(tblLayout);
        }

        private Panel TaoCardContainer(string tieuDe, ref int currentY, int height)
        {
            var pnl = new Panel
            {
                Location = new Point(0, currentY),
                Size = new Size(pnlLeftScroll.ClientSize.Width - 10, height),
                BackColor = Color.White,
                Padding = new Padding(20, 16, 20, 16),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnl.Paint += (s, e) => UITheme.DrawCard(s, e, 10);

            var lblT = new Label
            {
                Text = tieuDe,
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(20, 14),
                AutoSize = true
            };
            pnl.Controls.Add(lblT);

            currentY += height + 16;
            return pnl;
        }

        private static Label TaoNhan(Control parent, string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                ForeColor = UITheme.TextMain,
                Location = new Point(x, y),
                AutoSize = true
            };
            parent.Controls.Add(lbl);
            return lbl;
        }

        private static Panel TaoDivider(int y)
        {
            return new Panel
            {
                Location = new Point(20, y),
                Size = new Size(330, 1),
                BackColor = Color.FromArgb(241, 245, 249),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
        }

        private void NapThongTinKhachNeuCo()
        {
            if (_maKH <= 0) return;
            try
            {
                var kh = _khachHangBLL.LayTheoMa(_maKH);
                if (kh != null)
                {
                    txtSDT.Text = kh.DienThoai ?? "";
                    txtEmail.Text = kh.Email ?? "";
                    txtEmailXacNhan.Text = kh.Email ?? "";

                    if (!string.IsNullOrWhiteSpace(kh.HoTen))
                    {
                        var parts = kh.HoTen.Trim().Split(' ');
                        if (parts.Length > 1)
                        {
                            txtTen.Text = parts[^1];
                            txtHo.Text = string.Join(" ", parts[..^1]);
                        }
                        else
                        {
                            txtTen.Text = kh.HoTen;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(kh.QuocTich))
                    {
                        for (int i = 0; i < cboQuocGia.Items.Count; i++)
                        {
                            if (cboQuocGia.Items[i].ToString().IndexOf(kh.QuocTich, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                cboQuocGia.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void BtnApDungVoucher_Click(object sender, EventArgs e)
        {
            string code = txtVoucherCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Vui lòng nhập mã ưu đãi hoặc voucher.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var v = _voucherBLL.TimVoucherTheoCode(code);
                if (v == null || !v.TrangThai || DateTime.Now.Date < v.NgayBatDau.Date || DateTime.Now.Date > v.NgayHetHan.Date || (v.DonToiThieu > 0 && _chiPhi.TienPhong < v.DonToiThieu))
                {
                    MessageBox.Show("Mã voucher không hợp lệ, đã hết hạn hoặc chưa đạt điều kiện đơn hàng tối thiểu.", "Không thể áp dụng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _maVoucherDaApDung = v.MaVoucher;
                if (v.LoaiGiam == "PhanTram")
                {
                    _tienGiamGia = Math.Round(_chiPhi.TienPhong * (v.GiaTri / 100M), 0);
                }
                else
                {
                    _tienGiamGia = Math.Min(_chiPhi.TienPhong, v.GiaTri);
                }

                lblSummaryGiamGia.Text = $"Giảm giá ({v.Code}): -{_tienGiamGia:N0} VNĐ";
                lblSummaryGiamGia.Visible = true;
                CapNhatTongChiPhi();

                MessageBox.Show($"Áp dụng mã [{v.Code}] thành công! Bạn được giảm {_tienGiamGia:N0} VNĐ.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra voucher: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTongChiPhi()
        {
            decimal tongSauGiam = Math.Max(0, _chiPhi.TongTien - _tienGiamGia);
            lblSummaryTongGia.Text = $"{tongSauGiam:N0} VNĐ";
        }

        private void BtnThucHienDatPhong_Click(object sender, EventArgs e)
        {
            string ho = txtHo.Text.Trim();
            string ten = txtTen.Text.Trim();
            string hoTen = $"{ho} {ten}".Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string emailXacNhan = txtEmailXacNhan.Text.Trim();

            if (string.IsNullOrWhiteSpace(ten))
            {
                MessageBox.Show("Vui lòng nhập Tên khách đặt phòng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại liên hệ.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(emailXacNhan) && !email.Equals(emailXacNhan, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Email xác nhận không khớp với Email đã nhập. Vui lòng kiểm tra lại.", "Lỗi email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailXacNhan.Focus();
                return;
            }

            if (!chkDongYDieuKhoan.Checked)
            {
                MessageBox.Show("Vui lòng tích chọn đồng ý với điều khoản đặt phòng trước khi thực hiện.", "Yêu cầu đồng ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Tự động lưu hoặc liên kết khách hàng
            if (_maKH <= 0)
            {
                var dtTim = LeTanDatPhongBLL.TimKhach(sdt);
                if (dtTim != null && dtTim.Rows.Count > 0)
                {
                    _maKH = Convert.ToInt32(dtTim.Rows[0]["MaKH"]);
                }
                else
                {
                    var khMoi = new KhachHang
                    {
                        HoTen = hoTen,
                        DienThoai = sdt,
                        Email = string.IsNullOrEmpty(email) ? null : email,
                        QuocTich = cboQuocGia.SelectedItem?.ToString() ?? "Việt Nam",
                        GhiChu = txtYeuCauThem.Text.Trim(),
                        DongYNhanUuDai = true,
                        NhomKhach = "Mới"
                    };
                    var kq = _khachHangBLL.Them(khMoi);
                    if (kq.ThanhCong)
                    {
                        _maKH = kq.MaKhachHang;
                    }
                    else
                    {
                        MessageBox.Show("Không thể khởi tạo hồ sơ khách: " + kq.ThongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            decimal tongSauGiam = Math.Max(0, _chiPhi.TongTien - _tienGiamGia);

            // 2. Mở cổng thanh toán tương ứng trước khi lưu
            string maTam = "DP" + DateTime.Now.ToString("yyMMddHHmm");
            string phuongThucChon = cboPTTT.SelectedItem?.ToString() ?? "";

            if (phuongThucChon.Contains("VNPAY") || phuongThucChon.Contains("VietQR") || phuongThucChon.Contains("EMVCo"))
            {
                using var qr = new frmVNPAYQR(maTam, tongSauGiam, $"DAT PHONG {_soPhong} {hoTen}");
                if (qr.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
            }
            else if (phuongThucChon.Contains("MoMo"))
            {
                using var mm = new frmMoMoQR(maTam, tongSauGiam, $"DAT PHONG {_soPhong} {hoTen}");
                if (mm.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
            }
            else if (phuongThucChon.Contains("Apple Pay"))
            {
                using var ap = new frmApplePayModal(maTam, tongSauGiam, $"DAT PHONG {_soPhong} {hoTen}");
                if (ap.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
            }

            // 3. Thực hiện tạo đặt phòng qua BLL
            try
            {
                string phuongThucCoc = phuongThucChon.Contains("Apple Pay") ? "POS"
                    : phuongThucChon.Contains("VNPAY") || phuongThucChon.Contains("VietQR")
                      || phuongThucChon.Contains("EMVCo") || phuongThucChon.Contains("MoMo") ? "CK" : "TM";
                if (MessageBox.Show(this, $"Xác nhận đã nhận tạm ứng {tongSauGiam:N0} VNĐ cho đơn đặt phòng?",
                    "Xác nhận tiền đã nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                string maDat = LeTanDatPhongBLL.TaoDatPhong(
                    _maKH,
                    _maPhong,
                    _ngayNhan,
                    _ngayTra,
                    _nguoiLon,
                    _treEm,
                    tongSauGiam,
                    phuongThucCoc,
                    _maNhanVien,
                    _giaMotDem);

                if (string.IsNullOrEmpty(maDat))
                {
                    MessageBox.Show("Không thể tạo đơn đặt phòng. Phòng có thể đã có khách đặt trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MaDatPhongTao = maDat;

                // Nếu có voucher, trừ số lượng
                if (_maVoucherDaApDung.HasValue)
                {
                    _voucherBLL.GiamSoLuongVoucher(_maVoucherDaApDung.Value);
                }

                MessageBox.Show(
                    $"🎉 ĐẶT PHÒNG THÀNH CÔNG!\n\n" +
                    $"• Mã đặt phòng: {maDat}\n" +
                    $"• Phòng: {_soPhong} ({_tenLoai})\n" +
                    $"• Khách hàng: {hoTen} ({sdt})\n" +
                    $"• Thời gian: {_ngayNhan:dd/MM/yyyy} – {_ngayTra:dd/MM/yyyy} ({_chiPhi.SoDem} đêm)\n" +
                    $"• Tổng tiền: {tongSauGiam:N0} VNĐ\n\n" +
                    $"Hệ thống đã lưu đơn đặt phòng và phiếu thu tạm ứng.",
                    "Thực hiện thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo đặt phòng: " + ex.Message, "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
