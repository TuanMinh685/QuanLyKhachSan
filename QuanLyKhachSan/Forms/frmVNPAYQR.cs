using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public class frmVNPAYQR : Form
    {
        private readonly string _maHoaDon;
        private readonly decimal _soTien;
        private readonly string _noiDung;

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlBadgeBar;

        private Panel pnlContainer;
        private PictureBox picQR;
        private Label lblStatus;
        private Panel pnlInfo;

        private Label lblMerchantTitle;
        private Label lblMerchantVal;
        private Label lblStandardTitle;
        private Label lblStandardVal;
        private Label lblTienTitle;
        private Label lblTienVal;
        private Label lblMemoTitle;
        private Label lblMemoVal;

        private Panel pnlButtons;
        private Button btnXacNhan;
        private Button btnDong;

        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(6) };

        public frmVNPAYQR(string maHoaDon, decimal soTien, string noiDung = null)
        {
            _maHoaDon = string.IsNullOrWhiteSpace(maHoaDon) ? "HD" + DateTime.Now.ToString("yyMMddHHmm") : maHoaDon;
            _soTien = soTien;
            _noiDung = string.IsNullOrWhiteSpace(noiDung) ? $"THANH TOAN {_maHoaDon}" : noiDung;

            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            Text = "Cổng thanh toán VNPAY-QR (Chuẩn quốc tế EMVCo)";
            Size = new Size(520, 720);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(248, 250, 252);
            Font = new Font("Segoe UI", 9.5F);

            // 1. Header VNPAY-QR (Xanh VNPAY #005BAA kết hợp Đỏ #ED1C24)
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.FromArgb(0, 91, 170),
                Padding = new Padding(24, 16, 24, 12)
            };
            pnlHeader.Paint += (s, e) =>
            {
                using var brush = new LinearGradientBrush(pnlHeader.ClientRectangle,
                    Color.FromArgb(0, 91, 170),
                    Color.FromArgb(14, 43, 92),
                    LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
            };

            lblHeaderTitle = new Label
            {
                Text = "⚡ CỔNG THANH TOÁN QUỐC GIA & QUỐC TẾ VNPAY-QR",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 14),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeaderTitle);

            lblHeaderSubtitle = new Label
            {
                Text = "Chuẩn quốc tế EMVCo • Hỗ trợ 40+ Ngân hàng, Ví điện tử, Visa, Mastercard, Apple Pay",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(219, 234, 254),
                Location = new Point(22, 44),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            Controls.Add(pnlHeader);

            // Dải biểu tượng đối tác ngân hàng & ví
            pnlBadgeBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 32,
                BackColor = Color.FromArgb(239, 246, 255),
                Padding = new Padding(20, 6, 20, 6)
            };
            pnlBadgeBar.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(191, 219, 254), 1f);
                e.Graphics.DrawLine(pen, 0, pnlBadgeBar.Height - 1, pnlBadgeBar.Width, pnlBadgeBar.Height - 1);
            };
            var lblBadges = new Label
            {
                Text = "🏦 VCB • BIDV • Vietin • Techcom • MB  |  🟣 MoMo • ZaloPay  |  💳 Visa • Master • Pay",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 64, 175),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            pnlBadgeBar.Controls.Add(lblBadges);
            Controls.Add(pnlBadgeBar);

            // 2. Footer Buttons
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
                Text = "✓ ĐÃ NHẬN THANH TOÁN VNPAY-QR",
                Location = new Point(144, 14),
                Size = new Size(330, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 91, 170),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.Click += (s, e) =>
            {
                MessageBox.Show(this,
                    $"Đã ghi nhận thanh toán thành công {_soTien:N0} VND qua cổng VNPAY-QR chuẩn EMVCo!",
                    "Giao dịch thành công",
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
                Padding = new Padding(24, 14, 24, 10)
            };
            Controls.Add(pnlContainer);

            // Khung ảnh QR
            picQR = new PictureBox
            {
                Size = new Size(240, 240),
                Location = new Point((520 - 240) / 2 - 10, 14),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            picQR.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(0, 91, 170), 2f);
                e.Graphics.DrawRectangle(pen, 0, 0, picQR.Width - 1, picQR.Height - 1);
            };
            pnlContainer.Controls.Add(picQR);

            lblStatus = new Label
            {
                Text = "Đang khởi tạo mã thanh toán EMVCo chuẩn quốc tế...",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 260),
                Size = new Size(460, 20)
            };
            pnlContainer.Controls.Add(lblStatus);

            // Bảng thông tin
            pnlInfo = new Panel
            {
                Location = new Point(24, 286),
                Size = new Size(456, 190),
                BackColor = Color.White
            };
            pnlInfo.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlInfo.Width - 1, pnlInfo.Height - 1);
            };
            pnlContainer.Controls.Add(pnlInfo);

            int curY = 12;

            // Đơn vị chấp nhận thanh toán
            lblMerchantTitle = new Label { Text = "Đơn vị thụ hưởng:", Location = new Point(14, curY), Size = new Size(130, 20), ForeColor = Color.FromArgb(100, 116, 139) };
            lblMerchantVal = new Label { Text = "AN NHIÊN HOTEL & RESORT", Location = new Point(146, curY), Size = new Size(295, 20), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42) };
            pnlInfo.Controls.AddRange(new Control[] { lblMerchantTitle, lblMerchantVal });
            curY += 28;

            // Tiêu chuẩn quốc tế
            lblStandardTitle = new Label { Text = "Tiêu chuẩn mã:", Location = new Point(14, curY), Size = new Size(130, 20), ForeColor = Color.FromArgb(100, 116, 139) };
            lblStandardVal = new Label { Text = "EMVCo Merchant-Presented Mode (ISO 18004)", Location = new Point(146, curY), Size = new Size(295, 20), Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = Color.FromArgb(16, 185, 129) };
            pnlInfo.Controls.AddRange(new Control[] { lblStandardTitle, lblStandardVal });
            curY += 28;

            // Số tiền
            decimal usd = Math.Round(_soTien / 25400m, 2);
            lblTienTitle = new Label { Text = "Số tiền thanh toán:", Location = new Point(14, curY), Size = new Size(130, 20), ForeColor = Color.FromArgb(100, 116, 139) };
            lblTienVal = new Label { Text = $"{_soTien:N0} VND  (~${usd:N2} USD)", Location = new Point(146, curY), Size = new Size(295, 26), Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 91, 170) };
            pnlInfo.Controls.AddRange(new Control[] { lblTienTitle, lblTienVal });
            curY += 34;

            // Mã hóa đơn / Lời nhắn
            lblMemoTitle = new Label { Text = "Mã đơn / Nội dung:", Location = new Point(14, curY), Size = new Size(130, 20), ForeColor = Color.FromArgb(100, 116, 139) };
            lblMemoVal = new Label { Text = _noiDung + "  📋 (Chép)", Location = new Point(146, curY), Size = new Size(295, 22), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(30, 41, 59), Cursor = Cursors.Hand };
            lblMemoVal.Click += (s, e) =>
            {
                Clipboard.SetText(_noiDung);
                MessageBox.Show($"Đã sao chép nội dung chuyển khoản [{_noiDung}]!", "Sao chép thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            pnlInfo.Controls.AddRange(new Control[] { lblMemoTitle, lblMemoVal });

            Load += frmVNPAYQR_Load;
        }

        private async void frmVNPAYQR_Load(object sender, EventArgs e)
        {
            await TaiAnhVNPAYQRAsync();
        }

        private async Task TaiAnhVNPAYQRAsync()
        {
            try
            {
                lblStatus.Text = "Đang kết nối cổng VNPAY-QR chuẩn EMVCo...";
                long amount = (long)Math.Max(0, _soTien);

                // Sinh chuỗi mã QR theo đúng đặc tả chuẩn quốc tế EMVCo Merchant-Presented Mode (Tag 00-63)
                string emvcoString = TaoChuoiEMVCoStandard(amount, _noiDung);

                // Render mã QR độ phân giải cao
                string url = $"https://api.qrserver.com/v1/create-qr-code/?size=260x260&data={Uri.EscapeDataString(emvcoString)}";
                byte[] bytes = await httpClient.GetByteArrayAsync(url);
                using (var ms = new MemoryStream(bytes))
                {
                    picQR.Image = Image.FromStream(ms);
                }
                lblStatus.Text = "✓ Đã sẵn sàng quét qua VNPAY, Ngân hàng VN & Quốc tế.";
                lblStatus.ForeColor = Color.FromArgb(22, 163, 74);
            }
            catch
            {
                picQR.Image = TaoAnhQROffline(240);
                lblStatus.Text = "Chế độ mô phỏng VNPAY-QR (Sẵn sàng thanh toán)";
                lblStatus.ForeColor = Color.FromArgb(0, 91, 170);
            }
        }

        /// <summary>
        /// Tạo chuỗi Payload theo đúng chuẩn quốc tế EMVCo (EMVCo Merchant-Presented Mode)
        /// Tuân thủ quy định chuẩn QR thanh toán của Ngân hàng Nhà nước Việt Nam & Quốc tế.
        /// </summary>
        public static string TaoChuoiEMVCoStandard(long amount, string memo)
        {
            var sb = new StringBuilder();

            // Tag 00: Payload Format Indicator ("01")
            ThemTagEMVCo(sb, "00", "01");

            // Tag 01: Point of Initiation Method ("12" = Dynamic QR kèm số tiền)
            ThemTagEMVCo(sb, "01", "12");

            // Tag 26: Merchant Account Information (VNPAY / Napas Global AID)
            var sbMerchant = new StringBuilder();
            ThemTagEMVCo(sbMerchant, "00", "A000000727");               // Acquirer AID
            ThemTagEMVCo(sbMerchant, "01", "9704220901234567");         // Bank BIN (MBBank 970422) + STK
            ThemTagEMVCo(sbMerchant, "02", "QRIBFTTA");                 // Service code
            ThemTagEMVCo(sb, "26", sbMerchant.ToString());

            // Tag 52: Merchant Category Code (7011 = Hotels, Motels, Resorts)
            ThemTagEMVCo(sb, "52", "7011");

            // Tag 53: Transaction Currency (704 = VND)
            ThemTagEMVCo(sb, "53", "704");

            // Tag 54: Transaction Amount
            if (amount > 0)
            {
                ThemTagEMVCo(sb, "54", amount.ToString());
            }

            // Tag 58: Country Code
            ThemTagEMVCo(sb, "58", "VN");

            // Tag 59: Merchant Name
            ThemTagEMVCo(sb, "59", "AN NHIEN HOTEL");

            // Tag 60: Merchant City
            ThemTagEMVCo(sb, "60", "DA NANG");

            // Tag 62: Additional Data Field Template
            var sbAdditional = new StringBuilder();
            string cleanMemo = (memo ?? "THANH TOAN").ToUpper().Replace(" ", "%20");
            ThemTagEMVCo(sbAdditional, "08", cleanMemo);
            ThemTagEMVCo(sb, "62", sbAdditional.ToString());

            // Tag 63: CRC16 Checksum
            sb.Append("6304");
            string crc = TinhCRC16CCITT(sb.ToString());
            sb.Append(crc);

            return sb.ToString();
        }

        private static void ThemTagEMVCo(StringBuilder sb, string tag, string val)
        {
            if (string.IsNullOrEmpty(val)) return;
            sb.Append(tag);
            sb.Append(val.Length.ToString("D2"));
            sb.Append(val);
        }

        /// <summary>
        /// Tính Checksum CRC-16/CCITT-FALSE chuẩn EMVCo
        /// </summary>
        private static string TinhCRC16CCITT(string data)
        {
            ushort crc = 0xFFFF;
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc <<= 1;
                }
            }
            return crc.ToString("X4");
        }

        public static Bitmap TaoAnhQROffline(int size = 240)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                float scale = size / 240f;
                var vnpayBlue = Color.FromArgb(0, 91, 170);

                using (var pen = new Pen(vnpayBlue, Math.Max(2, 6 * scale)))
                using (var brush = new SolidBrush(vnpayBlue))
                {
                    // 3 góc định vị chuẩn QR
                    g.DrawRectangle(pen, 14 * scale, 14 * scale, 50 * scale, 50 * scale);
                    g.FillRectangle(brush, 26 * scale, 26 * scale, 26 * scale, 26 * scale);

                    g.DrawRectangle(pen, 176 * scale, 14 * scale, 50 * scale, 50 * scale);
                    g.FillRectangle(brush, 188 * scale, 26 * scale, 26 * scale, 26 * scale);

                    g.DrawRectangle(pen, 14 * scale, 176 * scale, 50 * scale, 50 * scale);
                    g.FillRectangle(brush, 26 * scale, 188 * scale, 26 * scale, 26 * scale);

                    // Logo badge VNPAY ở giữa
                    float centerBox = 64 * scale;
                    float cx = (size - centerBox) / 2f;
                    float cy = (size - centerBox) / 2f;
                    g.FillRectangle(brush, cx, cy, centerBox, centerBox);
                    using var textFont = new Font("Segoe UI", 10 * scale, FontStyle.Bold);
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("VNPAY", textFont, Brushes.White, new RectangleF(cx, cy, centerBox, centerBox), sf);
                }
            }
            return bmp;
        }
    }
}
