namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmKhachHang
    {
        private Label lblKhachDangXem, lblTenHoSo, lblMaHoSo, lblAnhDaiDien;
        private Label lblSoLanTomTat, lblLanGanNhat, lblNgayTaoHoSo;
        private TableLayoutPanel khungNhapLieu;
        private int dongLoiGiayTo;

        private void ThietKeGiaoDienKhachHang()
        {
            SuspendLayout();
            MinimumSize = Size.Empty;
            BackColor = UITheme.BackgroundGray;
            Font = UITheme.MainFont;
            Controls.Clear();
            pnlDanhSach.Controls.Clear();
            pnlThongTin.Controls.Clear();

            lblTieuDe.Text = "Quản lý khách hàng";
            lblMoTa.Text = "Hồ sơ khách hàng, lịch sử lưu trú và giao dịch đặt cọc";
            btnThemKhachHang.Text = "+  Thêm khách hàng";
            LeTanUi.Button(btnThemKhachHang, UITheme.PrimaryOrange, true);
            var root = LeTanUi.Table();
            var header = LeTanUi.Header(lblTieuDe, lblMoTa, btnThemKhachHang);
            lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            LeTanUi.Row(root, header, 86);

            var left = LeTanUi.Table();
            LeTanUi.Row(left, TaoTheDanhSach());
            LeTanUi.Row(left, new Panel { BackColor = UITheme.BackgroundGray }, 16);
            LeTanUi.Row(left, TaoTheLichSu(), 232);
            LeTanUi.Row(root, new KhachHangWorkspace(left, TaoTheThongTin()));
            Controls.Add(root);
            CapNhatTomTatKhachHang(null);
            ResumeLayout(true);
        }

        private Control TaoTheDanhSach()
        {
            var card = new LeTanCard { Padding = new Padding(18, 14, 18, 10) };
            var layout = LeTanUi.Table();
            var title = HangNgang(2);
            title.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            title.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 112);
            var caption = LeTanUi.Caption("Danh sách khách hàng", true);
            caption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            title.Controls.Add(caption, 0, 0);
            LeTanUi.Badge(lblTongHoSo, LeTanUi.OrangeLight, UITheme.PrimaryOrange);
            lblTongHoSo.Dock = DockStyle.Fill;
            lblTongHoSo.Margin = new Padding(0, 3, 0, 3);
            title.Controls.Add(lblTongHoSo, 1, 0);
            LeTanUi.Row(layout, title, 34);

            var filters = HangNgang(2);
            filters.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            filters.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 170);
            txtTimKiem.PlaceholderText = "Tìm tên, số điện thoại, giấy tờ…";
            txtTimKiem.AccessibleName = "Tìm kiếm khách hàng";
            cboNhomKhach.AccessibleName = "Lọc theo nhóm khách hàng";
            var search = LeTanUi.Input(txtTimKiem);
            var group = LeTanUi.Input(cboNhomKhach);
            search.Dock = group.Dock = DockStyle.Fill;
            search.Margin = new Padding(0, 10, 10, 10);
            group.Margin = new Padding(0, 10, 0, 10);
            filters.Controls.Add(search, 0, 0);
            filters.Controls.Add(group, 1, 0);
            LeTanUi.Row(layout, filters, 60);

            LeTanUi.Grid(dgvKhachHang);
            dgvKhachHang.RowTemplate.Height = 46;
            dgvKhachHang.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            foreach (DataGridViewColumn column in dgvKhachHang.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMaKH.MinimumWidth = 84; colMaKH.FillWeight = 85;
            colHoTen.MinimumWidth = 150; colHoTen.FillWeight = 165;
            colDienThoai.MinimumWidth = 110; colDienThoai.FillWeight = 115;
            colSoGiayTo.MinimumWidth = 124; colSoGiayTo.FillWeight = 125;
            colSoLanLuuTru.MinimumWidth = 76; colSoLanLuuTru.FillWeight = 76;
            colSoLanLuuTru.HeaderText = "Lưu trú";
            colSoLanLuuTru.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNhomKhach.MinimumWidth = 108; colNhomKhach.FillWeight = 110;
            UITheme.DongBoCanLeTieuDe(dgvKhachHang);
            LeTanUi.EmptyGrid(dgvKhachHang, "Chưa có khách hàng phù hợp", "Thử đổi từ khóa, nhóm khách hoặc thêm hồ sơ mới.");
            LeTanUi.Row(layout, dgvKhachHang);

            var footer = HangNgang(2);
            lblSoKetQua.AutoSize = false;
            lblSoKetQua.Font = new Font("Segoe UI", 8.5F);
            lblSoKetQua.ForeColor = UITheme.TextMuted;
            lblSoKetQua.TextAlign = ContentAlignment.MiddleLeft;
            lblSoKetQua.Dock = DockStyle.Fill;
            lblNgayHienTai.AutoSize = false;
            lblNgayHienTai.BorderStyle = BorderStyle.None;
            lblNgayHienTai.Font = new Font("Segoe UI", 8.5F);
            lblNgayHienTai.ForeColor = UITheme.TextMuted;
            lblNgayHienTai.TextAlign = ContentAlignment.MiddleRight;
            lblNgayHienTai.Dock = DockStyle.Fill;
            footer.Controls.Add(lblSoKetQua, 0, 0);
            footer.Controls.Add(lblNgayHienTai, 1, 0);
            LeTanUi.Row(layout, footer, 30);
            card.Controls.Add(layout);
            return card;
        }

        private Control TaoTheLichSu()
        {
            var card = new LeTanCard { Padding = new Padding(14, 8, 14, 10) };
            var layout = LeTanUi.Table();
            lblKhachDangXem = LeTanUi.Caption("Chọn khách hàng để xem chi tiết", true);
            LeTanUi.Row(layout, lblKhachDangXem, 30);
            tpThongTinChung.Text = "Tổng quan";
            tpLichSu.Text = "Lưu trú (0)";
            tpGhiChu.Text = "Ghi chú";
            tpCoc.Text = "Thu / hoàn (0)";
            tpThongTinChung.ToolTipText = "Thông tin tổng quan của khách đang chọn";
            tpLichSu.ToolTipText = "Lịch sử nhận và trả phòng của khách đang chọn";
            tpCoc.ToolTipText = "Phiếu thu và hoàn cọc của khách đang chọn";
            tabChiTiet.ShowToolTips = true;
            tabChiTiet.Font = new Font("Segoe UI", 9F);
            tabChiTiet.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabChiTiet.SizeMode = TabSizeMode.Fixed;
            tabChiTiet.ItemSize = new Size(118, 34);
            tabChiTiet.DrawItem += VeTabChiTiet;
            foreach (TabPage page in tabChiTiet.TabPages)
            {
                page.BackColor = Color.White;
                page.UseVisualStyleBackColor = false;
                page.Padding = new Padding(8);
            }

            tpThongTinChung.Controls.Clear();
            var overview = LeTanUi.Table();
            var metrics = HangNgang(3);
            metrics.Controls.Add(TaoChiSoHoSo("SỐ LẦN LƯU TRÚ", out lblSoLanTomTat, true), 0, 0);
            metrics.Controls.Add(TaoChiSoHoSo("LƯU TRÚ GẦN NHẤT", out lblLanGanNhat), 1, 0);
            metrics.Controls.Add(TaoChiSoHoSo("NGÀY TẠO HỒ SƠ", out lblNgayTaoHoSo), 2, 0);
            LeTanUi.Row(overview, metrics);
            lblThongTinChung.AutoSize = false;
            lblThongTinChung.Font = new Font("Segoe UI", 8.5F);
            lblThongTinChung.ForeColor = UITheme.TextMuted;
            lblThongTinChung.TextAlign = ContentAlignment.MiddleLeft;
            lblThongTinChung.AutoEllipsis = true;
            LeTanUi.Row(overview, lblThongTinChung, 28);
            tpThongTinChung.Controls.Add(overview);

            tpGhiChu.AutoScroll = true;
            lblGhiChuXem.AutoSize = true;
            lblGhiChuXem.Dock = DockStyle.None;
            lblGhiChuXem.Location = new Point(10, 10);
            lblGhiChuXem.Font = UITheme.MainFont;
            lblGhiChuXem.ForeColor = UITheme.TextMain;
            lblGhiChuXem.Padding = new Padding(6);
            tpGhiChu.SizeChanged += (_, __) =>
                lblGhiChuXem.MaximumSize = new Size(Math.Max(100, tpGhiChu.ClientSize.Width - 36), 0);
            LeTanUi.Grid(dgvLichSu);
            LeTanUi.Grid(dgvCoc);
            dgvLichSu.ColumnHeadersVisible = true;
            dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            foreach (DataGridViewColumn column in dgvLichSu.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvCoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            HienThiBangTrong(dgvLichSu, "Chưa có lịch sử lưu trú", "Các lần nhận và trả phòng của khách sẽ hiển thị tại đây.");
            HienThiBangTrong(dgvCoc, "Chưa có giao dịch cọc", "Phiếu thu và hoàn cọc của khách sẽ hiển thị tại đây.");
            LeTanUi.Row(layout, tabChiTiet);
            card.Controls.Add(layout);
            return card;
        }

        private static Control TaoChiSoHoSo(string caption, out Label value, bool accent = false)
        {
            var box = new Panel
            {
                Dock = DockStyle.Fill, BackColor = accent ? LeTanUi.OrangeLight : UITheme.BackgroundLight,
                Padding = new Padding(12, 8, 8, 6), Margin = new Padding(0, 3, 8, 0)
            };
            var layout = LeTanUi.Table();
            var label = LeTanUi.Caption(caption);
            label.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            value = LeTanUi.Caption("—", true);
            value.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            value.ForeColor = accent ? UITheme.PrimaryOrange : UITheme.DarkNavy;
            LeTanUi.Row(layout, label, 22);
            LeTanUi.Row(layout, value);
            box.Controls.Add(layout);
            return box;
        }

        private Control TaoTheThongTin()
        {
            var card = new LeTanCard { Padding = new Padding(18, 14, 18, 10) };
            var layout = LeTanUi.Table();
            lblTieuDeThongTin.AutoSize = false;
            lblTieuDeThongTin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTieuDeThongTin.ForeColor = UITheme.DarkNavy;
            lblTieuDeThongTin.TextAlign = ContentAlignment.MiddleLeft;
            LeTanUi.Row(layout, lblTieuDeThongTin, 30);
            LeTanUi.Row(layout, LeTanUi.Caption("Thông tin có dấu * là bắt buộc"), 24);

            var identity = HangNgang(3);
            identity.Padding = new Padding(0, 8, 0, 8);
            identity.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 48);
            identity.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
            identity.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 86);
            lblAnhDaiDien = LeTanUi.Caption("KH", true);
            lblAnhDaiDien.BackColor = LeTanUi.OrangeLight;
            lblAnhDaiDien.ForeColor = UITheme.PrimaryOrange;
            lblAnhDaiDien.TextAlign = ContentAlignment.MiddleCenter;
            lblAnhDaiDien.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAnhDaiDien.Margin = new Padding(0, 0, 6, 0);
            identity.Controls.Add(lblAnhDaiDien, 0, 0);
            var name = LeTanUi.Table();
            name.Margin = new Padding(4, 0, 4, 0);
            lblTenHoSo = LeTanUi.Caption("Chưa chọn khách", true);
            lblMaHoSo = LeTanUi.Caption("Chọn từ danh sách bên trái");
            lblMaHoSo.Font = new Font("Segoe UI", 8.5F);
            LeTanUi.Row(name, lblTenHoSo, 23);
            LeTanUi.Row(name, lblMaHoSo);
            identity.Controls.Add(name, 1, 0);
            LeTanUi.Badge(lblNhomBadge);
            lblNhomBadge.Dock = DockStyle.Fill;
            lblNhomBadge.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblNhomBadge.Margin = new Padding(0, 8, 0, 8);
            identity.Controls.Add(lblNhomBadge, 2, 0);
            LeTanUi.Row(layout, identity, 64);
            LeTanUi.Row(layout, new Panel { BackColor = UITheme.BorderColor }, 1);

            pnlThongTin.AutoScroll = true;
            pnlThongTin.AutoScrollMinSize = Size.Empty;
            pnlThongTin.Padding = new Padding(0, 8, 8, 0);
            pnlThongTin.BackColor = Color.White;
            khungNhapLieu = LeTanUi.Table();
            khungNhapLieu.Dock = DockStyle.Top;
            khungNhapLieu.AutoSize = true;
            txtHoTen.PlaceholderText = "Nhập họ và tên khách hàng";
            txtDienThoai.PlaceholderText = "Số điện thoại liên hệ";
            txtEmail.PlaceholderText = "Email (không bắt buộc)";
            txtSoGiayTo.PlaceholderText = "Nhập số CCCD hoặc hộ chiếu";
            txtGhiChu.PlaceholderText = "Sở thích, yêu cầu đặc biệt của khách…";
            ThemDongNhap(LeTanUi.Field(lblHoTen, txtHoTen));
            ThemDongNhap(TaoCapTruong(lblDienThoai, txtDienThoai, lblGioiTinh, cboGioiTinh, 60));
            ThemDongNhap(TaoCapTruong(lblNgaySinh, dtpNgaySinh, lblQuocTich, txtQuocTich));
            ThemDongNhap(LeTanUi.Field(lblEmail, txtEmail));
            ThemDongNhap(LeTanUi.Field(lblSoGiayTo, txtSoGiayTo));
            lblLoiSoGiayTo.AutoSize = false;
            lblLoiSoGiayTo.Font = new Font("Segoe UI", 8.5F);
            lblLoiSoGiayTo.ForeColor = UITheme.DangerRed;
            dongLoiGiayTo = khungNhapLieu.RowCount;
            LeTanUi.Row(khungNhapLieu, lblLoiSoGiayTo, 36);
            khungNhapLieu.RowStyles[dongLoiGiayTo].Height = 0;
            txtGhiChu.Multiline = true;
            txtGhiChu.ScrollBars = ScrollBars.Vertical;
            ThemDongNhap(LeTanUi.Field(lblGhiChu, txtGhiChu), 94);
            pnlThongTin.Controls.Add(khungNhapLieu);
            LeTanUi.Row(layout, pnlThongTin);

            LeTanUi.Row(layout, new Panel { BackColor = UITheme.BorderColor }, 1);
            LeTanUi.Button(btnCapNhat, UITheme.PrimaryOrange, true);
            LeTanUi.Button(btnHuy);
            btnCapNhat.Text = "Lưu thay đổi";
            btnHuy.Text = "Bỏ thay đổi";
            var actions = HangNgang(2);
            actions.Padding = new Padding(0, 12, 0, 2);
            actions.ColumnStyles[0].Width = 42;
            actions.ColumnStyles[1].Width = 58;
            btnHuy.Dock = btnCapNhat.Dock = DockStyle.Fill;
            btnHuy.Margin = new Padding(0, 0, 8, 0);
            btnCapNhat.Margin = Padding.Empty;
            actions.Controls.Add(btnHuy, 0, 0);
            actions.Controls.Add(btnCapNhat, 1, 0);
            LeTanUi.Row(layout, actions, 56);
            card.Controls.Add(layout);
            return card;
        }

        private void ThemDongNhap(Control field, int height = 70)
        {
            LeTanUi.Row(khungNhapLieu, field, height);
            field.Margin = new Padding(0, 0, 0, 8);
            field.TabIndex = khungNhapLieu.RowCount - 1;
        }

        private static Control TaoCapTruong(Label firstLabel, Control first, Label secondLabel, Control second, int firstWidth = 50)
        {
            var row = HangNgang(2);
            row.ColumnStyles[0].Width = firstWidth;
            row.ColumnStyles[1].Width = 100 - firstWidth;
            var left = LeTanUi.Field(firstLabel, first);
            var right = LeTanUi.Field(secondLabel, second);
            left.Dock = right.Dock = DockStyle.Fill;
            left.Margin = new Padding(0, 0, 10, 0);
            right.Margin = Padding.Empty;
            left.TabIndex = 0; right.TabIndex = 1;
            row.Controls.Add(left, 0, 0);
            row.Controls.Add(right, 1, 0);
            foreach (Control input in new[] { first, second })
                if (input is ComboBox || input is DateTimePicker)
                    input.Parent.Padding = new Padding(8, 4, 8, 4);
            return row;
        }

        private static TableLayoutPanel HangNgang(int columns)
        {
            var row = LeTanUi.Table(columns);
            row.RowCount = 1;
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            return row;
        }

        private static void HienThiBangTrong(DataGridView grid, string title, string detail)
        {
            grid.Paint += (_, e) =>
            {
                if (grid.Rows.Count != 0) return;
                int available = grid.ClientSize.Height - grid.ColumnHeadersHeight;
                if (available < 28) return;
                int top = grid.ColumnHeadersHeight + Math.Max(2, (available - 60) / 2);
                var bounds = new Rectangle(8, top, Math.Max(1, grid.ClientSize.Width - 16), 26);
                var flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
                TextRenderer.DrawText(e.Graphics, title, UITheme.BoldFont, bounds, UITheme.DarkNavy, flags);
                if (available < 60) return;
                bounds.Y += 28;
                TextRenderer.DrawText(e.Graphics, detail, UITheme.MainFont, bounds, UITheme.TextMuted, flags);
            };
        }

        private void CapNhatTomTatKhachHang(Models.KhachHang kh)
        {
            lblKhachDangXem.Text = kh == null ? "Hồ sơ khách hàng" : "Hồ sơ · " + kh.HoTen;
            lblTenHoSo.Text = kh?.HoTen ?? (cheDoThem ? "Khách hàng mới" : "Chưa chọn khách");
            lblMaHoSo.Text = kh?.MaKH ?? "Mã hồ sơ được tạo khi lưu";
            lblAnhDaiDien.Text = "KH";
            if (!string.IsNullOrWhiteSpace(kh?.HoTen))
            {
                var words = kh.HoTen.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                lblAnhDaiDien.Text = (words.Length > 1 ? words[0][..1] + words[^1][..1] : words[0][..1]).ToUpper(Vi);
            }
            lblSoLanTomTat.Text = kh == null ? "—" : kh.SoLanLuuTru + " lần";
            lblLanGanNhat.Text = kh?.LanLuuTruGanNhat?.ToString("dd/MM/yyyy") ?? "—";
            lblNgayTaoHoSo.Text = kh?.NgayTao.ToString("dd/MM/yyyy") ?? "—";
            lblThongTinChung.Text = kh == null
                ? "Chọn một khách hàng để xem lịch sử và các giao dịch."
                : $"{kh.MaKH}  ·  {kh.NhomKhach}  ·  Hồ sơ tạo lúc {kh.NgayTao:HH:mm, dd/MM/yyyy}";
        }

        private void VeTabChiTiet(object sender, DrawItemEventArgs e)
        {
            var bounds = tabChiTiet.GetTabRect(e.Index);
            bool selected = tabChiTiet.SelectedIndex == e.Index;
            using var background = new SolidBrush(selected ? LeTanUi.OrangeLight : Color.White);
            e.Graphics.FillRectangle(background, bounds);
            TextRenderer.DrawText(e.Graphics, tabChiTiet.TabPages[e.Index].Text, tabChiTiet.Font, bounds,
                selected ? UITheme.PrimaryOrange : UITheme.TextMuted,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            if (selected)
            {
                using var pen = new Pen(UITheme.PrimaryOrange, 3);
                e.Graphics.DrawLine(pen, bounds.Left + 10, bounds.Bottom - 2, bounds.Right - 10, bounds.Bottom - 2);
                if (tabChiTiet.Focused) ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(bounds, -4, -4));
            }
        }
    }

    // Bố cục riêng để thay đổi trang khách hàng không ảnh hưởng trang đặt phòng.
    internal sealed class KhachHangWorkspace : Panel
    {
        private readonly Control danhSach, hoSo;
        private readonly Panel noiDung = new() { Margin = Padding.Empty };
        private bool dangBoTri;
        private Size kichThuocDaBoTri;

        internal KhachHangWorkspace(Control danhSach, Control hoSo)
        {
            this.danhSach = danhSach;
            this.hoSo = hoSo;
            DoubleBuffered = true;
            AutoScroll = true;
            BackColor = UITheme.BackgroundGray;
            danhSach.Dock = hoSo.Dock = DockStyle.None;
            danhSach.Anchor = hoSo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            noiDung.Controls.Add(danhSach);
            noiDung.Controls.Add(hoSo);
            Controls.Add(noiDung);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (dangBoTri || danhSach == null || hoSo == null) return;
            if (kichThuocDaBoTri == Size)
            {
                base.OnLayout(e);
                return;
            }
            dangBoTri = true;
            SuspendLayout();
            try
            {
                kichThuocDaBoTri = Size;
                float scale = DeviceDpi / 96F;
                int pad = (int)(20 * scale), gap = (int)(16 * scale);
                bool wide = Width - pad * 2 >= 980 * scale;
                int height = Math.Max((int)(500 * scale), Height - pad * 2);
                int detailWidth = (int)(380 * scale);
                int listHeight = (int)(570 * scale), detailHeight = (int)(650 * scale);
                int contentHeight = wide ? height + pad * 2 : listHeight + detailHeight + gap + pad * 2;
                int scrollbar = contentHeight > Height ? SystemInformation.VerticalScrollBarWidth : 0;
                int width = Math.Max((int)(500 * scale), Width - scrollbar - pad * 2);
                noiDung.Size = new Size(width + pad * 2, contentHeight);
                if (wide)
                {
                    danhSach.SetBounds(pad, pad, width - detailWidth - gap, height);
                    hoSo.SetBounds(pad + width - detailWidth, pad, detailWidth, height);
                    ((TableLayoutPanel)danhSach).RowStyles[2].Height = Math.Clamp(height * .35F, 208 * scale, 232 * scale);
                }
                else
                {
                    danhSach.SetBounds(pad, pad, width, listHeight);
                    hoSo.SetBounds(pad, pad + listHeight + gap, width, detailHeight);
                    ((TableLayoutPanel)danhSach).RowStyles[2].Height = 210 * scale;
                }
            }
            finally
            {
                ResumeLayout(false);
                base.OnLayout(e);
                dangBoTri = false;
            }
        }

        protected override void OnDpiChangedAfterParent(EventArgs e)
        {
            kichThuocDaBoTri = Size.Empty;
            base.OnDpiChangedAfterParent(e);
            PerformLayout();
        }
    }
}
