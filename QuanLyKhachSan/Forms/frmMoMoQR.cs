using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmMoMoQR : Form
    {
        private readonly string _maDon;
        private readonly decimal _soTien;
        private readonly string _noiDung;

        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(6) };

        public frmMoMoQR(string maDon, decimal soTien, string noiDung = null)
        {
            _maDon = string.IsNullOrWhiteSpace(maDon) ? "MM" + DateTime.Now.ToString("yyMMddHHmm") : maDon;
            _soTien = soTien;
            _noiDung = string.IsNullOrWhiteSpace(noiDung) ? $"MOMO {_maDon}" : noiDung;

            InitializeComponent();
            SetupCustomUI();
        }

        private void SetupCustomUI()
        {
            // 1. Header MoMo Pink
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 85;
            pnlHeader.BackColor = Color.FromArgb(165, 0, 100);
            pnlHeader.Padding = new Padding(24, 16, 24, 14);
            pnlHeader.Paint += (s, e) =>
            {
                using var brush = new LinearGradientBrush(pnlHeader.ClientRectangle,
                    Color.FromArgb(165, 0, 100),
                    Color.FromArgb(216, 45, 139),
                    LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
            };

            lblHeaderTitle.Text = "🟣 CỔNG THANH TOÁN VÍ MOMO";
            lblHeaderTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(24, 14);
            lblHeaderTitle.AutoSize = true;

            lblHeaderSubtitle.Text = "Mở ứng dụng MoMo trên điện thoại và quét mã QR để chuyển tiền tức thì";
            lblHeaderSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(253, 230, 240);
            lblHeaderSubtitle.Location = new Point(24, 44);
            lblHeaderSubtitle.AutoSize = true;

            // 2. Buttons Bottom
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

            btnXacNhan.Text = "✓ ĐÃ NHẬN TIỀN MOMO";
            btnXacNhan.Location = new Point(144, 14);
            btnXacNhan.Size = new Size(300, 44);
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.BackColor = Color.FromArgb(165, 0, 100);
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXacNhan.Cursor = Cursors.Hand;
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

            // 3. Container
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.BackColor = Color.FromArgb(248, 250, 252);
            pnlContainer.Padding = new Padding(24, 16, 24, 12);

            // Khung ảnh QR
            picQR.Size = new Size(240, 240);
            picQR.Location = new Point((490 - 240) / 2 - 10, 16);
            picQR.SizeMode = PictureBoxSizeMode.Zoom;
            picQR.BackColor = Color.White;
            picQR.Cursor = Cursors.Hand;
            picQR.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(216, 45, 139), 1.5f);
                e.Graphics.DrawRectangle(pen, 0, 0, picQR.Width - 1, picQR.Height - 1);
            };

            lblStatus.Text = "Đang tạo mã thanh toán MoMo...";
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.Location = new Point(20, 262);
            lblStatus.Size = new Size(430, 22);

            // Khung thông tin
            pnlInfo.Location = new Point(20, 288);
            pnlInfo.Size = new Size(430, 185);
            pnlInfo.BackColor = Color.White;
            pnlInfo.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlInfo.Width - 1, pnlInfo.Height - 1);
            };

            int curY = 12;
            lblMerchantTitle.Text = "Tài khoản nhận:";
            lblMerchantTitle.Location = new Point(14, curY);
            lblMerchantTitle.Size = new Size(130, 20);
            lblMerchantTitle.ForeColor = Color.FromArgb(100, 116, 139);

            lblMerchantVal.Text = "AN NHIÊN HOTEL";
            lblMerchantVal.Location = new Point(150, curY);
            lblMerchantVal.Size = new Size(260, 20);
            lblMerchantVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMerchantVal.ForeColor = Color.FromArgb(15, 23, 42);
            curY += 30;

            lblSdtTitle.Text = "Số ví MoMo:";
            lblSdtTitle.Location = new Point(14, curY);
            lblSdtTitle.Size = new Size(130, 20);
            lblSdtTitle.ForeColor = Color.FromArgb(100, 116, 139);

            lblSdtVal.Text = "0901234567  📋 (Bấm chép)";
            lblSdtVal.Location = new Point(150, curY);
            lblSdtVal.Size = new Size(260, 20);
            lblSdtVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSdtVal.ForeColor = Color.FromArgb(165, 0, 100);
            lblSdtVal.Cursor = Cursors.Hand;
            lblSdtVal.Click += (s, e) =>
            {
                Clipboard.SetText("0901234567");
                MessageBox.Show("Đã sao chép số Ví MoMo [0901234567]!", "Sao chép thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            curY += 30;

            lblTienTitle.Text = "Số tiền:";
            lblTienTitle.Location = new Point(14, curY);
            lblTienTitle.Size = new Size(130, 20);
            lblTienTitle.ForeColor = Color.FromArgb(100, 116, 139);

            lblTienVal.Text = _soTien.ToString("N0") + " VND";
            lblTienVal.Location = new Point(150, curY);
            lblTienVal.Size = new Size(260, 24);
            lblTienVal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTienVal.ForeColor = Color.FromArgb(165, 0, 100);
            curY += 36;

            lblMemoTitle.Text = "Lời nhắn / Mã đơn:";
            lblMemoTitle.Location = new Point(14, curY);
            lblMemoTitle.Size = new Size(130, 20);
            lblMemoTitle.ForeColor = Color.FromArgb(100, 116, 139);

            lblMemoVal.Text = _noiDung + "  📋";
            lblMemoVal.Location = new Point(150, curY);
            lblMemoVal.Size = new Size(260, 22);
            lblMemoVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMemoVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblMemoVal.Cursor = Cursors.Hand;
            lblMemoVal.Click += (s, e) =>
            {
                Clipboard.SetText(_noiDung);
                MessageBox.Show($"Đã sao chép nội dung MoMo [{_noiDung}]!", "Sao chép thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
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

                // Giao thức định dạng QR MoMo P2P
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
                    using var textFont = new Font("Segoe UI", 12 * scale, FontStyle.Bold);
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("momo", textFont, Brushes.White, new RectangleF(cx, cy, centerBox, centerBox), sf);
                }
            }
            return bmp;
        }
    }
}
