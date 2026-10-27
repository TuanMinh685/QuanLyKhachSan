namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class ucDatPhong
    {
        private void ThietKeGiaoDienDatPhong()
        {
            SuspendLayout();
            BackColor = UITheme.BackgroundGray; Font = UITheme.MainFont;
            Controls.Clear(); pnlTimPhong.Controls.Clear(); pnlKetQua.Controls.Clear();
            pnlTomTat.Controls.Clear(); pnlKhach.Controls.Clear(); pnlNutLenh.Controls.Clear();
            lblTieuDe.Text = "ĐẶT PHÒNG & LƯU TRÚ";
            lblPhuDe.Text = "Tìm phòng phù hợp, chọn khách và xác nhận khoản cọc trong cùng một màn hình";
            LeTanUi.Badge(lblNgayHienTai);
            var root = LeTanUi.Table();
            LeTanUi.Row(root, LeTanUi.Header(lblTieuDe, lblPhuDe, lblNgayHienTai), 88);

            var roomsCard = new LeTanCard { Padding = new Padding(16) };
            var rooms = LeTanUi.Table();
            LeTanUi.Row(rooms, LeTanUi.Caption("01   TÌM PHÒNG TRỐNG", true), 34);
            var filters = LeTanUi.Table(3);
            filters.RowCount = 2;
            filters.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            filters.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            filters.Controls.Add(LeTanUi.Field(lblNgayNhan, dtpNgayNhan), 0, 0);
            filters.Controls.Add(LeTanUi.Field(lblNgayTra, dtpNgayTra), 1, 0);
            filters.Controls.Add(LeTanUi.Field("Loại phòng", cboLoaiPhong), 2, 0);
            filters.Controls.Add(LeTanUi.Field("Người lớn", cboNguoiLon), 0, 1);
            filters.Controls.Add(LeTanUi.Field("Trẻ em", cboTreEm), 1, 1);
            btnTimPhong.Text = "Tìm phòng trống";
            LeTanUi.Button(btnTimPhong, UITheme.PrimaryOrange, true);
            btnTimPhong.Margin = new Padding(0, 26, 0, 8);
            filters.Controls.Add(btnTimPhong, 2, 1);
            foreach (Control control in filters.Controls) control.Dock = DockStyle.Fill;
            LeTanUi.Row(rooms, filters, 152);

            var results = LeTanUi.Table(2);
            lblPhongPhuHop.Text = "Phòng phù hợp"; lblPhongPhuHop.AutoSize = false;
            lblPhongPhuHop.Font = UITheme.BoldFont; lblPhongPhuHop.ForeColor = UITheme.DarkNavy;
            lblPhongPhuHop.TextAlign = ContentAlignment.MiddleLeft; lblPhongPhuHop.Dock = DockStyle.Fill;
            LeTanUi.Badge(lblSoPhongTrong, Color.FromArgb(240, 253, 244), UITheme.SuccessGreen);
            lblSoPhongTrong.Dock = DockStyle.Fill;
            results.Controls.Add(lblPhongPhuHop, 0, 0); results.Controls.Add(lblSoPhongTrong, 1, 0);
            LeTanUi.Row(rooms, results, 38);
            lblGhiChuGia.AutoSize = false; lblGhiChuGia.ForeColor = UITheme.TextMuted;
            lblGhiChuGia.Font = new Font("Segoe UI", 9F); lblGhiChuGia.TextAlign = ContentAlignment.MiddleLeft;
            LeTanUi.Row(rooms, lblGhiChuGia, 32);
            LeTanUi.Grid(dgvPhong);
            LeTanUi.EmptyGrid(dgvPhong, "Chọn ngày để tìm phòng phù hợp", "Nhấn Tìm phòng trống hoặc điều chỉnh bộ lọc nếu chưa có kết quả.");
            LeTanUi.Row(rooms, dgvPhong);
            btnNhanPhong.Text = "Nhận phòng · F5"; btnTraPhong.Text = "Trả phòng · F6"; btnHuyPhong.Text = "Hủy phòng";
            LeTanUi.Button(btnNhanPhong, UITheme.SuccessGreen);
            LeTanUi.Button(btnTraPhong, UITheme.PrimaryBlue);
            LeTanUi.Button(btnHuyPhong, UITheme.DangerRed);
            var stayActions = LeTanUi.Table(3);
            stayActions.Padding = new Padding(0, 10, 0, 0);
            stayActions.Controls.Add(btnNhanPhong, 0, 0); stayActions.Controls.Add(btnTraPhong, 1, 0);
            stayActions.Controls.Add(btnHuyPhong, 2, 0);
            foreach (Control button in stayActions.Controls) { button.Dock = DockStyle.Fill; button.Margin = new Padding(0, 0, 8, 0); }
            LeTanUi.Row(rooms, stayActions, 50);
            roomsCard.Controls.Add(rooms);

            var summaryCard = new LeTanCard { Padding = new Padding(16) };
            var summary = LeTanUi.Table();
            lblTomTat.Text = "02   KHÁCH & ĐẶT CỌC"; lblTomTat.AutoSize = false;
            lblTomTat.Font = UITheme.BoldFont; lblTomTat.ForeColor = UITheme.DarkNavy;
            LeTanUi.Row(summary, lblTomTat, 32);
            LeTanUi.Badge(lblTrangThaiTao, LeTanUi.OrangeLight, UITheme.PrimaryOrange);
            lblTrangThaiTao.Text = "Đơn đặt phòng mới";
            LeTanUi.Row(summary, lblTrangThaiTao, 30);
            pnlTomTat.Dock = DockStyle.Fill; pnlTomTat.AutoScroll = true;
            pnlTomTat.BackColor = Color.White; pnlTomTat.Padding = new Padding(0, 8, 8, 0);
            var details = LeTanUi.Table(); details.AutoSize = true; details.Dock = DockStyle.Top;
            lblKhachDatPhong.AutoSize = false; lblKhachDatPhong.Font = UITheme.BoldFont;
            lblKhachDatPhong.ForeColor = UITheme.TextMuted;
            LeTanUi.Row(details, lblKhachDatPhong, 26);

            var guest = LeTanUi.Table(2); guest.BackColor = UITheme.BackgroundLight;
            guest.Padding = new Padding(10);
            guest.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 42);
            guest.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
            LeTanUi.Badge(lblAvatar, Color.FromArgb(255, 237, 213), UITheme.PrimaryOrange);
            lblAvatar.Dock = DockStyle.Fill; lblAvatar.Margin = new Padding(0, 2, 8, 2);
            var guestText = LeTanUi.Table();
            lblTenKhach.AutoSize = false; lblTenKhach.AutoEllipsis = true;
            lblTenKhach.Font = UITheme.BoldFont; lblTenKhach.ForeColor = UITheme.DarkNavy;
            lblSdtMa.AutoSize = false; lblSdtMa.AutoEllipsis = true;
            lblSdtMa.Font = new Font("Segoe UI", 8.5F); lblSdtMa.ForeColor = UITheme.TextMuted;
            LeTanUi.Row(guestText, lblTenKhach, 25); LeTanUi.Row(guestText, lblSdtMa, 22);
            guest.Controls.Add(lblAvatar, 0, 0); guest.Controls.Add(guestText, 1, 0);
            LeTanUi.Row(details, guest, 70);
            btnDoiKhach.Text = "Chọn / đổi khách hàng";
            LeTanUi.Button(btnDoiKhach, UITheme.PrimaryBlue);
            LeTanUi.Row(details, btnDoiKhach, 40);

            lblDongPhong.AutoSize = false; lblDongPhong.AutoEllipsis = true;
            lblDongPhong.Font = UITheme.BoldFont; lblDongPhong.ForeColor = UITheme.DarkNavy;
            lblDongPhong.TextAlign = ContentAlignment.MiddleLeft;
            LeTanUi.Row(details, lblDongPhong, 44);
            ThemDongChiPhi(details, LeTanUi.Caption("Giá / đêm × số đêm"), lblTienPhong);
            ThemDongChiPhi(details, lblPhuThuTen, lblPhuThuTien);
            ThemDongChiPhi(details, lblThueTen, lblThueTien);
            lblKe.BackColor = UITheme.BorderColor;
            LeTanUi.Row(details, lblKe, 1);
            var total = LeTanUi.Table(2); total.BackColor = LeTanUi.OrangeLight;
            lblTongCongTen.AutoSize = false; lblTongCongTen.Font = UITheme.BoldFont;
            lblTongCongTen.ForeColor = UITheme.DarkNavy; lblTongCongTen.TextAlign = ContentAlignment.MiddleLeft;
            lblTongCongTien.AutoSize = false; lblTongCongTien.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTongCongTien.ForeColor = UITheme.PrimaryOrange;
            lblTongCongTen.Dock = lblTongCongTien.Dock = DockStyle.Fill;
            total.ColumnStyles[0].Width = 35; total.ColumnStyles[1].Width = 65;
            total.Controls.Add(lblTongCongTen, 0, 0); total.Controls.Add(lblTongCongTien, 1, 0);
            LeTanUi.Row(details, total, 60);
            lblCocTen.Text = "Cọc / tạm ứng thực nhận (VNĐ)";
            LeTanUi.Row(details, LeTanUi.Field(lblCocTen, txtTienCoc), 76);
            lblCocToiThieu.AutoSize = false; lblCocToiThieu.ForeColor = UITheme.TextMuted;
            lblCocToiThieu.Text = "Tối thiểu 30% · Tối đa 100% tiền đặt phòng";
            lblCocToiThieu.TextAlign = ContentAlignment.MiddleLeft;
            LeTanUi.Row(details, lblCocToiThieu, 28);

            var methods = LeTanUi.Table(3);
            int index = 0;
            foreach (var radio in new[] { rdoTienMat, rdoChuyenKhoan, rdoThe })
            {
                radio.Dock = DockStyle.Fill; radio.Margin = new Padding(0, 0, 5, 0);
                radio.Appearance = Appearance.Button; radio.FlatStyle = FlatStyle.Flat;
                radio.UseVisualStyleBackColor = false; radio.BackColor = Color.White;
                radio.FlatAppearance.BorderColor = UITheme.BorderColor;
                radio.FlatAppearance.CheckedBackColor = LeTanUi.OrangeLight;
                radio.Font = new Font("Segoe UI", 8.5F); radio.ForeColor = UITheme.DarkNavy;
                radio.TextAlign = ContentAlignment.MiddleCenter; radio.Cursor = Cursors.Hand;
                methods.Controls.Add(radio, index++, 0);
            }
            LeTanUi.Row(details, methods, 38);
            lblSoDu.Font = new Font("Segoe UI", 9F); lblSoDu.Padding = new Padding(0, 10, 0, 0);
            LeTanUi.Row(details, lblSoDu, 76);
            pnlTomTat.Controls.Add(details);
            LeTanUi.Row(summary, pnlTomTat);
            btnDatPhong.Text = "Xác nhận đặt & thu cọc · F4";
            LeTanUi.Button(btnDatPhong, UITheme.PrimaryOrange, true);
            LeTanUi.Row(summary, btnDatPhong, 44);
            lblPhimTat.AutoSize = false; lblPhimTat.Text = "Xác nhận sau khi đã nhận đủ tiền cọc.";
            lblPhimTat.Font = new Font("Segoe UI", 8.5F); lblPhimTat.ForeColor = UITheme.TextMuted;
            lblPhimTat.TextAlign = ContentAlignment.MiddleCenter;
            LeTanUi.Row(summary, lblPhimTat, 30);
            summaryCard.Controls.Add(summary);
            LeTanUi.Row(root, new LeTanWorkspace(roomsCard, summaryCard));
            Controls.Add(root);
            ResumeLayout(true);
        }

        private static void ThemDongChiPhi(TableLayoutPanel parent, Label label, Label value)
        {
            var row = LeTanUi.Table(2);
            row.ColumnStyles[0].Width = 52; row.ColumnStyles[1].Width = 48;
            label.AutoSize = false; label.Dock = DockStyle.Fill;
            label.Font = new Font("Segoe UI", 9F); label.ForeColor = UITheme.TextMuted;
            label.TextAlign = ContentAlignment.MiddleLeft;
            value.AutoSize = false; value.Dock = DockStyle.Fill;
            value.Font = new Font("Segoe UI", 9F); value.ForeColor = UITheme.TextMain;
            value.TextAlign = ContentAlignment.MiddleRight;
            row.Controls.Add(label, 0, 0); row.Controls.Add(value, 1, 0);
            LeTanUi.Row(parent, row, 32);
        }
    }
}
