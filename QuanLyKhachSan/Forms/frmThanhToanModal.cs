using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmThanhToanModal : Form
    {
        private HoaDonBLL hoaDonBLL = new HoaDonBLL();
        private ChiTietHoaDonDAL chiTietDAL = new ChiTietHoaDonDAL();
        private DichVuDAL dichVuDAL = new DichVuDAL();
        private PhuongThucThanhToanDAL ptttDAL = new PhuongThucThanhToanDAL();
        private VoucherBLL voucherBLL = new VoucherBLL();

        private List<DichVu> danhSachDichVu = new List<DichVu>();
        private DataTable dtChiTietTam = new DataTable();
        private int? maVoucherDaApDung = null;

        public frmThanhToanModal(string maDatPhongMacDinh = null)
        {
            InitializeComponent();
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);
            KhoiTaoBangChiTietTam();
            TaiDanhSachPhuongThucTT();
            TaiDanhSachDichVu();
            TaiDanhSachDatPhong();
            if (!string.IsNullOrEmpty(maDatPhongMacDinh))
            {
                ChonDatPhongTheoMa(maDatPhongMacDinh);
            }
            TinhTongTienTuDong();
        }

        private void InitializeComponentCustom()
        {
            Text = "Lập hóa đơn & Thanh toán trả phòng";
            Size = new Size(880, 720);
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
                Padding = new Padding(24, 16, 24, 8)
            };

            lblTitle = new Label
            {
                Text = "💳 LẬP HÓA ĐƠN & THANH TOÁN TRẢ PHÒNG",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Chọn lượt đặt phòng đang ở, cộng dịch vụ phát sinh, áp dụng mã ưu đãi và chốt hóa đơn",
                Font = new Font("Segoe UI", 9F),
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
                Height = 68,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(24, 14, 24, 14)
            };
            pnlFooter.Paint += (s, e) =>
            {
                using var p = new Pen(UITheme.BorderColor, 1F);
                e.Graphics.DrawLine(p, 0, 0, pnlFooter.Width, 0);
            };

            btnVNPAYQR = new Button
            {
                Text = "⚡ VNPAY-QR",
                Size = new Size(110, 40),
                Location = new Point(24, 14),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 91, 170),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnVNPAYQR.FlatAppearance.BorderSize = 0;
            btnVNPAYQR.Click += btnVNPAYQR_Click;

            btnMoMo = new Button
            {
                Text = "🟣 MoMo",
                Size = new Size(95, 40),
                Location = new Point(135, 14),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(165, 0, 100),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnMoMo.FlatAppearance.BorderSize = 0;
            btnMoMo.Click += btnMoMo_Click;

            btnApplePay = new Button
            {
                Text = "🍎 Apple Pay",
                Size = new Size(110, 40),
                Location = new Point(236, 14),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(28, 28, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnApplePay.FlatAppearance.BorderSize = 0;
            btnApplePay.Click += btnApplePay_Click;

            btnInHoaDon = new Button
            {
                Text = "🖨️ In biên lai",
                Size = new Size(110, 40),
                Location = new Point(352, 14),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UITheme.DarkNavy,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };
            btnInHoaDon.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnInHoaDon.Click += btnInHoaDon_Click;

            btnXacNhan = new Button
            {
                Text = "💳 Xác nhận thu & Trả phòng",
                Size = new Size(220, 40),
                Location = new Point(pnlFooter.Width - 365, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.Click += btnXacNhan_Click;

            btnHuy = new Button
            {
                Text = "✕ Đóng",
                Size = new Size(95, 40),
                Location = new Point(pnlFooter.Width - 130, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UITheme.TextMuted,
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand
            };
            btnHuy.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnHuy.Click += (_, __) => Close();

            pnlFooter.Controls.Add(btnVNPAYQR);
            pnlFooter.Controls.Add(btnMoMo);
            pnlFooter.Controls.Add(btnApplePay);
            pnlFooter.Controls.Add(btnInHoaDon);
            pnlFooter.Controls.Add(btnXacNhan);
            pnlFooter.Controls.Add(btnHuy);
            Controls.Add(pnlFooter);

            // 3. Body Content Panel
            Panel pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(24, 16, 24, 16),
                BackColor = Color.White
            };

            // Dòng 1: Mã HĐ + Lựa chọn Đặt phòng
            lblMaHD = TaoNhan(pnlBody, "Mã hóa đơn", 24, 14);
            var pnlMaHDWrap = TaoBox(pnlBody, 24, 36, 180, 36);
            txtMaHD = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                ReadOnly = true,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Text = "HD" + DateTime.Now.ToString("yyMMddHHmmssfff") + Guid.NewGuid().ToString("N").Substring(0, 3)
            };
            pnlMaHDWrap.Controls.Add(txtMaHD);

            lblDatPhong = TaoNhan(pnlBody, "Lượt đặt phòng đang ở (Cần thanh toán) *", 218, 14);
            var pnlDatPhongWrap = TaoBox(pnlBody, 218, 36, 598, 36);
            cboDatPhong = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboDatPhong.SelectedIndexChanged += cboDatPhong_SelectedIndexChanged;
            pnlDatPhongWrap.Controls.Add(cboDatPhong);

            // Dòng 2: Tiền phòng & Tiền cọc
            lblTienPhong = TaoNhan(pnlBody, "Tiền thuê phòng (VNĐ)", 24, 82);
            var pnlTienPhongWrap = TaoBox(pnlBody, 24, 104, 270, 36);
            txtTienPhong = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.TextMain,
                ReadOnly = true,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Text = "0",
                TextAlign = HorizontalAlignment.Right
            };
            pnlTienPhongWrap.Controls.Add(txtTienPhong);

            lblTienCoc = TaoNhan(pnlBody, "Tiền đã đặt cọc / tạm ứng (- VNĐ)", 310, 82);
            var pnlTienCocWrap = TaoBox(pnlBody, 310, 104, 250, 36);
            txtTienCoc = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74),
                ReadOnly = true,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Text = "0",
                TextAlign = HorizontalAlignment.Right
            };
            pnlTienCocWrap.Controls.Add(txtTienCoc);

            lblTienDV = TaoNhan(pnlBody, "Tiền dịch vụ (+ VNĐ)", 576, 82);
            var pnlTienDVWrap = TaoBox(pnlBody, 576, 104, 240, 36);
            txtTienDV = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange,
                ReadOnly = true,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Text = "0",
                TextAlign = HorizontalAlignment.Right
            };
            pnlTienDVWrap.Controls.Add(txtTienDV);

            // Dòng 3: Bảng dịch vụ phát sinh
            lblDichVuSection = TaoNhan(pnlBody, "🛎️ THÊM DỊCH VỤ SỬ DỤNG (Ăn uống, giặt ủi, đưa đón...):", 24, 150);
            lblDichVuSection.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDichVuSection.ForeColor = UITheme.DarkNavy;

            var pnlDvWrap = TaoBox(pnlBody, 24, 174, 340, 36);
            cboDichVu = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F)
            };
            pnlDvWrap.Controls.Add(cboDichVu);

            nudSoLuongDV = new NumericUpDown
            {
                Location = new Point(374, 176),
                Size = new Size(60, 32),
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                Font = new Font("Segoe UI", 10F),
                TextAlign = HorizontalAlignment.Center
            };
            pnlBody.Controls.Add(nudSoLuongDV);

            btnThemDV = new Button
            {
                Text = "➕ Thêm DV",
                Location = new Point(444, 174),
                Size = new Size(110, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = UITheme.DarkNavy,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThemDV.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnThemDV.Click += btnThemDV_Click;
            pnlBody.Controls.Add(btnThemDV);

            // Grid chi tiết dịch vụ
            dgvChiTietDV = new DataGridView
            {
                Location = new Point(24, 218),
                Size = new Size(792, 130),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false
            };
            UITheme.StyleModernGrid(dgvChiTietDV);
            dgvChiTietDV.CellDoubleClick += dgvChiTietDV_CellDoubleClick;
            pnlBody.Controls.Add(dgvChiTietDV);

            Label lblHintXoaDV = new Label
            {
                Text = "💡 Nhấp đúp chuột vào 1 dòng dịch vụ trong bảng trên để hủy/xóa bớt",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, 352),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblHintXoaDV);

            // Dòng 4: Voucher & PTTT
            lblVoucherSection = TaoNhan(pnlBody, "🏷️ MÃ ƯU ĐÃI (VOUCHER):", 24, 380);
            lblVoucherSection.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblVoucherSection.ForeColor = UITheme.DarkNavy;

            var pnlVoucherWrap = TaoBox(pnlBody, 24, 404, 210, 36);
            txtVoucherCode = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                CharacterCasing = CharacterCasing.Upper,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập mã..."
            };
            pnlVoucherWrap.Controls.Add(txtVoucherCode);

            btnGoiYVoucher = new Button
            {
                Text = "💡 Gợi ý",
                Location = new Point(240, 404),
                Size = new Size(80, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(254, 243, 199),
                ForeColor = Color.FromArgb(180, 83, 9),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnGoiYVoucher.FlatAppearance.BorderColor = Color.FromArgb(253, 230, 138);
            btnGoiYVoucher.Click += btnGoiYVoucher_Click;
            pnlBody.Controls.Add(btnGoiYVoucher);

            cmsVoucher = new ContextMenuStrip();

            btnApDungVoucher = new Button
            {
                Text = "✓ Áp dụng",
                Location = new Point(326, 404),
                Size = new Size(95, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 252, 231),
                ForeColor = Color.FromArgb(22, 163, 74),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnApDungVoucher.FlatAppearance.BorderColor = Color.FromArgb(187, 247, 208);
            btnApDungVoucher.Click += btnApDungVoucher_Click;
            pnlBody.Controls.Add(btnApDungVoucher);

            TaoNhan(pnlBody, "Tiền giảm voucher (- VNĐ)", 435, 380);
            var pnlGiamGiaWrap = TaoBox(pnlBody, 435, 404, 180, 36);
            txtTienGiamGia = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74),
                ReadOnly = true,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Text = "0",
                TextAlign = HorizontalAlignment.Right
            };
            pnlGiamGiaWrap.Controls.Add(txtTienGiamGia);

            lblPTTT = TaoNhan(pnlBody, "Phương thức TT *", 630, 380);
            var pnlPTTTWrap = TaoBox(pnlBody, 630, 404, 186, 36);
            cboPTTT = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F)
            };
            pnlPTTTWrap.Controls.Add(cboPTTT);

            // Dòng 5: Khung tổng tiền to
            pnlTongTienBox = new Panel
            {
                Location = new Point(24, 460),
                Size = new Size(792, 60),
                BackColor = Color.FromArgb(255, 247, 237),
                Padding = new Padding(20, 10, 20, 10)
            };
            pnlTongTienBox.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, pnlTongTienBox.Width - 1, pnlTongTienBox.Height - 1);
                using GraphicsPath path = UITheme.GetRoundedRectPath(rect, 8);
                using Pen pen = new Pen(ColorTranslator.FromHtml("#FDBA74"), 1.2F);
                e.Graphics.DrawPath(pen, path);
            };

            lblTongTienTieuDe = new Label
            {
                Text = "TỔNG TIỀN THỰC THU (CẦN THANH TOÁN):",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(16, 18),
                AutoSize = true
            };

            lblTongTienHienThi = new Label
            {
                Text = "0 đ",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange,
                Location = new Point(pnlTongTienBox.Width - 320, 12),
                Size = new Size(300, 36),
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            pnlTongTienBox.Controls.Add(lblTongTienTieuDe);
            pnlTongTienBox.Controls.Add(lblTongTienHienThi);
            pnlBody.Controls.Add(pnlTongTienBox);

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

        private void KhoiTaoBangChiTietTam()
        {
            dtChiTietTam = new DataTable();
            dtChiTietTam.Columns.Add("Mã DV", typeof(string));
            dtChiTietTam.Columns.Add("Tên Dịch Vụ", typeof(string));
            dtChiTietTam.Columns.Add("SL", typeof(decimal));
            dtChiTietTam.Columns.Add("Đơn Giá", typeof(decimal));
            dtChiTietTam.Columns.Add("Thành Tiền", typeof(decimal));
            dgvChiTietDV.DataSource = dtChiTietTam;
        }

        private void TaiDanhSachPhuongThucTT()
        {
            try
            {
                var list = ptttDAL.GetAll();
                cboPTTT.DataSource = list;
                cboPTTT.DisplayMember = "TenPhuongThuc";
                cboPTTT.ValueMember = "MaPhuongThuc";
            }
            catch { }
        }

        private void TaiDanhSachDichVu()
        {
            try
            {
                danhSachDichVu = dichVuDAL.GetAll();
                cboDichVu.DataSource = danhSachDichVu;
                cboDichVu.DisplayMember = "TenDichVu";
                cboDichVu.ValueMember = "MaDichVu";
            }
            catch { }
        }

        public void TaiDanhSachDatPhong()
        {
            try
            {
                var ds = ThanhToanDAL.GetDanhSachChuaThanhToan();
                cboDatPhong.DataSource = ds;
                cboDatPhong.DisplayMember = "HienThi";
                cboDatPhong.ValueMember = "MaDatPhong";

                if (ds.Count > 0)
                {
                    cboDatPhong.SelectedIndex = 0;
                }
                else
                {
                    txtTienPhong.Text = "0";
                    txtTienCoc.Text = "0";
                    btnXacNhan.Enabled = false;
                }
            }
            catch { }
        }

        public void ChonDatPhongTheoMa(string maDP)
        {
            if (string.IsNullOrWhiteSpace(maDP)) return;
            string raw = maDP.Replace("DP-", "").Trim();
            if (cboDatPhong.DataSource is List<ThongTinDatPhongThanhToan> ds)
            {
                var found = ds.FirstOrDefault(d =>
                    d.MaDatPhong.ToString() == maDP ||
                    d.MaDatPhong.ToString() == raw ||
                    (!string.IsNullOrEmpty(d.MaDat) && d.MaDat.Equals(maDP, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(d.MaDat) && d.MaDat.Equals(raw, StringComparison.OrdinalIgnoreCase)));
                if (found != null)
                {
                    cboDatPhong.SelectedItem = found;
                }
            }
        }

        private void cboDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDatPhong.SelectedItem is ThongTinDatPhongThanhToan dp)
            {
                btnXacNhan.Enabled = true;
                txtTienPhong.Text = dp.TienPhong.ToString("N0");
                txtTienCoc.Text = dp.TienCoc.ToString("N0");

                // Tự động tải tất cả dịch vụ đã phát sinh của phòng này
                dtChiTietTam.Clear();
                try
                {
                    var dtDaGhi = chiTietDAL.LayChiTietTheoDatPhong(dp.MaDatPhong.ToString());
                    if (dtDaGhi != null && dtDaGhi.Rows.Count > 0)
                    {
                        foreach (DataRow r in dtDaGhi.Rows)
                        {
                            string maDV = r["MaDichVu"].ToString();
                            string tenDV = r["TenDichVu"].ToString();
                            decimal sl = Convert.ToDecimal(r["SoLuong"]);
                            decimal dg = Convert.ToDecimal(r["DonGia"]);
                            decimal tt = Convert.ToDecimal(r["ThanhTien"]);
                            dtChiTietTam.Rows.Add(maDV, tenDV, sl, dg, tt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    btnXacNhan.Enabled = false;
                    MessageBox.Show(this, "Chưa tải được dịch vụ của đơn. Hãy chọn lại đơn trước khi thanh toán.\n" + ex.Message,
                        "Không thể tải dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal tongDV = 0;
                foreach (DataRow r in dtChiTietTam.Rows)
                {
                    tongDV += Convert.ToDecimal(r["Thành Tiền"]);
                }
                txtTienDV.Text = tongDV.ToString("N0");
            }
            else
            {
                txtTienPhong.Text = "0";
                txtTienCoc.Text = "0";
                txtTienDV.Text = "0";
                dtChiTietTam.Clear();
                btnXacNhan.Enabled = false;
            }
            TinhTongTienTuDong();
        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            if (cboDichVu.SelectedItem is not DichVu dv) return;
            decimal sl = nudSoLuongDV.Value;
            if (sl <= 0) return;

            decimal donGia = dv.DonGia;
            decimal thanhTien = sl * donGia;

            DataRow existing = null;
            foreach (DataRow r in dtChiTietTam.Rows)
            {
                if (r["Mã DV"].ToString() == dv.MaDichVu)
                {
                    existing = r;
                    break;
                }
            }

            if (existing != null)
            {
                decimal currentSl = Convert.ToDecimal(existing["SL"]);
                existing["SL"] = currentSl + sl;
                existing["Thành Tiền"] = (currentSl + sl) * donGia;
            }
            else
            {
                dtChiTietTam.Rows.Add(dv.MaDichVu, dv.TenDichVu, sl, donGia, thanhTien);
            }

            decimal tongDV = 0;
            foreach (DataRow r in dtChiTietTam.Rows)
            {
                tongDV += Convert.ToDecimal(r["Thành Tiền"]);
            }
            txtTienDV.Text = tongDV.ToString("N0");
            TinhTongTienTuDong();
        }

        private void dgvChiTietDV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dtChiTietTam.Rows.Count)
            {
                dtChiTietTam.Rows.RemoveAt(e.RowIndex);
                decimal tongDV = 0;
                foreach (DataRow r in dtChiTietTam.Rows)
                {
                    tongDV += Convert.ToDecimal(r["Thành Tiền"]);
                }
                txtTienDV.Text = tongDV.ToString("N0");
                TinhTongTienTuDong();
            }
        }

        private void btnGoiYVoucher_Click(object sender, EventArgs e)
        {
            cmsVoucher.Items.Clear();
            try
            {
                var vouchers = voucherBLL.LayDanhSachVoucher();
                var activeVouchers = vouchers.FindAll(v => v.TrangThai && DateTime.Now.Date <= v.NgayHetHan.Date && DateTime.Now.Date >= v.NgayBatDau.Date);

                if (activeVouchers.Count == 0)
                {
                    cmsVoucher.Items.Add(new ToolStripMenuItem("Không có mã voucher nào đang khả dụng") { Enabled = false });
                }
                else
                {
                    decimal tienPhong = ParseMoney(txtTienPhong.Text);
                    foreach (var v in activeVouchers)
                    {
                        string giamStr = v.LoaiGiam == "PhanTram" ? $"Giảm {v.GiaTri}%" : $"Giảm {v.GiaTri:N0} đ";
                        string minStr = v.DonToiThieu > 0 ? $"(Đơn từ {v.DonToiThieu:N0} đ)" : "(Không giới hạn)";
                        string itemText = $"Mã {v.Code} — {v.TenChuongTrinh} [{giamStr} {minStr}]";

                        var item = new ToolStripMenuItem(itemText);
                        item.Font = new Font("Segoe UI", 9.5F);
                        if (v.DonToiThieu > 0 && tienPhong < v.DonToiThieu)
                        {
                            item.ForeColor = Color.Gray;
                            item.ToolTipText = $"Cần tối thiểu {v.DonToiThieu:N0} đ tiền phòng";
                        }
                        else
                        {
                            item.ForeColor = UITheme.PrimaryOrange;
                            item.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                        }

                        item.Click += (_, __) =>
                        {
                            txtVoucherCode.Text = v.Code;
                            btnApDungVoucher_Click(null, null);
                        };
                        cmsVoucher.Items.Add(item);
                    }
                }
                cmsVoucher.Show(btnGoiYVoucher, new Point(0, btnGoiYVoucher.Height));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy danh sách voucher: " + ex.Message);
            }
        }

        private void btnApDungVoucher_Click(object sender, EventArgs e)
        {
            string code = txtVoucherCode.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Vui lòng nhập mã Voucher.");
                return;
            }

            var v = voucherBLL.TimVoucherTheoCode(code);
            if (v == null)
            {
                MessageBox.Show("Mã Voucher không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!v.TrangThai || DateTime.Now.Date < v.NgayBatDau.Date || DateTime.Now.Date > v.NgayHetHan.Date)
            {
                MessageBox.Show("Mã Voucher đã hết hạn hoặc tạm khóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tienPhong = ParseMoney(txtTienPhong.Text);
            if (v.DonToiThieu > 0 && tienPhong < v.DonToiThieu)
            {
                MessageBox.Show($"Tiền phòng chưa đạt đơn tối thiểu ({v.DonToiThieu:N0} đ)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal giamGia = hoaDonBLL.TinhGiamGiaVoucher(v, tienPhong);
            maVoucherDaApDung = v.MaVoucher;
            txtTienGiamGia.Text = giamGia.ToString("N0");
            TinhTongTienTuDong();

            MessageBox.Show($"Áp dụng thành công [{v.Code}]! Đã giảm: {giamGia:N0} đ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TinhTongTienTuDong()
        {
            decimal tienPhong = ParseMoney(txtTienPhong.Text);
            decimal tienDV = ParseMoney(txtTienDV.Text);
            decimal tienCoc = ParseMoney(txtTienCoc.Text);
            decimal tienGiam = ParseMoney(txtTienGiamGia.Text);

            decimal canThu = tienPhong + tienDV - tienGiam - tienCoc;
            lblTongTienHienThi.Text = canThu < 0 ? "Hoàn khách: " + (-canThu).ToString("N0") + " đ" : canThu.ToString("N0") + " đ";
        }

        private static decimal ParseMoney(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            string clean = Regex.Replace(text, @"[^\d]", "");
            return decimal.TryParse(clean, out decimal val) ? val : 0;
        }

        private string LayMaPTTT()
        {
            if (cboPTTT.SelectedItem is PhuongThucThanhToanModel model)
            {
                string ten = (model.TenPhuongThuc ?? "").ToLower();
                if (ten.Contains("momo")) return "MOMO";
                if (ten.Contains("apple pay") || ten.Contains("contactless")) return "APPLEPAY";
                if (ten.Contains("vnpay") || ten.Contains("emvco")) return "VNPAY";
                if (ten.Contains("chuyển khoản") || ten.Contains("qr")) return "CK";
                if (ten.Contains("thẻ") || ten.Contains("pos")) return "POS";
                return "TM";
            }
            return "TM";
        }

        private void btnVNPAYQR_Click(object sender, EventArgs e)
        {
            decimal canThu = Math.Max(0, ParseMoney(txtTienPhong.Text) + ParseMoney(txtTienDV.Text) - ParseMoney(txtTienGiamGia.Text) - ParseMoney(txtTienCoc.Text));
            if (canThu <= 0)
            {
                MessageBox.Show("Số tiền cần thu bằng 0 đ, không cần mở cổng VNPAY-QR.", "Thông báo");
                return;
            }
            using var qr = new frmVNPAYQR(txtMaHD.Text, canThu, $"THANH TOAN {txtMaHD.Text}");
            qr.ShowDialog(this);
        }

        private void btnMoMo_Click(object sender, EventArgs e)
        {
            decimal canThu = Math.Max(0, ParseMoney(txtTienPhong.Text) + ParseMoney(txtTienDV.Text) - ParseMoney(txtTienGiamGia.Text) - ParseMoney(txtTienCoc.Text));
            if (canThu <= 0)
            {
                MessageBox.Show("Số tiền cần thu bằng 0 đ, không cần quét mã MoMo.", "Thông báo");
                return;
            }
            using var mm = new frmMoMoQR(txtMaHD.Text, canThu, $"THANH TOAN {txtMaHD.Text}");
            mm.ShowDialog(this);
        }

        private void btnApplePay_Click(object sender, EventArgs e)
        {
            decimal canThu = Math.Max(0, ParseMoney(txtTienPhong.Text) + ParseMoney(txtTienDV.Text) - ParseMoney(txtTienGiamGia.Text) - ParseMoney(txtTienCoc.Text));
            if (canThu <= 0)
            {
                MessageBox.Show("Số tiền cần thu bằng 0 đ, không cần chạm POS Apple Pay.", "Thông báo");
                return;
            }
            using var ap = new frmApplePayModal(txtMaHD.Text, canThu, $"THANH TOAN {txtMaHD.Text}");
            ap.ShowDialog(this);
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += InBienLai_PrintPage;
                PrintPreviewDialog dlg = new PrintPreviewDialog { Document = pd, Width = 800, Height = 600 };
                dlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message);
            }
        }

        private void InBienLai_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fTitle = new Font("Segoe UI", 16F, FontStyle.Bold);
            Font fSub = new Font("Segoe UI", 9F, FontStyle.Regular);
            Font fBold = new Font("Segoe UI", 10F, FontStyle.Bold);
            Font fRegular = new Font("Segoe UI", 10F, FontStyle.Regular);

            int x = 50;
            int y = 40;

            g.DrawString("KHÁCH SẠN AN NHIÊN", fTitle, Brushes.Black, x, y);
            y += 30;
            g.DrawString("Địa chỉ: 123 Đường Du Lịch, Thành phố Biển", fSub, Brushes.Gray, x, y);
            y += 20;
            g.DrawString("Hotline: 0901.234.567 | Hóa đơn thanh toán trả phòng", fSub, Brushes.Gray, x, y);
            y += 35;
            g.DrawLine(Pens.Black, x, y, 700, y);
            y += 15;

            g.DrawString($"Mã HĐ: {txtMaHD.Text}", fBold, Brushes.Black, x, y);
            g.DrawString($"Ngày lập: {DateTime.Now:dd/MM/yyyy HH:mm}", fRegular, Brushes.Black, 400, y);
            y += 25;

            if (cboDatPhong.SelectedItem is ThongTinDatPhongThanhToan dp)
            {
                g.DrawString($"Khách hàng: {dp.TenKhachHang}", fRegular, Brushes.Black, x, y);
                g.DrawString($"Phòng: {dp.SoPhong} ({dp.MaDat})", fRegular, Brushes.Black, 400, y);
                y += 25;
            }

            g.DrawLine(Pens.Gray, x, y, 700, y);
            y += 15;

            // Bảng dịch vụ
            g.DrawString("DỊCH VỤ SỬ DỤNG", fBold, Brushes.Black, x, y);
            y += 25;
            g.DrawString("Tên DV", fBold, Brushes.Black, x, y);
            g.DrawString("SL", fBold, Brushes.Black, 380, y);
            g.DrawString("Đơn giá", fBold, Brushes.Black, 460, y);
            g.DrawString("Thành tiền", fBold, Brushes.Black, 580, y);
            y += 20;

            foreach (DataRow r in dtChiTietTam.Rows)
            {
                g.DrawString(r["Tên Dịch Vụ"].ToString(), fRegular, Brushes.Black, x, y);
                g.DrawString(r["SL"].ToString(), fRegular, Brushes.Black, 380, y);
                g.DrawString(Convert.ToDecimal(r["Đơn Giá"]).ToString("N0"), fRegular, Brushes.Black, 460, y);
                g.DrawString(Convert.ToDecimal(r["Thành Tiền"]).ToString("N0"), fRegular, Brushes.Black, 580, y);
                y += 20;
            }

            y += 15;
            g.DrawLine(Pens.Black, x, y, 700, y);
            y += 15;

            g.DrawString($"Tiền thuê phòng: {txtTienPhong.Text} đ", fRegular, Brushes.Black, 400, y); y += 22;
            g.DrawString($"Tiền dịch vụ: +{txtTienDV.Text} đ", fRegular, Brushes.Black, 400, y); y += 22;
            g.DrawString($"Tiền đặt cọc: -{txtTienCoc.Text} đ", fRegular, Brushes.Black, 400, y); y += 22;
            g.DrawString($"Giảm giá voucher: -{txtTienGiamGia.Text} đ", fRegular, Brushes.Black, 400, y); y += 25;

            g.DrawString($"THỰC THU: {lblTongTienHienThi.Text}", new Font("Segoe UI", 12F, FontStyle.Bold), Brushes.DarkOrange, 400, y);
            y += 40;
            g.DrawString("Cảm ơn Quý khách & Hẹn gặp lại!", new Font("Segoe UI", 10F, FontStyle.Italic), Brushes.Black, 250, y);
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (cboDatPhong.SelectedItem is not ThongTinDatPhongThanhToan dp)
            {
                MessageBox.Show("Vui lòng chọn lượt đặt phòng cần thanh toán.");
                return;
            }

            decimal canThu = Math.Max(0, ParseMoney(txtTienPhong.Text) + ParseMoney(txtTienDV.Text) - ParseMoney(txtTienGiamGia.Text) - ParseMoney(txtTienCoc.Text));
            HoaDon hd = new HoaDon
            {
                MaHoaDon = txtMaHD.Text.Trim(),
                MaDatPhong = dp.MaDatPhong.ToString(),
                NgayLap = DateTime.Now,
                NgayThanhToan = DateTime.Now,
                TienPhong = ParseMoney(txtTienPhong.Text),
                TienDichVu = ParseMoney(txtTienDV.Text),
                TienGiamGia = ParseMoney(txtTienGiamGia.Text),
                TienDatCoc = ParseMoney(txtTienCoc.Text),
                TongTien = canThu,
                MaPTTT = LayMaPTTT(),
                MaVoucher = maVoucherDaApDung,
                TrangThai = "Đã thanh toán"
            };

            try
            {
                btnXacNhan.Enabled = false;
                var chiTiet = dtChiTietTam.AsEnumerable().Select(r => new ChiTietHoaDon
                {
                    MaDichVu = r["Mã DV"].ToString(), SoLuong = Convert.ToDecimal(r["SL"]),
                    DonGia = Convert.ToDecimal(r["Đơn Giá"])
                }).ToList();
                decimal hoanDu = Math.Max(0, hd.TienDatCoc - (hd.TienPhong + chiTiet.Sum(x => x.ThanhTien) - hd.TienGiamGia));
                string hoi = hoanDu > 0
                    ? $"Xác nhận đã hoàn khách {hoanDu:N0} đ bằng phương thức đang chọn?"
                    : $"Xác nhận thu {canThu:N0} đ, đã trừ cọc {hd.TienDatCoc:N0} đ?";
                if (MessageBox.Show(this, hoi, "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                if ((hd.MaPTTT == "CK" || hd.MaPTTT == "VNPAY") && canThu > 0)
                {
                    using var qr = new frmVNPAYQR(hd.MaHoaDon, canThu, $"THANH TOAN {hd.MaHoaDon}");
                    if (qr.ShowDialog(this) != DialogResult.OK) return;
                }
                else if (hd.MaPTTT == "MOMO" && canThu > 0)
                {
                    using var qr = new frmMoMoQR(hd.MaHoaDon, canThu, $"MOMO {hd.MaHoaDon}");
                    if (qr.ShowDialog(this) != DialogResult.OK) return;
                }
                else if (hd.MaPTTT == "APPLEPAY" && canThu > 0)
                {
                    using var pos = new frmApplePayModal(hd.MaHoaDon, canThu, $"APPLE PAY {hd.MaHoaDon}");
                    if (pos.ShowDialog(this) != DialogResult.OK) return;
                }
                if (!hoaDonBLL.ThanhToanVaTraPhong(hd, dp.MaDatPhong.ToString(), chiTiet, Session.MaNhanVien)) return;
                // Giữ thao tác giảm lượt voucher của module thanh toán hiện có.
                if (hd.MaVoucher.HasValue)
                {
                    try { voucherBLL.GiamSoLuongVoucher(hd.MaVoucher.Value); }
                    catch (Exception ex) { MessageBox.Show(this, "Hóa đơn đã lưu, nhưng chưa cập nhật lượt voucher: " + ex.Message); }
                }
                // Phiếu thu/hoàn và các dòng dịch vụ đã được lưu trong cùng giao dịch.
                MessageBox.Show($"Thanh toán hóa đơn [{hd.MaHoaDon}] thành công! Thực thu: {hd.TongTien:N0} đ" +
                    (hoanDu > 0 ? $"\nĐã hoàn cọc dư: {hoanDu:N0} đ" : ""), "Thành công");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { MessageBox.Show(this, "Chưa lưu thanh toán: " + ex.Message, "Lỗi thanh toán"); }
            finally { btnXacNhan.Enabled = true; }
        }
    }
}
