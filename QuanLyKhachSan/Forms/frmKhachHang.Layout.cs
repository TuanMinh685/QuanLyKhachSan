namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmKhachHang
    {
        private void ThietKeGiaoDienKhachHang()
        {
            SuspendLayout();
            MinimumSize = Size.Empty;
            BackColor = UITheme.BackgroundGray;
            Font = UITheme.MainFont;
            Controls.Clear();
            pnlDanhSach.Controls.Clear();
            pnlThongTin.Controls.Clear();

            lblTieuDe.Text = "QUẢN LÝ KHÁCH HÀNG";
            lblMoTa.Text = "Quản lý hồ sơ, lịch sử lưu trú và các khoản thu / hoàn cọc";
            btnThemKhachHang.Text = "+  Thêm khách hàng";
            LeTanUi.Button(btnThemKhachHang, UITheme.PrimaryOrange, true);
            var root = LeTanUi.Table();
            LeTanUi.Row(root, LeTanUi.Header(lblTieuDe, lblMoTa, btnThemKhachHang), 88);

            var listCard = new LeTanCard { Padding = new Padding(16) };
            var listLayout = LeTanUi.Table();
            var counts = LeTanUi.Table(2);
            counts.ColumnStyles[0].Width = 65; counts.ColumnStyles[1].Width = 35;
            lblSoKetQua.Font = UITheme.BoldFont; lblSoKetQua.ForeColor = UITheme.DarkNavy;
            lblSoKetQua.AutoSize = false; lblSoKetQua.Dock = DockStyle.Fill;
            lblSoKetQua.TextAlign = ContentAlignment.MiddleLeft;
            LeTanUi.Badge(lblTongHoSo, LeTanUi.OrangeLight, UITheme.PrimaryOrange);
            lblTongHoSo.Dock = DockStyle.Fill;
            counts.Controls.Add(lblSoKetQua, 0, 0); counts.Controls.Add(lblTongHoSo, 1, 0);
            LeTanUi.Row(listLayout, counts, 38);

            var filters = LeTanUi.Table(2);
            filters.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            filters.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 178);
            txtTimKiem.PlaceholderText = "Tên, SĐT, giấy tờ… (Ctrl+F)";
            var search = LeTanUi.Input(txtTimKiem); search.Dock = DockStyle.Fill;
            var group = LeTanUi.Input(cboNhomKhach); group.Dock = DockStyle.Fill;
            search.Margin = new Padding(0, 10, 10, 10); group.Margin = new Padding(0, 10, 0, 10);
            filters.Controls.Add(search, 0, 0); filters.Controls.Add(group, 1, 0);
            LeTanUi.Row(listLayout, filters, 62);

            LeTanUi.Grid(dgvKhachHang);
            foreach (DataGridViewColumn column in dgvKhachHang.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMaKH.MinimumWidth = 84; colHoTen.MinimumWidth = 150;
            colHoTen.FillWeight = 170; colDienThoai.MinimumWidth = 120;
            colSoGiayTo.MinimumWidth = 140; colSoLanLuuTru.MinimumWidth = 82;
            colSoLanLuuTru.HeaderText = "Lưu trú"; colNhomKhach.MinimumWidth = 112;
            LeTanUi.EmptyGrid(dgvKhachHang, "Chưa có khách hàng phù hợp", "Thử đổi từ khóa, nhóm khách hoặc thêm hồ sơ mới.");
            var danhSachTab = new TabPage("Danh sách");
            danhSachTab.Controls.Add(dgvKhachHang);
            tabChiTiet.TabPages.Insert(0, danhSachTab);
            tabChiTiet.SelectedIndex = 0;
            tpThongTinChung.Text = "Tổng quan";
            tpLichSu.Text = "Lưu trú (0)";
            tpCoc.Text = "Thu / hoàn (0)";
            tpThongTinChung.ToolTipText = "Thông tin tổng quan của khách đang chọn";
            tpLichSu.ToolTipText = "Lịch sử lưu trú của khách đang chọn";
            tpCoc.ToolTipText = "Phiếu thu và hoàn cọc của khách đang chọn";
            tabChiTiet.ShowToolTips = true;
            tabChiTiet.Dock = DockStyle.Fill; tabChiTiet.Margin = new Padding(0, 14, 0, 0);
            tabChiTiet.Font = new Font("Segoe UI", 9F);
            tabChiTiet.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabChiTiet.SizeMode = TabSizeMode.Fixed; tabChiTiet.ItemSize = new Size(112, 36);
            tabChiTiet.DrawItem += VeTabChiTiet;
            foreach (TabPage page in tabChiTiet.TabPages)
            {
                page.BackColor = Color.White; page.UseVisualStyleBackColor = false;
                page.Padding = new Padding(10);
            }
            lblThongTinChung.AutoSize = false; lblThongTinChung.Dock = DockStyle.Fill;
            lblThongTinChung.Font = UITheme.MainFont; lblThongTinChung.ForeColor = UITheme.TextMain;
            lblThongTinChung.Padding = new Padding(6); lblThongTinChung.AutoEllipsis = true;
            tpGhiChu.AutoScroll = true;
            lblGhiChuXem.AutoSize = true; lblGhiChuXem.Dock = DockStyle.None;
            lblGhiChuXem.Location = new Point(10, 10);
            lblGhiChuXem.Font = UITheme.MainFont; lblGhiChuXem.Padding = new Padding(6);
            tpGhiChu.SizeChanged += (_, __) =>
                lblGhiChuXem.MaximumSize = new Size(Math.Max(100, tpGhiChu.ClientSize.Width - 36), 0);
            LeTanUi.Grid(dgvLichSu); LeTanUi.Grid(dgvCoc);
            dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvCoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            LeTanUi.EmptyGrid(dgvLichSu, "Chưa có lịch sử lưu trú", "Chọn khách hàng để xem các lần nhận và trả phòng.");
            LeTanUi.EmptyGrid(dgvCoc, "Chưa có giao dịch cọc", "Phiếu thu và hoàn cọc của khách sẽ hiển thị tại đây.");
            LeTanUi.Row(listLayout, tabChiTiet);
            tabChiTiet.Margin = new Padding(0, 4, 0, 0);
            LeTanUi.Badge(lblNgayHienTai);
            lblNgayHienTai.TextAlign = ContentAlignment.MiddleRight;
            LeTanUi.Row(listLayout, lblNgayHienTai, 30);
            listCard.Controls.Add(listLayout);

            var detailCard = new LeTanCard { Padding = new Padding(16) };
            var detailLayout = LeTanUi.Table();
            lblTieuDeThongTin.AutoSize = false; lblTieuDeThongTin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTieuDeThongTin.ForeColor = UITheme.DarkNavy;
            LeTanUi.Row(detailLayout, lblTieuDeThongTin, 32);
            var detailHint = LeTanUi.Table(2);
            detailHint.ColumnStyles[0].Width = 65; detailHint.ColumnStyles[1].Width = 35;
            detailHint.Controls.Add(LeTanUi.Caption("* Thông tin bắt buộc"), 0, 0);
            LeTanUi.Badge(lblNhomBadge); lblNhomBadge.Dock = DockStyle.Fill;
            detailHint.Controls.Add(lblNhomBadge, 1, 0);
            LeTanUi.Row(detailLayout, detailHint, 30);

            pnlThongTin.AutoScroll = true; pnlThongTin.AutoScrollMinSize = Size.Empty;
            pnlThongTin.Padding = new Padding(0, 8, 8, 0); pnlThongTin.BackColor = Color.White;
            var fields = LeTanUi.Table(); fields.Dock = DockStyle.Top; fields.AutoSize = true;
            LeTanUi.Row(fields, LeTanUi.Field(lblHoTen, txtHoTen), 76);
            var phoneRow = LeTanUi.Table(2);
            phoneRow.Controls.Add(LeTanUi.Field(lblDienThoai, txtDienThoai), 0, 0);
            phoneRow.Controls.Add(LeTanUi.Field(lblGioiTinh, cboGioiTinh), 1, 0);
            foreach (Control field in phoneRow.Controls) field.Dock = DockStyle.Fill;
            LeTanUi.Row(fields, phoneRow, 76);
            var birthRow = LeTanUi.Table(2);
            birthRow.Controls.Add(LeTanUi.Field(lblNgaySinh, dtpNgaySinh), 0, 0);
            birthRow.Controls.Add(LeTanUi.Field(lblQuocTich, txtQuocTich), 1, 0);
            foreach (Control field in birthRow.Controls) field.Dock = DockStyle.Fill;
            LeTanUi.Row(fields, birthRow, 76);
            LeTanUi.Row(fields, LeTanUi.Field(lblEmail, txtEmail), 76);
            LeTanUi.Row(fields, LeTanUi.Field(lblDiaChi, txtDiaChi), 76);
            LeTanUi.Row(fields, LeTanUi.Field(lblSoGiayTo, txtSoGiayTo), 76);
            lblLoiSoGiayTo.AutoSize = false; lblLoiSoGiayTo.Font = new Font("Segoe UI", 8.5F);
            lblLoiSoGiayTo.ForeColor = UITheme.DangerRed;
            LeTanUi.Row(fields, lblLoiSoGiayTo, 36);
            txtGhiChu.Multiline = true; txtGhiChu.ScrollBars = ScrollBars.Vertical;
            LeTanUi.Row(fields, LeTanUi.Field(lblGhiChu, txtGhiChu, 100), 100);
            chkNhanUuDai.AutoSize = false; chkNhanUuDai.Font = new Font("Segoe UI", 9F);
            LeTanUi.Row(fields, chkNhanUuDai, 42);
            pnlThongTin.Controls.Add(fields);
            LeTanUi.Row(detailLayout, pnlThongTin);
            LeTanUi.Button(btnCapNhat, UITheme.PrimaryOrange, true); btnCapNhat.Width = 158;
            LeTanUi.Button(btnHuy); btnHuy.Text = "Bỏ thay đổi"; btnHuy.Width = 132;
            LeTanUi.Row(detailLayout, LeTanUi.Actions(btnCapNhat, btnHuy), 56);
            detailCard.Controls.Add(detailLayout);

            LeTanUi.Row(root, new LeTanWorkspace(listCard, detailCard));
            Controls.Add(root);
            ResumeLayout(true);
        }

        private void VeTabChiTiet(object sender, DrawItemEventArgs e)
        {
            var bounds = tabChiTiet.GetTabRect(e.Index);
            bool selected = tabChiTiet.SelectedIndex == e.Index;
            using var background = new SolidBrush(selected ? LeTanUi.OrangeLight : UITheme.BackgroundLight);
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
}
