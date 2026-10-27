using System.Globalization;
using Quản_Lý_Khách_Sạn.BLL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public sealed class frmHoanCoc : Form
    {
        private readonly decimal tienCoc;
        private readonly TextBox txtHoan = new() { Dock = DockStyle.Fill, PlaceholderText = "Nhập 0 nếu giữ toàn bộ cọc" };
        private readonly ComboBox cboPhuongThuc = new() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly Label lblGiu = new() { Dock = DockStyle.Fill, ForeColor = Color.Firebrick };
        public decimal TienHoan { get; private set; }
        public string PhuongThuc => cboPhuongThuc.SelectedIndex == 1 ? "CK" : cboPhuongThuc.SelectedIndex == 2 ? "POS" : "TM";

        public frmHoanCoc(string maDat, decimal coc)
        {
            tienCoc = coc;
            Text = "Xử lý cọc khi hủy " + maDat;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new Size(560, 540);
            Font = UITheme.MainFont;
            BackColor = UITheme.BackgroundGray;
            ShowInTaskbar = false;
            AutoScaleMode = AutoScaleMode.Dpi;
            var root = LeTanUi.Table();
            Controls.Add(root);
            LeTanUi.Row(root, LeTanUi.Header(LeTanUi.Caption("XỬ LÝ CỌC KHI HỦY PHÒNG"),
                LeTanUi.Caption("Đơn " + maDat + " · Xác định khoản hoàn và số tiền giữ lại")), 88);
            var body = new Panel { Padding = new Padding(24, 16, 24, 16), AutoScroll = true };
            var card = new LeTanCard { Dock = DockStyle.Top, Height = 352, Padding = new Padding(16) };
            var fields = LeTanUi.Table();
            var deposit = LeTanUi.Caption($"Cọc đã thu   {coc.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))} đ", true);
            deposit.BackColor = LeTanUi.OrangeLight; deposit.ForeColor = UITheme.PrimaryOrange;
            deposit.Font = new Font("Segoe UI", 14F, FontStyle.Bold); deposit.Padding = new Padding(12, 0, 12, 0);
            LeTanUi.Row(fields, deposit, 60);
            LeTanUi.Row(fields, LeTanUi.Field("Số tiền hoàn cho khách (VNĐ)", txtHoan), 76);
            txtHoan.TextAlign = HorizontalAlignment.Right;
            cboPhuongThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ" });
            cboPhuongThuc.SelectedIndex = 0;
            LeTanUi.Row(fields, LeTanUi.Field("Phương thức hoàn", cboPhuongThuc), 76);
            lblGiu.Text = "Nhập số tiền hoàn để xem khoản giữ lại.";
            lblGiu.Font = UITheme.BoldFont; lblGiu.ForeColor = UITheme.TextMuted;
            lblGiu.TextAlign = ContentAlignment.MiddleLeft;
            LeTanUi.Row(fields, lblGiu, 44);
            var note = LeTanUi.Caption("Nhập 0 nếu giữ toàn bộ cọc. Bước tiếp theo sẽ xác nhận hủy đơn và khoản tiền đã thực tế xử lý.");
            note.AutoEllipsis = false; note.Font = new Font("Segoe UI", 9F);
            LeTanUi.Row(fields, note);
            card.Controls.Add(fields); body.Controls.Add(card); LeTanUi.Row(root, body);
            var ok = new Button { Text = "Tiếp tục xác nhận", Width = 176 };
            var cancel = new Button { Text = "Quay lại", DialogResult = DialogResult.Cancel, Width = 112 };
            LeTanUi.Button(ok, UITheme.PrimaryOrange, true); LeTanUi.Button(cancel);
            var buttons = LeTanUi.Actions(ok, cancel);
            buttons.BackColor = Color.White; buttons.Padding = new Padding(24, 14, 24, 14);
            LeTanUi.Row(root, buttons, 68);
            CancelButton = cancel; AcceptButton = ok;
            txtHoan.TextChanged += (_, __) =>
            {
                try
                {
                    decimal value = DocTienHoan();
                    lblGiu.ForeColor = UITheme.DarkNavy;
                    lblGiu.Text = "Số tiền giữ lại: " + (tienCoc - value).ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
                    cboPhuongThuc.Enabled = value > 0;
                }
                catch (NghiepVuException ex) { lblGiu.ForeColor = UITheme.DangerRed; lblGiu.Text = ex.Message; }
            };
            ok.Click += (_, __) =>
            {
                try { TienHoan = DocTienHoan(); DialogResult = DialogResult.OK; }
                catch (NghiepVuException ex) { MessageBox.Show(this, ex.Message, "Kiểm tra tiền hoàn"); txtHoan.Focus(); }
            };
            if (coc == 0) txtHoan.Text = "0";
        }

        private decimal DocTienHoan()
        {
            decimal value = LeTanDatPhongBLL.DocTienCoc(txtHoan.Text);
            LeTanLuuTruBLL.KiemTraHoanCoc(value, tienCoc);
            return value;
        }
    }
}
