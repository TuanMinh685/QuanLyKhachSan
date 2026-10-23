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
                lblStatus.Text = "Đang kết nối cổng VNPAY-QR chuẩn EMVCo...";
                long amount = (long)Math.Max(0, _soTien);

                // Tạo chuỗi EMVCo chuẩn quốc tế
                string emvcoPayload = frmVNPAYQR.TaoChuoiEMVCoStandard(amount, _noiDung);
                string url = $"https://api.qrserver.com/v1/create-qr-code/?size=260x260&data={Uri.EscapeDataString(emvcoPayload)}";

                byte[] imageBytes = await httpClient.GetByteArrayAsync(url);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    picQR.Image = Image.FromStream(ms);
                }
                lblStatus.Text = "✓ Đã tải mã VNPAY-QR (EMVCo). Quét bằng bất kỳ app ngân hàng / ví điện tử.";
                lblStatus.ForeColor = Color.FromArgb(16, 185, 129);
            }
            catch
            {
                picQR.Image = TaoAnhQROffline(260);
                lblStatus.Text = "Chế độ mô phỏng VNPAY-QR (Sẵn sàng nhận thanh toán)";
                lblStatus.ForeColor = Color.FromArgb(0, 91, 170);
            }
        }

        public static Bitmap TaoAnhQROffline(int size = 260)
        {
            return frmVNPAYQR.TaoAnhQROffline(size);
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
