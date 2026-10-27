namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmChonKhach
    {
        private readonly Label lblDemKhach = LeTanUi.Caption("Danh sách khách hàng", true);

        private void ThietKeChonKhach(Button btnThemKhach)
        {
            SuspendLayout(); Controls.Clear();
            Text = "Chọn khách đặt phòng"; Font = UITheme.MainFont;
            BackColor = UITheme.BackgroundGray;
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(700, 480); ClientSize = new Size(820, 560);
            MaximizeBox = false; MinimizeBox = false; ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            var root = LeTanUi.Table();
            LeTanUi.Row(root, LeTanUi.Header(LeTanUi.Caption("CHỌN KHÁCH ĐẶT PHÒNG"),
                LeTanUi.Caption("Tìm hồ sơ có sẵn hoặc thêm khách mới để tiếp tục đặt phòng")), 88);
            var body = new Panel { Padding = new Padding(24, 14, 24, 14) };
            var card = new LeTanCard { Dock = DockStyle.Fill, Padding = new Padding(16) };
            var content = LeTanUi.Table();
            var search = LeTanUi.Table(2);
            search.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            search.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 110);
            txtTimKhach.PlaceholderText = "Nhập tên khách, số điện thoại hoặc mã khách…";
            var input = LeTanUi.Input(txtTimKhach); input.Dock = DockStyle.Fill; input.Margin = new Padding(0, 0, 10, 0);
            LeTanUi.Button(btnTimKhach, UITheme.DarkNavy, true);
            btnTimKhach.Text = "Tìm kiếm"; btnTimKhach.Dock = DockStyle.Fill;
            search.Controls.Add(input, 0, 0); search.Controls.Add(btnTimKhach, 1, 0);
            LeTanUi.Row(content, search, 40);
            LeTanUi.Row(content, lblDemKhach, 42);
            LeTanUi.Grid(dgvKhach);
            LeTanUi.EmptyGrid(dgvKhach, "Không tìm thấy khách hàng", "Đổi từ khóa hoặc chọn Thêm khách mới để tạo hồ sơ.");
            LeTanUi.Row(content, dgvKhach);
            card.Controls.Add(content); body.Controls.Add(card); LeTanUi.Row(root, body);
            var footer = LeTanUi.Table(2); footer.BackColor = Color.White; footer.Padding = new Padding(24, 6, 24, 6);
            footer.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 195);
            footer.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
            btnThemKhach.Text = "+  Thêm khách mới";
            LeTanUi.Button(btnThemKhach, UITheme.PrimaryBlue); btnThemKhach.Dock = DockStyle.Fill;
            btnThemKhach.Margin = new Padding(0, 7, 0, 7);
            LeTanUi.Button(btnChon, UITheme.PrimaryOrange, true); btnChon.Text = "Chọn khách"; btnChon.Width = 134;
            LeTanUi.Button(btnDong); btnDong.Width = 96;
            footer.Controls.Add(btnThemKhach, 0, 0);
            footer.Controls.Add(LeTanUi.Actions(btnChon, btnDong), 1, 0);
            LeTanUi.Row(root, footer, 68);
            Controls.Add(root); ResumeLayout(true);
        }
    }
}
