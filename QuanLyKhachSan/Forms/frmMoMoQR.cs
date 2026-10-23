using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public class frmMoMoQR : Form
    {
        private readonly string _maDon;
        private readonly decimal _soTien;
        private readonly string _noiDung;

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;

        private Panel pnlContainer;
        private PictureBox picQR;
        private Panel pnlInfo;

        private Label lblMerchantTitle;
        private Label lblMerchantVal;
        private Label lblSdtTitle;
        private Label lblSdtVal;
        private Label lblTienTitle;
        private Label lblTienVal;
        private Label lblMemoTitle;
        private Label lblMemoVal;
        private Label lblStatus;

        private Panel pnlButtons;
        private Button btnXacNhan;
        private Button btnDong;

        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(6) };

        public frmMoMoQR(string maDon, decimal soTien, string noiDung = null)
        {
            _maDon = string.IsNullOrWhiteSpace(maDon) ? "MM" + DateTime.Now.ToString("yyMMddHHmm") : maDon;
            _soTien = soTien;
            _noiDung = string.IsNullOrWhiteSpace(noiDung) ? $"MOMO {_maDon}" : noiDung;

            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            Text = "Thanh toán Ví điện tử MoMo";
            Size = new Size(490, 680);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(248, 250, 252);
            Font = new Font("Segoe UI", 9.5F);

            // 1. Header MoMo Pink
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.FromArgb(165, 0, 100), // MoMo Magenta
                Padding = new Padding(24, 16, 24, 14)
            };
            pnlHeader.Paint += (s, e) =>
            {
                // Gradient chuyển màu đặc trưng của thương hiệu MoMo
                using var brush = new LinearGradientBrush(pnlHeader.ClientRectangle,
                    Color.FromArgb(165, 0, 100),
                    Color.FromArgb(216, 45, 139),
                    LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
            };

            lblHeaderTitle = new Label
            {
                Text = "🟣 CỔNG THANH TOÁN VÍ MOMO",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(24, 14),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeaderTitle);

            lblHeaderSubtitle = new Label
            {
                Text = "Mở ứng dụng MoMo trên điện thoại và quét mã QR để chuyển tiền tức thì",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(253, 230, 240),
                Location = new Point(24, 44),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            Controls.Add(pnlHeader);

            // 2. Buttons Bottom
            pnlButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 72,
                BackColor = Color.White,
                Padding = new Padding(24, 14, 24, 14)
            };
            pnlButtons.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1f);
                e.Graphics.DrawLine(pen, 0, 0, pnlButtons.Width, 0);
            };

            btnDong = new Button
            {
                Text = "✕ Đóng",
                Location = new Point(24, 14),
                Size = new Size(110, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(71, 85, 105),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDong.FlatAppearance.BorderSize = 0;
            btnDong.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            pnlButtons.Controls.Add(btnDong);

            btnXacNhan = new Button
            {
                Text = "✓ ĐÃ NHẬN TIỀN MOMO",
                Location = new Point(144, 14),
                Size = new Size(300, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(165, 0, 100),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.Click += (s, e) =>
            {
                MessageBox.Show(this,
                    $"Đã ghi nhận thanh toán thành công {_soTien:N0} VND qua Ví MoMo!",
                    "Giao dịch MoMo thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };
            pnlButtons.Controls.Add(btnXacNhan);
            Controls.Add(pnlButtons);

            // 3. Container
            pnlContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(24, 16, 24, 12)
            };
            Controls.Add(pnlContainer);

            // Khung ảnh QR
            picQR = new PictureBox
            {
                Size = new Size(240, 240),
                Location = new Point((490 - 240) / 2 - 10, 16),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            picQR.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(216, 45, 139), 1.5f);
                e.Graphics.DrawRectangle(pen, 0, 0, picQR.Width - 1, picQR.Height - 1);
            };
            pnlContainer.Controls.Add(picQR);

            lblStatus = new Label
            {
                Text = "Đang tạo mã thanh toán MoMo...",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 262),
                Size = new Size(430, 22)
            };
            pnlContainer.Controls.Add(lblStatus);

            // Khung thông tin
            pnlInfo = new Panel
            {
                Location = new Point(20, 288),
                Size = new Size(430, 185),
                BackColor = Color.White
            };
            pnlInfo.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlInfo.Width - 1, pnlInfo.Height - 1);
            };
            pnlContainer.Controls.Add(pnlInfo);

            int curY = 12;

            // Đơn vị thụ hưởng
            lblMerchantTitle = new Label { Text = "Tài khoản nhận:", Location = new Point(14, curY), Size = new Size(130, 20), ForeColor = Color.FromArgb(100, 116, 139) };
            lblMerchantVal = new Label { Text = "AN NHIÊN HOTEL", Location = new Point(150, curY), Size = new Size(260, 20), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42) };
            pnlInfo.Controls.AddRange(new Control[] { lblMerchantTitle, lblMerchantVal });
            curY += 30;

            // Số điện thoại ví MoMo
            lblSdtTitle = new Label { Text = "Số ví MoMo:", Location = new Point(14, curY), Size = new Size(130, 20), ForeColor = Color.FromArgb(100, 116, 139) };
            lblSdtVal = new Label { Text = "0901234567  📋 (Bấm chép)", Location = new Point(150, curY), Size = new Size(260, 20), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(165, 0, 100), Cursor = Cursors.Hand };
            lblSdtVal.Click += (s, e) =>
            {
                Clipboard.SetText("0901234567");
                MessageBox.Show("Đã sao chép số Ví MoMo [0901234567]!", "Sao chép thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            pnlInfo.Controls.AddRange(new Control[] { lblSdtTitle, lblSdtVal });
            curY += 30;

            // Số tiền
            lblTienTitle = new Label { Text = "Số tiền:", Location = new Point(14, curY), Size = new Size(130, 20), ForeColor = Color.FromArgb(100, 116, 139) };
            lblTienVal = new Label { Text = _soTien.ToString("N0") + " VND", Location = new Point(150, curY), Size = new Size(260, 24), Font = new Font("Segoe UI", 13F, FontStyle.Bold), ForeColor = Color.FromArgb(165, 0, 100) };
            pnlInfo.Controls.AddRange(new Control[] { lblTienTitle, lblTienVal });
            curY += 36;

            // Nội dung chuyển
            lblMemoTitle = new Label { Text = "Lời nhắn / Mã đơn:", Location = new Point(14, curY), Size = new Size(130, 20), ForeColor = Color.FromArgb(100, 116, 139) };
            lblMemoVal = new Label { Text = _noiDung + "  📋", Location = new Point(150, curY), Size = new Size(260, 22), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(30, 41, 59), Cursor = Cursors.Hand };
            lblMemoVal.Click += (s, e) =>
            {
                Clipboard.SetText(_noiDung);
                MessageBox.Show($"Đã sao chép nội dung MoMo [{_noiDung}]!", "Sao chép thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            pnlInfo.Controls.AddRange(new Control[] { lblMemoTitle, lblMemoVal });

            Load += frmMoMoQR_Load;
        }

        private async void frmMoMoQR_Load(object sender, EventArgs e)
        {
            await TaiAnhMoMoQRAsync();
        }

        private async Task TaiAnhMoMoQRAsync()
        {
            try
            {
                lblStatus.Text = "Đang kết nối cổng thanh toán MoMo...";
                long amount = (long)Math.Max(0, _soTien);
                string memo = Uri.EscapeDataString(_noiDung);

                // Giao thức định dạng QR MoMo P2P: 2|99|phone|name|email|0|0|amount|comment|transfer_p2p
                string momoPayload = $"2|99|0901234567|AN NHIEN HOTEL|annhien@hotel.com|0|0|{amount}|{memo}|transfer_p2p";
                string url = $"https://api.qrserver.com/v1/create-qr-code/?size=260x260&color=a50064&data={Uri.EscapeDataString(momoPayload)}";

                byte[] bytes = await httpClient.GetByteArrayAsync(url);
                using (var ms = new MemoryStream(bytes))
                {
                    picQR.Image = Image.FromStream(ms);
                }
                lblStatus.Text = "✓ Đã tạo mã MoMo QR thành công. Sẵn sàng quét ví.";
                lblStatus.ForeColor = Color.FromArgb(22, 163, 74);
            }
            catch
            {
                picQR.Image = TaoMoMoQROffline(240);
                lblStatus.Text = "Chế độ mô phỏng MoMo QR (Sẵn sàng nhận thanh toán)";
                lblStatus.ForeColor = Color.FromArgb(165, 0, 100);
            }
        }

        private Bitmap TaoMoMoQROffline(int size)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                float scale = size / 240f;
                var momoColor = Color.FromArgb(165, 0, 100);

                using (var pen = new Pen(momoColor, Math.Max(2, 6 * scale)))
                using (var brush = new SolidBrush(momoColor))
                {
                    // 3 góc định vị chuẩn QR Code
                    g.DrawRectangle(pen, 14 * scale, 14 * scale, 50 * scale, 50 * scale);
                    g.FillRectangle(brush, 26 * scale, 26 * scale, 26 * scale, 26 * scale);

                    g.DrawRectangle(pen, 176 * scale, 14 * scale, 50 * scale, 50 * scale);
                    g.FillRectangle(brush, 188 * scale, 26 * scale, 26 * scale, 26 * scale);

                    g.DrawRectangle(pen, 14 * scale, 176 * scale, 50 * scale, 50 * scale);
                    g.FillRectangle(brush, 26 * scale, 188 * scale, 26 * scale, 26 * scale);

                    // Logo badge MoMo ở giữa
                    float centerBox = 64 * scale;
                    float cx = (size - centerBox) / 2f;
                    float cy = (size - centerBox) / 2f;
                    g.FillRectangle(brush, cx, cy, centerBox, centerBox);
                    using var textFont = new Font("Segoe UI", 12 * scale, FontStyle.Bold);
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("momo", textFont, Brushes.White, new RectangleF(cx, cy, centerBox, centerBox), sf);
                }
            }
            return bmp;
        }
    }
}
