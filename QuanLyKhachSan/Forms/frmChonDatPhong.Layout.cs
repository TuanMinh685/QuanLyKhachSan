using Quản_Lý_Khách_Sạn.BLL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmChonDatPhong
    {
        private void ThietKeChonDatPhong()
        {
            SuspendLayout(); Controls.Clear();
            Font = UITheme.MainFont; BackColor = UITheme.BackgroundGray;
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(900, 560); ClientSize = new Size(1080, 650);
            var root = LeTanUi.Table();
            bool huy = _thaoTac == ThaoTacDatPhong.HuyPhong;
            string description = huy ? "Chọn đơn, nhập lý do hủy và tiếp tục xử lý khoản cọc đã thu"
                : _thaoTac == ThaoTacDatPhong.NhanPhong ? "Tra cứu đơn đã đặt và xác nhận khách đến nhận phòng"
                : "Tra cứu khách đang lưu trú và chọn đơn cần trả phòng";
            LeTanUi.Row(root, LeTanUi.Header(lblTieuDe, LeTanUi.Caption(description)), 88);
            var body = new Panel { Padding = new Padding(24, 14, 24, 14) };
            var card = new LeTanCard { Dock = DockStyle.Fill, Padding = new Padding(16) };
            var content = LeTanUi.Table();
            var search = LeTanUi.Table(2);
            search.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            search.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 116);
            txtTim.PlaceholderText = "Tìm mã đặt phòng, tên khách, số điện thoại hoặc số phòng…";
            var input = LeTanUi.Input(txtTim); input.Dock = DockStyle.Fill; input.Margin = new Padding(0, 0, 12, 0);
            LeTanUi.Button(btnTim, UITheme.DarkNavy, true); btnTim.Text = "Tìm kiếm"; btnTim.Dock = DockStyle.Fill;
            search.Controls.Add(input, 0, 0); search.Controls.Add(btnTim, 1, 0);
            LeTanUi.Row(content, search, 40);
            lblKetQua.AutoSize = false; lblKetQua.Font = UITheme.BoldFont;
            lblKetQua.ForeColor = UITheme.TextMuted; lblKetQua.TextAlign = ContentAlignment.MiddleLeft;
            LeTanUi.Row(content, lblKetQua, 42);
            LeTanUi.Grid(dgvDatPhong);
            LeTanUi.EmptyGrid(dgvDatPhong, "Chưa có đơn phù hợp", "Thử tìm theo mã đặt, tên khách hoặc số phòng.");
            LeTanUi.Row(content, dgvDatPhong);
            if (huy)
            {
                txtLyDo.PlaceholderText = "Nhập lý do hủy đơn (bắt buộc)…";
                txtLyDo.Multiline = true; txtLyDo.ScrollBars = ScrollBars.Vertical;
                var reason = LeTanUi.Field(lblLyDo, txtLyDo, 86);
                LeTanUi.Row(content, reason, 96); reason.Margin = new Padding(0, 10, 0, 0);
            }
            card.Controls.Add(content); body.Controls.Add(card); LeTanUi.Row(root, body);
            LeTanUi.Button(btnXacNhan, huy ? UITheme.DangerRed : UITheme.PrimaryOrange, true);
            btnXacNhan.Width = 154;
            LeTanUi.Button(btnDong); btnDong.Width = 108;
            var footer = LeTanUi.Actions(btnXacNhan, btnDong);
            footer.BackColor = Color.White; footer.Padding = new Padding(24, 14, 24, 14);
            LeTanUi.Row(root, footer, 68);
            Controls.Add(root); ResumeLayout(true);
        }
    }
}
