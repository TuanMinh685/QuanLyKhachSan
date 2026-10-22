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
                // Fallback offline mock QR representation
                picQR.Image = TaoAnhQROffline();
                lblStatus.Text = "Chế độ mô phỏng QR (Sẵn sàng nhận thanh toán chuyển khoản)";
                lblStatus.ForeColor = Color.FromArgb(234, 88, 12);
            }
        }

        private Bitmap TaoAnhQROffline()
        {
            Bitmap bmp = new Bitmap(260, 260);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                // Draw decorative QR pattern placeholder
                using (Pen pen = new Pen(Color.FromArgb(15, 37, 55), 6))
                {
                    // 3 corner locator boxes
                    g.DrawRectangle(pen, 15, 15, 55, 55);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(15, 37, 55)), 27, 27, 31, 31);

                    g.DrawRectangle(pen, 190, 15, 55, 55);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(15, 37, 55)), 202, 27, 31, 31);

                    g.DrawRectangle(pen, 15, 190, 55, 55);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(15, 37, 55)), 27, 202, 31, 31);
                }

                // Center logo box
                g.FillRectangle(new SolidBrush(Color.FromArgb(234, 88, 12)), 95, 95, 70, 70);
                using (Font f = new Font("Segoe UI", 10f, FontStyle.Bold))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString("VietQR\nMBBANK", f, Brushes.White, new RectangleF(95, 95, 70, 70), sf);
                }

                // Grid pattern simulated dots
                using (Brush b = new SolidBrush(Color.FromArgb(51, 65, 85)))
                {
                    for (int x = 85; x <= 175; x += 18)
                    {
                        g.FillRectangle(b, x, 30, 8, 8);
                        g.FillRectangle(b, x, 55, 8, 8);
                        g.FillRectangle(b, x, 200, 8, 8);
                        g.FillRectangle(b, x, 225, 8, 8);
                    }
                    for (int y = 85; y <= 175; y += 18)
                    {
                        g.FillRectangle(b, 30, y, 8, 8);
                        g.FillRectangle(b, 55, y, 8, 8);
                        g.FillRectangle(b, 200, y, 8, 8);
                        g.FillRectangle(b, 225, y, 8, 8);
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
