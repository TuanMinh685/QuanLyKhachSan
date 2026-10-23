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
            ClientSize = new Size(480, 340);
            Font = new Font("Segoe UI", 10);
            AutoScaleMode = AutoScaleMode.Dpi;
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 1, RowCount = 8 };
            int[] heights = { 34, 28, 34, 28, 34, 38, 50, 40 };
            foreach (int height in heights) root.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
            Controls.Add(root);
            root.Controls.Add(new Label { Dock = DockStyle.Fill, Text = $"Cọc đã thu: {coc.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))} đ" });
            root.Controls.Add(new Label { Dock = DockStyle.Fill, Text = "Số tiền hoàn cho khách (VNĐ)" });
            root.Controls.Add(txtHoan);
            root.Controls.Add(new Label { Dock = DockStyle.Fill, Text = "Phương thức hoàn" });
            cboPhuongThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ" });
            cboPhuongThuc.SelectedIndex = 0;
            root.Controls.Add(cboPhuongThuc);
            root.Controls.Add(lblGiu);
            root.Controls.Add(new Label { Dock = DockStyle.Fill, Text = "Bước tiếp theo sẽ xác nhận hủy đơn và khoản hoàn/giữ. Chỉ xác nhận khoản tiền đã thực tế xử lý." });
            var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
            var ok = new Button { Text = "Tiếp tục", AutoSize = true };
            var cancel = new Button { Text = "Quay lại", DialogResult = DialogResult.Cancel, AutoSize = true };
            buttons.Controls.Add(ok); buttons.Controls.Add(cancel); root.Controls.Add(buttons);
            CancelButton = cancel; AcceptButton = ok;
            txtHoan.TextChanged += (_, __) =>
            {
                try
                {
                    decimal value = DocTienHoan();
                    lblGiu.Text = "Số tiền giữ lại: " + (tienCoc - value).ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
                    cboPhuongThuc.Enabled = value > 0;
                }
                catch (NghiepVuException ex) { lblGiu.Text = ex.Message; }
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
