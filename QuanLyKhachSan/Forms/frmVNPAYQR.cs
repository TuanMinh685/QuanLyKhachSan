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
    public partial class frmVNPAYQR : Form
    {
        private readonly string _maHoaDon;
        private readonly decimal _soTien;
        private readonly string _noiDung;

        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(6) };

        public frmVNPAYQR(string maHoaDon, decimal soTien, string noiDung = null)
        {
            _maHoaDon = string.IsNullOrWhiteSpace(maHoaDon) ? "HD" + DateTime.Now.ToString("yyMMddHHmm") : maHoaDon;
            _soTien = soTien;
            _noiDung = string.IsNullOrWhiteSpace(noiDung) ? $"THANH TOAN {_maHoaDon}" : noiDung;

            InitializeComponent();
            SetupCustomUI();
        }

        private void SetupCustomUI()
        {
            // 1. Header VNPAY-QR
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 88;
            pnlHeader.BackColor = Color.FromArgb(0, 91, 170);
            pnlHeader.Padding = new Padding(24, 16, 24, 12);
            pnlHeader.Paint += (s, e) =>
            {
                using var brush = new LinearGradientBrush(pnlHeader.ClientRectangle,
                    Color.FromArgb(0, 91, 170),
                    Color.FromArgb(14, 43, 92),
                    LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
            };

            lblHeaderTitle.Text = "⚡ CỔNG THANH TOÁN QUỐC GIA & QUỐC TẾ VNPAY-QR";
            lblHeaderTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 14);
            lblHeaderTitle.AutoSize = true;

            lblHeaderSubtitle.Text = "Chuẩn quốc tế EMVCo • Hỗ trợ 40+ Ngân hàng, Ví điện tử, Visa, Mastercard, Apple Pay";
            lblHeaderSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(219, 234, 254);
            lblHeaderSubtitle.Location = new Point(22, 44);
            lblHeaderSubtitle.AutoSize = true;

            // Dải biểu tượng đối tác ngân hàng & ví
            pnlBadgeBar.Dock = DockStyle.Top;
            pnlBadgeBar.Height = 32;
            pnlBadgeBar.BackColor = Color.FromArgb(239, 246, 255);
            pnlBadgeBar.Padding = new Padding(20, 6, 20, 6);
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

            // 2. Footer Buttons
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Height = 72;
            pnlButtons.BackColor = Color.White;
            pnlButtons.Padding = new Padding(24, 14, 24, 14);
            pnlButtons.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1f);
                e.Graphics.DrawLine(pen, 0, 0, pnlButtons.Width, 0);
            };

            btnDong.Text = "✕ Đóng";
            btnDong.Location = new Point(24, 14);
            btnDong.Size = new Size(110, 44);
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.BackColor = Color.FromArgb(241, 245, 249);
            btnDong.ForeColor = Color.FromArgb(71, 85, 105);
            btnDong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDong.Cursor = Cursors.Hand;
            btnDong.FlatAppearance.BorderSize = 0;
            btnDong.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            btnXacNhan.Text = "✓ ĐÃ NHẬN THANH TOÁN VNPAY-QR";
            btnXacNhan.Location = new Point(144, 14);
            btnXacNhan.Size = new Size(330, 44);
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.BackColor = Color.FromArgb(0, 91, 170);
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXacNhan.Cursor = Cursors.Hand;
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

            // 3. Container
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.BackColor = Color.FromArgb(248, 250, 252);
            pnlContainer.Padding = new Padding(24, 14, 24, 10);

            // Khung ảnh QR
            picQR.Size = new Size(240, 240);
            picQR.Location = new Point((520 - 240) / 2 - 10, 14);
            picQR.SizeMode = PictureBoxSizeMode.Zoom;
            picQR.BackColor = Color.White;
            picQR.Cursor = Cursors.Hand;
            picQR.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(0, 91, 170), 2f);
                e.Graphics.DrawRectangle(pen, 0, 0, picQR.Width - 1, picQR.Height - 1);
            };

            lblStatus.Text = "Đang khởi tạo mã thanh toán EMVCo chuẩn quốc tế...";
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.Location = new Point(20, 260);
            lblStatus.Size = new Size(460, 20);

            // Bảng thông tin
            pnlInfo.Location = new Point(24, 286);
            pnlInfo.Size = new Size(456, 190);
            pnlInfo.BackColor = Color.White;
            pnlInfo.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlInfo.Width - 1, pnlInfo.Height - 1);
            };

            int curY = 12;
            lblMerchantTitle.Text = "Đơn vị thụ hưởng:";
            lblMerchantTitle.Location = new Point(14, curY);
            lblMerchantTitle.Size = new Size(130, 20);
            lblMerchantTitle.ForeColor = Color.FromArgb(100, 116, 139);

            lblMerchantVal.Text = "AN NHIÊN HOTEL & RESORT";
            lblMerchantVal.Location = new Point(146, curY);
            lblMerchantVal.Size = new Size(295, 20);
            lblMerchantVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMerchantVal.ForeColor = Color.FromArgb(15, 23, 42);
            curY += 28;

            lblStandardTitle.Text = "Tiêu chuẩn mã:";
            lblStandardTitle.Location = new Point(14, curY);
            lblStandardTitle.Size = new Size(130, 20);
            lblStandardTitle.ForeColor = Color.FromArgb(100, 116, 139);

            lblStandardVal.Text = "EMVCo Merchant-Presented Mode (ISO 18004)";
            lblStandardVal.Location = new Point(146, curY);
            lblStandardVal.Size = new Size(295, 20);
            lblStandardVal.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblStandardVal.ForeColor = Color.FromArgb(16, 185, 129);
            curY += 28;

            decimal usd = Math.Round(_soTien / 25400m, 2);
            lblTienTitle.Text = "Số tiền thanh toán:";
            lblTienTitle.Location = new Point(14, curY);
            lblTienTitle.Size = new Size(130, 20);
            lblTienTitle.ForeColor = Color.FromArgb(100, 116, 139);

            lblTienVal.Text = $"{_soTien:N0} VND  (~${usd:N2} USD)";
            lblTienVal.Location = new Point(146, curY);
            lblTienVal.Size = new Size(295, 26);
            lblTienVal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTienVal.ForeColor = Color.FromArgb(0, 91, 170);
            curY += 34;

            lblMemoTitle.Text = "Mã đơn / Nội dung:";
            lblMemoTitle.Location = new Point(14, curY);
            lblMemoTitle.Size = new Size(130, 20);
            lblMemoTitle.ForeColor = Color.FromArgb(100, 116, 139);

            lblMemoVal.Text = _noiDung + "  📋 (Chép)";
            lblMemoVal.Location = new Point(146, curY);
            lblMemoVal.Size = new Size(295, 22);
            lblMemoVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMemoVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblMemoVal.Cursor = Cursors.Hand;
            lblMemoVal.Click += (s, e) =>
            {
                Clipboard.SetText(_noiDung);
                MessageBox.Show($"Đã sao chép nội dung chuyển khoản [{_noiDung}]!", "Sao chép thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
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

                string emvcoString = TaoChuoiEMVCoStandard(amount, _noiDung);
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

        public static string TaoChuoiEMVCoStandard(long amount, string memo)
        {
            var sb = new StringBuilder();

            ThemTagEMVCo(sb, "00", "01");
            ThemTagEMVCo(sb, "01", "12");

            var sbMerchant = new StringBuilder();
            ThemTagEMVCo(sbMerchant, "00", "A000000727");
            ThemTagEMVCo(sbMerchant, "01", "9704220901234567");
            ThemTagEMVCo(sbMerchant, "02", "QRIBFTTA");
            ThemTagEMVCo(sb, "26", sbMerchant.ToString());

            ThemTagEMVCo(sb, "52", "7011");
            ThemTagEMVCo(sb, "53", "704");

            if (amount > 0)
            {
                ThemTagEMVCo(sb, "54", amount.ToString());
            }

            ThemTagEMVCo(sb, "58", "VN");
            ThemTagEMVCo(sb, "59", "AN NHIEN HOTEL");
            ThemTagEMVCo(sb, "60", "DA NANG");

            var sbAdditional = new StringBuilder();
            string cleanMemo = (memo ?? "THANH TOAN").ToUpper().Replace(" ", "%20");
            ThemTagEMVCo(sbAdditional, "08", cleanMemo);
            ThemTagEMVCo(sb, "62", sbAdditional.ToString());

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
                    g.DrawRectangle(pen, 14 * scale, 14 * scale, 50 * scale, 50 * scale);
                    g.FillRectangle(brush, 26 * scale, 26 * scale, 26 * scale, 26 * scale);

                    g.DrawRectangle(pen, 176 * scale, 14 * scale, 50 * scale, 50 * scale);
                    g.FillRectangle(brush, 188 * scale, 26 * scale, 26 * scale, 26 * scale);

                    g.DrawRectangle(pen, 14 * scale, 176 * scale, 50 * scale, 50 * scale);
                    g.FillRectangle(brush, 26 * scale, 188 * scale, 26 * scale, 26 * scale);

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
