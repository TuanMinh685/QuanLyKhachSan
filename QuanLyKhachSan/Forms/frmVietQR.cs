using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmVietQR : Form
    {
        private readonly string _maHoaDon;
        private readonly decimal _soTien;
        private readonly string _noiDung;

        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

        public frmVietQR(string maHoaDon, decimal soTien, string noiDung = null)
        {
            InitializeComponent();
            _maHoaDon = string.IsNullOrWhiteSpace(maHoaDon) ? "HD" + DateTime.Now.ToString("yyMMddHHmm") : maHoaDon;
            _soTien = soTien;
            _noiDung = string.IsNullOrWhiteSpace(noiDung) ? $"THANH TOAN {_maHoaDon}" : noiDung;
        }

        private async void frmVietQR_Load(object sender, EventArgs e)
        {
            lblTienValue.Text = _soTien.ToString("N0") + " VND";
            lblMemoValue.Text = _noiDung;

            btnXacNhan.Text = "✓ Đã chuyển khoản xong";
            btnDong.Text = "✕ Đóng";

            lblStkValue.Cursor = Cursors.Hand;
            lblStkValue.Text = "0901234567 📋";
            lblStkValue.Click += (s, ev) =>
            {
                Clipboard.SetText("0901234567");
                MessageBox.Show("Đã sao chép STK [0901234567] vào Clipboard!", "Sao chép thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            lblMemoValue.Cursor = Cursors.Hand;
            lblMemoValue.Text = _noiDung + " 📋";
            lblMemoValue.Click += (s, ev) =>
            {
                Clipboard.SetText(_noiDung);
                MessageBox.Show($"Đã sao chép nội dung chuyển khoản [{_noiDung}] vào Clipboard!", "Sao chép thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            await LoadVietQRImageAsync();
        }

        private async Task LoadVietQRImageAsync()
        {
            try
            {
                lblStatus.Text = "Đang kết nối cổng VietQR tự động...";
                long amount = (long)Math.Max(0, _soTien);
                string encodedMemo = Uri.EscapeDataString(_noiDung);
                string url = $"https://img.vietqr.io/image/mbbank-0901234567-compact2.png?amount={amount}&addInfo={encodedMemo}&accountName=KHACH%20SAN%20LUXURY%20RESORT";

                byte[] imageBytes = await httpClient.GetByteArrayAsync(url);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    picQR.Image = Image.FromStream(ms);
                }
                lblStatus.Text = "✓ Đã tải mã VietQR thành công. Vui lòng quét để chuyển khoản.";
                lblStatus.ForeColor = Color.FromArgb(16, 185, 129);
            }
            catch
            {
                picQR.Image = TaoAnhQROffline(260);
                lblStatus.Text = "Chế độ mô phỏng QR (Sẵn sàng nhận thanh toán chuyển khoản)";
                lblStatus.ForeColor = Color.FromArgb(234, 88, 12);
            }
        }

        public static Bitmap TaoAnhQROffline(int size = 260)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                float scale = size / 260f;

                using (Pen pen = new Pen(Color.FromArgb(15, 37, 55), Math.Max(2, 6 * scale)))
                {
                    // 3 corner locator boxes
                    g.DrawRectangle(pen, 15 * scale, 15 * scale, 55 * scale, 55 * scale);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(15, 37, 55)), 27 * scale, 27 * scale, 31 * scale, 31 * scale);

                    g.DrawRectangle(pen, 190 * scale, 15 * scale, 55 * scale, 55 * scale);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(15, 37, 55)), 202 * scale, 27 * scale, 31 * scale, 31 * scale);

                    g.DrawRectangle(pen, 15 * scale, 190 * scale, 55 * scale, 55 * scale);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(15, 37, 55)), 27 * scale, 202 * scale, 31 * scale, 31 * scale);
                }

                // Center logo box
                g.FillRectangle(new SolidBrush(Color.FromArgb(234, 88, 12)), 95 * scale, 95 * scale, 70 * scale, 70 * scale);
                using (Font f = new Font("Segoe UI", Math.Max(6f, 10f * scale), FontStyle.Bold))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString("VietQR\nMBBANK", f, Brushes.White, new RectangleF(95 * scale, 95 * scale, 70 * scale, 70 * scale), sf);
                }

                // Grid pattern simulated dots
                using (Brush b = new SolidBrush(Color.FromArgb(51, 65, 85)))
                {
                    for (int x = 85; x <= 175; x += 18)
                    {
                        g.FillRectangle(b, x * scale, 30 * scale, 8 * scale, 8 * scale);
                        g.FillRectangle(b, x * scale, 55 * scale, 8 * scale, 8 * scale);
                        g.FillRectangle(b, x * scale, 200 * scale, 8 * scale, 8 * scale);
                        g.FillRectangle(b, x * scale, 225 * scale, 8 * scale, 8 * scale);
                    }
                    for (int y = 85; y <= 175; y += 18)
                    {
                        g.FillRectangle(b, 30 * scale, y * scale, 8 * scale, 8 * scale);
                        g.FillRectangle(b, 55 * scale, y * scale, 8 * scale, 8 * scale);
                        g.FillRectangle(b, 200 * scale, y * scale, 8 * scale, 8 * scale);
                        g.FillRectangle(b, 225 * scale, y * scale, 8 * scale, 8 * scale);
                    }
                }
            }
            return bmp;
        }

        private void DrawInfoCard(object sender, PaintEventArgs e)
        {
            UITheme.DrawCard(sender, e);
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
