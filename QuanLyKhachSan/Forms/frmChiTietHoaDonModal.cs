using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmChiTietHoaDonModal : Form
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");
        private readonly string _maHoaDon;

        // Data cache for printing & display
        private DataRow _drHoaDon;
        private DataTable _dtChiTietDV;

        public frmChiTietHoaDonModal(string maHoaDon)
        {
            _maHoaDon = maHoaDon;
            InitializeComponent();
            KhoiTaoGiaoDien();
            TaiDuLieu();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = $"Chi tiết hóa đơn [{_maHoaDon}]";
            Size = new Size(880, 740);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(248, 250, 252);
            Font = UITheme.MainFont;

            // 1. HEADER
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Color.White,
                Padding = new Padding(24, 12, 24, 12)
            };
            pnlHeader.Paint += (s, e) =>
            {
                using var p = new Pen(UITheme.BorderColor, 1F);
                e.Graphics.DrawLine(p, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            lblTitle = new Label
            {
                Text = "🧾 CHI TIẾT HÓA ĐƠN THANH TOÁN",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Khách sạn An Nhiên • 615 Âu Cơ, Q. Tân Phú, TP. Hồ Chí Minh",
                Font = new Font("Segoe UI", 8.8F, FontStyle.Regular),
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, 40),
                AutoSize = true
            };

            lblBadgeTrangThai = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "● ĐÃ THANH TOÁN",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74),
                BackColor = Color.FromArgb(220, 252, 231),
                Padding = new Padding(12, 6, 12, 6),
                AutoSize = true
            };

            lblMaHDTag = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = $"MÃ HĐ: {_maHoaDon}",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange,
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblBadgeTrangThai);
            pnlHeader.Controls.Add(lblMaHDTag);

            pnlHeader.Resize += (_, __) =>
            {
                lblBadgeTrangThai.Location = new Point(pnlHeader.Width - lblBadgeTrangThai.Width - 24, 12);
                lblMaHDTag.Location = new Point(pnlHeader.Width - lblMaHDTag.Width - 24, 42);
            };

            // 2. FOOTER
            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 62,
                BackColor = Color.White,
                Padding = new Padding(24, 12, 24, 12)
            };
            pnlFooter.Paint += (s, e) =>
            {
                using var p = new Pen(UITheme.BorderColor, 1F);
                e.Graphics.DrawLine(p, 0, 0, pnlFooter.Width, 0);
            };

            btnInHoaDon = new Button
            {
                Text = "🖨️ In hóa đơn / Xuất biên lai",
                Size = new Size(220, 38),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnInHoaDon.FlatAppearance.BorderSize = 0;
            btnInHoaDon.Click += BtnInHoaDon_Click;

            btnDong = new Button
            {
                Text = "✕ Đóng",
                Size = new Size(100, 38),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = UITheme.DarkNavy,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDong.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnDong.Click += (_, __) => Close();

            pnlFooter.Controls.Add(btnInHoaDon);
            pnlFooter.Controls.Add(btnDong);
            pnlFooter.Resize += (_, __) =>
            {
                btnDong.Location = new Point(pnlFooter.Width - btnDong.Width - 24, 12);
                btnInHoaDon.Location = new Point(pnlFooter.Width - btnDong.Width - btnInHoaDon.Width - 36, 12);
            };

            // 3. BODY CONTAINER (Scrollable)
            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(24, 14, 24, 14)
            };

            // 3.1 CUSTOMER & STAY CARD
            pnlCustomerCard = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.White,
                Padding = new Padding(18, 14, 18, 14),
                Margin = new Padding(0, 0, 0, 12)
            };
            pnlCustomerCard.Paint += (s, e) => UITheme.DrawCard(s, e, 8);

            lblTenKhach = new Label { Location = new Point(18, 14), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = UITheme.DarkNavy };
            lblSDT = new Label { Location = new Point(18, 42), AutoSize = true, Font = new Font("Segoe UI", 9F), ForeColor = UITheme.TextMuted };
            lblPhong = new Label { Location = new Point(440, 14), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = UITheme.DarkNavy };
            lblThoiGian = new Label { Location = new Point(440, 42), AutoSize = true, Font = new Font("Segoe UI", 9F), ForeColor = UITheme.TextMuted };

            pnlCustomerCard.Controls.Add(lblTenKhach);
            pnlCustomerCard.Controls.Add(lblSDT);
            pnlCustomerCard.Controls.Add(lblPhong);
            pnlCustomerCard.Controls.Add(lblThoiGian);

            // 3.2 GRID WRAPPER (Chi tiết các mục dịch vụ & phòng)
            pnlGridWrapper = new Panel
            {
                Dock = DockStyle.Top,
                Height = 220,
                BackColor = Color.White,
                Padding = new Padding(16, 12, 16, 12),
                Margin = new Padding(0, 12, 0, 12)
            };
            pnlGridWrapper.Paint += (s, e) => UITheme.DrawCard(s, e, 8);

            var lblGridHeader = new Label
            {
                Dock = DockStyle.Top,
                Height = 26,
                Text = "DANH SÁCH CÁC KHOẢN TÍNH PHÍ",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy
            };

            dgvChiTiet = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(241, 245, 249),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UITheme.FormatDataGridView(dgvChiTiet);
            dgvChiTiet.RowTemplate.Height = 34;

            pnlGridWrapper.Controls.Add(dgvChiTiet);
            pnlGridWrapper.Controls.Add(lblGridHeader);
            dgvChiTiet.BringToFront();

            // 3.3 FINANCE SUMMARY CARD
            pnlFinanceSummary = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160,
                BackColor = Color.White,
                Padding = new Padding(20, 14, 20, 14),
                Margin = new Padding(0, 12, 0, 12)
            };
            pnlFinanceSummary.Paint += (s, e) => UITheme.DrawCard(s, e, 8);

            lblTienPhong = new Label { Location = new Point(20, 16), AutoSize = true, Font = new Font("Segoe UI", 9.2F), ForeColor = UITheme.TextMain };
            lblTienDV = new Label { Location = new Point(20, 42), AutoSize = true, Font = new Font("Segoe UI", 9.2F), ForeColor = UITheme.TextMain };
            lblTienCoc = new Label { Location = new Point(20, 68), AutoSize = true, Font = new Font("Segoe UI", 9.2F), ForeColor = Color.FromArgb(220, 38, 38) };
            lblTienGiam = new Label { Location = new Point(20, 94), AutoSize = true, Font = new Font("Segoe UI", 9.2F), ForeColor = Color.FromArgb(22, 163, 74) };

            lblTongTien = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "TỔNG THANH TOÁN: 0 đ",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange,
                AutoSize = true
            };

            lblPTTT = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "Phương thức: Tiền mặt",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                AutoSize = true
            };

            pnlFinanceSummary.Controls.Add(lblTienPhong);
            pnlFinanceSummary.Controls.Add(lblTienDV);
            pnlFinanceSummary.Controls.Add(lblTienCoc);
            pnlFinanceSummary.Controls.Add(lblTienGiam);
            pnlFinanceSummary.Controls.Add(lblTongTien);
            pnlFinanceSummary.Controls.Add(lblPTTT);

            pnlFinanceSummary.Resize += (_, __) =>
            {
                lblTongTien.Location = new Point(pnlFinanceSummary.Width - lblTongTien.Width - 24, 24);
                lblPTTT.Location = new Point(pnlFinanceSummary.Width - lblPTTT.Width - 24, 66);
            };

            // Thêm các thẻ vào Body theo thứ tự docking
            pnlBody.Controls.Add(pnlFinanceSummary);
            pnlBody.Controls.Add(pnlGridWrapper);
            pnlBody.Controls.Add(pnlCustomerCard);

            pnlCustomerCard.SendToBack();
            pnlGridWrapper.SendToBack();
            pnlFinanceSummary.SendToBack();

            Controls.Add(pnlBody);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
        }

        private void TaiDuLieu()
        {
            try
            {
                string sqlHD = @"
                    SELECT 
                        hd.MaHoaDon, hd.MaDatPhong, hd.NgayLap, hd.NgayThanhToan,
                        COALESCE(hd.TienPhong, 0) AS TienPhong, 
                        COALESCE(hd.TienDichVu, 0) AS TienDichVu, 
                        COALESCE(hd.TienGiamGia, 0) AS TienGiamGia, 
                        COALESCE(hd.TienDatCoc, 0) AS TienDatCoc, 
                        COALESCE(hd.TongTien, 0) AS TongTien,
                        COALESCE(hd.MaPTTT, 'TM') AS MaPTTT, 
                        COALESCE(hd.TrangThai, 'Đã thanh toán') AS TrangThai,
                        dp.MaDat, dp.NgayNhan, dp.NgayTra, 
                        COALESCE(dp.SoDem, GREATEST(1, CAST((dp.NgayTra - dp.NgayNhan) AS INT))) AS SoDem, 
                        COALESCE(dp.GiaMotDem, 0) AS GiaMotDem,
                        COALESCE(kh.HoTen, 'Khách vãng lai') AS TenKhach, 
                        COALESCE(kh.SoDienThoai, '---') AS SoDienThoai, 
                        COALESCE(kh.CCCD, '---') AS CCCD,
                        COALESCE(p.SoPhong, '---') AS SoPhong, 
                        COALESCE(lp.TenLoai, '---') AS TenLoai,
                        COALESCE(pt.TenPhuongThuc, hd.MaPTTT) AS TenPhuongThuc,
                        COALESCE(vc.MaCode, '') AS CodeVoucher
                    FROM HoaDon hd
                    LEFT JOIN DatPhong dp ON (CAST(dp.MaDatPhong AS TEXT) = hd.MaDatPhong OR dp.MaDat = hd.MaDatPhong)
                    LEFT JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                    LEFT JOIN Phong p ON dp.MaPhong = p.MaPhong
                    LEFT JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
                    LEFT JOIN PhuongThucThanhToan pt ON (pt.TenPhuongThuc ILIKE '%' || hd.MaPTTT || '%' OR CAST(pt.MaPhuongThuc AS TEXT) = hd.MaPTTT)
                    LEFT JOIN Voucher vc ON hd.MaVoucher = vc.MaVoucher
                    WHERE hd.MaHoaDon = @MaHD
                    LIMIT 1";

                DataTable dtHD = DatabaseHelper.ExecuteQuery(sqlHD, new SqlParameter[] { new SqlParameter("@MaHD", _maHoaDon) });
                if (dtHD == null || dtHD.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin hóa đơn này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _drHoaDon = dtHD.Rows[0];

                // 1. Cập nhật thẻ Khách hàng
                string tenKhach = _drHoaDon["TenKhach"].ToString();
                string sdt = _drHoaDon["SoDienThoai"].ToString();
                string cccd = _drHoaDon["CCCD"].ToString();
                string phong = _drHoaDon["SoPhong"].ToString();
                string loai = _drHoaDon["TenLoai"].ToString();

                DateTime ngayNhan = _drHoaDon["NgayNhan"] is DateTime d1 ? d1 : DateTime.Today;
                DateTime ngayTra = _drHoaDon["NgayTra"] is DateTime d2 ? d2 : DateTime.Today;
                int soDem = Convert.ToInt32(_drHoaDon["SoDem"]);

                lblTenKhach.Text = $"👤 Khách hàng: {tenKhach}";
                lblSDT.Text = $"📞 Điện thoại: {sdt}   •   CCCD/CMND: {cccd}";
                lblPhong.Text = $"🚪 Phòng: {phong} ({loai})";
                lblThoiGian.Text = $"📅 Lưu trú: {ngayNhan:dd/MM/yyyy} – {ngayTra:dd/MM/yyyy} ({soDem} đêm)";

                // 2. Bảng chi tiết
                DataTable dtGrid = new DataTable();
                dtGrid.Columns.Add("Khoản mục / Dịch vụ", typeof(string));
                dtGrid.Columns.Add("Số lượng", typeof(int));
                dtGrid.Columns.Add("Đơn giá", typeof(string));
                dtGrid.Columns.Add("Thành tiền", typeof(string));

                decimal tienPhong = Convert.ToDecimal(_drHoaDon["TienPhong"]);
                decimal giaMotDem = Convert.ToDecimal(_drHoaDon["GiaMotDem"]);
                if (giaMotDem <= 0 && soDem > 0) giaMotDem = tienPhong / soDem;

                dtGrid.Rows.Add($"Tiền thuê phòng {phong} ({loai})", soDem, giaMotDem.ToString("N0", Vi) + " đ", tienPhong.ToString("N0", Vi) + " đ");

                // Nạp dịch vụ chi tiết
                var chiTietDAL = new ChiTietHoaDonDAL();
                _dtChiTietDV = chiTietDAL.LayChiTietTheoHoaDon(_maHoaDon);
                if (_dtChiTietDV != null && _dtChiTietDV.Rows.Count > 0)
                {
                    foreach (DataRow r in _dtChiTietDV.Rows)
                    {
                        string tenDV = r["Tên Dịch Vụ"].ToString();
                        int sl = Convert.ToInt32(r["SL"]);
                        decimal donGia = Convert.ToDecimal(r["Đơn Giá"]);
                        decimal thanhTien = Convert.ToDecimal(r["Thành Tiền"]);
                        dtGrid.Rows.Add(tenDV, sl, donGia.ToString("N0", Vi) + " đ", thanhTien.ToString("N0", Vi) + " đ");
                    }
                }

                dgvChiTiet.DataSource = dtGrid;

                if (dgvChiTiet.Columns["Khoản mục / Dịch vụ"] != null) dgvChiTiet.Columns["Khoản mục / Dịch vụ"].FillWeight = 48;
                if (dgvChiTiet.Columns["Số lượng"] != null)
                {
                    dgvChiTiet.Columns["Số lượng"].FillWeight = 14;
                    dgvChiTiet.Columns["Số lượng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvChiTiet.Columns["Đơn giá"] != null)
                {
                    dgvChiTiet.Columns["Đơn giá"].FillWeight = 19;
                    dgvChiTiet.Columns["Đơn giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvChiTiet.Columns["Thành tiền"] != null)
                {
                    dgvChiTiet.Columns["Thành tiền"].FillWeight = 19;
                    dgvChiTiet.Columns["Thành tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvChiTiet.Columns["Thành tiền"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }

                // 3. Tài chính
                decimal tienDV = Convert.ToDecimal(_drHoaDon["TienDichVu"]);
                decimal tienCoc = Convert.ToDecimal(_drHoaDon["TienDatCoc"]);
                decimal tienGiam = Convert.ToDecimal(_drHoaDon["TienGiamGia"]);
                decimal tongTien = Convert.ToDecimal(_drHoaDon["TongTien"]);
                string pttt = _drHoaDon["TenPhuongThuc"].ToString();
                string voucherCode = _drHoaDon["CodeVoucher"].ToString();

                lblTienPhong.Text = $"• Tiền phòng ({soDem} đêm): {tienPhong:N0} VNĐ";
                lblTienDV.Text = $"• Tiền dịch vụ sử dụng: {tienDV:N0} VNĐ";
                lblTienCoc.Text = $"• Khấu trừ tiền đặt cọc: -{tienCoc:N0} VNĐ";
                lblTienGiam.Text = string.IsNullOrEmpty(voucherCode)
                    ? $"• Giảm giá voucher: -{tienGiam:N0} VNĐ"
                    : $"• Giảm giá voucher [{voucherCode}]: -{tienGiam:N0} VNĐ";

                lblTongTien.Text = $"TỔNG THANH TOÁN: {tongTien:N0} VNĐ";
                lblPTTT.Text = $"💳 Phương thức: {pttt}";

                lblBadgeTrangThai.Text = "● " + _drHoaDon["TrangThai"].ToString().ToUpper();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnInHoaDon_Click(object sender, EventArgs e)
        {
            if (_drHoaDon == null) return;
            try
            {
                using var printDoc = new PrintDocument();
                printDoc.PrintPage += PrintDoc_PrintPage;
                using var previewDlg = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 800,
                    Height = 650,
                    StartPosition = FormStartPosition.CenterParent
                };
                previewDlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_drHoaDon == null) return;

            Graphics g = e.Graphics;
            Font fHeader = new Font("Segoe UI", 14F, FontStyle.Bold);
            Font fBold = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Font fRegular = new Font("Segoe UI", 9F, FontStyle.Regular);
            Font fSmall = new Font("Segoe UI", 8.5F, FontStyle.Italic);

            Brush b = Brushes.Black;
            Pen pDash = new Pen(Color.Black, 1) { DashStyle = DashStyle.Dash };

            int y = 20;
            int w = 290;
            StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat sfRight = new StringFormat { Alignment = StringAlignment.Far };

            g.DrawString("AN NHIÊN HOTEL", fHeader, b, new RectangleF(0, y, w, 24), sfCenter);
            y += 24;
            g.DrawString("615 Âu Cơ, Phường Tân Phú, TP. HCM", fSmall, b, new RectangleF(0, y, w, 18), sfCenter);
            y += 18;
            g.DrawString("Hotline: 028 3849 5555", fSmall, b, new RectangleF(0, y, w, 18), sfCenter);
            y += 24;

            g.DrawLine(pDash, 0, y, w, y);
            y += 10;
            g.DrawString("PHIẾU THANH TOÁN DỊCH VỤ", fBold, b, new RectangleF(0, y, w, 20), sfCenter);
            y += 20;
            g.DrawString($"Số HĐ: {_maHoaDon}", fSmall, b, new RectangleF(0, y, w, 18), sfCenter);
            y += 24;

            DateTime ngayTT = _drHoaDon["NgayThanhToan"] is DateTime d ? d : DateTime.Now;
            g.DrawString($"Ngày: {ngayTT:dd/MM/yyyy HH:mm}", fRegular, b, 0, y);
            y += 18;
            g.DrawString($"Khách hàng: {_drHoaDon["TenKhach"]}", fRegular, b, 0, y);
            y += 18;
            g.DrawString($"Phòng: {_drHoaDon["SoPhong"]} ({_drHoaDon["TenLoai"]})", fRegular, b, 0, y);
            y += 22;

            g.DrawLine(pDash, 0, y, w, y);
            y += 6;
            g.DrawString("Mục thanh toán", fBold, b, 0, y);
            g.DrawString("T.Tiền", fBold, b, new RectangleF(0, y, w, 18), sfRight);
            y += 20;
            g.DrawLine(pDash, 0, y, w, y);
            y += 8;

            decimal tienPhong = Convert.ToDecimal(_drHoaDon["TienPhong"]);
            int soDem = Convert.ToInt32(_drHoaDon["SoDem"]);
            g.DrawString($"Tiền phòng ({soDem} đêm)", fRegular, b, 0, y);
            g.DrawString(tienPhong.ToString("N0", Vi), fRegular, b, new RectangleF(0, y, w, 18), sfRight);
            y += 18;

            if (_dtChiTietDV != null && _dtChiTietDV.Rows.Count > 0)
            {
                foreach (DataRow r in _dtChiTietDV.Rows)
                {
                    string ten = r["Tên Dịch Vụ"].ToString();
                    int sl = Convert.ToInt32(r["SL"]);
                    decimal tt = Convert.ToDecimal(r["Thành Tiền"]);
                    g.DrawString($"{ten} x{sl}", fRegular, b, 0, y);
                    g.DrawString(tt.ToString("N0", Vi), fRegular, b, new RectangleF(0, y, w, 18), sfRight);
                    y += 18;
                }
            }

            y += 6;
            g.DrawLine(pDash, 0, y, w, y);
            y += 8;

            decimal tienCoc = Convert.ToDecimal(_drHoaDon["TienDatCoc"]);
            if (tienCoc > 0)
            {
                g.DrawString("Trừ tiền cọc:", fRegular, b, 0, y);
                g.DrawString("-" + tienCoc.ToString("N0", Vi), fRegular, b, new RectangleF(0, y, w, 18), sfRight);
                y += 18;
            }

            decimal tienGiam = Convert.ToDecimal(_drHoaDon["TienGiamGia"]);
            if (tienGiam > 0)
            {
                g.DrawString("Giảm giá voucher:", fRegular, b, 0, y);
                g.DrawString("-" + tienGiam.ToString("N0", Vi), fRegular, b, new RectangleF(0, y, w, 18), sfRight);
                y += 18;
            }

            decimal tong = Convert.ToDecimal(_drHoaDon["TongTien"]);
            g.DrawString("TỔNG CỘNG:", fBold, b, 0, y);
            g.DrawString(tong.ToString("N0", Vi) + " đ", fBold, b, new RectangleF(0, y, w, 18), sfRight);
            y += 24;

            g.DrawString($"Phương thức: {_drHoaDon["TenPhuongThuc"]}", fSmall, b, 0, y);
            y += 26;

            g.DrawLine(pDash, 0, y, w, y);
            y += 12;
            g.DrawString("Cảm ơn Quý khách & Hẹn gặp lại!", fSmall, b, new RectangleF(0, y, w, 18), sfCenter);
        }
    }
}
